using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WinIsland.Services;

/// <summary>
/// 本地上岛 API（类似 iOS 第三方 App 的“灵动岛”集成）。
/// 监听 http://127.0.0.1:{port}，其他软件可推送信息显示到灵动岛：
///   POST   /v1/island/push        推送/更新一条灵动岛卡片
///   DELETE /v1/island/push/{id}   移除一条
///   GET    /v1/island/active      查询当前活跃推送
///   GET    /v1/health             健康检查
/// 端口、可选 Token、默认显示时长等由设置控制；推送方可按条自定义时长与按钮。
/// </summary>
public sealed class IslandApiServer : IDisposable
{
    private readonly SettingsService _settings;
    private readonly HttpListener _listener = new();
    private CancellationTokenSource _cts = new();
    private Task? _loop;
    private readonly ConcurrentDictionary<string, IslandPush> _active = new();
    private readonly ConcurrentDictionary<string, long> _order = new();   // id -> 入队序号（同 id 更新保持原位）
    private long _seq;   // 入队序号计数器
    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

    public IslandApiServer(SettingsService settings)
    {
        _settings = settings;
    }

    /// <summary>收到新推送/更新时触发（参数：完整推送，含服务端计算的过期时间）。</summary>
    public event Action<IslandPush>? PushReceived;
    /// <summary>推送被移除或过期时触发。</summary>
    public event Action<string>? PushRemoved;

    public bool IsRunning { get; private set; }

    public IReadOnlyList<IslandPush> ActivePushes
        => _active.Values
            .OrderByDescending(p => p.PriorityRank)
            .ThenBy(p => _order.TryGetValue(p.Id, out var s) ? s : long.MaxValue)
            .ToList();

    public void Start()
    {
        if (IsRunning) return;
        var port = Math.Clamp(_settings.Current.IslandApiPort, 1, 65535);
        var prefix = $"http://127.0.0.1:{port}/";
        try
        {
            _listener.Prefixes.Clear();
            _listener.Prefixes.Add(prefix);
            _listener.Start();
            IsRunning = true;
            _cts = new CancellationTokenSource();
            _loop = Task.Run(() => AcceptLoopAsync(_cts.Token));
            AppLogger.Info($"Island API listening on {prefix}");
        }
        catch (Exception ex)
        {
            IsRunning = false;
            AppLogger.Error($"Island API failed to start on {prefix}", ex);
        }
    }

    public void Stop()
    {
        if (!IsRunning) return;
        try
        {
            _cts.Cancel();
            _listener.Stop();
            _listener.Close();
            IsRunning = false;
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Island API stop: {ex.Message}");
        }
    }

    private async Task AcceptLoopAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            try
            {
                var ctx = await _listener.GetContextAsync().ConfigureAwait(false);
                _ = Task.Run(() => HandleAsync(ctx, ct));
            }
            catch (HttpListenerException) { break; }
            catch (OperationCanceledException) { break; }
            catch (ObjectDisposedException) { break; }
            catch (Exception ex) { AppLogger.Warn($"Island API accept: {ex.Message}"); }
        }
    }

    private async Task HandleAsync(HttpListenerContext ctx, CancellationToken ct)
    {
        try
        {
            // 可选 Token 校验
            var token = _settings.Current.IslandApiToken;
            if (!string.IsNullOrEmpty(token))
            {
                var got = ctx.Request.Headers["X-WinIsland-Token"]
                          ?? ctx.Request.QueryString["token"];
                if (!string.Equals(got, token, StringComparison.Ordinal))
                {
                    await WriteJsonAsync(ctx, 401, new { error = "unauthorized" }, ct);
                    return;
                }
            }

            var path = ctx.Request.Url?.AbsolutePath ?? "/";
            var method = ctx.Request.HttpMethod;

            if (method == "GET" && path == "/v1/health")
            {
                await WriteJsonAsync(ctx, 200, new { status = "ok" }, ct);
                return;
            }

            if (method == "GET" && path == "/v1/island/active")
            {
                await WriteJsonAsync(ctx, 200, ActivePushes, ct);
                return;
            }

            if (method == "POST" && path == "/v1/island/push")
            {
                using var reader = new StreamReader(ctx.Request.InputStream, Encoding.UTF8);
                var body = await reader.ReadToEndAsync();
                var push = JsonSerializer.Deserialize<IslandPush>(body, JsonOpts);
                if (push is null || string.IsNullOrWhiteSpace(push.Title))
                {
                    await WriteJsonAsync(ctx, 400, new { error = "title is required" }, ct);
                    return;
                }
                var added = AddOrUpdate(push);
                await WriteJsonAsync(ctx, 200, new { id = added.Id, position = PositionOf(added.Id), expires_at = added.ExpiresAt }, ct);
                return;
            }

            if (method == "DELETE" && path.StartsWith("/v1/island/push/"))
            {
                var id = Uri.UnescapeDataString(path.Substring("/v1/island/push/".Length));
                _active.TryRemove(id, out _);
                _order.TryRemove(id, out _);
                _ = Task.Run(() => PushRemoved?.Invoke(id));
                await WriteJsonAsync(ctx, 200, new { ok = true }, ct);
                return;
            }

            await WriteJsonAsync(ctx, 404, new { error = "not found" }, ct);
        }
        catch (Exception ex)
        {
            AppLogger.Warn($"Island API handler: {ex.Message}");
            try { await WriteJsonAsync(ctx, 500, new { error = "internal error" }, ct); }
            catch { /* ignore */ }
        }
    }

    /// <summary>新增或更新推送（供 HTTP 处理与测试复用）。同 id 保留原过期时间与队列位置。</summary>
    public IslandPush AddOrUpdate(IslandPush push)
    {
        if (string.IsNullOrWhiteSpace(push.Id)) push.Id = Guid.NewGuid().ToString("N");

        if (_active.ContainsKey(push.Id))
        {
            // 同 id 更新：刷新内容、保留原过期时间与队列位置（位置不变）
            if (_active[push.Id].ExpiresAt is DateTime oldExp) push.ExpiresAt = oldExp;
        }
        else
        {
            // 新推送：显示时长可自定义，留空用全局默认；分配入队序号
            var seconds = push.DurationSeconds ?? Math.Max(1, _settings.Current.IslandApiDefaultDuration);
            push.DurationSeconds = seconds;
            push.ExpiresAt = DateTime.UtcNow.AddSeconds(seconds);
            _order[push.Id] = Interlocked.Increment(ref _seq);
        }

        _active[push.Id] = push;
        _ = Task.Run(() => PushReceived?.Invoke(push));
        return push;
    }

    /// <summary>移除指定推送（过期/点击后）。</summary>
    public void Remove(string id)
    {
        if (_active.TryRemove(id, out _))
        {
            _order.TryRemove(id, out _);
            PushRemoved?.Invoke(id);
        }
    }

    /// <summary>推送在显示队列中的位置（从 1 开始；不在队列中返回 0），用于 POST push 响应。</summary>
    private int PositionOf(string id)
    {
        var list = ActivePushes;
        for (var i = 0; i < list.Count; i++)
            if (string.Equals(list[i].Id, id, StringComparison.Ordinal)) return i + 1;
        return 0;
    }

    private static async Task WriteJsonAsync(HttpListenerContext ctx, int code, object payload, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(payload);
        var bytes = Encoding.UTF8.GetBytes(json);
        ctx.Response.StatusCode = code;
        ctx.Response.ContentType = "application/json; charset=utf-8";
        ctx.Response.ContentLength64 = bytes.Length;
        await ctx.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length, ct);
        ctx.Response.Close();
    }

    public void Dispose()
    {
        Stop();
        _cts.Dispose();
    }
}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WinIsland.Services;

/// <summary>
/// 一个按钮动作：
///   url    -> 用默认浏览器/系统打开 Value（URL 或文件）
///   launch -> 启动 Value 指定的程序（可含参数）
///   notify -> 通知推送方处理（推送方需自行注册回呼；暂未实现，留作扩展）
/// </summary>
public sealed class IslandPushButton
{
    [JsonPropertyName("label")]
    public string Label { get; set; } = "";

    [JsonPropertyName("action")]
    public string Action { get; set; } = "url"; // url | launch | notify

    [JsonPropertyName("value")]
    public string Value { get; set; } = "";
}

/// <summary>
/// 第三方软件通过本地上岛 API 推送的“灵动岛卡片”。
/// 字段均为可选：WinIsland 会用设置里的全局默认补齐（显示时长、点击行为）。
/// </summary>
public sealed class IslandPush
{
    /// <summary>推送方自定义 ID，用于更新或移除同一条推送。</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = "";

    /// <summary>标题（必填，显示在紧凑态与展开态）。</summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = "";

    /// <summary>正文详情（可选，展开态显示）。</summary>
    [JsonPropertyName("body")]
    public string Body { get; set; } = "";

    /// <summary>图标：Segoe MDL2 Assets 字形代码（如 "\uE8D6"）或 emoji/文本（可选）。</summary>
    [JsonPropertyName("icon")]
    public string Icon { get; set; } = "";

    /// <summary>进度 0..1（可选，展开态显示进度条）。</summary>
    [JsonPropertyName("progress")]
    public double? Progress { get; set; }

    /// <summary>显示时长（秒）。留空则用 WinIsland 设置的全局默认时长。</summary>
    [JsonPropertyName("duration_seconds")]
    public int? DurationSeconds { get; set; }

    /// <summary>操作按钮（可选）。</summary>
    [JsonPropertyName("buttons")]
    public List<IslandPushButton>? Buttons { get; set; }

    /// <summary>服务端计算的过期时间（UTC），客户端推送时忽略。</summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }
}

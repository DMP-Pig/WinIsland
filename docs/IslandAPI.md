# WinIsland 上岛 API 接入教程

让任何第三方软件把信息推送到 WinIsland 灵动岛（类似 iOS 第三方 App 的“灵动岛”集成）。

## 一、启用

1. 运行 WinIsland（版本 ≥ 1.0.4-beta2）。
2. 打开设置 → 「上岛 API」：
   - 勾选「启用上岛 API」
   - 端口默认 `9840`（可改）
   - 可选设置 Token（防局域网误连；设置后所有请求需带 `X-WinIsland-Token` 头）
   - 「默认显示时长」是全局兜底值（秒），第三方可按条覆盖

## 二、接口

| 方法 | 路径 | 说明 |
|---|---|---|
| POST | `/v1/island/push` | 推送 / 更新一张卡片 |
| DELETE | `/v1/island/push/{id}` | 移除一张卡片 |
| GET | `/v1/island/active` | 查询当前活跃卡片 |
| GET | `/v1/health` | 健康检查 |

基址：`http://127.0.0.1:9840`（端口按设置）。

### POST /v1/island/push 请求体（JSON）

| 字段 | 必填 | 类型 | 说明 |
|---|---|---|---|
| `title` | 是 | string | 标题（紧凑态 + 展开态显示） |
| `body` | 否 | string | 正文详情（展开态显示） |
| `icon` | 否 | string | 图标：Segoe MDL2 字形（如 `"\uE8D6"`）或 emoji/文本 |
| `progress` | 否 | number | 进度 0~1（展开态显示进度条） |
| `duration_seconds` | 否 | number | 显示时长（秒），**覆盖** WinIsland 全局默认 |
| `buttons` | 否 | array | 按钮列表 |
| `id` | 否 | string | 自定义 ID；同 ID 重复推送会**更新**原卡片（并延长显示） |

`buttons[]` 每项：

| 字段 | 说明 |
|---|---|
| `label` | 按钮文字 |
| `action` | `url`（默认，用系统默认方式打开 value）或 `launch`（启动 value 指定的程序） |
| `value` | url 地址 / 程序路径（可带参数） |

### 响应

成功返回 JSON：
```json
{ "id": "abc123", "expires_at": "2026-08-23T15:05:30Z" }
```

## 三、各语言示例

### PowerShell
```powershell
$body = @{
    title = "外卖送达"
    body  = "您的订单已到，请取餐"
    icon  = "\uE7F4"
    duration_seconds = 30
    buttons = @(@{ label = "查看"; action = "url"; value = "https://example.com" })
} | ConvertTo-Json -Depth 5

Invoke-RestMethod -Uri "http://127.0.0.1:9840/v1/island/push" -Method Post `
    -Body $body -ContentType "application/json"
```

### Python
```python
import requests

requests.post("http://127.0.0.1:9840/v1/island/push", json={
    "title": "外卖送达",
    "body": "您的订单已到，请取餐",
    "icon": "\ue7f4",
    "duration_seconds": 30,
    "buttons": [{"label": "查看", "action": "url", "value": "https://example.com"}],
})
```

### C# (.NET)
```csharp
using System.Net.Http;
using System.Text;
using System.Text.Json;

var payload = JsonSerializer.Serialize(new {
    title = "外卖送达",
    body = "您的订单已到，请取餐",
    icon = "\uE7F4",
    duration_seconds = 30,
    buttons = new[] { new { label = "查看", action = "url", value = "https://example.com" } }
});
using var client = new HttpClient();
var resp = await client.PostAsync(
    "http://127.0.0.1:9840/v1/island/push",
    new StringContent(payload, Encoding.UTF8, "application/json"));
```

### curl
```bash
curl -X POST http://127.0.0.1:9840/v1/island/push \
  -H "Content-Type: application/json" \
  -d '{"title":"外卖送达","body":"您的订单已到","icon":"\ue7f4","duration_seconds":30,"buttons":[{"label":"查看","action":"url","value":"https://example.com"}]}'
```

### Node.js
```javascript
fetch("http://127.0.0.1:9840/v1/island/push", {
  method: "POST",
  headers: { "Content-Type": "application/json" },
  body: JSON.stringify({
    title: "外卖送达",
    body: "您的订单已到",
    icon: "\ue7f4",
    duration_seconds: 30,
    buttons: [{ label: "查看", action: "url", value: "https://example.com" }],
  }),
});
```

## 四、移除 / 查询

```powershell
# 移除
Invoke-RestMethod -Uri "http://127.0.0.1:9840/v1/island/push/<id>" -Method Delete

# 查询当前活跃
Invoke-RestMethod -Uri "http://127.0.0.1:9840/v1/island/active" -Method Get
```

## 五、常见场景

1. **下载 / 任务进度**：推送带 `progress` 的卡片，同一 `id` 持续更新，完成后 `duration_seconds=5` 短暂停留或直接 DELETE。
2. **倒计时 / 专注提醒**：推送 `duration_seconds` 为剩余时长，结束自动消失。
3. **实时状态（如网速、电量、Git 分支）**：设置较长时长或高频率更新同一 `id`。
4. **操作入口**：用 `buttons` 提供「打开链接」「启动程序」等快捷动作。

## 六、注意事项

- 服务仅监听 `127.0.0.1`（本机回环），外部网络无法访问。
- 若设置了 Token，请求需带请求头 `X-WinIsland-Token: <你的Token>`。
- `title` 为空会返回 `400`。
- 同一条推送可反复 `POST`（相同 `id`）更新内容并续期；也可以 `DELETE` 立即移除。
- 灵动岛点击展开后显示完整卡片（正文/进度/按钮）；按钮点击后自动关闭该推送。

- **不影响灵动岛宽度**：上岛推送**不会**导致灵动岛宽度变化。灵动岛宽度始终由 WinIsland 的「紧凑长度」设置（自动 / 手动）决定；推送卡片会在固定宽度内自适应显示（标题自动截断、正文换行）。第三方软件无法通过推送改变灵动岛宽度。

# WinIsland — Windows 灵动岛

> macOS Dynamic Island 风格的 Windows 桌面悬浮窗：媒体控制、同步歌词、系统托盘常驻。
> 基于 **.NET 8 + WPF**，适配 Windows 11（兼容 Windows 10，1809+）。

![compact](docs/screenshot-compact.png) 


## 目录

- [功能特性](#功能特性)
- [技术选型与理由](#技术选型与理由)
- [架构总览](#架构总览)
- [快速开始](#快速开始)
- [构建与打包](#构建与打包)
- [使用说明](#使用说明)
- [配置项说明](#配置项说明)
- [Cider 集成](#cider-集成)
- [歌词说明](#歌词说明)
- [隐私与安全](#隐私与安全)
- [非功能性指标](#非功能性指标)
- [已知限制](#已知限制)
- [验证指南（需要配合真实播放器测试）](#验证指南需要配合真实播放器测试)
- [常见问题](#常见问题)
- [开源许可](#开源许可)

---

## 功能特性

### P0（已实现）
- **灵动岛悬浮 UI（仿 iOS）**：默认顶部居中（可配置顶部右侧）；圆角胶囊；跟随系统明暗主题或手动主题色；紧凑 ↔ 完整卡片**形变动画**（固定窗口 + 单元素缩放/淡入，WPF 合成线程 60fps 驱动，带 iOS 弹簧回弹）；**点击展开/收起**（悬停不展开），移出自动收起（700ms 防误触缓冲）；卡片外区域点击穿透。
- **锁定与拖动**：默认上锁不可移动；右键菜单可**解锁**（解锁后鼠标拖动灵动岛）、**居中对齐**（上下不变、左右居中）、再次**上锁**。解锁拖动后重新上锁会**保持拖动位置**（不回归默认）。
- **紧凑态排版**：歌名/歌手/歌词左对齐（贴封面）、垂直居中。
- **专辑封面展示**：胶囊与展开卡片均显示封面（展开时为大封面 64px，无封面时显示占位图标）；SMTC 缩略图与 Cider 封面自动缓存。
- **媒体播放控制**：显示歌名、歌手、专辑；可拖拽进度条 seek；播放/暂停、上一首、下一首；需要时提供音量调节（Cider 用其 API，其它来源控制系统音量，可关闭）。
- **多来源接入**：
  1. Windows 全局媒体会话（`Windows.Media.Control` / SMTC）——网易云、QQ音乐、Spotify、Apple Music 官方版、Groove、电影和电视等；
  2. **Cider** 本地 HTTP API（端口 10767，兼容旧版 10769 RPC，自动扫描端口 + 手动配置，支持 `apptoken` 鉴权）；
  3. 兜底：窗口标题 + 进程识别（仅展示信息，无控制能力）。
- **歌词显示（逐字卡拉OK模式）**：点击展开后，歌词区以卡拉OK方式显示——**当前句的字逐个点亮**——高亮进度为连续值，边界字符在 60fps 缓动下从基础色平滑混色到高亮色，按阅读顺序从左到右流动（换行歌词也正确，不会多条线同时点亮）；每句从 0 开始（第一个字先不亮）；暂停时高亮冻结在暂停时刻；退出后重启自动恢复上次暂停位置（含 SMTC 只报时长不报位置、持续报 0 的情况，均不跳回开头）；当前句仅文字高亮（无背景胶囊，避免双重高亮；20px 大号），其余句淡化，**平滑滚动自动居中**（60fps 逐帧逼近当前句，展开即自动跟随）；紧凑态左对齐实时显示当前句并同样逐字点亮；可选独立悬浮歌词小窗。
  - **进度同步**：自动读取 Cider 的本地 API Token（零配置）获取真实播放进度，卡拉OK逐字与歌曲精确同步；无进度可用的播放器用本地时钟推进。
  - **歌词来源**：本地 `.lrc`（`%APPDATA%\WinIsland\Lyrics` 或音乐目录）→ Cider 歌词接口 → 在线歌词（右键灵动岛可一键开关）。无歌词时显示"暂无歌词"，不报错。
- **系统托盘**：常驻图标，右键菜单（显示/隐藏、独立歌词窗口、开机自启、设置、退出），双击切换显示。

### P1（已实现）
- 多显示器：主屏幕 / 所有屏幕 / 指定屏幕编号。
- 高 DPI：PerMonitorV2，120/150/200% 缩放下不错位。
- 自定义配置：位置、偏移、不透明度、主题色、紧凑模式内容、无媒体时隐藏等。
- 无媒体播放时自动隐藏灵动岛（可关闭）。

### P2（部分实现）
- 简体中文 + English 界面切换。
- 导出 / 导入 JSON 配置文件。
- 尚未实现：Windows 通知接入（来电/日历/电量）。

---

## 技术选型与理由

| 方案 | 结论 | 理由 |
| --- | --- | --- |
| **C# + WPF (.NET 8)** | ✅ 采用 | 资源占用低、启动快（相比 Electron/Tauri 的 WebView）、系统集成能力最强（SMTC/CoreAudio/托盘原生支持）、单文件打包简单 |
| C++ + Qt | ❌ | 开发效率低，许可证复杂（LGPL），与 Windows 媒体栈集成需要大量手写代码 |
| Tauri / Electron | ❌ | 内存占用高（常驻 >150MB 难以达成），启动慢，违背“资源占用低、启动快”的要求 |
| WinUI 3 | ❌ | 与 WPF 相比打包/部署更复杂（需 Windows App SDK），且对非打包桌面应用的 SMTC 支持与 WPF 相同 |

**关键点**：
- `Windows.Media.Control.GlobalSystemMediaTransportControlsSessionManager` 通过 .NET 8 的 Windows SDK 投影（CsWinRT）直接可用，无需 UWP 打包身份。
- 除系统自带的 WPF/WinForms/Windows SDK 投影外，**运行时零第三方依赖**（见 [THIRD_PARTY.md](THIRD_PARTY.md)）。
- 亚克力效果：Win10/Win11 均通过 `SetWindowCompositionAttribute`（`ACCENT_ENABLE_ACRYLICBLURBEHIND`）实现，并用 `SetWindowRgn` 裁剪圆角，使模糊跟随胶囊形状。

---

## 架构总览

```
src/WinIsland/
├── App.xaml(.cs)              # 组合根：单实例、异常捕获、托盘、窗口生命周期
├── Services/
│   ├── MediaModels.cs         # 统一媒体快照模型（TrackInfo / MediaSnapshot）
│   ├── SmtcMediaProvider.cs   # Windows 全局媒体会话（事件驱动 + 节流推送）
│   ├── CiderClient.cs         # Cider 本地 API 封装（V3 + LegacyV2、端口扫描、容错解析）
│   ├── CiderMediaProvider.cs  # Cider 会话层（连接生命周期）
│   ├── WindowTitleMediaProvider.cs # 兜底：窗口标题识别
│   ├── MediaCoordinator.cs    # 中央调度：Cider > SMTC > 窗口标题，封面缓存、音量附加
│   ├── LrcParser.cs           # LRC 解析（多时间戳、offset、时长格式）
│   ├── LyricsService.cs       # 歌词解析（本地 .lrc → Cider → 在线）
│   ├── OnlineLyricsService.cs # 在线歌词（网易云/QQ音乐非官方接口，默认开启可一键开关）
│   ├── ArtworkCache.cs        # 封面下载/缓存（Cider 远程封面 → 本地文件）
│   ├── SystemVolume.cs        # CoreAudio 系统音量（COM P/Invoke）
│   ├── AppSettings.cs         # JSON 配置读写（%APPDATA%\WinIsland\settings.json）
│   ├── SingleInstance.cs      # 命名互斥体 + 命名管道（二次启动显示灵动岛）
│   ├── AutoStart.cs           # HKCU Run 键自启
│   └── AppLogger.cs           # 轻量文件日志
├── UI/
│   ├── IslandWindow.xaml(.cs) # 灵动岛窗口（动画、亚克力、定位、悬停交互）
│   ├── IslandViewModel.cs     # 主视图模型（进度插值、歌词索引、可见性）
│   ├── SettingsWindow.xaml(.cs) / SettingsViewModel.cs
│   ├── LyricsWindow.xaml(.cs) # 独立歌词小窗
│   ├── ThemeService.cs        # 明暗主题 + 主题色画刷
│   ├── WindowEffects.cs       # 亚克力 / 暗色模式 / 圆角区域
│   ├── ScreenHelper.cs        # 多显示器 + PerMonitorV2 DPI 换算
│   ├── TrayIcon.cs            # 托盘图标与菜单
│   └── Localization.cs        # 中/英文案表
└── Diagnostics/DiagnosticsCommand.cs  # --diagnose 诊断信息
tests/WinIsland.Tests/         # xunit 单元测试（LRC/配置/Cider 解析/窗口标题解析）
build/
├── publish.ps1                # 一键发布（自包含或框架依赖 + zip）
├── WinIsland.iss              # Inno Setup 安装脚本
└── make-icon.ps1 / IconGen.cs # 图标生成工具
```

**数据流**：`MediaCoordinator` 每秒轮询各 Provider（异步、不阻塞 UI）→ 生成统一 `MediaSnapshot`（含本地封面路径、音量）→ 经 Dispatcher 发布到 `IslandViewModel` → 200ms 插值器平滑推进进度条与歌词高亮 → WPF 绑定渲染。

---

## 快速开始

> 💡 **预编译版**：`releases/` 目录按版本提供单文件自包含可执行文件（如 `releases/1.0.1beta2/win-x64/WinIsland.exe`，约 70MB，含 .NET 8 运行时，双击即可运行）。Beta 版本仅本地保留；稳定版本才发布到 GitHub（含 win-x86 / win-arm64 及 Windows 安装包）。
## 快速开始

### 环境要求
- Windows 10 1809+ / Windows 11
- 构建机：.NET 8 SDK（或更高 SDK 并指定 `net8.0-windows10.0.19041.0`）

### 构建
```powershell
# 还原 + 构建 + 测试
dotnet build WinIsland.slnx -c Release
dotnet test  WinIsland.slnx -c Release

# 运行（Debug）
dotnet run --project src\WinIsland -c Debug
```

### 一键发布
```powershell
# 自包含（含 .NET 8 运行时，免安装，约 170MB）
.\build\publish.ps1

# 框架依赖（体积小，需安装 .NET 8 Desktop Runtime）
.\build\publish.ps1 -FrameworkDependent
```
产物位于 `publish\win-x64\`（含 `WinIsland.exe`），zip 为 `publish\WinIsland-win-x64.zip`。

### 安装包（可选）
安装 [Inno Setup 6](https://jrsoftware.org/isinfo.php) 后：
```powershell
iscc.exe build\release-1.0.1.iss
```
生成 `releases\<版本>\WinIsland-Setup-<版本>.exe`（如 `WinIsland-Setup-1.0.1.exe`）。

---

## 使用说明

1. 启动 `WinIsland.exe`（或设为开机自启 / 安装包勾选自启）。托盘出现图标。
2. 播放任意音乐：
   - 网易云、QQ音乐、Spotify、Apple Music 官方版等 → 自动通过系统媒体会话显示；
   - Cider → 详见 [Cider 集成](#cider-集成)；
   - 其它播放器 → 兜底窗口标题识别（仅展示）。
3. 悬停灵动岛展开完整卡片：进度拖拽 seek、播放控制、音量、同步歌词。
4. 托盘菜单：显示/隐藏、独立歌词窗口、开机自启、设置、退出。**关闭主窗口不会退出进程**（仅托盘化）。
5. 常用命令行参数：
   ```powershell
   WinIsland.exe --demo       # 演示模式（无媒体时预览界面 + 示例歌词）
   WinIsland.exe --diagnose   # 输出诊断报告到 %APPDATA%\WinIsland\diagnostics.txt
   WinIsland.exe --settings   # 启动时打开设置
   ```

---

## 配置项说明

配置文件：`%APPDATA%\WinIsland\settings.json`（JSON，修改后重启生效；设置界面可导出/导入）。

| 键 | 默认 | 说明 |
| --- | --- | --- |
| `Language` | `zh-CN` | `zh-CN` / `en-US` |
| `Theme` | `Auto` | `Auto` / `Light` / `Dark` |
| `AccentColor` | `#6C5CE7` | 主题色（#RRGGBB） |
| `Position` | `Center` | `Center` 顶部居中 / `Right` 顶部右侧 |
| `Monitor` | `Primary` | `Primary` 主屏 / `All` 所有屏 / `Index` 指定屏 |
| `MonitorIndex` | `0` | `Monitor=Index` 时的屏幕编号 |
| `OffsetX` / `OffsetY` | `0` / `16` | 像素偏移 |
| `Opacity` | `0.92` | 0.3–1.0 |
| `HideWhenNoMedia` | `true` | 无媒体播放时隐藏灵动岛 |
| `ShowWhenPaused` | `true` | 暂停时仍显示 |
| `StartWithWindows` | `false` | 开机自启 |
| `StartHidden` | `false` | 启动时隐藏 |
| `CompactShowArt/Title/Progress` | `true/true/false` | 紧凑模式内容 |
| `CiderEnabled` | `true` | 启用 Cider 本地 API |
| `CiderPort` | `0` | `0` 自动检测（默认 10767）；手动填端口 |
| `CiderToken` | `""` | Cider API Token（可留空） |
| `OnlineLyricsEnabled` | `true` | 在线歌词（默认开启，右键灵动岛可一键开关；见版权提示） |
| `LyricsFolder` | `""` | 额外 .lrc 目录；留空自动搜索 `%APPDATA%\WinIsland\Lyrics`、`音乐\Lyrics`、`音乐` 顶层 |
| `StandaloneLyricsWindow` | `false` | 独立歌词小窗 |
| `KaraokeHighlight` | `true` | 逐字卡拉OK高亮（当前句按字符点亮） |
| `UseSystemVolume` | `true` | 非 Cider 来源时用系统音量条 |

---

## Cider 集成

Cider（Apple Music 第三方客户端）提供本地 HTTP API。WinIsland 已封装独立模块（`CiderClient.cs`），自动适配版本差异。

**开启步骤（重要）**：
1. 打开 Cider：**设置 → 连接性 → 允许外部控制（Manage External Application Access）**，开启后 Cider 会显示 API Token（若为空白则点击生成）。
2. 将 Token 复制到 **WinIsland 设置 → Cider → API Token** 并保存。
3. 默认端口 `10767`，WinIsland 自动探测；旧版 RPC 为 `10769`。

> ⚠️ Cider 2.x 新版默认**所有 API 请求都需要 Token**（无 Token 会返回 `403 UNAUTHORIZED_APP_TOKEN`）。若诊断日志提示需要 Token，请按上述步骤填入；否则 Cider 歌词/控制不可用（曲目仍可通过 SMTC 显示）。

> ⚠️ 若日志反复出现 HttpClient.Timeout（原 2s），多为本机安全软件/代理拦截回环 HTTP 所致（Cider 实际响应约 30ms）。1.0.1beta12 起数据读取超时放宽到 5s；仍超时请检查杀毒软件对 WinIsland 的联网拦截。

**已实现的 API 能力**（依据 Cider 社区文档 / `cider-api` crate 实测整理，2026 年版本）：
- `GET /api/v1/playback/active`、`GET /now-playing`（曲目/封面/进度/状态）
- `POST /api/v1/playback/play|pause|playpause|next|previous|seek`
- `GET|POST /api/v1/playback/volume`
- `GET /api/v1/lyrics`（含 `?id=` 回退）
- 鉴权头：`apptoken`（兼容 `apitoken`）
- 旧版 10769：`/active`、`/currentPlayingSong`、`/playPause`、`/next`、`/previous`、`/seekto/{t}`、`/audio`

> ⚠️ Cider API 为非官方接口，版本变动快；所有请求 2 秒超时、失败自动降级到 SMTC / 窗口标题，**不影响主流程**。请保持 WinIsland 更新以适配新版本。

---

## 歌词说明

优先级：
1. **本地 .lrc**：按 `歌名.lrc` / `歌手 - 歌名.lrc` 在歌词目录（默认 `%APPDATA%\WinIsland\Lyrics`、`音乐\Lyrics`、`音乐` 顶层）查找；
2. **Cider 歌词接口**（来源为 Cider 时）；
3. **在线歌词**（网易云 / QQ音乐非官方接口）：**默认开启**，右键灵动岛可一键开关，也可在设置中关闭。

> ⚠️ 在线歌词使用非官方接口，仅限个人学习使用，请尊重版权；如版权方要求可随时关闭该功能（关闭后完全无联网）。

---

## 播放状态恢复

- 应用退出、暂停、切歌时会把「曲目 + 播放位置」保存到 `%APPDATA%\WinIsland\state.json`（仅本地）。
- 下次启动若仍是同一曲目且播放器暂未返回真实进度，会先按上次位置恢复，避免「先显示第 0 行、再跳到暂停句」的跳动；超过 1 小时或换了曲目不恢复。

---
## 隐私与安全

- **无遥测、无广告、无上报**。除用户手动开启的“在线歌词”功能外，应用不进行任何网络请求。
- 唯一联网场景：Cider 封面下载（`mzstatic.com`，本地 API 返回的公开封面 URL）、用户开启后的在线歌词。
- 所有数据本地存储于 `%APPDATA%\WinIsland\`。
- 日志仅记录本地运行信息（`logs\app-*.log`）。

---

## 非功能性指标

在测试机（Windows 11 24H2, 2560×1440@100%）实测（Release 自包含）：

| 指标 | 实测 | 目标 |
| --- | --- | --- |
| 空闲 CPU（无媒体） | < 0.5%（Debug 实测 0.3%） | ≈ 0% |
| 常驻内存（Private） | ~72 MB | ≤ 150 MB |
| 启动 | < 1 s（冷启动） | ≤ 2 s |
| 关闭主窗口 | 不退出，仅托盘化 | ✅ |
| 多实例 | 仅单实例，二次启动显示灵动岛 | ✅ |
| 异常 | 统一捕获并写入日志，不弹崩溃框 | ✅ |

> 说明：自包含部署的 WorkingSet（含 .NET 运行时共享页）约 160MB，但 **Private 内存约 72MB**；若使用框架依赖部署，WorkingSet 会更低。

---

## 已知限制

- **逐字卡拉OK依赖歌词来源与进度**：无歌词或播放器不提供真实进度时，逐字效果降级为整句高亮（本地时钟推进）。
- **播放器偶发回退进度**（如 Cider/SMTC 瞬间上报 0 或过期位置）：已做位置守卫——瞬间回退会被忽略，保持当前进度推进，不会把歌词/进度条打回开头；持续回退超过约 4 秒才判定为真正的重播或播放器端 seek。
- **Windows 通知接入**（来电/日历/低电量）：未实现（P2 可选）。低电量提示实现成本低但价值有限，暂缓。
- **SMTC 覆盖范围**：依赖播放器是否注册全局媒体会话；个别旧播放器不注册时仅能通过窗口标题兜底（无控制按钮）。
- **Cider 1.x（端口 9000 旧 API）**：未适配，仅支持 2.x 及以上。

---

## 验证指南（需要配合真实播放器测试）

以下场景需要真实环境配合验证（本仓库开发环境已通过自动化验证的部分会注明）：

| 场景 | 状态 |
| --- | --- |
| SMTC 会话枚举（`--diagnose` 可见会话列表） | ✅ 已实测（检测到 Bilibili 等真实会话） |
| 灵动岛自动显示/隐藏、悬停展开收起、进度插值 | ✅ 已实测（demo + 真实暂停会话） |
| 播放/暂停/切歌/seek（真实播放器） | ⚠️ 需配合测试（代码路径与 SMTC 控制 API 直接对应） |
| Cider API 连接与控制 | ⚠️ 需本机安装 Cider 并开启外部控制后验证 |
| 本地 .lrc 歌词同步滚动 | ✅ LRC 解析已单测；端到端需真实歌曲验证 |
| 在线歌词 | ✅ 已接入网易云/QQ音乐；端到端效果需配合真实歌曲验证 |

**验证步骤建议**：
1. `WinIsland.exe --diagnose` → 确认 `System media sessions` 列出了播放器；
2. 播放网易云/QQ音乐/Spotify 任意一首 → 灵动岛应显示曲目并可控制；
3. 打开 Cider 并开启外部控制 → 灵动岛来源应显示 `Cider`，可 seek/调音量；
4. 在歌曲目录放置同名 `.lrc` → 展开后歌词应随进度滚动高亮。

---

## 常见问题

**Q: 灵动岛没有出现？**
- 确认正在播放（暂停时默认仍显示）；`HideWhenNoMedia` 默认开启，无媒体时隐藏属正常。
- 运行 `--diagnose` 查看会话列表；若列表为空，说明播放器未注册 SMTC。

**Q: Cider 显示“未连接”？**
- 确认 Cider 设置中开启“允许外部控制”；检查端口（默认 10767）；WinIsland 设置里确认 Cider 已启用。

**Q: 在线歌词打不开？**
- 在线歌词默认开启（右键灵动岛 → 在线歌词 可一键开关）；若仍无歌词，请在设置 → 歌词中确认已开启，并自测网络可达性。

**Q: 退出后托盘图标仍在？**
- 托盘菜单 → 退出；直接关闭灵动岛窗口仅隐藏（符合“托盘常驻”设计）。

---

## 开源许可

- 应用本体：MIT（见 [LICENSE](LICENSE)）
- 第三方组件：见 [THIRD_PARTY.md](THIRD_PARTY.md)












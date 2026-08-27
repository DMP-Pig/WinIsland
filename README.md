# WinIsland — Windows 灵动岛

> 把 iOS 灵动岛带到 Windows 的桌面悬浮窗：媒体控制、同步歌词、自定义组件、通知中心、系统托盘常驻。
> 基于 **.NET 8 + WPF**，适配 Windows 11（兼容 Windows 10，1809+）。

---


> **Bring the iOS Dynamic Island to Windows | 把 iOS 的灵动岛带到 Windows —— 一款现代化、多功能的灵动岛组件。**

把 iOS 的灵动岛带到 Windows 11 / 10 —— 媒体播放控制、卡拉OK逐字歌词、可定制组件、通知中心、上岛 API，一个胶囊全搞定。基于 **.NET 8 + WPF**，免费开源（MIT），**无广告 · 无遥测**。

🌐 **官网：https://WinIsland.JudeKwong.com**

---

## ✨ 功能亮点

- **▶ 媒体播放控制**：原生接入 Windows 全局媒体会话（SMTC），兼容网易云、QQ音乐、Spotify、Apple Music、Groove、电影和电视等；额外专门支持 Cider 本地 API；无法接入时窗口标题兜底。专辑封面、进度拖拽 seek、播放/暂停/切歌一应俱全。
- **♪ 卡拉OK逐字歌词**：展开卡片同步滚动高亮，逐字卡拉OK点亮；本地 `.lrc` → 播放器歌词接口 → 可选在线歌词三级来源；双语歌词、翻译开关、一键复制当前行；暂停冻结、重启自动恢复上次播放位置，绝不跳动。
- **▦ 可定制组件系统**：时间、天气、日期（含农历/节气）、CPU/GPU/内存/磁盘、网络速度、电量、输入法、快捷开关（WiFi/蓝牙/夜间/静音）等 30+ 组件；每组件可自定义图标，勾选与拖拽排序，单行/多行模式随时切换。
- **⇪ 上岛 API**：本地 HTTP / WebSocket 接口，让任何第三方软件把信息实时推送到灵动岛（类似 iOS 第三方 App 的灵动岛集成）。v3 支持图片、动态进度、心跳续期；推送不影响灵动岛长宽，不遮挡其他组件。
- **🔔 通知中心**：右上角玻璃横幅，macOS 风格滑入/滑出动画：蓝牙设备、系统通知接管、正在播放、低电量/充电完成、断网/恢复；通知历史、折叠、勿扰白名单、规则自动化。
- **✦ 外观与动效**：18 种主题皮肤、自定义强调色与背景、液态玻璃毛玻璃、4 种动效皮肤（iOS 弹簧等）；展开/收起非线性缓动，60fps 丝滑；PerMonitorV2 高 DPI，120/150/200% 缩放不错位。
- **⚡ 效率工具与自动化**：番茄钟、待办、剪贴板历史、快速启动器、日程提醒；会议静音助手、屏幕录制/截图提示、文件复制/下载进度上岛；全局快捷键与规则引擎（按条件自动显示/隐藏）。
- **🛡 隐私安全**：无遥测、无广告、无数据上报。除用户手动开启的在线歌词/天气外完全离线；所有配置与数据仅存于本机 `%APPDATA%\WinIsland`。

---

## 📥 下载（最新稳定版 1.1.1）

| 平台 | 下载 | 说明 |
| --- | --- | --- |
| Windows x64 | [x64 便携版](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.1/WinIsland-1.1.1-win-x64.exe) | 主流 64 位电脑首选，单文件免安装，直接运行 |
| Windows ARM64 | [ARM64 便携版](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.1/WinIsland-1.1.1-win-arm64.exe) | Surface Pro X / 骁龙机型等 ARM 设备，单文件免安装 |
| Windows 通用 | [通用安装包](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.1/WinIsland-Setup-1.1.1.exe) | Inno Setup 安装向导，x64 / ARM64 自动按架构安装 |

所有历史版本与完整更新日志见 [GitHub Releases](https://github.com/DMP-Pig/WinIsland/releases)。

---

## 📊 性能指标

| 指标 | 数值 |
| --- | --- |
| 常驻内存（Private） | ~72 MB |
| 冷启动 | < 1 s |
| 空闲 CPU | ≈ 0% |
| 动效帧率 | 60 fps 丝滑 |
| 多实例 | 单实例防重复运行 |
| 遥测 | 0 遥测 · 无上报 · 无广告 |

---

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
- **媒体播放控制**：显示歌名、歌手、专辑；可拖拽进度条 seek；播放/暂停、上一首、下一首；需要时提供音量调节（Cider 用其 API，其它来源控制系统音量，可关闭）；媒体组件上显示当前播放来源徽标（Spotify / Cider / 网易云 / QQ音乐等）。
- **迷你播放器**：独立悬浮小窗（设置 → 媒体可开关），展示专辑封面 / 歌名 / 歌手 / 进度条与播放控制，可自由拖动并记忆位置，随媒体播放自动显示 / 隐藏。
- **音频输出设备切换**：设置 → 媒体可枚举并切换系统默认播放设备（切换后建议重启播放器生效）。
- **多来源接入**：
  1. Windows 全局媒体会话（`Windows.Media.Control` / SMTC）——网易云、QQ音乐、Spotify、Apple Music 官方版、Groove、电影和电视等；
  2. **Cider** 本地 HTTP API（端口 10767，兼容旧版 10769 RPC，自动扫描端口 + 手动配置，支持 `apptoken` 鉴权）；
  3. 兜底：窗口标题 + 进程识别（仅展示信息，无控制能力）。
- **歌词显示（逐字卡拉OK模式）**：点击展开后，歌词区以卡拉OK方式显示——**当前句的字逐个点亮**——高亮进度为连续值，边界字符在 60fps 缓动下从基础色平滑混色到高亮色，按阅读顺序从左到右流动（换行歌词也正确，不会多条线同时点亮）；每句从 0 开始（第一个字先不亮）；暂停时高亮冻结在暂停时刻：Cider 无显式状态时按「位置是否移动」判定播放/暂停（不再因 remainingTime>0 误判为播放），SMTC 优先跟随 Cider 会话（避免被 Bilibili 等其它活跃会话抢走）；退出后重启自动恢复上次暂停位置（不跳回开头）；当前句仅文字高亮（无背景胶囊，避免双重高亮；20px 大号），其余句淡化，**平滑滚动自动居中**（60fps 逐帧逼近当前句，展开即自动跟随）；紧凑态左对齐实时显示当前句并同样逐字点亮；可选独立悬浮歌词小窗。
  - **进度同步**：自动读取 Cider 的本地 API Token（零配置）获取真实播放进度，卡拉OK逐字与歌曲精确同步；无进度可用的播放器用本地时钟推进。
  - **歌词来源**：本地 `.lrc`（`%APPDATA%\WinIsland\Lyrics` 或音乐目录）→ Cider 歌词接口 → 在线歌词（右键灵动岛可一键开关）。无歌词时显示"暂无歌词"，不报错。
  - **双语歌词**：自动合并相邻时间戳的翻译行，可在设置中关闭（无需额外歌词文件）；歌词翻译显示 / 隐藏开关，「复制当前行」一键复制当前歌词。
- **系统托盘**：常驻图标，右键菜单（显示/隐藏、独立歌词窗口、开机自启、设置、退出），双击切换显示。

### P1（已实现）
- **组件系统（自定义灵动岛内容）**：设置 → 组件，可分别勾选「无歌曲播放时 / 有歌曲播放时」显示哪些组件，并拖拽调整摆放顺序：
  - 时间、天气（Open-Meteo，需填写城市并联网）、日期（可附加农历与节气）、CPU 占用、GPU 占用、内存占用、网络速度（可显示最近 32 秒迷你曲线）、电量、磁盘剩余空间、输入法状态（中 / 英 + 输入法名）、快捷开关（WiFi / 蓝牙 / 夜间模式 / 静音 一键切换）、音量、键盘指示灯（CapsLock）、剪贴板、待办、番茄钟、日程、节假日倒计时、会议中、麦克风、摄像头；
  - 歌曲信息（封面/歌名/歌手/歌词/进度条，仅播放时显示，顺序条中始终保留）。
  - 顺序条只显示已勾选的组件；列表与顺序条支持鼠标滚轮和滚动条；每个组件可单独自定义图标（MDL2 图标或 Emoji，设置 → 组件）。
  - 临时上岛组件：音量变化、截图 / 录屏、文件复制 / 移动、下载进行中（后两者默认关闭）——事件发生时即使灵动岛隐藏也会临时显示对应组件。
  - **「使用中」合并胶囊**（设置 → 组件，默认关闭）：开启后，勾选的「麦克风 / 摄像头 / 会议中 / 录屏」合并为单个「使用中 · …」状态胶囊，参与合并的项不再单独显示。
  - **单行模式**（设置 → 外观，默认开启）：紧凑态所有组件一行显示，未展开时同样显示歌曲信息与当前歌词（逐字卡拉OK高亮），歌词过长自动截断；进度条与完整歌词列表在展开卡片中显示。
- **展开卡片内容自定义**：封面+标题、进度条、控制按钮与音量、歌词区可分别开关。
- **外观个性化（macOS System Settings 风格设置页）**：左侧导航 + 右侧内容、圆角液态玻璃；**18 种主题皮肤预设**（默认 / 海洋 / 森林 / 日落 / 霓虹 / 单色 / 葡萄紫 / 天空 / 玫瑰 / 琥珀 / 青柠 / 青碧 / 薰衣草 / 绯红 / 午夜 / 咖啡 / 樱花 / 极光 / 自定义）、自定义强调色与背景色（#RRGGBB）、自定义字体、字号缩放（0.8–1.4）、圆角半径（16–40）、**4 种动效皮肤**（iOS 弹簧 / 柔和弹簧 / 弹性回弹 / 简洁渐隐）；展开背景可随专辑封面取色；未读通知角标。**改动即时生效，无“确定 / 完成”按钮**。
- **声音波纹**：播放媒体时，控制按钮左侧显示随系统音量实时抖动的波纹（设置 → 外观可开关，默认开启）。
- **音量 / 静音临时上岛**：系统音量变化或静音时，灵动岛短暂显示音量指示（显示秒数可调，设置 → 通知可关闭）。
- **文件复制 / 移动上岛**：检测到资源管理器「正在复制 / 移动文件」时灵动岛显示提示（纯本地窗口标题识别，可关闭）。
- **下载进度上岛**：检测下载目录中的浏览器临时文件（.crdownload / .part / .download 等），显示「正在下载 N 个文件」（默认关闭，设置 → 效率工具可开启）。
- **番茄钟增强**：点击灵动岛上的番茄钟组件可暂停 / 继续计时。
- **效率工具（设置 → 效率工具）**：剪贴板历史（可选开启，最多保留 N 条）、番茄钟计时（工作/休息时长可调）、待办列表、日程提醒；对应组件可放入灵动岛展示。
- **通知系统（右上角玻璃横幅，带 macOS 风格滑入/滑出动画）**：
  - 蓝牙设备连接/断开提示；
  - 接管 Windows 通知（尽力而为，UI 自动化镜像通知中心）；
  - 正在播放通知（切歌时弹出）；
  - 低电量提醒（阈值可调，每充电周期提醒一次）、充电完成提醒（目标电量阈值可调，默认 100%）、断网 / 网络恢复提示；
  - 通知历史：最近 50 条记录，设置中可查看/清空。
  - 通知历史增强：未读红点标记、全部已读、单条删除、点击条目打开来源应用、清空历史；
  - 通知折叠：同来源同标题的重复通知复用同一横幅并累加数量；
  - 勿扰白名单：白名单内来源（逗号分隔 exe 名）不受勿扰影响，仍正常弹出横幅。
- **全局快捷键**（均可关闭 / 自定义）：`Ctrl+Alt+P` 播放/暂停 · `Ctrl+Alt+←/→` 上一首/下一首 · `Ctrl+Alt+I` 显示/隐藏 · `Ctrl+Alt+Space` 展开/收起 · `Ctrl+Space` 快速启动器 · `Ctrl+Alt+V` 剪贴板历史面板。
- **减少动态效果**（无障碍 / 省电）：一键关闭弹簧动画，瞬时切换。
- **灵动岛尺寸调节**：设置 → 外观，可调紧凑长度/宽度、展开长度。
- **灵动岛常驻**：即使无媒体播放也始终显示（显示配置的组件）。
- **多显示器**：主屏幕 / 所有屏幕 / 指定屏幕编号。
- **高 DPI**：PerMonitorV2，120/150/200% 缩放下不错位。
- **自定义配置**：位置、偏移、不透明度、主题色、紧凑模式内容、无媒体时隐藏等，改动即时生效。
- **无媒体播放时自动隐藏灵动岛**（可关闭）。
- **勿扰模式**：手动一键开启或按时间段自动静默通知（托盘菜单一键切换，设置中可配置时段）。
- **检查更新**：托盘菜单 / 设置中手动检查 GitHub 新版本；可选自动检查（默认关闭，需联网）。
- **双击灵动岛快捷动作**（设置 → 通用）：可设为「播放 / 暂停」（默认）、「打开设置」或「无动作」。
- **开会静音助手（会议检测）**：识别 Teams / Zoom / 腾讯会议 / 钉钉 / 飞书 / Webex / Slack / Discord / Google Meet 等会议窗口，会议中自动开启勿扰并显示「会议中」组件（纯本地启发式，不联网）。
- **屏幕录制 / 截图提示**：按 `PrintScreen` / `Alt+PrintScreen` 截图时弹出提示；检测到 OBS、Bandicam、Fraps、Camtasia、XSplit、Streamlabs、Xbox Game Bar 等录制软件时弹出「屏幕录制中」（纯本机进程检测，不联网）。
- **日历事件提醒（.ics）**：解析本地 iCalendar 文件（Outlook / Google 日历 / 手机导出），事件到点（可提前 N 分钟）弹横幅；纯本地解析，不联网。
- **RSS 订阅提醒**：轮询 RSS 2.0 / Atom 订阅（间隔可调），出现新条目弹横幅；仅在填写的订阅地址联网。
- **邮件提醒（POP3）**：定期拉取邮件头，新邮件弹横幅（只读邮件头、不下载正文、不上传数据；建议使用授权码）。
- **快速启动器（Spotlight 风格）**：`Ctrl+Space` 呼出，搜索已安装应用 / 开始菜单程序，或直接输入网址打开；快捷键可自定义。
- **剪贴板历史面板**：`Ctrl+Alt+V` 呼出独立剪贴板历史窗口，点击条目复制回剪贴板、可清空；快捷键可自定义。
- **规则（自动化）**：设置 → 规则，组合条件（始终 / 未播放媒体 / 播放中 / 时间段 / 指定媒体程序）与动作（隐藏 / 强制收起 / 强制显示）自动控制灵动岛；隐藏优先、折叠其次、强制显示最后。
- **低功耗模式**：空闲时降低波纹帧率、简化动画，更省电（设置 → 通用）。

### P2（已实现）
- 简体中文 + English 界面切换。
- 导出 / 导入 JSON 配置文件。
- Windows 通知接入（蓝牙 / 系统通知接管 / 正在播放 / 低电量）。
- 待定：来电提醒（未实现）；日程提醒已实现（组件 + 效率工具）。

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
│   ├── GlobalHotkeyService.cs # 全局快捷键（Win32 RegisterHotKey）
│   ├── NotificationService.cs # 右上角玻璃通知横幅
│   ├── NotificationHistoryService.cs # 通知历史（最近 50 条，JSON 持久化）
│   ├── BluetoothMonitor.cs    # 蓝牙设备连接/断开监控
│   ├── SystemNotificationMonitor.cs # 接管 Windows 通知（UI 自动化镜像）
│   ├── MediaAppRegistry.cs    # 媒体程序注册表（启用/禁用/排序）
│   ├── AudioWaveService.cs    # 声音波纹（系统音量采样，驱动波纹抖动）
│   ├── KeyboardIndicatorMonitor.cs # 键盘指示灯（CapsLock 状态监听）
│   ├── ClipboardHistoryService.cs # 剪贴板历史
│   ├── TodoService.cs         # 待办列表
│   ├── PomodoroService.cs     # 番茄钟计时
│   ├── ScheduleService.cs     # 日程提醒
│   ├── IcsCalendar.cs       # .ics 日历解析（事件 / VALARM）
│   ├── MeetingMonitor.cs    # 会议窗口检测（开会静音助手）
│   ├── PrivacyDeviceMonitor.cs # 麦克风/摄像头使用状态（隐私注册表轮询）
│   ├── RssMailService.cs    # RSS 订阅 + 邮件（POP3）提醒
│   ├── ScreenCaptureMonitor.cs # 截图 / 录屏检测提示
│   ├── IslandApiServer.cs   # 上岛 API（v1 + v3 HTTP / WebSocket）
│   ├── IslandPushModels.cs  # 上岛卡片模型（图片/动态进度/心跳）
│   ├── DoNotDisturb.cs        # 勿扰模式（手动/时段）
│   ├── UpdaterService.cs      # GitHub 更新检查
│   ├── ProfileService.cs      # 配置档案（多套设置切换）
│   ├── WeatherService.cs      # 天气组件（Open-Meteo，需联网）
│   ├── PlaybackStateStore.cs  # 播放位置持久化（退出/暂停后恢复）
│   ├── CiderTokenAutoDetect.cs # Cider API Token 自动检测
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
│   ├── ClipboardPanelWindow.xaml(.cs) # 剪贴板历史面板
│   ├── QuickLauncherWindow.xaml(.cs)  # 快速启动器（Ctrl+Space）
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

> 💡 **预编译版**：`releases/` 目录按版本提供单文件自包含可执行文件（如 `releases/1.1.1/win-x64/WinIsland-1.1.1-win-x64.exe`，含 .NET 8 运行时，双击即可运行）。Beta 版本仅本地保留；稳定版本才发布到 GitHub（含 win-x64 / win-arm64 便携版及通用安装包）。

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
# 自包含（含 .NET 8 运行时，免安装，约 73MB（单文件））
.\build\publish.ps1

# 框架依赖（体积小，需安装 .NET 8 Desktop Runtime）
.\build\publish.ps1 -FrameworkDependent
```
产物位于 `publish\win-x64\`（含 `WinIsland.exe`），zip 为 `publish\WinIsland-win-x64.zip`。

### 安装包（可选）
安装 [Inno Setup 6](https://jrsoftware.org/isinfo.php) 后：
```powershell
iscc.exe build\release-1.1.1.iss
```
生成 `releases\<version>\WinIsland-Setup-<version>.exe`（通用安装包，同时支持 x64 与 ARM64，自动按架构安装）。稳定版发布时在 `build\` 下按版本复制一份 `release-<version>.iss` 并更新版本号。

---

## 使用说明

1. 启动 `WinIsland.exe`（或设为开机自启 / 安装包勾选自启）。托盘出现图标。
2. 播放任意音乐：
   - 网易云、QQ音乐、Spotify、Apple Music 官方版等 → 自动通过系统媒体会话显示；
   - Cider → 详见 [Cider 集成](#cider-集成)；
   - 其它播放器 → 兜底窗口标题识别（仅展示）。
3. **点击**灵动岛展开完整卡片（悬停不展开）：进度拖拽 seek、播放控制、音量、同步歌词；再点一下收回（移出卡片后 700ms 自动收回）。
4. 托盘菜单：显示/隐藏、独立歌词窗口、开机自启、**勿扰模式**（勾选即静默通知）、**检查更新**、**查看日志**、设置、退出。**关闭主窗口不会退出进程**（仅托盘化）。
5. 全局快捷键：`Ctrl+Alt+P` 播放/暂停 · `Ctrl+Alt+←/→` 上一首/下一首 · `Ctrl+Alt+I` 显示/隐藏 · `Ctrl+Alt+Space` 展开/收起 · `Ctrl+Space` 快速启动器（搜索应用 / 直接输入网址回车）· `Ctrl+Alt+V` 剪贴板历史面板（均可关闭 / 自定义）。
6. 通知与提示（蓝牙 / Windows 通知 / 正在播放 / 低电量）默认在屏幕右上角弹出玻璃横幅，可在设置 → 通知中开关；**勿扰模式**开启时不弹横幅（角标仍计数）。
7. 常用命令行参数：
   ```powershell
   WinIsland.exe --demo       # 演示模式（无媒体时预览界面 + 示例歌词）
   WinIsland.exe --diagnose   # 输出诊断报告到 %APPDATA%\WinIsland\diagnostics.txt
   WinIsland.exe --settings   # 启动时打开设置
   ```

---

## 配置项说明

配置文件：`%APPDATA%\WinIsland\settings.json`（JSON；设置界面改动即时生效，可导出/导入）。

| 键 | 默认 | 说明 |
| --- | --- | --- |
| `Language` | `zh-CN` | `zh-CN` / `en-US` |
| `ThemePreset` | `Default` | 主题皮肤：`Default/Ocean/Forest/Sunset/Neon/Mono/Grape/Sky/Rose/Amber/Lime/Teal/Lavender/Crimson/Midnight/Coffee/Sakura/Aurora/Custom`（覆盖 AccentColor） |
| `FontFamily` | `Segoe UI` | 界面字体 |
| `FontScale` | `1.0` | 字号缩放 0.8–1.4 |
| `CornerRadius` | `28` | 胶囊圆角 16–40 |
| `BadgeEnabled` | `true` | 未读通知角标（右上角红点 + 数字） |
| `CoverTintBackground` | `true` | 展开背景随专辑封面取色 |
| `WaveVisualizerEnabled` | `true` | 播放媒体时控制按钮左侧声音波纹 |
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
| `IsLocked` | `true` | 上锁（不可拖动）；右键菜单可解锁/上锁/居中对齐 |
| `IslandAlwaysVisible` | `false` | 灵动岛常驻（无媒体时也显示组件） |
| `ShowMediaInfo` | `true` | 显示媒体播放信息（歌名/封面/歌词等） |
| `ReduceMotion` | `false` | 减少动态效果（关闭弹簧动画，无障碍/省电） |
| `GlobalHotkeysEnabled` | `true` | 全局快捷键开关 |
| `LowBatteryThreshold` | `20` | 低电量提醒阈值（%），0 关闭 |
| `ExpandedShowArtTitle/Progress/Controls/Lyrics` | `true` | 展开卡片各区块（封面+标题/进度条/控制与音量/歌词）开关 |
| `Components` | 对象 | 组件勾选：`Time/Weather/Date/Cpu/Gpu/Ram/Net/Battery/Volume/CapsLock/Clipboard/Todo/Timer/Schedule/Holiday/Meeting/Mic/Cam` 各有 `WhenIdle`/`WhenPlaying` 两列；`Cover/Title/Artist/Lyrics/Progress` 播放时显示；`ComponentBadges` 字典为各组件填角标文本 |
| `WidgetOrder` | `Time,Weather,...` | 组件摆放顺序（逗号分隔键名，含 `Song`） |
| `MediaApps` | `[]` | 媒体程序启用/禁用与优先级（空=全部启用） |
| `CompactWidth` / `CompactHeight` | `360` / `72` | 紧凑长度 / 紧凑宽度（手动拖拽调整会自动关闭自动调整） |
| `CompactWidthAuto` / `CompactHeightAuto` | `true` | 紧凑尺寸随组件内容自动调整（默认开启） |
| `ExpandedWidthAuto` / `MaxExpandedHeightAuto` | `true` | 展开尺寸自动调整（默认开启） |
| `ExpandedWidth` / `MaxExpandedHeight` | `400` / `384` | 展开长度 / 展开最大高度 |
| `BluetoothNotifyEnabled` | `false` | 蓝牙连接/断开提示 |
| `NotificationTakeoverEnabled` | `false` | 接管 Windows 通知（尽力而为） |
| `NotificationTimeoutSeconds` | `6` | 通知横幅显示时长（秒） |
| `NotificationPosition` | `TopRight` | 通知弹出位置（右上角） |
| `DoNotDisturbEnabled` / `DoNotDisturbManual` | `false` | 勿扰：按时段自动 / 手动开关 |
| `DoNotDisturbStartHour` / `DoNotDisturbEndHour` | `22` / `8` | 勿扰时段（小时） |
| `DnDAllowlist` | `[]` | 勿扰白名单（`QQ.exe,WeChat.exe`，白名单内仍弹通知） |
| `Rules` | `[]` | 自动化规则列表（条件 + 动作） |
| `ClipboardHistoryEnabled` / `ClipboardHistoryMax` | `false` / `15` | 剪贴板历史开关与条数上限 |
| `PomodoroEnabled` / `PomodoroWorkMinutes` / `PomodoroBreakMinutes` | `false` / `25` / `5` | 番茄钟开关与工作/休息时长（分钟） |
| `KeyIndicatorSeconds` | `3` | 键盘指示灯（CapsLock）出现时长（秒） |
| `VolumeTempIndicatorEnabled` / `VolumeTempIndicatorSeconds` | `true` / `4` | 音量 / 静音临时上岛开关与显示秒数 |
| `FileCopyNotifyEnabled` | `true` | 文件复制 / 移动进行中上岛（纯本地窗口标题识别） |
| `DownloadProgressEnabled` | `false` | 下载进行中上岛（扫描下载目录临时文件，默认关） |
| `UsageMergeEnabled` / `UsageMergeItems` | `false` / `Mic,Cam,Meeting,Recording` | 「使用中」合并胶囊开关与参与合并的组件（默认关） |
| `AutoUpdateCheck` | `false` | 自动检查 GitHub 新版本（默认关，需联网） |
| `DoubleClickAction` | `PlayPause` | 双击灵动岛动作：`PlayPause` / `OpenSettings` / `None` |
| `AnimationStyle` | `Spring` | 动效皮肤：`Spring` / `Soft` / `Elastic` / `Fade` |
| `ThemeTint` | `""` | 自定义背景色 #RRGGBB（预设为 Custom 时生效） |
| `ExpandedCardStyle` | `Classic` | 展开卡片模板：`Classic` / `Hero` |
| `NetCurveEnabled` | `true` | 网络组件显示最近 32 秒迷你曲线 |
| `LowPowerMode` | `false` | 低功耗模式（空闲降低波纹帧率、简化动画） |
| `MeetingAssistantEnabled` | `false` | 开会静音助手：检测会议窗口 + 自动勿扰 |
| `MeetingAutoDnd` / `MeetingKeywords` | `true` / `""` | 会议中自动勿扰 / 自定义会议关键词 |
| `ScreenCaptureNotifyEnabled` / `ScreenshotNotifyEnabled` / `RecordingNotifyEnabled` | `false` / `true` / `true` | 截图/录屏提示总开关与分项 |
| `CalendarEnabled` / `CalendarIcsPath` / `CalendarAdvanceMinutes` | `false` / `""` / `10` | .ics 日历提醒开关 / 文件路径 / 提前提醒分钟 |
| `RssNotifyEnabled` / `RssUrls` / `RssIntervalMinutes` | `false` / `""` / `15` | RSS 订阅提醒 / 订阅地址 / 轮询间隔（分钟） |
| `MailNotifyEnabled` / `MailPop3Server` / `MailPop3Port` / `MailUseSsl` / `MailUser` / `MailPassword` / `MailCheckMinutes` | `false` / `""` / `995` / `true` / `""` / `""` / `5` | 邮件提醒（POP3）开关、服务器、端口、SSL、账号、授权码、检查间隔 |
| `QuickLauncherEnabled` / `HotkeyLauncher` | `true` / `Ctrl+Space` | 快速启动器开关与快捷键 |
| `ClipboardPanelEnabled` / `HotkeyClipboardPanel` | `true` / `Ctrl+Alt+V` | 剪贴板历史面板开关与快捷键 |
| `HotkeyExpand` | `Ctrl+Alt+Space` | 展开/收起快捷键 |
| `NotifyFoldEnabled` | `true` | 折叠同类通知（同来源同标题只显示一条） |
| `ActiveProfile` | `Default` | 配置档案名（多套设置切换） |

---


## Cider 集成

Cider（Apple Music 第三方客户端）提供本地 HTTP API。WinIsland 已封装独立模块（`CiderClient.cs`），自动适配版本差异。

**开启步骤（重要）**：
1. 打开 Cider：**设置 → 连接性 → 允许外部控制（Manage External Application Access）**，开启后 Cider 会显示 API Token（若为空白则点击生成）。
2. 将 Token 复制到 **WinIsland 设置 → Cider → API Token** 并保存。
3. 默认端口 `10767`，WinIsland 自动探测；旧版 RPC 为 `10769`。

> ⚠️ Cider 2.x 新版默认**所有 API 请求都需要 Token**（无 Token 会返回 `403 UNAUTHORIZED_APP_TOKEN`）。若诊断日志提示需要 Token，请按上述步骤填入；否则 Cider 歌词/控制不可用（曲目仍可通过 SMTC 显示）。

> ⚠️ 若日志反复出现 HttpClient.Timeout（原 2s），多为本机安全软件/代理拦截回环 HTTP 所致（Cider 实际响应约 30ms）。自 1.0.1 起数据读取超时放宽到 5s；仍超时请检查杀毒软件对 WinIsland 的联网拦截。

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

## 通知与提示（1.0.2 起，1.0.3 完善）

所有通知均为**右上角玻璃横幅**，带 macOS 风格滑入（从右侧滑出 + 淡入）与滑出动画，显示时长可配置（3~15 秒）。

- **蓝牙连接提示**：设置 → 通知，开启后蓝牙设备连接/断开时弹出。
- **接管 Windows 通知**：设置 → 通知，开启后通过 UI 自动化尽力镜像通知中心内容（QQ 等应用的通知）到右上角横幅。
  > ⚠️ Windows 未提供公开的“拦截其它应用通知”API，此功能为尽力而为（best effort），部分通知可能无法捕获；不影响主流程。
- **正在播放通知**：切换歌曲时自动弹出「正在播放 - 歌名」横幅（1.0.3 起）。
- **低电量提醒**：电量低于阈值（默认 20%，0~50 可调）时弹出，每个充电周期提醒一次（1.0.3 起）。
- **通知历史**：最近 50 条通知记录，设置 → 通知 页可查看 / 清空（1.0.3 起）。
- **灵动岛尺寸调节**：设置 → 外观，可调紧凑长度/宽度、展开长度。

---
## 上岛 API（其他软件推送到灵动岛）

WinIsland 内置本地 HTTP 服务，其他软件可将信息实时推送到灵动岛（类似 iOS 灵动岛第三方 App 集成）。**开发文档见 [docs/IslandAPI.md](docs/IslandAPI.md)**。

| 接口 | 说明 |
|---|---|
| `POST /v1/island/push` | 推送 / 更新一张灵动岛卡片（v3 起支持图片 / 动态进度 / 心跳） |
| `PATCH /v3/island/push/{id}` | 部分更新：只覆盖请求体里出现的字段（保留过期时间 / 队列位置） |
| `DELETE /v1/island/push/{id}` | 移除一张卡片 |
| `GET /v1/island/active`（或 `/v3/island/active`） | 查询当前活跃卡片 |
| `GET /v3/ws` | WebSocket 双向通道：客户端发 `push/update/remove/ping`，服务端广播 `push_updated/push_removed` 事件 |
| `GET /v1/health` | 健康检查 |

- 设置 → 上岛 API：启用开关、端口（默认 9840）、可选 Token、全局默认显示时长
- 上岛推送**不会改变灵动岛长宽**，卡片在紧凑态单行展示、不遮挡其它组件
- 按钮支持「打开链接 / 启动程序」，推送方可按条自定义显示时长（覆盖全局默认）
- v3 新增：`image`（data URI 或 http 图片）、`progress_from/progress_to/progress_duration_seconds`（进度自动推进）、`heartbeat_seconds`（心跳续期，超过 2 倍间隔未续期自动移除）；完整开发文档见 [docs/IslandAPI.md](docs/IslandAPI.md)

---
## 播放状态恢复

- 应用退出、暂停、切歌时会把「曲目 + 播放位置」保存到 `%APPDATA%\WinIsland\state.json`（仅本地）。
- 下次启动若仍是同一曲目且播放器暂未返回真实进度，会先按上次位置恢复，避免「先显示第 0 行、再跳到暂停句」的跳动；超过 1 小时或换了曲目不恢复。

---
## 隐私与安全

- **无遥测、无广告、无上报**。除用户手动开启的“在线歌词”功能外，应用不进行任何网络请求。
- **天气组件**：仅当你开启“显示天气”并填写城市时，会请求 Open-Meteo（免费、无 Key、无账号）获取当前天气；未开启则完全离线。
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
- **来电提醒**：未实现（P2 可选）。已实现：蓝牙提示、Windows 通知接管（尽力而为）、正在播放通知、低电量提醒、日程提醒。
- **SMTC 覆盖范围**：依赖播放器是否注册全局媒体会话；个别旧播放器不注册时仅能通过窗口标题兜底（无控制按钮）。
- **Cider 1.x（端口 9000 旧 API）**：未适配，仅支持 2.x 及以上。

---

## 验证指南（需要配合真实播放器测试）

以下场景需要真实环境配合验证（本仓库开发环境已通过自动化验证的部分会注明）：

| 场景 | 状态 |
| --- | --- |
| SMTC 会话枚举（`--diagnose` 可见会话列表） | ✅ 已实测（检测到 Bilibili 等真实会话） |
| 灵动岛自动显示/隐藏、点击展开收起、进度插值 | ✅ 已实测（demo + 真实暂停会话） |
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












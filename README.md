<div align="center">

**🌐 选择语言 / Select Language**

[简体中文](#简体中文) · [繁體中文](#繁體中文) · [English](#english) · [Español](#español) · [Français](#français) · [العربية](#العربية) · [Русский](#русский) · [Português](#português)

</div>

> **说明 / Note**: 以简体中文为标准 · Simplified Chinese is the standard reference.

---

## 简体中文

## WinIsland — Windows 灵动岛

> 把 iOS 灵动岛带到 Windows 的桌面悬浮窗：媒体控制、同步歌词、自定义组件、通知中心、系统托盘常驻。
> 基于 **.NET 8 + WPF**，适配 Windows 11（兼容 Windows 10，1809+）。

---


> **Bring the iOS Dynamic Island to Windows | 把 iOS 的灵动岛带到 Windows —— 一款现代化、多功能的灵动岛组件。**

把 iOS 的灵动岛带到 Windows 11 / 10 —— 媒体播放控制、卡拉OK逐字歌词、可定制组件、通知中心、上岛 API，一个胶囊全搞定。基于 **.NET 8 + WPF**，免费开源（MIT），**无广告 · 无遥测**。

🌐 **官网：https://WinIsland.JudeKwong.com**

---

## ✨ 功能亮点

- **▶ 媒体播放控制**：原生接入 Windows 全局媒体会话（SMTC），兼容网易云、QQ音乐、Spotify、Apple Music、Groove、电影和电视等；额外专门支持 Cider 本地 API；无法接入时窗口标题兜底。专辑封面、进度拖拽 seek、播放/暂停/切歌一应俱全；多播放器同时打开时可一键切换控制来源；点击封面可全屏沉浸预览。
- **♪ 卡拉OK逐字歌词**：展开卡片同步滚动高亮，逐字卡拉OK点亮；本地 `.lrc` → 播放器歌词接口 → 可选在线歌词三级来源；双语歌词、翻译开关、一键复制当前行；歌词时间可每首歌微调对齐，独立歌词小窗可调透明度与锁定。
- **▦ 可定制组件系统**：时间、天气、日期（含农历/节气）、CPU/GPU/内存/磁盘、网络速度、电量、输入法、快捷开关（WiFi/蓝牙/夜间/静音）等 30+ 组件；每组件可自定义图标，勾选与拖拽排序，单行/多行模式随时切换。
- **⇪ 上岛 API**：本地 HTTP / WebSocket 接口，让任何第三方软件把信息实时推送到灵动岛（类似 iOS 第三方 App 的灵动岛集成）。v3 支持图片、动态进度、心跳续期、卡片深浅主题；推送不影响灵动岛长宽，不遮挡其他组件；按钮支持打开链接 / 启动程序 / 执行本地命令，notify 按钮点击可通过 WebSocket 回调推送方。
- **🔔 通知中心**：右上角玻璃横幅，macOS 风格滑入/滑出动画：蓝牙设备、微信/QQ 语音视频通话提醒、系统通知接管、正在播放、低电量/充电完成、断网/恢复；通知历史、折叠、勿扰白名单、规则自动化；横幅可携带操作按钮（如蓝牙「断开」「设置」）。
- **✦ 外观与动效**：18 种主题皮肤、自定义强调色与背景、液态玻璃毛玻璃、**壁纸取色**（自动从当前壁纸提取主题色）、**跑马灯**（长歌名/歌词自动横向滚动）、4 种动效皮肤（iOS 弹簧等）、**4 种音频波纹样式**（柱状 / 频谱 / 环形 / 粒子，随音乐节奏抖动）；展开/收起非线性缓动，60fps 丝滑；封面取色背景可缓慢「呼吸」起伏（动态主题）；PerMonitorV2 高 DPI，120/150/200% 缩放不错位。
- **🖱 交互与智能**：解锁拖动 + **边缘吸附**（松手吸顶/吸边/居中）、**全屏自动隐藏**（全屏视频/游戏/演示时自动收起）、双击动作自定义、**快捷操作按钮**（展开卡片一键锁屏/静音/播放暂停/截图/显示桌面等，可自定义顺序）、**拖文件上岛**、**录屏智能勿扰**；按下鼠标立即响应展开/收起（点击抢先）。
- **⚡ 效率工具与自动化**：番茄钟、待办、剪贴板历史、快速启动器、日程提醒；会议静音助手、屏幕录制/截图提示、文件复制/下载进度上岛；全局快捷键与规则引擎（按条件自动显示/隐藏）。
- **🛡 隐私安全**：无遥测、无广告、无数据上报。除用户手动开启的在线歌词/天气外完全离线；所有配置与数据仅存于本机 `%APPDATA%\WinIsland`。

---

## 📥 下载（最新稳定版 1.1.5）

| 平台 | 下载 | 说明 |
| --- | --- | --- |
| Windows x64 | [x64 便携版](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-x64.exe) | 主流 64 位电脑首选，单文件免安装，直接运行 |
| Windows ARM64 | [ARM64 便携版](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-arm64.exe) | Surface Pro X / 骁龙机型等 ARM 设备，单文件免安装 |
| Windows 通用 | [通用安装包](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-Setup-1.1.5.exe) | Inno Setup 安装向导，x64 / ARM64 自动按架构安装 |

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
- **锁定与拖动**：默认上锁不可移动；右键菜单可**解锁**（解锁后鼠标拖动灵动岛）、**居中对齐**（上下不变、左右居中）、再次**上锁**。解锁拖动后重新上锁会**保持拖动位置**（不回归默认）；拖动松手可**边缘吸附**（吸附屏幕边缘/居中，设置 → 通用可开关）。
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
  - **歌词来源**：本地 `.lrc`（`%APPDATA%\WinIsland\Lyrics` 或音乐目录）→ AMLL 逐字歌词 → Cider 歌词接口 → 在线歌词（右键灵动岛可一键开关）。无歌词时显示"暂无歌词"，不报错。
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
- **外观个性化（macOS System Settings 风格设置页）**：左侧导航 + 右侧内容、圆角液态玻璃；**18 种主题皮肤预设**（默认 / 海洋 / 森林 / 日落 / 霓虹 / 单色 / 葡萄紫 / 天空 / 玫瑰 / 琥珀 / 青柠 / 青碧 / 薰衣草 / 绯红 / 午夜 / 咖啡 / 樱花 / 极光 / 自定义）、自定义强调色与背景色（#RRGGBB）、自定义字体、字号缩放（0.8–1.4）、圆角半径（16–40）、**4 种动效皮肤**（iOS 弹簧 / 柔和弹簧 / 弹性回弹 / 简洁渐隐）；展开背景可随专辑封面取色；**壁纸取色主题**（从当前壁纸提取主色作为主题色，纯本地、不联网）；未读通知角标。**改动即时生效，无“确定 / 完成”按钮**。
- **音频波纹**：播放媒体时，控制按钮左侧显示**随音乐节奏实时抖动**的波纹（采集系统输出声音，非音量条），**4 种样式**（柱状 / 频谱 / 环形 / 粒子），灵敏度、高度可调；设置 → 外观可开关，默认开启。
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
- **双击灵动岛快捷动作**（设置 → 通用）：可设为「播放 / 暂停」（默认）、「展开 / 收起」、「显示桌面」、「隐藏 / 显示灵动岛」、「上一首」、「下一首」、「打开设置」或「无动作」。
- **开会静音助手（会议检测）**：识别 Teams / Zoom / 腾讯会议 / 钉钉 / 飞书 / Webex / Slack / Discord / Google Meet 等会议窗口，会议中自动开启勿扰并显示「会议中」组件（纯本地启发式，不联网）。
- **屏幕录制 / 截图提示**：按 `PrintScreen` / `Alt+PrintScreen` 截图时弹出提示；检测到 OBS、Bandicam、Fraps、Camtasia、XSplit、Streamlabs、Xbox Game Bar 等录制软件时弹出「屏幕录制中」（纯本机进程检测，不联网）。
- **智能勿扰（录屏）**：检测到屏幕录制进行中时自动静默通知（不弹横幅），结束录制自动恢复；设置 → 通知可开关。
- **全屏自动隐藏**：检测到全屏视频 / 游戏 / 演示（PowerPoint 放映等）时灵动岛自动隐藏/收起，退出全屏恢复；设置 → 通用可开关。
- **拖文件上岛**：把文件 / 文件夹拖到灵动岛上可执行「复制路径 / 打开所在文件夹 / 固定到上岛」等操作（右键灵动岛或拖放菜单选择）。
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

> 💡 **预编译版**：`releases/` 目录按版本提供单文件自包含可执行文件（如 `releases/1.1.5/win-x64/WinIsland-1.1.5-win-x64.exe`，含 .NET 8 运行时，双击即可运行）。Beta 版本仅本地保留；稳定版本才发布到 GitHub（含 win-x64 / win-arm64 便携版及通用安装包）。

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
iscc.exe build\release-1.1.5.iss
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
| `WaveVisualizerEnabled` | `true` | 播放媒体时控制按钮左侧音频波纹 |
| `WaveStyle` | `Bars` | 波纹样式：`Bars`（柱状）/ `Spectrum`（频谱）/ `Ring`（环形）/ `Particles`（粒子） |
| `WaveSyncEnabled` | `true` | 波纹跟随音乐节奏（采集系统输出声音驱动） |
| `WaveSensitivity` | `1.0` | 波纹灵敏度 0.2–3.0 |
| `WaveHeight` | `1.0` | 波纹高度 0.4–1.6 |
| `WallpaperThemeColorEnabled` | `false` | 壁纸取色：从当前壁纸提取主色作为主题色（纯本地） |
| `MarqueeTextEnabled` | `false` | 跑马灯：歌名/歌词超宽时自动横向滚动 |
| `EdgeSnapEnabled` | `true` | 解锁拖动松手自动吸附屏幕边缘/居中 |
| `FullScreenAutoHideEnabled` | `true` | 全屏（视频/游戏/演示）时自动隐藏灵动岛 |
| `RecordingDndEnabled` | `false` | 录屏时自动勿扰（不弹通知横幅） |
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
| `DoubleClickAction` | `PlayPause` | 双击灵动岛动作：`PlayPause` / `ToggleExpand` / `ShowDesktop` / `ToggleVisible` / `NextTrack` / `PrevTrack` / `OpenSettings` / `None` |
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
2. **AMLL 逐字歌词**（amll.dev 曲库 TTML 逐字时间轴，默认开启）；
3. **Cider 歌词接口**（来源为 Cider 时）；
4. **在线歌词**（网易云 / QQ音乐非官方接口）：**默认开启**，右键灵动岛可一键开关，也可在设置中关闭。

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
- v3 新增：`image`（data URI 或 http 图片）、`progress_from/progress_to/progress_duration_seconds`（进度自动推进）、`heartbeat_seconds`（心跳续期，超过 2 倍间隔未续期自动移除）、`theme`（卡片 dark/light/auto 主题）、`action: "command"`（按钮执行本地命令）；完整开发文档见 [docs/IslandAPI.md](docs/IslandAPI.md)

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

- **逐字卡拉OK依赖歌词来源与进度**：有 AMLL TTML/LRC 逐字时间轴时按字高亮；无逐字时间轴或播放器不提供真实进度时降级为整句高亮（本地时钟推进）。
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

---

## 繁體中文

## WinIsland — Windows 靈動島

> 把 iOS 的靈動島帶到 Windows 的桌面懸浮視窗：媒體控制、同步歌詞、自訂元件、通知中心、系統匣常駐。
> 基於 **.NET 8 + WPF**，適用於 Windows 11（相容 Windows 10，1809+）。

---


> **Bring the iOS Dynamic Island to Windows | 把 iOS 的靈動島帶到 Windows —— 一款現代化、多功能的靈動島元件。**

把 iOS 的靈動島帶到 Windows 11 / 10 —— 媒體播放控制、卡拉OK逐字歌詞、可自訂元件、通知中心、上島 API，一個膠囊全搞定。基於 **.NET 8 + WPF**，免費開源（MIT），**無廣告 · 無遙測**。

🌐 **官網：https://WinIsland.JudeKwong.com**

---

## ✨ 功能亮點

- **▶ 媒體播放控制**：原生整合 Windows 全域媒體工作階段（SMTC），相容網易雲音樂、QQ音樂、Spotify、Apple Music、Groove、電影與電視等；另外也專門支援 Cider 本機 API；無法整合時以視窗標題為備援。專輯封面、進度拖曳 seek、播放/暫停/切歌一應俱全；同時開啟多個播放器時可一鍵切換控制來源；點擊封面可全螢幕沉浸式預覽。
- **♪ 卡拉OK逐字歌詞**：展開卡片同步捲動高亮，逐字卡拉OK點亮；本機 `.lrc` → 播放器歌詞介面 → 可選線上歌詞三級來源；雙語歌詞、翻譯開關、一鍵複製目前這一行；歌詞時間可每首歌微調對齊，獨立歌詞小視窗可調整透明度與鎖定。
- **▦ 可自訂元件系統**：時間、天氣、日期（含農曆/節氣）、CPU/GPU/記憶體/磁碟、網路速度、電量、輸入法、快速切換（WiFi/藍牙/夜間/靜音）等 30+ 元件；每個元件可自訂圖示，勾選與拖曳排序，單行/多行模式隨時切換。
- **⇪ 上島 API**：本機 HTTP / WebSocket 介面，讓任何第三方軟體即時推播資訊到靈動島（類似 iOS 第三方 App 的靈動島整合）。v3 支援圖片、動態進度、心跳續期、卡片深淺主題；推播不影響靈動島長寬，不遮擋其他元件；按鈕支援開啟連結 / 啟動程式 / 執行本機命令，notify 按鈕點擊可透過 WebSocket 回呼推播方。
- **🔔 通知中心**：右上角玻璃橫幅，macOS 風格滑入/滑出動畫：藍牙裝置、微信/QQ 語音視訊通話提醒、系統通知接管、正在播放、低電量/充電完成、斷網/恢復；通知紀錄、摺疊、請勿打擾白名單、規則自動化；橫幅可攜帶操作按鈕（如藍牙「中斷連線」「設定」）。
- **✦ 外觀與動效**：18 種主題面板、自訂強調色與背景、液態玻璃毛玻璃、**桌布取色**（自動從目前桌布擷取主題色）、**跑馬燈**（長歌名/歌詞自動橫向捲動）、4 種動效面板（iOS 彈簧等）、**4 種音訊波紋樣式**（柱狀 / 頻譜 / 環形 / 粒子，隨音樂節奏抖動）；展開/收合非線性緩動，60fps 流暢；封面取色背景可緩慢「呼吸」起伏（動態主題）；PerMonitorV2 高 DPI，120/150/200% 縮放不錯位。
- **🖱 互動與智慧**：解鎖拖曳 + **邊緣吸附**（放開滑鼠吸附頂部/側邊/置中）、**全螢幕自動隱藏**（全螢幕影片/遊戲/簡報時自動收合）、自訂雙擊動作、**快速操作按鈕**（展開卡片一鍵鎖定螢幕/靜音/播放暫停/擷圖/顯示桌面等，可自訂順序）、**拖曳檔案上島**、**螢幕錄製智慧請勿打擾**；按下滑鼠立即回應展開/收合（點擊優先）。
- **⚡ 效率工具與自動化**：番茄鐘、待辦、剪貼簿紀錄、快速啟動器、行程提醒；會議靜音助手、螢幕錄製/擷圖提示、檔案複製/下載進度上島；全域快速鍵與規則引擎（依條件自動顯示/隱藏）。
- **🛡 隱私安全**：無遙測、無廣告、不傳送資料。除了使用者手動開啟的線上歌詞/天氣外完全離線；所有設定與資料僅存放在本機 `%APPDATA%\WinIsland`。

---

## 📥 下載（最新穩定版 1.1.5）

| 平台 | 下載 | 說明 |
| --- | --- | --- |
| Windows x64 | [x64 可攜版](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-x64.exe) | 主流 64 位元電腦首選，單檔免安裝，直接執行 |
| Windows ARM64 | [ARM64 可攜版](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-arm64.exe) | Surface Pro X / 驍龍機型等 ARM 裝置，單檔免安裝 |
| Windows 通用 | [通用安裝套件](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-Setup-1.1.5.exe) | Inno Setup 安裝精靈，x64 / ARM64 自動依架構安裝 |

所有歷史版本與完整更新紀錄見 [GitHub Releases](https://github.com/DMP-Pig/WinIsland/releases)。

---

## 📊 效能指標

| 指標 | 數值 |
| --- | --- |
| 常駐記憶體（Private） | ~72 MB |
| 冷啟動 | < 1 s |
| 閒置 CPU | ≈ 0% |
| 動效幀率 | 60 fps 流暢 |
| 多執行個體 | 單一執行個體防止重複執行 |
| 遙測 | 0 遙測 · 不上報 · 無廣告 |

---

## 目錄

- [功能特性](#功能特性)
- [技術選型與理由](#技術選型與理由)
- [架構總覽](#架構總覽)
- [快速開始](#快速開始)
- [建置與封裝](#建置與封裝)
- [使用說明](#使用說明)
- [設定項目說明](#設定項目說明)
- [Cider 整合](#cider-整合)
- [歌詞說明](#歌詞說明)
- [隱私與安全](#隱私與安全)
- [非功能性指標](#非功能性指標)
- [已知限制](#已知限制)
- [驗證指南（需要搭配真實播放器測試）](#驗證指南需要搭配真實播放器測試)
- [常見問題](#常見問題)
- [開源授權](#開源授權)

---

## 功能特性

### P0（已實作）
- **靈動島懸浮 UI（仿 iOS）**：預設頂部置中（可設定為頂部右側）；圓角膠囊；跟隨系統明暗主題或手動主題色；緊湊 ↔ 完整卡片**形變動畫**（固定視窗 + 單一元素縮放/淡入，由 WPF 合成執行緒以 60fps 驅動，帶 iOS 彈簧回彈）；**點擊展開/收合**（懸停不展開），移出自動收合（700ms 防誤觸緩衝）；卡片外區域點擊穿透。
- **鎖定與拖曳**：預設上鎖不可移動；右鍵選單可**解鎖**（解鎖後可用滑鼠拖曳靈動島）、**置中對齊**（上下不變、左右置中）、再次**上鎖**。解鎖拖曳後重新上鎖會**保持拖曳位置**（不會回復預設）；拖曳放開可**邊緣吸附**（吸附螢幕邊緣/置中，設定 → 一般可開關）。
- **緊湊態排版**：歌名/歌手/歌詞靠左對齊（貼近封面）、垂直置中。
- **專輯封面顯示**：膠囊與展開卡片皆顯示封面（展開時為大封面 64px，無封面時顯示佔位圖示）；SMTC 縮圖與 Cider 封面自動快取。
- **媒體播放控制**：顯示歌名、歌手、專輯；可拖曳進度列 seek；播放/暫停、上一首、下一首；需要時提供音量調整（Cider 使用其 API，其他來源控制系統音量，可關閉）；媒體元件上顯示目前播放來源徽章（Spotify / Cider / 網易雲音樂 / QQ音樂等）。
- **迷你播放器**：獨立懸浮小視窗（設定 → 媒體可開關），顯示專輯封面 / 歌名 / 歌手 / 進度列與播放控制，可自由拖曳並記住位置，隨媒體播放自動顯示 / 隱藏。
- **音訊輸出裝置切換**：設定 → 媒體可列舉並切換系統預設播放裝置（切換後建議重新啟動播放器生效）。
- **多來源整合**：
  1. Windows 全域媒體工作階段（`Windows.Media.Control` / SMTC）——網易雲音樂、QQ音樂、Spotify、Apple Music 官方版、Groove、電影與電視等；
  2. **Cider** 本機 HTTP API（連接埠 10767，相容舊版 10769 RPC，自動掃描連接埠 + 手動設定，支援 `apptoken` 驗證）；
  3. 備援：視窗標題 + 處理程序辨識（僅顯示資訊，無控制能力）。
- **歌詞顯示（逐字卡拉OK模式）**：點擊展開後，歌詞區以卡拉OK方式顯示——**目前這一句的字逐個點亮**——高亮進度為連續值，邊界字元在 60fps 緩動下從基礎色平滑混到高亮色，依閱讀順序由左到右流動（換行歌詞也正確，不會多條線同時點亮）；每句從 0 開始（第一個字先不亮）；暫停時高亮凍結在暫停時刻：Cider 無明確狀態時依「位置是否移動」判斷播放/暫停（不再因 remainingTime>0 誤判為播放），SMTC 優先跟隨 Cider 工作階段（避免被 Bilibili 等其他活躍工作階段搶走）；結束後重新啟動自動恢復上次暫停位置（不會跳回開頭）；目前這一句僅文字高亮（無背景膠囊，避免雙重高亮；20px 大字），其餘句淡化，**平滑捲動自動置中**（60fps 逐幀逼近目前這一句，展開即自動跟隨）；緊湊態靠左即時顯示目前這一句並同樣逐字點亮；可選獨立懸浮歌詞小視窗。
  - **進度同步**：自動讀取 Cider 的本機 API Token（零設定）取得真實播放進度，卡拉OK逐字與歌曲精確同步；無進度可用的播放器以本機時鐘推進。
  - **歌詞來源**：本機 `.lrc`（`%APPDATA%\WinIsland\Lyrics` 或音樂目錄）→ AMLL 逐字歌詞 → Cider 歌詞介面 → 線上歌詞（右鍵靈動島可一鍵開關）。無歌詞時顯示「暫無歌詞」，不會報錯。
  - **雙語歌詞**：自動合併相鄰時間戳的翻譯行，可在設定中關閉（無需額外歌詞檔案）；歌詞翻譯顯示 / 隱藏開關，「複製目前這一行」一鍵複製目前歌詞。
- **系統匣**：常駐圖示，右鍵選單（顯示/隱藏、獨立歌詞視窗、開機自動啟動、設定、結束），雙擊切換顯示。

### P1（已實作）
- **元件系統（自訂靈動島內容）**：設定 → 元件，可分別勾選「無歌曲播放時 / 有歌曲播放時」顯示哪些元件，並拖曳調整擺放順序：
  - 時間、天氣（Open-Meteo，需填寫城市並連網）、日期（可附加農曆與節氣）、CPU 占用、GPU 占用、記憶體占用、網路速度（可顯示最近 32 秒迷你曲線）、電量、磁碟剩餘空間、輸入法狀態（中 / 英 + 輸入法名稱）、快速切換（WiFi / 藍牙 / 夜間模式 / 靜音 一鍵切換）、音量、鍵盤指示燈（CapsLock）、剪貼簿、待辦、番茄鐘、行程、節假日倒數、會議中、麥克風、攝影機；
  - 歌曲資訊（封面/歌名/歌手/歌詞/進度列，僅播放時顯示，順序列中始終保留）。
  - 順序列只顯示已勾選的元件；清單與順序列支援滑鼠滾輪和捲動軸；每個元件可單獨自訂圖示（MDL2 圖示或 Emoji，設定 → 元件）。
  - 臨時上島元件：音量變化、擷圖 / 螢幕錄製、檔案複製 / 移動、下載進行中（後兩者預設關閉）——事件發生時即使靈動島隱藏也會臨時顯示對應元件。
  - **「使用中」合併膠囊**（設定 → 元件，預設關閉）：開啟後，勾選的「麥克風 / 攝影機 / 會議中 / 螢幕錄製」合併為單一「使用中 · …」狀態膠囊，參與合併的項目不再單獨顯示。
  - **單行模式**（設定 → 外觀，預設開啟）：緊湊態所有元件一行顯示，未展開時同樣顯示歌曲資訊與目前歌詞（逐字卡拉OK高亮），歌詞過長自動截斷；進度列與完整歌詞清單在展開卡片中顯示。
- **展開卡片內容自訂**：封面+標題、進度列、控制按鈕與音量、歌詞區可分別開關。
- **外觀個人化（macOS System Settings 風格設定頁）**：左側導覽 + 右側內容、圓角液態玻璃；**18 種主題面板預設**（預設 / 海洋 / 森林 / 日落 / 霓虹 / 單色 / 葡萄紫 / 天空 / 玫瑰 / 琥珀 / 青檸 / 青碧 / 薰衣草 / 緋紅 / 午夜 / 咖啡 / 櫻花 / 極光 / 自訂）、自訂強調色與背景色（#RRGGBB）、自訂字型、字型大小縮放（0.8–1.4）、圓角半徑（16–40）、**4 種動效面板**（iOS 彈簧 / 柔和彈簧 / 彈性回彈 / 簡潔漸隱）；展開背景可隨專輯封面取色；**桌布取色主題**（從目前桌布擷取主色作為主題色，純本機、不連網）；未讀通知角標。**變更立即生效，沒有「確定 / 完成」按鈕**。
- **音訊波紋**：播放媒體時，控制按鈕左側顯示**隨音樂節奏即時抖動**的波紋（擷取系統輸出聲音，非音量條），**4 種樣式**（柱狀 / 頻譜 / 環形 / 粒子），靈敏度、高度可調；設定 → 外觀可開關，預設開啟。
- **音量 / 靜音臨時上島**：系統音量變化或靜音時，靈動島短暫顯示音量指示（顯示秒數可調，設定 → 通知可關閉）。
- **檔案複製 / 移動上島**：偵測到檔案總管「正在複製 / 移動檔案」時靈動島顯示提示（純本機視窗標題辨識，可關閉）。
- **下載進度上島**：偵測下載目錄中的瀏覽器暫存檔（.crdownload / .part / .download 等），顯示「正在下載 N 個檔案」（預設關閉，設定 → 效率工具可開啟）。
- **番茄鐘增強**：點擊靈動島上的番茄鐘元件可暫停 / 繼續計時。
- **效率工具（設定 → 效率工具）**：剪貼簿紀錄（可選開啟，最多保留 N 筆）、番茄鐘計時（工作/休息時長可調）、待辦清單、行程提醒；對應元件可放入靈動島展示。
- **通知系統（右上角玻璃橫幅，帶 macOS 風格滑入/滑出動畫）**：
  - 藍牙裝置連線/中斷提示；
  - 接管 Windows 通知（盡力而為，UI 自動化鏡像通知中心）；
  - 正在播放通知（切歌時彈出）；
  - 低電量提醒（閾值可調，每個充電週期提醒一次）、充電完成提醒（目標電量閾值可調，預設 100%）、斷網 / 網路恢復提示；
  - 通知紀錄：最近 50 筆，可在設定中檢視/清空。
  - 通知紀錄增強：未讀紅點標記、全部已讀、單筆刪除、點擊項目開啟來源應用程式、清空紀錄；
  - 通知摺疊：同來源同標題的重複通知重用同一橫幅並累加數量；
  - 請勿打擾白名單：白名單內的來源（逗號分隔 exe 名稱）不受請勿打擾影響，仍正常彈出橫幅。
- **全域快速鍵**（皆可關閉 / 自訂）：`Ctrl+Alt+P` 播放/暫停 · `Ctrl+Alt+←/→` 上一首/下一首 · `Ctrl+Alt+I` 顯示/隱藏 · `Ctrl+Alt+Space` 展開/收合 · `Ctrl+Space` 快速啟動器 · `Ctrl+Alt+V` 剪貼簿紀錄面板。
- **減少動態效果**（無障礙 / 省電）：一鍵關閉彈簧動畫，立即切換。
- **靈動島尺寸調整**：設定 → 外觀，可調整緊湊長度/寬度、展開長度。
- **靈動島常駐**：即使無媒體播放也始終顯示（顯示設定的元件）。
- **多螢幕**：主螢幕 / 所有螢幕 / 指定螢幕編號。
- **高 DPI**：PerMonitorV2，120/150/200% 縮放下不錯位。
- **自訂設定**：位置、偏移、不透明度、主題色、緊湊模式內容、無媒體時隱藏等，變更立即生效。
- **無媒體播放時自動隱藏靈動島**（可關閉）。
- **請勿打擾模式**：手動一鍵開啟或依時段自動靜音通知（系統匣選單一鍵切換，設定中可設定時段）。
- **檢查更新**：系統匣選單 / 設定中手動檢查 GitHub 新版本；可選自動檢查（預設關閉，需連網）。
- **雙擊靈動島快速動作**（設定 → 一般）：可設為「播放 / 暫停」（預設）、「展開 / 收合」、「顯示桌面」、「隱藏 / 顯示靈動島」、「上一首」、「下一首」、「開啟設定」或「無動作」。
- **開會靜音助手（會議偵測）**：辨識 Teams / Zoom / 騰訊會議 / 釘釘 / 飛書 / Webex / Slack / Discord / Google Meet 等會議視窗，會議中自動開啟請勿打擾並顯示「會議中」元件（純本機啟發式，不連網）。
- **螢幕錄製 / 擷圖提示**：按 `PrintScreen` / `Alt+PrintScreen` 擷圖時彈出提示；偵測到 OBS、Bandicam、Fraps、Camtasia、XSplit、Streamlabs、Xbox Game Bar 等錄製軟體時彈出「螢幕錄製中」（純本機處理程序偵測，不連網）。
- **智慧請勿打擾（螢幕錄製）**：偵測到螢幕錄製進行中時自動靜音通知（不彈橫幅），結束錄製自動恢復；設定 → 通知可開關。
- **全螢幕自動隱藏**：偵測到全螢幕影片 / 遊戲 / 簡報（PowerPoint 放映等）時靈動島自動隱藏/收合，離開全螢幕恢復；設定 → 一般可開關。
- **拖曳檔案上島**：把檔案 / 資料夾拖到靈動島上可執行「複製路徑 / 開啟所在資料夾 / 釘選到上島」等操作（右鍵靈動島或拖放選單選擇）。
- **行事曆事件提醒（.ics）**：解析本機 iCalendar 檔案（Outlook / Google 行事曆 / 手機匯出），事件到點（可提前 N 分鐘）彈橫幅；純本機解析，不連網。
- **RSS 訂閱提醒**：輪詢 RSS 2.0 / Atom 訂閱（間隔可調），出現新項目彈橫幅；僅在填寫的訂閱位址連網。
- **郵件提醒（POP3）**：定期收取郵件標頭，新郵件彈橫幅（唯讀郵件標頭、不下載內文、不上傳資料；建議使用授權碼）。
- **快速啟動器（Spotlight 風格）**：`Ctrl+Space` 呼叫，搜尋已安裝應用程式 / 開始選單程式，或直接輸入網址開啟；快速鍵可自訂。
- **剪貼簿紀錄面板**：`Ctrl+Alt+V` 呼叫獨立剪貼簿紀錄視窗，點擊項目複製回剪貼簿、可清空；快速鍵可自訂。
- **規則（自動化）**：設定 → 規則，組合條件（始終 / 未播放媒體 / 播放中 / 時間段 / 指定媒體程式）與動作（隱藏 / 強制收合 / 強制顯示）自動控制靈動島；隱藏優先、摺疊其次、強制顯示最後。
- **低功耗模式**：閒置時降低波紋幀率、簡化動畫，更省電（設定 → 一般）。

### P2（已實作）
- 繁體中文 + English 介面切換。
- 匯出 / 匯入 JSON 設定檔。
- Windows 通知整合（藍牙 / 系統通知接管 / 正在播放 / 低電量）。
- 待定：來電提醒（未實作）；行程提醒已實作（元件 + 效率工具）。

---

## 技術選型與理由

| 方案 | 結論 | 理由 |
| --- | --- | --- |
| **C# + WPF (.NET 8)** | ✅ 採用 | 資源占用低、啟動快（相較於 Electron/Tauri 的 WebView）、系統整合能力最強（SMTC/CoreAudio/系統匣原生支援）、單檔封裝簡單 |
| C++ + Qt | ❌ | 開發效率低，授權條款複雜（LGPL），與 Windows 媒體堆疊整合需要大量手寫程式碼 |
| Tauri / Electron | ❌ | 記憶體占用高（常駐 >150MB 難以達成），啟動慢，違背「資源占用低、啟動快」的要求 |
| WinUI 3 | ❌ | 與 WPF 相比封裝/部署更複雜（需 Windows App SDK），且對未封裝桌面應用程式的 SMTC 支援與 WPF 相同 |

**重點**：
- `Windows.Media.Control.GlobalSystemMediaTransportControlsSessionManager` 可透過 .NET 8 的 Windows SDK 投影（CsWinRT）直接使用，無需 UWP 封裝身分。
- 除系統內建的 WPF/WinForms/Windows SDK 投影外，**執行時零第三方相依**（見 [THIRD_PARTY.md](THIRD_PARTY.md)）。
- 亞克力效果：Win10/Win11 皆透過 `SetWindowCompositionAttribute`（`ACCENT_ENABLE_ACRYLICBLURBEHIND`）實作，並用 `SetWindowRgn` 裁切圓角，使模糊跟隨膠囊形狀。

---

## 架構總覽

```
src/WinIsland/
├── App.xaml(.cs)              # 組合根：單一執行個體、例外捕捉、系統匣、視窗生命週期
├── Services/
│   ├── MediaModels.cs         # 統一媒體快照模型（TrackInfo / MediaSnapshot）
│   ├── SmtcMediaProvider.cs   # Windows 全域媒體工作階段（事件驅動 + 節流推播）
│   ├── CiderClient.cs         # Cider 本機 API 封裝（V3 + LegacyV2、連接埠掃描、容錯解析）
│   ├── CiderMediaProvider.cs  # Cider 工作階段層（連線生命週期）
│   ├── WindowTitleMediaProvider.cs # 備援：視窗標題辨識
│   ├── MediaCoordinator.cs    # 中央調度：Cider > SMTC > 視窗標題，封面快取、音量附加
│   ├── LrcParser.cs           # LRC 解析（多時間戳、offset、時長格式）
│   ├── LyricsService.cs       # 歌詞解析（本機 .lrc → Cider → 線上）
│   ├── OnlineLyricsService.cs # 線上歌詞（網易雲音樂/QQ音樂非官方介面，預設開啟可一鍵切換）
│   ├── ArtworkCache.cs        # 封面下載/快取（Cider 遠端封面 → 本機檔案）
│   ├── SystemVolume.cs        # CoreAudio 系統音量（COM P/Invoke）
│   ├── AppSettings.cs         # JSON 設定讀寫（%APPDATA%\WinIsland\settings.json）
│   ├── SingleInstance.cs      # 具名 Mutex + 具名管道（第二次啟動顯示靈動島）
│   ├── AutoStart.cs           # HKCU Run 鍵自動啟動
│   ├── GlobalHotkeyService.cs # 全域快速鍵（Win32 RegisterHotKey）
│   ├── NotificationService.cs # 右上角玻璃通知橫幅
│   ├── NotificationHistoryService.cs # 通知紀錄（最近 50 筆，JSON 持續化）
│   ├── BluetoothMonitor.cs    # 藍牙裝置連線/中斷監控
│   ├── SystemNotificationMonitor.cs # 接管 Windows 通知（UI 自動化鏡像）
│   ├── MediaAppRegistry.cs    # 媒體程式登錄（啟用/停用/排序）
│   ├── AudioWaveService.cs    # 聲音波紋（系統音量取樣，驅動波紋抖動）
│   ├── KeyboardIndicatorMonitor.cs # 鍵盤指示燈（CapsLock 狀態監聽）
│   ├── ClipboardHistoryService.cs # 剪貼簿紀錄
│   ├── TodoService.cs         # 待辦清單
│   ├── PomodoroService.cs     # 番茄鐘計時
│   ├── ScheduleService.cs     # 行程提醒
│   ├── IcsCalendar.cs       # .ics 行事曆解析（事件 / VALARM）
│   ├── MeetingMonitor.cs    # 會議視窗偵測（開會靜音助手）
│   ├── PrivacyDeviceMonitor.cs # 麥克風/攝影機使用狀態（隱私登錄輪詢）
│   ├── RssMailService.cs    # RSS 訂閱 + 郵件（POP3）提醒
│   ├── ScreenCaptureMonitor.cs # 擷圖 / 螢幕錄製偵測提示
│   ├── IslandApiServer.cs   # 上島 API（v1 + v3 HTTP / WebSocket）
│   ├── IslandPushModels.cs  # 上島卡片模型（圖片/動態進度/心跳）
│   ├── DoNotDisturb.cs        # 請勿打擾模式（手動/時段）
│   ├── UpdaterService.cs      # GitHub 更新檢查
│   ├── ProfileService.cs      # 設定檔（多組設定切換）
│   ├── WeatherService.cs      # 天氣元件（Open-Meteo，需連網）
│   ├── PlaybackStateStore.cs  # 播放位置持續化（退出/暫停後還原）
│   ├── CiderTokenAutoDetect.cs # Cider API Token 自動偵測
│   └── AppLogger.cs           # 輕量檔案日誌
├── UI/
│   ├── IslandWindow.xaml(.cs) # 靈動島視窗（動畫、亞克力、定位、懸停互動）
│   ├── IslandViewModel.cs     # 主要檢視模型（進度插值、歌詞索引、可見性）
│   ├── SettingsWindow.xaml(.cs) / SettingsViewModel.cs
│   ├── LyricsWindow.xaml(.cs) # 獨立歌詞小視窗
│   ├── ThemeService.cs        # 明暗主題 + 主題色筆刷
│   ├── WindowEffects.cs       # 亞克力 / 深色模式 / 圓角區域
│   ├── ScreenHelper.cs        # 多螢幕 + PerMonitorV2 DPI 換算
│   ├── TrayIcon.cs            # 系統匣圖示與選單
│   ├── ClipboardPanelWindow.xaml(.cs) # 剪貼簿紀錄面板
│   ├── QuickLauncherWindow.xaml(.cs)  # 快速啟動器（Ctrl+Space）
│   └── Localization.cs        # 繁/英文案表
└── Diagnostics/DiagnosticsCommand.cs  # --diagnose 診斷資訊
tests/WinIsland.Tests/         # xunit 單元測試（LRC/設定/Cider 解析/視窗標題解析）
build/
├── publish.ps1                # 一鍵發佈（自包含或框架相依 + zip）
├── WinIsland.iss              # Inno Setup 安裝指令碼
└── make-icon.ps1 / IconGen.cs # 圖示產生工具
```

**資料流程**：`MediaCoordinator` 每秒輪詢各 Provider（非同步、不阻塞 UI）→ 產生統一 `MediaSnapshot`（含本機封面路徑、音量）→ 經 Dispatcher 發佈到 `IslandViewModel` → 200ms 插值器平滑推進進度列與歌詞高亮 → WPF 繫結轉譯。

---

## 快速開始

> 💡 **預先編譯版**：`releases/` 目錄依版本提供單檔自包含可執行檔（如 `releases/1.1.5/win-x64/WinIsland-1.1.5-win-x64.exe`，包含 .NET 8 執行階段，雙擊即可執行）。Beta 版本僅在本機保留；穩定版本才會發佈到 GitHub（含 win-x64 / win-arm64 可攜版及通用安裝套件）。

### 環境需求
- Windows 10 1809+ / Windows 11
- 建置機器：.NET 8 SDK（或更高 SDK 並指定 `net8.0-windows10.0.19041.0`）

### 建置
```powershell
# 還原 + 建置 + 測試
dotnet build WinIsland.slnx -c Release
dotnet test  WinIsland.slnx -c Release

# 執行（Debug）
dotnet run --project src\WinIsland -c Debug
```

### 一鍵發佈
```powershell
# 自包含（包含 .NET 8 執行階段，免安裝，約 73MB（單檔））
.\build\publish.ps1

# 框架相依（體積小，需安裝 .NET 8 Desktop Runtime）
.\build\publish.ps1 -FrameworkDependent
```
產物位於 `publish\win-x64\`（含 `WinIsland.exe`），zip 為 `publish\WinIsland-win-x64.zip`。

### 安裝套件（選用）
安裝 [Inno Setup 6](https://jrsoftware.org/isinfo.php) 後：
```powershell
iscc.exe build\release-1.1.5.iss
```
產生 `releases\<version>\WinIsland-Setup-<version>.exe`（通用安裝套件，同時支援 x64 與 ARM64，自動依架構安裝）。發佈穩定版時在 `build\` 下依版本複製一份 `release-<version>.iss` 並更新版本號。

---

## 使用說明

1. 啟動 `WinIsland.exe`（或設為開機自動啟動 / 安裝套件勾選自動啟動）。系統匣出現圖示。
2. 播放任意音樂：
   - 網易雲音樂、QQ音樂、Spotify、Apple Music 官方版等 → 自動透過系統媒體工作階段顯示；
   - Cider → 詳見 [Cider 整合](#cider-整合)；
   - 其他播放器 → 備援視窗標題辨識（僅顯示）。
3. **點擊**靈動島展開完整卡片（懸停不展開）：進度拖曳 seek、播放控制、音量、同步歌詞；再按一下收合（移出卡片後 700ms 自動收合）。
4. 系統匣選單：顯示/隱藏、獨立歌詞視窗、開機自動啟動、**請勿打擾模式**（勾選即靜音通知）、**檢查更新**、**檢視日誌**、設定、結束。**關閉主視窗不會結束處理程序**（僅縮到系統匣）。
5. 全域快速鍵：`Ctrl+Alt+P` 播放/暫停 · `Ctrl+Alt+←/→` 上一首/下一首 · `Ctrl+Alt+I` 顯示/隱藏 · `Ctrl+Alt+Space` 展開/收合 · `Ctrl+Space` 快速啟動器（搜尋應用程式 / 直接輸入網址按 Enter）· `Ctrl+Alt+V` 剪貼簿紀錄面板（皆可關閉 / 自訂）。
6. 通知與提示（藍牙 / Windows 通知 / 正在播放 / 低電量）預設在螢幕右上角彈出玻璃橫幅，可在設定 → 通知中切換；**請勿打擾模式**開啟時不彈橫幅（角標仍會計數）。
7. 常用命令列參數：
   ```powershell
   WinIsland.exe --demo       # 示範模式（無媒體時預覽介面 + 範例歌詞）
   WinIsland.exe --diagnose   # 輸出診斷報告到 %APPDATA%\WinIsland\diagnostics.txt
   WinIsland.exe --settings   # 啟動時開啟設定
   ```

---

## 設定項目說明

設定檔：`%APPDATA%\WinIsland\settings.json`（JSON；設定介面變更立即生效，可匯出/匯入）。
| 鍵 | 預設 | 說明 |
| --- | --- | --- |
| `Language` | `zh-CN` | `zh-CN` / `en-US` |
| `ThemePreset` | `Default` | 主題皮膚：`Default/Ocean/Forest/Sunset/Neon/Mono/Grape/Sky/Rose/Amber/Lime/Teal/Lavender/Crimson/Midnight/Coffee/Sakura/Aurora/Custom`（覆蓋 AccentColor） |
| `FontFamily` | `Segoe UI` | 介面字型 |
| `FontScale` | `1.0` | 字型縮放 0.8–1.4 |
| `CornerRadius` | `28` | 膠囊圓角 16–40 |
| `BadgeEnabled` | `true` | 未讀通知角標（右上角紅點 + 數字） |
| `CoverTintBackground` | `true` | 展開背景隨專輯封面取色 |
| `WaveVisualizerEnabled` | `true` | 播放媒體時控制按鈕左側音訊波紋 |
| `WaveStyle` | `Bars` | 波紋樣式：`Bars`（柱狀）/ `Spectrum`（頻譜）/ `Ring`（環形）/ `Particles`（粒子） |
| `WaveSyncEnabled` | `true` | 波紋跟隨音樂節奏（採集系統輸出聲音驅動） |
| `WaveSensitivity` | `1.0` | 波紋靈敏度 0.2–3.0 |
| `WaveHeight` | `1.0` | 波紋高度 0.4–1.6 |
| `WallpaperThemeColorEnabled` | `false` | 桌布取色：從目前桌布擷取主色作為主題色（純本機） |
| `MarqueeTextEnabled` | `false` | 跑馬燈：歌名/歌詞過寬時自動橫向捲動 |
| `EdgeSnapEnabled` | `true` | 解鎖拖動鬆手自動吸附螢幕邊緣/置中 |
| `FullScreenAutoHideEnabled` | `true` | 全螢幕（影片/遊戲/簡報）時自動隱藏動態島 |
| `RecordingDndEnabled` | `false` | 錄製螢幕時自動勿擾（不彈通知橫幅） |
| `Theme` | `Auto` | `Auto` / `Light` / `Dark` |
| `AccentColor` | `#6C5CE7` | 主題色（#RRGGBB） |
| `Position` | `Center` | `Center` 頂部置中 / `Right` 頂部右側 |
| `Monitor` | `Primary` | `Primary` 主螢幕 / `All` 所有螢幕 / `Index` 指定螢幕 |
| `MonitorIndex` | `0` | `Monitor=Index` 時的螢幕編號 |
| `OffsetX` / `OffsetY` | `0` / `16` | 像素偏移 |
| `Opacity` | `0.92` | 0.3–1.0 |
| `HideWhenNoMedia` | `true` | 無媒體播放時隱藏動態島 |
| `ShowWhenPaused` | `true` | 暫停時仍顯示 |
| `StartWithWindows` | `false` | 開機自動啟動 |
| `StartHidden` | `false` | 啟動時隱藏 |
| `CompactShowArt/Title/Progress` | `true/true/false` | 緊湊模式內容 |
| `CiderEnabled` | `true` | 啟用 Cider 本機 API |
| `CiderPort` | `0` | `0` 自動偵測（預設 10767）；手動填寫連接埠 |
| `CiderToken` | `""` | Cider API Token（可留空） |
| `OnlineLyricsEnabled` | `true` | 線上歌詞（預設開啟，右鍵動態島可一鍵開關；見版權提示） |
| `LyricsFolder` | `""` | 額外 .lrc 目錄；留空自動搜尋 `%APPDATA%\WinIsland\Lyrics`、`音樂\Lyrics`、`音樂` 頂層 |
| `StandaloneLyricsWindow` | `false` | 獨立歌詞小視窗 |
| `KaraokeHighlight` | `true` | 逐字卡拉OK高亮（目前句依字元點亮） |
| `UseSystemVolume` | `true` | 非 Cider 來源時使用系統音量列 |
| `IsLocked` | `true` | 上鎖（不可拖動）；右鍵選單可解鎖/上鎖/置中對齊 |
| `IslandAlwaysVisible` | `false` | 動態島常駐（無媒體時也顯示元件） |
| `ShowMediaInfo` | `true` | 顯示媒體播放資訊（歌名/封面/歌詞等） |
| `ReduceMotion` | `false` | 減少動態效果（關閉彈簧動畫，無障礙/省電） |
| `GlobalHotkeysEnabled` | `true` | 全域快速鍵開關 |
| `LowBatteryThreshold` | `20` | 低電量提醒閾值（%），0 關閉 |
| `ExpandedShowArtTitle/Progress/Controls/Lyrics` | `true` | 展開卡片各區塊（封面+標題/進度列/控制與音量/歌詞）開關 |
| `Components` | 物件 | 元件勾選：`Time/Weather/Date/Cpu/Gpu/Ram/Net/Battery/Volume/CapsLock/Clipboard/Todo/Timer/Schedule/Holiday/Meeting/Mic/Cam` 各有 `WhenIdle`/`WhenPlaying` 兩欄；`Cover/Title/Artist/Lyrics/Progress` 播放時顯示；`ComponentBadges` 字典為各元件填角標文字 |
| `WidgetOrder` | `Time,Weather,...` | 元件擺放順序（逗號分隔鍵名，含 `Song`） |
| `MediaApps` | `[]` | 媒體程式啟用/停用與優先順序（空=全部啟用） |
| `CompactWidth` / `CompactHeight` | `360` / `72` | 緊湊長度 / 緊湊寬度（手動拖曳調整會自動關閉自動調整） |
| `CompactWidthAuto` / `CompactHeightAuto` | `true` | 緊湊尺寸隨元件內容自動調整（預設開啟） |
| `ExpandedWidthAuto` / `MaxExpandedHeightAuto` | `true` | 展開尺寸自動調整（預設開啟） |
| `ExpandedWidth` / `MaxExpandedHeight` | `400` / `384` | 展開長度 / 展開最大高度 |
| `BluetoothNotifyEnabled` | `false` | 藍牙連線/中斷提示 |
| `NotificationTakeoverEnabled` | `false` | 接管 Windows 通知（盡力而為） |
| `NotificationTimeoutSeconds` | `6` | 通知橫幅顯示時長（秒） |
| `NotificationPosition` | `TopRight` | 通知彈出位置（右上角） |
| `DoNotDisturbEnabled` / `DoNotDisturbManual` | `false` | 勿擾：依時段自動 / 手動開關 |
| `DoNotDisturbStartHour` / `DoNotDisturbEndHour` | `22` / `8` | 勿擾時段（小時） |
| `DnDAllowlist` | `[]` | 勿擾白名單（`QQ.exe,WeChat.exe`，白名單內仍彈通知） |
| `Rules` | `[]` | 自動化規則清單（條件 + 動作） |
| `ClipboardHistoryEnabled` / `ClipboardHistoryMax` | `false` / `15` | 剪貼簿歷史開關與筆數上限 |
| `PomodoroEnabled` / `PomodoroWorkMinutes` / `PomodoroBreakMinutes` | `false` / `25` / `5` | 蕃茄鐘開關與工作/休息時長（分鐘） |
| `KeyIndicatorSeconds` | `3` | 鍵盤指示燈（CapsLock）出現時長（秒） |
| `VolumeTempIndicatorEnabled` / `VolumeTempIndicatorSeconds` | `true` / `4` | 音量 / 靜音臨時上島開關與顯示秒數 |
| `FileCopyNotifyEnabled` | `true` | 檔案複製 / 移動進行中上島（純本機視窗標題辨識） |
| `DownloadProgressEnabled` | `false` | 下載進行中上島（掃描下載目錄暫存檔，預設關） |
| `UsageMergeEnabled` / `UsageMergeItems` | `false` / `Mic,Cam,Meeting,Recording` | 「使用中」合併膠囊開關與參與合併的元件（預設關） |
| `AutoUpdateCheck` | `false` | 自動檢查 GitHub 新版本（預設關，需連網） |
| `DoubleClickAction` | `PlayPause` | 雙擊動態島動作：`PlayPause` / `ToggleExpand` / `ShowDesktop` / `ToggleVisible` / `NextTrack` / `PrevTrack` / `OpenSettings` / `None` |
| `AnimationStyle` | `Spring` | 動效皮膚：`Spring` / `Soft` / `Elastic` / `Fade` |
| `ThemeTint` | `""` | 自訂背景色 #RRGGBB（預設為 Custom 時生效） |
| `ExpandedCardStyle` | `Classic` | 展開卡片範本：`Classic` / `Hero` |
| `NetCurveEnabled` | `true` | 網路元件顯示最近 32 秒迷你曲線 |
| `LowPowerMode` | `false` | 低功耗模式（閒置降低波紋幀率、簡化動畫） |
| `MeetingAssistantEnabled` | `false` | 開會靜音助理：偵測會議視窗 + 自動勿擾 |
| `MeetingAutoDnd` / `MeetingKeywords` | `true` / `""` | 會議中自動勿擾 / 自訂會議關鍵字 |
| `ScreenCaptureNotifyEnabled` / `ScreenshotNotifyEnabled` / `RecordingNotifyEnabled` | `false` / `true` / `true` | 截圖/錄製螢幕提示總開關與分項 |
| `CalendarEnabled` / `CalendarIcsPath` / `CalendarAdvanceMinutes` | `false` / `""` / `10` | .ics 行事曆提醒開關 / 檔案路徑 / 提前提醒分鐘 |
| `RssNotifyEnabled` / `RssUrls` / `RssIntervalMinutes` | `false` / `""` / `15` | RSS 訂閱提醒 / 訂閱網址 / 輪詢間隔（分鐘） |
| `MailNotifyEnabled` / `MailPop3Server` / `MailPop3Port` / `MailUseSsl` / `MailUser` / `MailPassword` / `MailCheckMinutes` | `false` / `""` / `995` / `true` / `""` / `""` / `5` | 郵件提醒（POP3）開關、伺服器、連接埠、SSL、帳號、授權碼、檢查間隔 |
| `QuickLauncherEnabled` / `HotkeyLauncher` | `true` / `Ctrl+Space` | 快速啟動器開關與快速鍵 |
| `ClipboardPanelEnabled` / `HotkeyClipboardPanel` | `true` / `Ctrl+Alt+V` | 剪貼簿歷史面板開關與快速鍵 |
| `HotkeyExpand` | `Ctrl+Alt+Space` | 展開/收合快速鍵 |
| `NotifyFoldEnabled` | `true` | 摺疊同類通知（同來源同標題只顯示一條） |
| `ActiveProfile` | `Default` | 設定檔名稱（多組設定切換） |

---

## Cider 整合

Cider（Apple Music 第三方客戶端）提供本機 HTTP API。WinIsland 已封裝獨立模組（`CiderClient.cs`），自動適應版本差異。

**開啟步驟（重要）**：
1. 開啟 Cider：**設定 → 連線性 → 允許外部控制（Manage External Application Access）**，開啟後 Cider 會顯示 API Token（若為空白則點擊產生）。
2. 將 Token 複製到 **WinIsland 設定 → Cider → API Token** 並儲存。
3. 預設連接埠 `10767`，WinIsland 自動偵測；舊版 RPC 為 `10769`。

> ⚠️ Cider 2.x 新版預設**所有 API 請求都需要 Token**（無 Token 會回傳 `403 UNAUTHORIZED_APP_TOKEN`）。若診斷日誌提示需要 Token，請依上述步驟填入；否則 Cider 歌詞/控制不可用（曲目仍可透過 SMTC 顯示）。

> ⚠️ 若日誌反覆出現 HttpClient.Timeout（原 2s），多為本機安全軟體/代理攔截回環 HTTP 所致（Cider 實際回應約 30ms）。自 1.0.1 起資料讀取逾時放寬到 5s；仍逾時請檢查防毒軟體對 WinIsland 的聯網攔截。

**已實作的 API 能力**（依 Cider 社群文件 / `cider-api` crate 實測整理，2026 年版本）：
- `GET /api/v1/playback/active`、`GET /now-playing`（曲目/封面/進度/狀態）
- `POST /api/v1/playback/play|pause|playpause|next|previous|seek`
- `GET|POST /api/v1/playback/volume`
- `GET /api/v1/lyrics`（含 `?id=` 回退）
- 鑑權標頭：`apptoken`（相容 `apitoken`）
- 舊版 10769：`/active`、`/currentPlayingSong`、`/playPause`、`/next`、`/previous`、`/seekto/{t}`、`/audio`

> ⚠️ Cider API 為非官方介面，版本變動快；所有請求 2 秒逾時、失敗自動降級到 SMTC / 視窗標題，**不影響主流程**。請保持 WinIsland 更新以適配新版本。

---

## 歌詞說明

優先順序：
1. **本機 .lrc**：依 `歌名.lrc` / `歌手 - 歌名.lrc` 在歌詞目錄（預設 `%APPDATA%\WinIsland\Lyrics`、`音樂\Lyrics`、`音樂` 頂層）尋找；
2. **AMLL 逐字歌詞**（amll.dev 曲庫 TTML 逐字時間軸，預設開啟）；
3. **Cider 歌詞介面**（來源為 Cider 時）；
4. **線上歌詞**（網易雲 / QQ音樂非官方介面）：**預設開啟**，右鍵動態島可一鍵開關，也可在設定中關閉。

> ⚠️ 線上歌詞使用非官方介面，僅限個人學習使用，請尊重版權；如版權方要求可隨時關閉該功能（關閉後完全無聯網）。

---

## 通知與提示（1.0.2 起，1.0.3 完善）

所有通知均為**右上角玻璃橫幅**，帶 macOS 風格滑入（從右側滑出 + 淡入）與滑出動畫，顯示時長可設定（3~15 秒）。

- **藍牙連線提示**：設定 → 通知，開啟後藍牙裝置連線/中斷時彈出。
- **接管 Windows 通知**：設定 → 通知，開啟後透過 UI 自動化盡力鏡像通知中心內容（QQ 等應用程式的通知）到右上角橫幅。
  > ⚠️ Windows 未提供公開的「攔截其它應用程式通知」API，此功能為盡力而為（best effort），部分通知可能無法擷取；不影響主流程。
- **正在播放通知**：切換歌曲時自動彈出「正在播放 - 歌名」橫幅（1.0.3 起）。
- **低電量提醒**：電量低於閾值（預設 20%，0~50 可調）時彈出，每個充電週期提醒一次（1.0.3 起）。
- **通知歷史**：最近 50 條通知記錄，設定 → 通知 頁可檢視 / 清空（1.0.3 起）。
- **動態島尺寸調整**：設定 → 外觀，可調緊湊長度/寬度、展開長度。

---
## 上島 API（其他軟體推送到動態島）

WinIsland 內建本機 HTTP 服務，其他軟體可將資訊即時推送到動態島（類似 iOS 動態島第三方 App 整合）。**開發文件見 [docs/IslandAPI.md](docs/IslandAPI.md)**。

| 介面 | 說明 |
|---|---|
| `POST /v1/island/push` | 推送 / 更新一張動態島卡片（v3 起支援圖片 / 動態進度 / 心跳） |
| `PATCH /v3/island/push/{id}` | 部分更新：只覆蓋請求體裡出現的欄位（保留過期時間 / 佇列位置） |
| `DELETE /v1/island/push/{id}` | 移除一張卡片 |
| `GET /v1/island/active`（或 `/v3/island/active`） | 查詢目前活躍卡片 |
| `GET /v3/ws` | WebSocket 雙向通道：用戶端發 `push/update/remove/ping`，伺服器端廣播 `push_updated/push_removed` 事件 |
| `GET /v1/health` | 健康檢查 |

- 設定 → 上島 API：啟用開關、連接埠（預設 9840）、可選 Token、全域預設顯示時長
- 上島推送**不會改變動態島長寬**，卡片在緊湊態單行展示、不遮擋其它元件
- 按鈕支援「開啟連結 / 啟動程式」，推送方可依條目自訂顯示時長（覆蓋全域預設）
- v3 新增：`image`（data URI 或 http 圖片）、`progress_from/progress_to/progress_duration_seconds`（進度自動推進）、`heartbeat_seconds`（心跳續期，超過 2 倍間隔未續期自動移除）、`theme`（卡片 dark/light/auto 主題）、`action: "command"`（按鈕執行本機指令）；完整開發文件見 [docs/IslandAPI.md](docs/IslandAPI.md)

---
## 播放狀態復原

- 應用程式結束、暫停、切歌時會把「曲目 + 播放位置」儲存到 `%APPDATA%\WinIsland\state.json`（僅本機）。
- 下次啟動若仍是同一曲目且播放器暫未回傳真實進度，會先依上次位置復原，避免「先顯示第 0 行、再跳到暫停句」的跳動；超過 1 小時或換了曲目不復原。

---
## 隱私與安全

- **無遙測、無廣告、無上報**。除使用者手動開啟的「線上歌詞」功能外，應用程式不進行任何網路請求。
- **天氣元件**：僅當你開啟「顯示天氣」並填寫城市時，會請求 Open-Meteo（免費、無 Key、無帳號）取得目前天氣；未開啟則完全離線。
- 唯一聯網情境：Cider 封面下載（`mzstatic.com`，本機 API 回傳的公開封面 URL）、使用者開啟後的線上歌詞。
- 所有資料本機儲存於 `%APPDATA%\WinIsland\`。
- 日誌僅記錄本機執行資訊（`logs\app-*.log`）。

---

## 非功能性指標

在測試機（Windows 11 24H2, 2560×1440@100%）實測（Release 自包含）：

| 指標 | 實測 | 目標 |
| --- | --- | --- |
| 閒置 CPU（無媒體） | < 0.5%（Debug 實測 0.3%） | ≈ 0% |
| 常駐記憶體（Private） | ~72 MB | ≤ 150 MB |
| 啟動 | < 1 s（冷啟動） | ≤ 2 s |
| 關閉主視窗 | 不結束，僅托盤化 | ✅ |
| 多執行個體 | 僅單一執行個體，二次啟動顯示動態島 | ✅ |
| 例外 | 統一擷取並寫入日誌，不彈當機視窗 | ✅ |

> 說明：自包含部署的 WorkingSet（含 .NET 執行階段共用頁）約 160MB，但 **Private 記憶體約 72MB**；若使用框架依賴部署，WorkingSet 會更低。

---

## 已知限制

- **逐字卡拉OK依賴歌詞來源與進度**：有 AMLL TTML/LRC 逐字時間軸時依字元高亮；無逐字時間軸或播放器不提供真實進度時降級為整句高亮（本機時鐘推進）。
- **播放器偶發回退進度**（如 Cider/SMTC 瞬間上報 0 或過期位置）：已做位置守衛——瞬間回退會被忽略，保持目前進度推進，不會把歌詞/進度列打回開頭；持續回退超過約 4 秒才判定為真正的重播或播放器端 seek。
- **來電提醒**：未實作（P2 可選）。已實作：藍牙提示、Windows 通知接管（盡力而為）、正在播放通知、低電量提醒、行程提醒。
- **SMTC 涵蓋範圍**：依賴播放器是否註冊全域媒體工作階段；個別舊播放器不註冊時僅能透過視窗標題兜底（無控制按鈕）。
- **Cider 1.x（連接埠 9000 舊 API）**：未適配，僅支援 2.x 及以上。

---

## 驗證指南（需要搭配真實播放器測試）

以下情境需要真實環境搭配驗證（本倉庫開發環境已通過自動化驗證的部分會註明）：

| 情境 | 狀態 |
| --- | --- |
| SMTC 工作階段列舉（`--diagnose` 可見工作階段清單） | ✅ 已實測（偵測到 Bilibili 等真實工作階段） |
| 動態島自動顯示/隱藏、點擊展開收起、進度插值 | ✅ 已實測（demo + 真實暫停工作階段） |
| 播放/暫停/切歌/seek（真實播放器） | ⚠️ 需搭配測試（程式碼路徑與 SMTC 控制 API 直接對應） |
| Cider API 連線與控制 | ⚠️ 需本機安裝 Cider 並開啟外部控制後驗證 |
| 本機 .lrc 歌詞同步捲動 | ✅ LRC 解析已單元測試；端到端需真實歌曲驗證 |
| 線上歌詞 | ✅ 已接入網易雲/QQ音樂；端到端效果需搭配真實歌曲驗證 |

**驗證步驟建議**：
1. `WinIsland.exe --diagnose` → 確認 `System media sessions` 列出了播放器；
2. 播放網易雲/QQ音樂/Spotify 任意一首 → 動態島應顯示曲目並可控制；
3. 開啟 Cider 並開啟外部控制 → 動態島來源應顯示 `Cider`，可 seek/調整音量；
4. 在歌曲目錄放置同名 `.lrc` → 展開後歌詞應隨進度捲動高亮。

---

## 常見問題

**Q: 動態島沒有出現？**
- 確認正在播放（暫停時預設仍顯示）；`HideWhenNoMedia` 預設開啟，無媒體時隱藏屬正常。
- 執行 `--diagnose` 檢視工作階段清單；若清單為空，表示播放器未註冊 SMTC。

**Q: Cider 顯示「未連線」？**
- 確認 Cider 設定中開啟「允許外部控制」；檢查連接埠（預設 10767）；WinIsland 設定裡確認 Cider 已啟用。

**Q: 線上歌詞打不開？**
- 線上歌詞預設開啟（右鍵動態島 → 線上歌詞 可一鍵開關）；若仍無歌詞，請在設定 → 歌詞中確認已開啟，並自測網路可達性。

**Q: 結束後托盤圖示仍在？**
- 托盤選單 → 結束；直接關閉動態島視窗僅隱藏（符合「托盤常駐」設計）。

---

## 開源授權

- 應用程式本體：MIT（見 [LICENSE](LICENSE)）
- 第三方元件：見 [THIRD_PARTY.md](THIRD_PARTY.md)

---

## English

## WinIsland — Windows Dynamic Island

> Bring the iOS Dynamic Island to Windows as a desktop floating window: media controls, synced lyrics, customizable widgets, a notification center, and an always-on system tray presence.
> Built on **.NET 8 + WPF**, for Windows 11 (compatible with Windows 10, 1809+).

---


> **Bring the iOS Dynamic Island to Windows — a modern, feature-packed Dynamic Island component.**

Bring the iOS Dynamic Island to Windows 11 / 10 — media playback controls, karaoke word-by-word lyrics, customizable widgets, a notification center, and an Island Push API, all in one capsule. Built on **.NET 8 + WPF**, free and open source (MIT), **no ads · no telemetry**.

🌐 **Website: https://WinIsland.JudeKwong.com**

---

## ✨ Highlights

- **▶ Media playback controls**: Native integration with Windows global media sessions (SMTC), compatible with NetEase Cloud Music, QQ Music, Spotify, Apple Music, Groove, Films & TV, and more; dedicated support for Cider's local API as well; window-title fallback when integration is unavailable. Album art, draggable seek, play/pause/track switching — all included; with multiple players open, switch the control source with one click; click the cover for a fullscreen immersive preview.
- **♪ Karaoke word-by-word lyrics**: The expanded card scrolls and highlights in sync, lighting up word by word in karaoke style; a three-tier lyric source chain — local `.lrc` → player lyrics API → optional online lyrics; bilingual lyrics, translation toggle, one-click copy of the current line; per-song lyric timing fine-tuning, and a standalone lyrics window with adjustable opacity and locking.
- **▦ Customizable widget system**: 30+ widgets including time, weather, date (with lunar calendar/solar terms), CPU/GPU/memory/disk, network speed, battery, input method, quick toggles (WiFi/Bluetooth/night mode/mute), and more; custom icons per widget, checkbox selection and drag-to-reorder, one-row/multi-row modes switchable anytime.
- **⇪ Island Push API**: A local HTTP / WebSocket interface that lets any third-party software push information to the Dynamic Island in real time (like iOS third-party app Dynamic Island integrations). v3 supports images, dynamic progress, heartbeat renewal, and light/dark card themes; pushed content does not change the Island's size and doesn't overlap other widgets; buttons can open links / launch programs / run local commands, and notify button clicks can call back to the pusher over WebSocket.
- **🔔 Notification center**: Glass banner in the top-right corner with macOS-style slide-in/slide-out animations: Bluetooth devices, WeChat/QQ voice and video call alerts, Windows notification takeover, now playing, low battery/charging complete, network loss/recovery; notification history, collapsing, Do Not Disturb allowlist, rule automation; banners can carry action buttons (e.g., Bluetooth “Disconnect” / “Settings”).
- **✦ Appearance & motion**: 18 theme skins, custom accent colors and backgrounds, liquid-glass frosted glass, **wallpaper color extraction** (automatically derives the theme color from the current wallpaper), **marquee** (long song titles/lyrics auto-scroll horizontally), 4 motion skins (iOS spring, etc.), **4 audio wave styles** (bars / spectrum / ring / particles, pulsing to the music); non-linear easing for expand/collapse, silky 60fps; cover-tinted backgrounds can slowly “breathe” (dynamic theme); PerMonitorV2 high-DPI, no misalignment at 120/150/200% scaling.
- **🖱 Interaction & intelligence**: Dragging once unlocked + **edge snapping** (snap to top/edge/center on release), **auto-hide in fullscreen** (auto-collapse during fullscreen video/game/presentation), customizable double-click action, **quick action buttons** (one-click lock screen/mute/play-pause/screenshot/show desktop from the expanded card, reorderable), **drag files onto the Island**, **smart Do Not Disturb while recording**; expand/collapse responds immediately on mouse-down (click precedence).
- **⚡ Productivity tools & automation**: Pomodoro timer, to-dos, clipboard history, quick launcher, schedule reminders; meeting mute assistant, screen recording/screenshot alerts, file copy/download progress on the Island; global hotkeys and a rules engine (auto show/hide based on conditions).
- **🛡 Privacy & security**: No telemetry, no ads, no data reporting. Fully offline except for online lyrics/weather that you enable manually; all config and data are stored only locally in `%APPDATA%\WinIsland`.

---

## 📥 Download (latest stable 1.1.5)

| Platform | Download | Notes |
| --- | --- | --- |
| Windows x64 | [x64 Portable](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-x64.exe) | Best for mainstream 64-bit PCs; single file, no install, run directly |
| Windows ARM64 | [ARM64 Portable](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-arm64.exe) | For ARM devices such as Surface Pro X / Snapdragon models; single file, no install |
| Windows Universal | [Universal Installer](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-Setup-1.1.5.exe) | Inno Setup wizard, automatically installs x64 / ARM64 by architecture |

All historical versions and full changelogs: [GitHub Releases](https://github.com/DMP-Pig/WinIsland/releases).

---

## 📊 Performance metrics

| Metric | Value |
| --- | --- |
| Resident memory (Private) | ~72 MB |
| Cold start | < 1 s |
| Idle CPU | ≈ 0% |
| Animation frame rate | Silky 60 fps |
| Multiple instances | Single instance, prevents duplicate runs |
| Telemetry | 0 telemetry · no reporting · no ads |

---

## Table of contents

- [Features](#features)
- [Tech stack and rationale](#tech-stack-and-rationale)
- [Architecture overview](#architecture-overview)
- [Quick start](#quick-start)
- [Build and package](#build-and-package)
- [Usage](#usage)
- [Configuration reference](#configuration-reference)
- [Cider integration](#cider-integration)
- [Lyrics](#lyrics)
- [Privacy and security](#privacy-and-security)
- [Non-functional metrics](#non-functional-metrics)
- [Known limitations](#known-limitations)
- [Verification guide (requires testing with real players)](#verification-guide-requires-testing-with-real-players)
- [FAQ](#faq)
- [Open source license](#open-source-license)

---

## Features

### P0 (Implemented)
- **Island floating UI (iOS-inspired)**: centered at the top by default (configurable to top-right); rounded capsule; follows the system light/dark theme or a manual theme color; compact ↔ full-card **morph animation** (fixed window + single-element scale/fade, driven by the WPF composition thread at 60fps with iOS spring bounce); **click to expand/collapse** (hover does not expand), auto-collapse when the pointer leaves (700ms anti-mistouch buffer); clicks outside the card pass through.
- **Lock & drag**: locked and unmovable by default; the right-click menu can **unlock** (drag the Island with the mouse after unlocking), **center-align** (keep vertical position, center horizontally), or **lock again**. After dragging while unlocked, re-locking **keeps the dragged position** (no revert to default); releasing after dragging can **snap to edges** (snap to screen edge/center; toggle in Settings → General).
- **Compact layout**: song title/artist/lyrics left-aligned (next to the cover), vertically centered.
- **Album art display**: both the capsule and the expanded card show the cover (large 64px cover when expanded; placeholder icon when there is no cover); SMTC thumbnails and Cider covers are cached automatically.
- **Media playback controls**: shows song title, artist, album; draggable progress bar for seek; play/pause, previous/next; volume adjustment when needed (Cider via its API, other sources control system volume, can be disabled); the media widget shows the current playback source badge (Spotify / Cider / NetEase Cloud Music / QQ Music, etc.).
- **Mini player**: a standalone floating window (toggle in Settings → Media) showing album art / song title / artist / progress bar and playback controls; freely draggable with remembered position; auto-shows/hides with media playback.
- **Audio output device switching**: Settings → Media can enumerate and switch the system default playback device (restart the player after switching for it to take effect).
- **Multi-source integration**:
  1. Windows global media session (`Windows.Media.Control` / SMTC) — NetEase Cloud Music, QQ Music, Spotify, Apple Music official builds, Groove, Films & TV, and more;
  2. **Cider** local HTTP API (port 10767, compatible with the legacy 10769 RPC, auto port scan + manual config, supports `apptoken` auth);
  3. Fallback: window title + process identification (display only, no control).
- **Lyrics display (word-by-word karaoke mode)**: after clicking to expand, the lyric area displays in karaoke style — **each character of the current line lights up in sequence** — the highlight progress is a continuous value, and boundary characters smoothly blend from the base color to the highlight color at 60fps easing, flowing left to right in reading order (wrapped lines also work correctly; multiple lines never light up at once); every line starts from 0 (the first character is not lit at first); when paused, the highlight freezes at the pause moment: Cider without explicit state uses “whether the position moves” to determine play/pause (no longer misjudging as playing because remainingTime>0), SMTC prefers following the Cider session (avoiding being stolen by other active sessions like Bilibili); after exit and restart, the last paused position is restored (no jump back to the beginning); the current line is highlighted in text only (no background capsule, avoiding double highlighting; 20px large) while other lines are dimmed, **smooth scroll auto-centering** (60fps frame-by-frame approach to the current line; follows automatically once expanded); the compact state shows the current line left-aligned in real time and lights it word by word too; an optional standalone floating lyrics window.
  - **Progress sync**: automatically reads Cider's local API Token (zero-config) to get real playback progress, precisely syncing word-by-word karaoke with the song; players without available progress advance with the local clock.
  - **Lyric sources**: local `.lrc` (`%APPDATA%\WinIsland\Lyrics` or the music directory) → AMLL word-by-word lyrics → Cider lyrics API → online lyrics (one-click toggle via right-click on the Island). When there are no lyrics, “No lyrics” is shown without errors.
  - **Bilingual lyrics**: translatable adjacent timestamp lines are auto-merged; can be disabled in Settings (no extra lyric files needed); lyrics translation show/hide toggle, and “Copy current line” copies the current lyric with one click.
- **System tray**: always-resident icon with a right-click menu (show/hide, standalone lyrics window, start with Windows, settings, exit); double-click toggles visibility.

### P1 (Implemented)
- **Widget system (customize Island content)**: Settings → Widgets; check which widgets show in “when no song is playing / when a song is playing”, and drag to reorder:
  - Time, weather (Open-Meteo; requires a city and internet), date (optional lunar calendar and solar terms), CPU usage, GPU usage, memory usage, network speed (optional mini 32-second curve), battery, free disk space, input method status (Chinese / English + input method name), quick toggles (WiFi / Bluetooth / night mode / mute one-click toggle), volume, keyboard indicators (CapsLock), clipboard, to-dos, Pomodoro, schedule, holiday countdown, in meeting, microphone, camera;
  - Song info (cover/title/artist/lyrics/progress bar; shown only while playing, always kept in the order strip).
  - The order strip only shows checked widgets; the list and order strip support mouse wheel and scrollbars; each widget can have a custom icon (MDL2 icons or Emoji, Settings → Widgets).
  - Temporary Island widgets: volume changes, screenshots / screen recording, file copy / move, downloads in progress (the last two off by default) — when the event happens, the corresponding widget is shown temporarily even if the Island is hidden.
  - **“In use” merged capsule** (Settings → Widgets, off by default): when enabled, checked “microphone / camera / in meeting / recording” merge into a single “In use · …” status capsule; merged items are no longer shown separately.
  - **Single-row mode** (Settings → Appearance, on by default): all widgets display in one row in compact state; song info and current lyric (word-by-word karaoke highlight) are also shown when not expanded, auto-truncated if too long; the progress bar and the full lyric list appear in the expanded card.
- **Expanded card content customization**: cover+title, progress bar, control buttons and volume, and the lyrics area can each be toggled.
- **Appearance personalization (macOS System Settings style settings page)**: left navigation + right content, rounded liquid glass; **18 theme skin presets** (Default / Ocean / Forest / Sunset / Neon / Mono / Grape / Sky / Rose / Amber / Lime / Teal / Lavender / Crimson / Midnight / Coffee / Sakura / Aurora / Custom), custom accent and background colors (#RRGGBB), custom font, font size scaling (0.8–1.4), corner radius (16–40), **4 motion skins** (iOS spring / soft spring / elastic bounce / clean fade); expanded background can follow the album cover color; **wallpaper color theme** (extracts the main color of the current wallpaper as the theme color — purely local, no internet); unread notification badge. **Changes take effect instantly; there are no “OK / Done” buttons.**
- **Audio waves**: while media plays, a wave that **pulses in real time to the music rhythm** is shown to the left of the control buttons (samples system output audio, not the volume bar); **4 styles** (bars / spectrum / ring / particles), sensitivity and height adjustable; toggle in Settings → Appearance, on by default.
- **Volume / mute temporary Island**: when the system volume changes or it is muted, the Island briefly shows a volume indicator (display duration adjustable; can be disabled in Settings → Notifications).
- **File copy / move on the Island**: when Explorer is “copying / moving files”, the Island shows a notification (purely local window-title detection; can be disabled).
- **Download progress on the Island**: detects browser temporary files in the download directory (.crdownload / .part / .download, etc.) and shows “Downloading N file(s)” (off by default; enable in Settings → Productivity Tools).
- **Pomodoro enhancements**: click the Pomodoro widget on the Island to pause / resume the timer.
- **Productivity tools (Settings → Productivity Tools)**: clipboard history (optional, keeps up to N entries), Pomodoro timer (work/break durations adjustable), to-do list, schedule reminders; corresponding widgets can be placed on the Island.
- **Notification system (glass banner in the top-right corner with macOS-style slide-in/slide-out animations)**:
  - Bluetooth device connect/disconnect alerts;
  - take over Windows notifications (best effort, mirrors the notification center via UI automation);
  - now-playing notifications (popped up when the track changes);
  - low battery alerts (threshold adjustable; one alert per charge cycle), charging-complete alerts (target battery threshold adjustable, default 100%), network loss / recovery alerts;
  - notification history: last 50 entries, viewable/clearable in Settings.
  - notification history enhancements: unread red dot, mark all read, delete individual entries, click an entry to open its source app, clear history;
  - notification collapsing: duplicate notifications from the same source with the same title reuse one banner and accumulate the count;
  - Do Not Disturb allowlist: sources in the allowlist (comma-separated exe names) are unaffected by Do Not Disturb and still show banners normally.
- **Global hotkeys** (all can be disabled/customized): `Ctrl+Alt+P` play/pause · `Ctrl+Alt+←/→` previous/next · `Ctrl+Alt+I` show/hide · `Ctrl+Alt+Space` expand/collapse · `Ctrl+Space` quick launcher · `Ctrl+Alt+V` clipboard history panel.
- **Reduce motion** (accessibility / battery): one-click disable of spring animations; instant switching.
- **Island size adjustment**: Settings → Appearance; adjust compact length/width and expanded length.
- **Island always resident**: always shown even without media playing (shows configured widgets).
- **Multi-monitor**: primary screen / all screens / a specific screen number.
- **High DPI**: PerMonitorV2, no misalignment at 120/150/200% scaling.
- **Custom configuration**: position, offset, opacity, theme color, compact mode content, hide when no media, etc.; changes take effect instantly.
- **Auto-hide the Island when no media is playing** (can be disabled).
- **Do Not Disturb mode**: one-click manual enable or auto-silence by time period (one-click toggle in the tray menu; configure the period in Settings).
- **Check for updates**: manually check for new GitHub versions from the tray menu / Settings; optional auto-check (off by default, requires internet).
- **Double-click quick action on the Island** (Settings → General): set to “Play / Pause” (default), “Expand / Collapse”, “Show Desktop”, “Hide / Show Island”, “Previous Track”, “Next Track”, “Open Settings”, or “No Action”.
- **Meeting mute assistant (meeting detection)**: detects meeting windows of Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet, etc.; auto-enables Do Not Disturb during meetings and shows the “In Meeting” widget (purely local heuristics, no internet).
- **Screen recording / screenshot alerts**: shows a notification when taking screenshots with `PrintScreen` / `Alt+PrintScreen`; when recording software such as OBS, Bandicam, Fraps, Camtasia, XSplit, Streamlabs, Xbox Game Bar is detected, shows “Screen Recording in Progress” (purely local process detection, no internet).
- **Smart Do Not Disturb (recording)**: auto-silences notifications while screen recording is in progress (no banners), and restores automatically when recording ends; toggle in Settings → Notifications.
- **Auto-hide in fullscreen**: when fullscreen video / game / presentation (e.g., PowerPoint slideshow) is detected, the Island auto-hides/collapses and restores when leaving fullscreen; toggle in Settings → General.
- **Drag files onto the Island**: drag files/folders onto the Island to “copy path / open containing folder / pin to Island”, etc. (choose via right-click on the Island or the drag-drop menu).
- **Calendar event reminders (.ics)**: parses local iCalendar files (exported from Outlook / Google Calendar / phone); when an event is due (can remind N minutes early) a banner pops up; purely local parsing, no internet.
- **RSS subscription alerts**: polls RSS 2.0 / Atom feeds (interval adjustable); banners appear when new entries arrive; only contacts the network for the feeds you entered.
- **Email alerts (POP3)**: periodically fetches email headers; new emails trigger banners (read-only headers, no body download, no data upload; an app-specific password is recommended).
- **Quick launcher (Spotlight style)**: `Ctrl+Space` to summon; search installed apps / Start menu programs, or directly type a URL to open it; hotkey customizable.
- **Clipboard history panel**: `Ctrl+Alt+V` opens a standalone clipboard history window; click an entry to copy it back to the clipboard; can be cleared; hotkey customizable.
- **Rules (automation)**: Settings → Rules; combine conditions (always / no media playing / playing / time range / specific media app) with actions (hide / force collapse / force show) to automatically control the Island; hide takes priority, then collapse, then force show.
- **Low power mode**: when idle, lowers the wave frame rate and simplifies animations for power saving (Settings → General).

### P2 (Implemented)
- Simplified Chinese + English UI switching.
- Export / import JSON config files.
- Windows notification integration (Bluetooth / system notification takeover / now playing / low battery).
- Pending: incoming call reminder (not implemented); schedule reminders implemented (widget + productivity tools).

---

## Tech stack and rationale

| Approach | Verdict | Rationale |
| --- | --- | --- |
| **C# + WPF (.NET 8)** | ✅ Adopted | Low resource usage, fast startup (vs. Electron/Tauri WebView), strongest system integration (native SMTC/CoreAudio/tray support), simple single-file packaging |
| C++ + Qt | ❌ | Lower development efficiency, complex licensing (LGPL), lots of hand-written code needed to integrate with the Windows media stack |
| Tauri / Electron | ❌ | High memory usage (resident >150MB hard to achieve), slow startup, violates the “low resource usage, fast startup” requirement |
| WinUI 3 | ❌ | More complex packaging/deployment than WPF (requires Windows App SDK), and SMTC support for unpackaged desktop apps is identical to WPF |

**Key points**:
- `Windows.Media.Control.GlobalSystemMediaTransportControlsSessionManager` is directly usable through .NET 8's Windows SDK projection (CsWinRT) without a UWP packaged identity.
- Apart from the built-in WPF/WinForms/Windows SDK projections, **zero third-party runtime dependencies** (see [THIRD_PARTY.md](THIRD_PARTY.md)).
- Acrylic effect: on both Win10/Win11 it is implemented via `SetWindowCompositionAttribute` (`ACCENT_ENABLE_ACRYLICBLURBEHIND`), with rounded corners clipped via `SetWindowRgn` so the blur follows the capsule shape.

---

## Architecture overview

```
src/WinIsland/
├── App.xaml(.cs)              # Composition root: single instance, exception handling, tray, window lifecycle
├── Services/
│   ├── MediaModels.cs         # Unified media snapshot models (TrackInfo / MediaSnapshot)
│   ├── SmtcMediaProvider.cs   # Windows global media session (event-driven + throttled pushes)
│   ├── CiderClient.cs         # Cider local API wrapper (V3 + LegacyV2, port scanning, fault-tolerant parsing)
│   ├── CiderMediaProvider.cs  # Cider session layer (connection lifecycle)
│   ├── WindowTitleMediaProvider.cs # Fallback: window title detection
│   ├── MediaCoordinator.cs    # Central dispatch: Cider > SMTC > window title; cover cache, volume attachment
│   ├── LrcParser.cs           # LRC parsing (multi-timestamps, offset, duration formats)
│   ├── LyricsService.cs       # Lyric parsing (local .lrc → Cider → online)
│   ├── OnlineLyricsService.cs # Online lyrics (unofficial NetEase Cloud Music / QQ Music APIs, on by default with one-click toggle)
│   ├── ArtworkCache.cs        # Cover download/cache (Cider remote covers → local files)
│   ├── SystemVolume.cs        # CoreAudio system volume (COM P/Invoke)
│   ├── AppSettings.cs         # JSON config read/write (%APPDATA%\WinIsland\settings.json)
│   ├── SingleInstance.cs      # Named mutex + named pipe (second launch shows the Island)
│   ├── AutoStart.cs           # HKCU Run key autostart
│   ├── GlobalHotkeyService.cs # Global hotkeys (Win32 RegisterHotKey)
│   ├── NotificationService.cs # Top-right glass notification banner
│   ├── NotificationHistoryService.cs # Notification history (last 50, JSON persistence)
│   ├── BluetoothMonitor.cs    # Bluetooth device connect/disconnect monitoring
│   ├── SystemNotificationMonitor.cs # Take over Windows notifications (UI automation mirror)
│   ├── MediaAppRegistry.cs    # Media app registry (enable/disable/order)
│   ├── AudioWaveService.cs    # Audio wave (system volume sampling, drives wave motion)
│   ├── KeyboardIndicatorMonitor.cs # Keyboard indicators (CapsLock status listener)
│   ├── ClipboardHistoryService.cs # Clipboard history
│   ├── TodoService.cs         # To-do list
│   ├── PomodoroService.cs     # Pomodoro timer
│   ├── ScheduleService.cs     # Schedule reminders
│   ├── IcsCalendar.cs       # .ics calendar parsing (events / VALARM)
│   ├── MeetingMonitor.cs    # Meeting window detection (meeting mute assistant)
│   ├── PrivacyDeviceMonitor.cs # Microphone/camera usage status (privacy registry polling)
│   ├── RssMailService.cs    # RSS feeds + email (POP3) alerts
│   ├── ScreenCaptureMonitor.cs # Screenshot / screen recording detection alerts
│   ├── IslandApiServer.cs   # Island Push API (v1 + v3 HTTP / WebSocket)
│   ├── IslandPushModels.cs  # Island card models (image / dynamic progress / heartbeat)
│   ├── DoNotDisturb.cs        # Do Not Disturb mode (manual / time window)
│   ├── UpdaterService.cs      # GitHub update check
│   ├── ProfileService.cs      # Config profiles (switch between multiple settings sets)
│   ├── WeatherService.cs      # Weather widget (Open-Meteo, requires internet)
│   ├── PlaybackStateStore.cs  # Playback position persistence (restore after exit/pause)
│   ├── CiderTokenAutoDetect.cs # Cider API Token auto-detection
│   └── AppLogger.cs           # Lightweight file logging
├── UI/
│   ├── IslandWindow.xaml(.cs) # Island window (animation, acrylic, positioning, hover interaction)
│   ├── IslandViewModel.cs     # Main view model (progress interpolation, lyric index, visibility)
│   ├── SettingsWindow.xaml(.cs) / SettingsViewModel.cs
│   ├── LyricsWindow.xaml(.cs) # Standalone lyrics window
│   ├── ThemeService.cs        # Light/dark theme + theme color brushes
│   ├── WindowEffects.cs       # Acrylic / dark mode / rounded corners region
│   ├── ScreenHelper.cs        # Multi-monitor + PerMonitorV2 DPI conversion
│   ├── TrayIcon.cs            # Tray icon and menu
│   ├── ClipboardPanelWindow.xaml(.cs) # Clipboard history panel
│   ├── QuickLauncherWindow.xaml(.cs)  # Quick launcher (Ctrl+Space)
│   └── Localization.cs        # Chinese/English string tables
└── Diagnostics/DiagnosticsCommand.cs  # --diagnose diagnostics
tests/WinIsland.Tests/         # xunit unit tests (LRC/config/Cider parsing/window title parsing)
build/
├── publish.ps1                # One-click publish (self-contained or framework-dependent + zip)
├── WinIsland.iss              # Inno Setup installer script
└── make-icon.ps1 / IconGen.cs # Icon generation tools
```

**Data flow**: `MediaCoordinator` polls each Provider every second (async, non-blocking UI) → produces a unified `MediaSnapshot` (including local cover path and volume) → published to `IslandViewModel` via the Dispatcher → a 200ms interpolator smoothly advances the progress bar and lyric highlighting → rendered via WPF bindings.

---

## Quick start

> 💡 **Prebuilt release**: the `releases/` directory provides single-file self-contained executables per version (e.g., `releases/1.1.5/win-x64/WinIsland-1.1.5-win-x64.exe`, includes the .NET 8 runtime, double-click to run). Beta versions are kept locally only; stable versions are published to GitHub (win-x64 / win-arm64 portables and the universal installer).

### Environment requirements
- Windows 10 1809+ / Windows 11
- Build machine: .NET 8 SDK (or a newer SDK with `net8.0-windows10.0.19041.0` specified)

### Build
```powershell
# Restore + build + test
dotnet build WinIsland.slnx -c Release
dotnet test  WinIsland.slnx -c Release

# Run (Debug)
dotnet run --project src\WinIsland -c Debug
```

### One-click publish
```powershell
# Self-contained (includes .NET 8 runtime, no install needed, ~73MB (single file))
.\build\publish.ps1

# Framework-dependent (smaller, requires .NET 8 Desktop Runtime)
.\build\publish.ps1 -FrameworkDependent
```
Output is placed in `publish\win-x64\` (containing `WinIsland.exe`); the zip is `publish\WinIsland-win-x64.zip`.

### Installer (optional)
After installing [Inno Setup 6](https://jrsoftware.org/isinfo.php):
```powershell
iscc.exe build\release-1.1.5.iss
```
Produces `releases\<version>\WinIsland-Setup-<version>.exe` (universal installer supporting both x64 and ARM64, auto-installs by architecture). When publishing a stable release, copy a `release-<version>.iss` under `build\` for that version and update the version number.

---

## Usage

1. Launch `WinIsland.exe` (or set it to start with Windows / tick autostart in the installer). A tray icon appears.
2. Play any music:
   - NetEase Cloud Music, QQ Music, Spotify, Apple Music official builds, etc. → shown automatically through the system media session;
   - Cider → see [Cider integration](#cider-integration);
   - Other players → window-title fallback (display only).
3. **Click** the Dynamic Island to expand the full card (hover does not expand): draggable seek, playback controls, volume, synced lyrics; click again to collapse (auto-collapse 700ms after the pointer leaves the card).
4. Tray menu: show/hide, standalone lyrics window, start with Windows, **Do Not Disturb** (tick to silence notifications), **Check for updates**, **View logs**, settings, exit. **Closing the main window does not exit the process** (it only minimizes to tray).
5. Global hotkeys: `Ctrl+Alt+P` play/pause · `Ctrl+Alt+←/→` previous/next · `Ctrl+Alt+I` show/hide · `Ctrl+Alt+Space` expand/collapse · `Ctrl+Space` quick launcher (search apps / type a URL and press Enter) · `Ctrl+Alt+V` clipboard history panel (all can be disabled/customized).
6. Notifications and alerts (Bluetooth / Windows notifications / now playing / low battery) appear by default as glass banners in the top-right corner; toggle them in Settings → Notifications; with **Do Not Disturb** enabled no banners are shown (the badge still counts).
7. Common command-line arguments:
   ```powershell
   WinIsland.exe --demo       # Demo mode (preview UI + sample lyrics when there is no media)
   WinIsland.exe --diagnose   # Write a diagnostic report to %APPDATA%\WinIsland\diagnostics.txt
   WinIsland.exe --settings   # Open settings at startup
   ```

---

## Configuration reference

Config file: `%APPDATA%\WinIsland\settings.json` (JSON; changes in the settings UI take effect immediately; can be exported/imported).

| Key | Default | Description |
| --- | --- | --- |
| `Language` | `zh-CN` | `zh-CN` / `en-US` |
| `ThemePreset` | `Default` | Theme skin: `Default/Ocean/Forest/Sunset/Neon/Mono/Grape/Sky/Rose/Amber/Lime/Teal/Lavender/Crimson/Midnight/Coffee/Sakura/Aurora/Custom` (overrides AccentColor) |
| `FontFamily` | `Segoe UI` | UI font |
| `FontScale` | `1.0` | Font size scaling 0.8–1.4 |
| `CornerRadius` | `28` | Capsule corner radius 16–40 |
| `BadgeEnabled` | `true` | Unread notification badge (red dot + number in the top-right corner) |
| `CoverTintBackground` | `true` | Expanded background takes color from the album cover |
| `WaveVisualizerEnabled` | `true` | Audio waves to the left of the control buttons while media plays |
| `WaveStyle` | `Bars` | Wave style: `Bars` (bars) / `Spectrum` (spectrum) / `Ring` (ring) / `Particles` (particles) |
| `WaveSyncEnabled` | `true` | Waves follow the music rhythm (driven by sampled system output audio) |
| `WaveSensitivity` | `1.0` | Wave sensitivity 0.2–3.0 |
| `WaveHeight` | `1.0` | Wave height 0.4–1.6 |
| `WallpaperThemeColorEnabled` | `false` | Wallpaper color extraction: take the main color of the current wallpaper as the theme color (purely local) |
| `MarqueeTextEnabled` | `false` | Marquee: auto-scroll song titles/lyrics horizontally when too wide |
| `EdgeSnapEnabled` | `true` | Snap to screen edges/center when released after dragging while unlocked |
| `FullScreenAutoHideEnabled` | `true` | Auto-hide the Island in fullscreen (video/game/presentation) |
| `RecordingDndEnabled` | `false` | Auto Do Not Disturb while recording (no notification banners) |
| `Theme` | `Auto` | `Auto` / `Light` / `Dark` |
| `AccentColor` | `#6C5CE7` | Theme color (#RRGGBB) |
| `Position` | `Center` | `Center` top-center / `Right` top-right |
| `Monitor` | `Primary` | `Primary` primary screen / `All` all screens / `Index` a specific screen |
| `MonitorIndex` | `0` | Screen number when `Monitor=Index` |
| `OffsetX` / `OffsetY` | `0` / `16` | Pixel offsets |
| `Opacity` | `0.92` | 0.3–1.0 |
| `HideWhenNoMedia` | `true` | Hide the Island when no media is playing |
| `ShowWhenPaused` | `true` | Still show when paused |
| `StartWithWindows` | `false` | Start with Windows |
| `StartHidden` | `false` | Start hidden |
| `CompactShowArt/Title/Progress` | `true/true/false` | Compact mode content |
| `CiderEnabled` | `true` | Enable the Cider local API |
| `CiderPort` | `0` | `0` auto-detect (default 10767); or enter a port manually |
| `CiderToken` | `""` | Cider API Token (can be left empty) |
| `OnlineLyricsEnabled` | `true` | Online lyrics (on by default, one-click toggle via right-click on the Island; see the copyright note) |
| `LyricsFolder` | `""` | Extra .lrc directory; when empty, auto-searches `%APPDATA%\WinIsland\Lyrics`, `Music\Lyrics`, and the top level of `Music` |
| `StandaloneLyricsWindow` | `false` | Standalone lyrics window |
| `KaraokeHighlight` | `true` | Word-by-word karaoke highlight (current line lights up character by character) |
| `UseSystemVolume` | `true` | Use the system volume bar for non-Cider sources |
| `IsLocked` | `true` | Locked (cannot be dragged); right-click menu can unlock/lock/center-align |
| `IslandAlwaysVisible` | `false` | Island always resident (shows widgets even without media) |
| `ShowMediaInfo` | `true` | Show media playback info (song title/cover/lyrics, etc.) |
| `ReduceMotion` | `false` | Reduce motion (disable spring animations; accessibility/battery) |
| `GlobalHotkeysEnabled` | `true` | Global hotkeys on/off |
| `LowBatteryThreshold` | `20` | Low battery alert threshold (%), 0 disables |
| `ExpandedShowArtTitle/Progress/Controls/Lyrics` | `true` | Expanded card section toggles (cover+title / progress bar / controls & volume / lyrics) |
| `Components` | Object | Widget checkboxes: `Time/Weather/Date/Cpu/Gpu/Ram/Net/Battery/Volume/CapsLock/Clipboard/Todo/Timer/Schedule/Holiday/Meeting/Mic/Cam` each have `WhenIdle`/`WhenPlaying` columns; `Cover/Title/Artist/Lyrics/Progress` shown while playing; the `ComponentBadges` dictionary fills badge text for each widget |
| `WidgetOrder` | `Time,Weather,...` | Widget order (comma-separated keys, includes `Song`) |
| `MediaApps` | `[]` | Media app enable/disable and priority (empty = all enabled) |
| `CompactWidth` / `CompactHeight` | `360` / `72` | Compact width / compact height (manual drag adjustment turns off auto-adjustment) |
| `CompactWidthAuto` / `CompactHeightAuto` | `true` | Compact size auto-adjusts to widget content (on by default) |
| `ExpandedWidthAuto` / `MaxExpandedHeightAuto` | `true` | Expanded size auto-adjusts (on by default) |
| `ExpandedWidth` / `MaxExpandedHeight` | `400` / `384` | Expanded width / expanded max height |
| `BluetoothNotifyEnabled` | `false` | Bluetooth connect/disconnect alerts |
| `NotificationTakeoverEnabled` | `false` | Take over Windows notifications (best effort) |
| `NotificationTimeoutSeconds` | `6` | Notification banner display duration (seconds) |
| `NotificationPosition` | `TopRight` | Notification popup position (top-right) |
| `DoNotDisturbEnabled` / `DoNotDisturbManual` | `false` | Do Not Disturb: automatic by time period / manual toggle |
| `DoNotDisturbStartHour` / `DoNotDisturbEndHour` | `22` / `8` | Do Not Disturb time window (hours) |
| `DnDAllowlist` | `[]` | Do Not Disturb allowlist (`QQ.exe,WeChat.exe`; allowlisted sources still show notifications) |
| `Rules` | `[]` | Automation rules list (conditions + actions) |
| `ClipboardHistoryEnabled` / `ClipboardHistoryMax` | `false` / `15` | Clipboard history on/off and entry limit |
| `PomodoroEnabled` / `PomodoroWorkMinutes` / `PomodoroBreakMinutes` | `false` / `25` / `5` | Pomodoro on/off and work/break durations (minutes) |
| `KeyIndicatorSeconds` | `3` | Keyboard indicator (CapsLock) display duration (seconds) |
| `VolumeTempIndicatorEnabled` / `VolumeTempIndicatorSeconds` | `true` / `4` | Volume/mute temporary Island on/off and display seconds |
| `FileCopyNotifyEnabled` | `true` | File copy/move in progress on the Island (purely local window-title detection) |
| `DownloadProgressEnabled` | `false` | Downloads in progress on the Island (scans temporary files in the download directory; off by default) |
| `UsageMergeEnabled` / `UsageMergeItems` | `false` / `Mic,Cam,Meeting,Recording` | “In use” merged capsule on/off and merged widgets (off by default) |
| `AutoUpdateCheck` | `false` | Auto-check for new GitHub versions (off by default, requires internet) |
| `DoubleClickAction` | `PlayPause` | Double-click Island action: `PlayPause` / `ToggleExpand` / `ShowDesktop` / `ToggleVisible` / `NextTrack` / `PrevTrack` / `OpenSettings` / `None` |
| `AnimationStyle` | `Spring` | Motion skin: `Spring` / `Soft` / `Elastic` / `Fade` |
| `ThemeTint` | `""` | Custom background color #RRGGBB (effective when the preset is Custom) |
| `ExpandedCardStyle` | `Classic` | Expanded card template: `Classic` / `Hero` |
| `NetCurveEnabled` | `true` | Network widget shows the mini 32-second curve |
| `LowPowerMode` | `false` | Low power mode (lower wave frame rate and simplified animations when idle) |
| `MeetingAssistantEnabled` | `false` | Meeting mute assistant: detects meeting windows + auto Do Not Disturb |
| `MeetingAutoDnd` / `MeetingKeywords` | `true` / `""` | Auto Do Not Disturb during meetings / custom meeting keywords |
| `ScreenCaptureNotifyEnabled` / `ScreenshotNotifyEnabled` / `RecordingNotifyEnabled` | `false` / `true` / `true` | Screenshot/recording alerts master switch and sub-options |
| `CalendarEnabled` / `CalendarIcsPath` / `CalendarAdvanceMinutes` | `false` / `""` / `10` | .ics calendar reminder on/off / file path / advance reminder minutes |
| `RssNotifyEnabled` / `RssUrls` / `RssIntervalMinutes` | `false` / `""` / `15` | RSS subscription alerts / feed URLs / polling interval (minutes) |
| `MailNotifyEnabled` / `MailPop3Server` / `MailPop3Port` / `MailUseSsl` / `MailUser` / `MailPassword` / `MailCheckMinutes` | `false` / `""` / `995` / `true` / `""` / `""` / `5` | Email alerts (POP3) on/off, server, port, SSL, account, app-specific password, check interval |
| `QuickLauncherEnabled` / `HotkeyLauncher` | `true` / `Ctrl+Space` | Quick launcher on/off and hotkey |
| `ClipboardPanelEnabled` / `HotkeyClipboardPanel` | `true` / `Ctrl+Alt+V` | Clipboard history panel on/off and hotkey |
| `HotkeyExpand` | `Ctrl+Alt+Space` | Expand/collapse hotkey |
| `NotifyFoldEnabled` | `true` | Fold similar notifications (same source + same title shows as one) |
| `ActiveProfile` | `Default` | Config profile name (switch between multiple settings sets) |

---


## Cider integration

Cider (a third-party Apple Music client) provides a local HTTP API. WinIsland wraps it in a dedicated module (`CiderClient.cs`) that automatically adapts to version differences.

**Setup steps (important)**:
1. Open Cider: **Settings → Connectivity → Allow External Application Access**; once enabled, Cider shows an API Token (click Generate if it is blank).
2. Copy the token into **WinIsland Settings → Cider → API Token** and save.
3. Default port `10767`; WinIsland auto-detects it; the legacy RPC is `10769`.

> ⚠️ Cider 2.x new versions require a **Token for all API requests** by default (without a Token you get `403 UNAUTHORIZED_APP_TOKEN`). If the diagnostic log asks for a Token, fill it in as above; otherwise Cider lyrics/control are unavailable (tracks can still be shown via SMTC).

> ⚠️ If the log repeatedly shows HttpClient.Timeout (originally 2s), it is usually caused by local security software/proxy intercepting loopback HTTP (Cider actually responds in about 30ms). Since 1.0.1 the data-read timeout has been relaxed to 5s; if it still times out, check antivirus software for network interception of WinIsland.

**Implemented API capabilities** (compiled from Cider community docs / the `cider-api` crate, 2026 versions):
- `GET /api/v1/playback/active`, `GET /now-playing` (track/cover/progress/status)
- `POST /api/v1/playback/play|pause|playpause|next|previous|seek`
- `GET|POST /api/v1/playback/volume`
- `GET /api/v1/lyrics` (with `?id=` fallback)
- Auth header: `apptoken` (also compatible with `apitoken`)
- Legacy 10769: `/active`, `/currentPlayingSong`, `/playPause`, `/next`, `/previous`, `/seekto/{t}`, `/audio`

> ⚠️ The Cider API is unofficial and changes rapidly; all requests use a 2-second timeout and automatically fall back to SMTC / window title on failure, **without affecting the main flow**. Keep WinIsland updated to adapt to new versions.

---

## Lyrics

Priority:
1. **Local .lrc**: searched as `SongTitle.lrc` / `Artist - SongTitle.lrc` in the lyric folders (defaults: `%APPDATA%\WinIsland\Lyrics`, `Music\Lyrics`, and the top level of `Music`);
2. **AMLL word-by-word lyrics** (TTML word timeline from the amll.dev library, enabled by default);
3. **Cider lyrics API** (when the source is Cider);
4. **Online lyrics** (unofficial NetEase Cloud Music / QQ Music APIs): **enabled by default**; one-click toggle via right-click on the Island, or turn off in Settings.

> ⚠️ Online lyrics use unofficial APIs and are for personal study only; please respect copyright. If the rights holder requests, you can disable this feature at any time (fully offline once disabled).

---

## Notifications (since 1.0.2, improved in 1.0.3)

All notifications are **glass banners in the top-right corner** with macOS-style slide-in (slides in from the right + fades in) and slide-out animations; the display duration is configurable (3–15 seconds).

- **Bluetooth connection alerts**: Settings → Notifications; once enabled, banners appear when Bluetooth devices connect/disconnect.
- **Take over Windows notifications**: Settings → Notifications; once enabled, uses UI automation to mirror notification center content (e.g., QQ notifications) to the top-right banner on a best-effort basis.
  > ⚠️ Windows does not provide a public API to “intercept other apps' notifications”; this feature is best effort and some notifications may not be captured; it does not affect the main flow.
- **Now playing notification**: a “Now playing - Song title” banner pops up when the track changes (since 1.0.3).
- **Low battery alert**: shown when the battery drops below the threshold (default 20%, adjustable 0–50), once per charge cycle (since 1.0.3).
- **Notification history**: the last 50 notification records, viewable/clearable on the Settings → Notifications page (since 1.0.3).
- **Island size adjustment**: Settings → Appearance; adjust compact length/width and expanded length.

---
## Island Push API (push from other apps to the Dynamic Island)

WinIsland includes a built-in local HTTP service, so other software can push information to the Dynamic Island in real time (similar to iOS third-party Dynamic Island integrations). **Developer documentation: [docs/IslandAPI.md](docs/IslandAPI.md)**.

| Endpoint | Description |
|---|---|
| `POST /v1/island/push` | Push / update an Island card (image / dynamic progress / heartbeat supported since v3) |
| `PATCH /v3/island/push/{id}` | Partial update: only covers the fields present in the request body (keeps expiry / queue position) |
| `DELETE /v1/island/push/{id}` | Remove a card |
| `GET /v1/island/active` (or `/v3/island/active`) | Query currently active cards |
| `GET /v3/ws` | WebSocket two-way channel: clients send `push/update/remove/ping`, the server broadcasts `push_updated/push_removed` events |
| `GET /v1/health` | Health check |

- Settings → Island Push API: enable switch, port (default 9840), optional Token, global default display duration
- Pushes **do not change the Island's length/width**; cards show on a single row in compact state and do not obscure other widgets
- Buttons support "open link / launch program"; the pusher can set the display duration per entry (overriding the global default)
- New in v3: `image` (data URI or http image), `progress_from/progress_to/progress_duration_seconds` (progress advances automatically), `heartbeat_seconds` (heartbeat renewal; automatically removed if not renewed within 2x the interval), `theme` (dark/light/auto card theme), `action: "command"` (button runs a local command); full developer docs: [docs/IslandAPI.md](docs/IslandAPI.md)

---
## Playback state restore

- When the app exits, pauses, or changes track, it saves “track + playback position” to `%APPDATA%\WinIsland\state.json` (local only).
- On next launch, if it is still the same track and the player hasn't returned the real progress yet, it restores from the last position first, avoiding the “shows line 0 first, then jumps to the paused line” jump; no restore after more than 1 hour or when the track changed.

---
## Privacy and security

- **No telemetry, no ads, no reporting**. The app makes no network requests except for the “online lyrics” feature you enable manually.
- **Weather widget**: only when you enable “Show Weather” and fill in a city does it request Open-Meteo (free, no key, no account) for current weather; otherwise fully offline.
- The only network scenarios: Cider cover downloads (`mzstatic.com`, public cover URLs returned by the local API) and online lyrics once enabled by the user.
- All data is stored locally in `%APPDATA%\WinIsland\`.
- Logs only record local runtime information (`logs\app-*.log`).

---

## Non-functional metrics

Measured on the test machine (Windows 11 24H2, 2560×1440@100%) with a Release self-contained build:

| Metric | Measured | Target |
| --- | --- | --- |
| Idle CPU (no media) | < 0.5% (Debug measured 0.3%) | ≈ 0% |
| Resident memory (Private) | ~72 MB | ≤ 150 MB |
| Startup | < 1 s (cold start) | ≤ 2 s |
| Closing the main window | Does not exit, only minimizes to tray | ✅ |
| Multiple instances | Single instance only; a second launch shows the Island | ✅ |
| Exceptions | Caught uniformly and written to the log, no crash dialog | ✅ |

> Note: the WorkingSet of a self-contained deployment (including .NET runtime shared pages) is about 160MB, but **Private memory is about 72MB**; with a framework-dependent deployment, WorkingSet is lower.

---

## Known limitations

- **Word-by-word karaoke depends on the lyric source and progress**: with an AMLL TTML/LRC word timeline, words highlight individually; without a word timeline or when the player doesn't provide real progress, it degrades to whole-line highlighting (advanced by the local clock).
- **Occasional player progress rollback** (e.g., Cider/SMTC momentarily reporting 0 or a stale position): a position guard is in place — momentary rollbacks are ignored and the current progress continues, so lyrics/progress bar are not knocked back to the start; a rollback lasting more than about 4 seconds is treated as a real replay or a player-side seek.
- **Incoming call reminder**: not implemented (P2 optional). Implemented: Bluetooth alerts, Windows notification takeover (best effort), now-playing notifications, low battery alerts, schedule reminders.
- **SMTC coverage**: whether a player registers a global media session decides this; a few older players that don't register can only use the window-title fallback (no control buttons).
- **Cider 1.x (old API on port 9000)**: not adapted; only 2.x and above are supported.

---

## Verification guide (requires testing with real players)

The following scenarios need verification in a real environment (the parts already verified automatically in this repo's development environment are noted):

| Scenario | Status |
| --- | --- |
| SMTC session enumeration (`--diagnose` shows the session list) | ✅ Tested (detected real sessions such as Bilibili) |
| Island auto show/hide, click to expand/collapse, progress interpolation | ✅ Tested (demo + real paused session) |
| Play/pause/track switch/seek (real player) | ⚠️ Needs testing (code path corresponds directly to the SMTC control API) |
| Cider API connection & control | ⚠️ Requires Cider installed locally with external control enabled |
| Local .lrc lyrics synced scrolling | ✅ LRC parsing unit-tested; end-to-end needs a real song |
| Online lyrics | ✅ Integrated with NetEase Cloud Music/QQ Music; end-to-end needs a real song |

**Suggested verification steps**:
1. `WinIsland.exe --diagnose` → confirm `System media sessions` lists the player;
2. Play any track in NetEase Cloud Music/QQ Music/Spotify → the Island should show the track and allow control;
3. Open Cider and enable external control → the Island source should show `Cider`, and seek/volume should work;
4. Place a same-named `.lrc` in the song's directory → after expanding, the lyrics should scroll and highlight with progress.

---

## FAQ

**Q: The Dynamic Island doesn't appear?**
- Make sure something is playing (it is still shown by default when paused); `HideWhenNoMedia` is on by default, so hiding with no media is normal.
- Run `--diagnose` to view the session list; if the list is empty, the player hasn't registered SMTC.

**Q: Cider shows “Not connected”?**
- Make sure “Allow External Control” is enabled in Cider's settings; check the port (default 10767); confirm Cider is enabled in WinIsland settings.

**Q: Online lyrics won't load?**
- Online lyrics are enabled by default (right-click the Island → Online Lyrics to toggle); if there are still no lyrics, confirm it is enabled in Settings → Lyrics, and test network reachability.

**Q: The tray icon is still there after quitting?**
- Tray menu → Exit; closing the Island window directly only hides it (consistent with the “tray-resident” design).

---

## Open source license

- App: MIT (see [LICENSE](LICENSE))
- Third-party components: see [THIRD_PARTY.md](THIRD_PARTY.md)

---

## Español

## WinIsland — Isla Dinámica para Windows

> Lleva la Isla Dinámica de iOS a Windows: ventana flotante de escritorio con control de medios, letras sincronizadas, componentes personalizables, centro de notificaciones y residencia permanente en la bandeja del sistema.
> Basado en **.NET 8 + WPF**, compatible con Windows 11 (también con Windows 10, 1809+).

---

> **Lleva la Isla Dinámica de iOS a Windows | Una Isla Dinámica moderna y multifuncional.**

Lleva la Isla Dinámica de iOS a Windows 11 / 10: control de reproducción de medios, letras karaoke carácter por carácter, componentes personalizables, centro de notificaciones y API de la Isla, todo en una sola cápsula. Basado en **.NET 8 + WPF**, gratuito y de código abierto (MIT), **sin anuncios · sin telemetría**.

🌐 **Sitio web: https://WinIsland.JudeKwong.com**

---

## ✨ Destacados

- **▶ Control de reproducción de medios**: se conecta de forma nativa a las sesiones de medios globales de Windows (SMTC), compatible con NetEase Cloud Music, QQ Music, Spotify, Apple Music, Groove, Películas y TV, etc.; además, soporte específico para la API local de Cider; si no se puede conectar, se usa el título de la ventana como respaldo. Portada de álbum, arrastre de progreso (seek), reproducir/pausar/cambiar de canción, todo incluido; con varios reproductores abiertos a la vez puede cambiar la fuente de control con un clic; al hacer clic en la portada se abre una vista previa envolvente a pantalla completa.
- **♪ Letras karaoke carácter por carácter**: la tarjeta expandida se desplaza y resalta sincronizada, iluminando carácter por carácter al estilo karaoke; tres niveles de fuentes de letras: `.lrc` local → interfaz de letras del reproductor → letras en línea opcionales; letras bilingües, interruptor de traducción, copiar la línea actual con un clic; el tiempo de las letras se puede ajustar finamente por canción, y la ventana independiente de letras permite ajustar opacidad y bloqueo.
- **▦ Sistema de componentes personalizables**: hora, clima, fecha (con calendario lunar / términos solares), CPU/GPU/memoria/disco, velocidad de red, batería, método de entrada, interruptores rápidos (WiFi/Bluetooth/modo nocturno/silencio) y más de 30 componentes; cada componente puede tener un icono personalizado, selección con casillas y orden por arrastre, con modos de una línea o varias líneas en cualquier momento.
- **⇪ API de la Isla**: interfaz local HTTP / WebSocket que permite a cualquier software de terceros enviar información a la Isla Dinámica en tiempo real (similar a la integración de apps de terceros en la Isla Dinámica de iOS). v3 admite imágenes, progreso dinámico, renovación por latido (heartbeat) y temas claro/oscuro de la tarjeta; las notificaciones no cambian el ancho/alto de la Isla ni ocultan otros componentes; los botones admiten abrir enlaces / iniciar programas / ejecutar comandos locales, y el clic en el botón notify puede devolver la llamada al remitente mediante WebSocket.
- **🔔 Centro de notificaciones**: banners de vidrio en la esquina superior derecha, con animaciones de deslizamiento de estilo macOS: dispositivos Bluetooth, avisos de llamadas de voz/video de WeChat/QQ, toma de control de notificaciones del sistema, en reproducción, batería baja/carga completada, sin conexión/recuperada; historial de notificaciones, plegado, lista blanca de No molestar y automatización por reglas; los banners pueden incluir botones de acción (como «Desconectar» y «Configuración» para Bluetooth).
- **✦ Apariencia y efectos**: 18 temas de piel, color de acento y fondo personalizables, vidrio líquido esmerilado, **color extraído del fondo de pantalla** (extrae automáticamente el color del tema desde el fondo actual), **texto marquesina** (las letras/canciones largas se desplazan horizontalmente), 4 estilos de skin de animación (resorte iOS, etc.), **4 estilos de onda de audio** (barras / espectro / anillo / partículas, que vibran con el ritmo de la música); animaciones de expandir/contraer con easing no lineal a 60 fps; el fondo extraído de la portada puede «respirar» lentamente (tema dinámico); alta DPI PerMonitorV2, sin desalineaciones al 120/150/200 %.
- **🖱 Interacción e inteligencia**: desbloquear y arrastrar + **ajuste al borde** (se adhiere al borde/centro al soltar), **ocultar automáticamente en pantalla completa** (se contrae al reproducir video/juego/presentación a pantalla completa), acciones personalizadas al doble clic, **botones de acción rápida** (bloquear pantalla/silenciar/reproducir-pausar/captura/mostrar escritorio, etc., con orden personalizable), **arrastrar archivos a la Isla**, **No molestar inteligente durante grabación de pantalla**; respuesta inmediata al presionar el ratón (el clic tiene prioridad para expandir/contraer).
- **⚡ Herramientas de productividad y automatización**: Pomodoro, tareas pendientes, historial del portapapeles, lanzador rápido, recordatorios de agenda; asistente de silencio en reuniones, avisos de grabación de pantalla/captura, progreso de copia/descarga de archivos en la Isla; atajos globales y motor de reglas (mostrar/ocultar automáticamente según condiciones).
- **🛡 Privacidad y seguridad**: sin telemetría, sin anuncios, sin envío de datos. Totalmente sin conexión, salvo las letras en línea y el clima que el usuario active manualmente; toda la configuración y los datos se guardan solo localmente en `%APPDATA%\WinIsland`.

---

## 📥 Descarga (última versión estable 1.1.5)

| Plataforma | Descarga | Descripción |
| --- | --- | --- |
| Windows x64 | [Versión portátil x64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-x64.exe) | La opción principal para PC de 64 bits; un solo archivo, sin instalación, se ejecuta directamente |
| Windows ARM64 | [Versión portátil ARM64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-arm64.exe) | Dispositivos ARM como Surface Pro X / Snapdragon; un solo archivo, sin instalación |
| Windows universal | [Instalador universal](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-Setup-1.1.5.exe) | Asistente de instalación Inno Setup; instala automáticamente según la arquitectura x64 / ARM64 |

Todas las versiones anteriores y el registro de cambios completo están en [GitHub Releases](https://github.com/DMP-Pig/WinIsland/releases).

---

## 📊 Métricas de rendimiento

| Métrica | Valor |
| --- | --- |
| Memoria residente (Private) | ~72 MB |
| Inicio en frío | < 1 s |
| CPU en reposo | ≈ 0% |
| Fotogramas de animación | 60 fps fluidos |
| Múltiples instancias | Instancia única, evita la ejecución duplicada |
| Telemetría | 0 telemetría · sin envíos · sin anuncios |

---

## Tabla de contenidos

- [Características](#características)
- [Elección tecnológica y justificación](#elección-tecnológica-y-justificación)
- [Descripción general de la arquitectura](#descripción-general-de-la-arquitectura)
- [Inicio rápido](#inicio-rápido)
- [Compilación y empaquetado](#inicio-rápido)
- [Uso](#uso)
- [Referencia de configuración](#referencia-de-configuración)
- [Integración con Cider](#integración-con-cider)
- [Letras](#letras)
- [Privacidad y seguridad](#privacidad-y-seguridad)
- [Métricas no funcionales](#métricas-no-funcionales)
- [Limitaciones conocidas](#limitaciones-conocidas)
- [Guía de verificación (requiere probar con un reproductor real)](#guía-de-verificación-requiere-probar-con-un-reproductor-real)
- [Preguntas frecuentes](#preguntas-frecuentes)
- [Licencia de código abierto](#licencia-de-código-abierto)

---

## Características

### P0 (implementadas)
- **UI flotante de la Isla Dinámica (estilo iOS)**: por defecto centrada en la parte superior (configurable a la derecha); cápsula de esquinas redondeadas; sigue el tema claro/oscuro del sistema o el color del tema manual; **animación de transformación** entre cápsula compacta ↔ tarjeta completa (ventana fija + escala/aparición de un solo elemento, impulsada por el hilo de composición de WPF a 60 fps, con rebote elástico al estilo iOS); **clic para expandir/contraer** (al pasar el ratón no se expande), contracción automática al salir (buffer anti-toque accidental de 700 ms); los clics fuera de la tarjeta atraviesan la ventana.
- **Bloqueo y arrastre**: bloqueada por defecto (no se puede mover); el menú contextual permite **desbloquear** (arrastrar la Isla con el ratón una vez desbloqueada), **centrar** (igual vertical, centrado horizontal) y volver a **bloquear**. Al volver a bloquear tras arrastrar se **mantiene la posición** (no vuelve al valor predeterminado); al soltar el arrastre hay **ajuste al borde** (se adhiere al borde/centro de la pantalla, configurable en Configuración → General).
- **Diseño compacto**: título/artista/letras alineados a la izquierda (pegados a la portada) y centrados verticalmente.
- **Portada del álbum**: tanto la cápsula como la tarjeta expandida muestran la portada (64 px en grande al expandir; icono de marcador de posición si no hay portada); las miniaturas SMTC y las portadas de Cider se guardan en caché automáticamente.
- **Control de reproducción de medios**: muestra título, artista y álbum; barra de progreso arrastrable (seek); reproducir/pausar, anterior, siguiente; control de volumen cuando sea necesario (Cider usa su API, otras fuentes controlan el volumen del sistema; se puede desactivar); el componente de medios muestra la insignia de la fuente actual (Spotify / Cider / NetEase Cloud Music / QQ Music, etc.).
- **Mini reproductor**: ventana flotante independiente (configurable en Configuración → Medios), muestra portada / título / artista / barra de progreso y controles de reproducción; se puede arrastrar libremente y recuerda la posición; se muestra/oculta automáticamente con la reproducción.
- **Cambio de dispositivo de salida de audio**: Configuración → Medios permite enumerar y cambiar el dispositivo de salida predeterminado del sistema (se recomienda reiniciar el reproductor tras el cambio).
- **Soporte de múltiples fuentes**:
  1. Sesiones de medios globales de Windows (`Windows.Media.Control` / SMTC): NetEase Cloud Music, QQ Music, Spotify, Apple Music oficial, Groove, Películas y TV, etc.;
  2. **Cider**: API HTTP local (puerto 10767, compatible con el RPC antiguo 10769, escaneo automático de puertos + configuración manual, soporta autenticación `apptoken`);
  3. Respaldo: título de ventana + identificación de proceso (solo muestra información, sin capacidad de control).
- **Visualización de letras (modo karaoke carácter por carácter)**: al expandir, la zona de letras se muestra en modo karaoke: **los caracteres de la línea actual se iluminan uno a uno**; el progreso de resaltado es un valor continuo y los caracteres en el límite pasan suavemente del color base al color de resaltado con easing a 60 fps, fluyendo de izquierda a derecha según el orden de lectura (también correcto con letras con saltos de línea, sin iluminar varias líneas a la vez); cada línea comienza desde 0 (el primer carácter no se ilumina al inicio); al pausar, el resaltado se congela en el momento de la pausa: cuando Cider no tiene estado explícito, se determina reproducir/pausar según si «la posición se mueve» (ya no se confunde con reproducción solo por remainingTime>0), SMTC prioriza seguir la sesión de Cider (evita que otras sesiones activas como Bilibili la capturen); al salir y reiniciar se restaura automáticamente la última posición de pausa (no vuelve al inicio); la línea actual solo resalta el texto (sin cápsula de fondo, para evitar doble resaltado; tamaño grande de 20 px), las demás líneas se atenúan, **desplazamiento suave con centrado automático** (se aproxima a la línea actual fotograma a fotograma a 60 fps y la sigue al expandir); en estado compacto la línea actual se muestra en tiempo real alineada a la izquierda y también se ilumina carácter por carácter; ventana flotante independiente de letras opcional.
  - **Sincronización de progreso**: lee automáticamente el token de la API local de Cider (sin configuración) para obtener el progreso real de reproducción y sincronizar con precisión el karaoke con la canción; los reproductores sin progreso disponible usan el reloj local.
  - **Fuentes de letras**: `.lrc` local (`%APPDATA%\WinIsland\Lyrics` o carpeta de música) → letras karaoke carácter por carácter de AMLL → interfaz de letras de Cider → letras en línea (interruptor de un clic con el clic derecho en la Isla). Sin letras muestra «Sin letras», sin errores.
  - **Letras bilingües**: combina automáticamente las líneas de traducción con marcas de tiempo adyacentes; se puede desactivar en la configuración (no se necesitan archivos de letras adicionales); interruptor de mostrar/ocultar traducción y «Copiar línea actual» para copiar con un clic.
- **Bandeja del sistema**: icono permanente, menú contextual (mostrar/ocultar, ventana de letras independiente, iniciar con Windows, configuración, salir), doble clic para alternar la visibilidad.

### P1 (implementadas)
- **Sistema de componentes (contenido personalizable de la Isla)**: Configuración → Componentes permite marcar qué componentes mostrar «sin canción / con canción» y ajustar el orden arrastrando:
  - Hora, clima (Open-Meteo, requiere ciudad y conexión), fecha (con calendario lunar y términos solares), uso de CPU, uso de GPU, uso de memoria, velocidad de red (puede mostrar la mini curva de los últimos 32 segundos), batería, espacio libre en disco, estado del método de entrada (chino / inglés + nombre del IME), interruptores rápidos (WiFi / Bluetooth / modo nocturno / silencio con un clic), volumen, indicador de teclado (CapsLock), portapapeles, tareas pendientes, Pomodoro, agenda, cuenta atrás de días festivos, en reunión, micrófono, cámara;
  - Información de la canción (portada/título/artista/letras/barra de progreso, solo durante la reproducción, siempre presente en la barra de orden).
  - La barra de orden solo muestra los componentes marcados; la lista y la barra admiten rueda del ratón y barras de desplazamiento; cada componente puede tener un icono personalizado (iconos MDL2 o emoji, Configuración → Componentes).
  - Componentes temporales en la Isla: cambio de volumen, captura / grabación de pantalla, copia / movimiento de archivos, descarga en curso (los dos últimos desactivados por defecto): cuando ocurre el evento, el componente correspondiente se muestra temporalmente incluso si la Isla está oculta.
  - **Cápsula combinada «En uso»** (Configuración → Componentes, desactivada por defecto): al activarla, «Micrófono / Cámara / En reunión / Grabación» seleccionados se combinan en una sola cápsula de estado «En uso · …», y los elementos combinados ya no se muestran por separado.
  - **Modo de una línea** (Configuración → Apariencia, activado por defecto): todos los componentes en una sola línea en estado compacto; sin expandir también muestra la información de la canción y la línea de letra actual (resaltado karaoke carácter por carácter), truncando las letras largas automáticamente; la barra de progreso y la lista completa de letras se muestran en la tarjeta expandida.
- **Personalización del contenido de la tarjeta expandida**: portada + título, barra de progreso, botones de control y volumen, y zona de letras se pueden activar/desactivar por separado.
- **Personalización de apariencia (página de ajustes estilo Configuración del Sistema de macOS)**: navegación izquierda + contenido derecho, vidrio líquido redondeado; **18 temas de piel predefinidos** (predeterminado / océano / bosque / atardecer / neón / monocromo / uva / cielo / rosa / ámbar / lima / verde azulado / lavanda / carmesí / medianoche / café / sakura / aurora, más personalizado); **color extraído del fondo de pantalla** (extrae automáticamente el color principal del fondo actual como color del tema, puramente local); **marquesina** (las letras largas se desplazan automáticamente); **4 estilos de onda de audio** (barras / espectro / anillo / partículas) y **4 skins de animación** (resorte iOS / suave / elástico / desvanecido); **modo de bajo consumo** (reduce la frecuencia de fotogramas de las ondas y simplifica las animaciones en reposo); PerMonitorV2 alta DPI.
- **Atajos globales**: `Ctrl+Alt+P` reproducir/pausar · `Ctrl+Alt+←/→` anterior/siguiente · `Ctrl+Alt+I` mostrar/ocultar · `Ctrl+Alt+Space` expandir/contraer · `Ctrl+Space` lanzador rápido · `Ctrl+Alt+V` panel del historial del portapapeles.
- **Reducir efectos dinámicos** (accesibilidad / ahorro de energía): desactiva las animaciones de resorte con un clic, cambio instantáneo.
- **Ajuste del tamaño de la Isla**: Configuración → Apariencia, permite ajustar largo/ancho compactos y largo expandido.
- **Isla Dinámica permanente**: siempre visible incluso sin reproducción (muestra los componentes configurados).
- **Varios monitores**: pantalla principal / todas las pantallas / número de pantalla especificado.
- **Alta DPI**: PerMonitorV2, sin desalineaciones al 120/150/200 %.
- **Configuración personalizada**: posición, desplazamiento, opacidad, color del tema, contenido del modo compacto, ocultar sin medios, etc.; los cambios se aplican al instante.
- **Ocultar automáticamente la Isla sin reproducción** (se puede desactivar).
- **No molestar**: activación manual con un clic o silencio automático por franja horaria (alternar con un clic en el menú de la bandeja; las franjas se configuran en los ajustes).
- **Buscar actualizaciones**: comprobación manual de nuevas versiones de GitHub en el menú de la bandeja / ajustes; comprobación automática opcional (desactivada por defecto, requiere conexión).
- **Acción rápida al doble clic** (Configuración → General): puede ser «Reproducir / Pausar» (predeterminado), «Expandir / Contraer», «Mostrar escritorio», «Ocultar / Mostrar la Isla», «Anterior», «Siguiente», «Abrir configuración» o «Sin acción».
- **Asistente de silencio en reuniones (detección de reuniones)**: identifica ventanas de reuniones como Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet, activa automáticamente No molestar durante la reunión y muestra el componente «En reunión» (heurística puramente local, sin conexión).
- **Avisos de grabación de pantalla / captura**: al pulsar `PrintScreen` / `Alt+PrintScreen` aparece un aviso; al detectar software de grabación como OBS, Bandicam, Fraps, Camtasia, XSplit, Streamlabs, Xbox Game Bar, aparece «Grabando pantalla» (detección local de procesos, sin conexión).
- **No molestar inteligente (grabación)**: al detectar grabación de pantalla en curso, se silencian automáticamente las notificaciones (sin banner); al terminar, se restaura automáticamente; configurable en Configuración → Notificaciones.
- **Ocultar automáticamente en pantalla completa**: al detectar video / juego / presentación a pantalla completa (como PowerPoint), la Isla se oculta/contrae automáticamente y se restaura al salir de pantalla completa; configurable en Configuración → General.
- **Arrastrar archivos a la Isla**: arrastrar archivos/carpetas a la Isla permite «Copiar ruta / Abrir carpeta contenedora / Fijar en la Isla», etc. (elija con el clic derecho en la Isla o en el menú de arrastrar y soltar).
- **Recordatorios de eventos de calendario (.ics)**: analiza archivos iCalendar locales (Outlook / Google Calendar / exportados del móvil); cuando llega la hora del evento (con N minutos de antelación configurable) aparece un banner; análisis puramente local, sin conexión.
- **Recordatorios de suscripciones RSS**: consulta periódicamente fuentes RSS 2.0 / Atom (intervalo ajustable); al aparecer una entrada nueva muestra un banner; solo se conecta a las direcciones de suscripción configuradas.
- **Recordatorios de correo (POP3)**: recupera periódicamente las cabeceras de los mensajes; con correo nuevo muestra un banner (solo lee cabeceras, no descarga el cuerpo ni sube datos; se recomienda usar un código de autorización).
- **Lanzador rápido (estilo Spotlight)**: se abre con `Ctrl+Space`, busca aplicaciones instaladas / programas del menú Inicio o abre directamente una URL; el atajo es personalizable.
- **Panel del historial del portapapeles**: `Ctrl+Alt+V` abre una ventana independiente del historial; al hacer clic en un elemento se copia de nuevo al portapapeles; se puede vaciar; el atajo es personalizable.
- **Reglas (automatización)**: Configuración → Reglas combina condiciones (siempre / sin medios reproduciéndose / reproduciendo / franja horaria / programa de medios específico) y acciones (ocultar / contraer forzosamente / mostrar forzosamente) para controlar la Isla automáticamente; ocultar tiene prioridad, luego contraer y por último mostrar forzosamente.
- **Modo de bajo consumo**: reduce la frecuencia de fotogramas de las ondas y simplifica las animaciones en reposo para ahorrar energía (Configuración → General).

### P2 (implementadas)
- Cambio de idioma de la interfaz entre chino simplificado e inglés.
- Exportar / importar archivo de configuración JSON.
- Integración de notificaciones de Windows (Bluetooth / toma de control de notificaciones del sistema / en reproducción / batería baja).
- Pendiente: aviso de llamada entrante (no implementado); recordatorios de agenda implementados (componente + herramienta de productividad).

---

## Elección tecnológica y justificación

| Opción | Conclusión | Justificación |
| --- | --- | --- |
| **C# + WPF (.NET 8)** | ✅ Adoptado | Bajo uso de recursos y arranque rápido (frente al WebView de Electron/Tauri), la mayor capacidad de integración con el sistema (soporte nativo SMTC/CoreAudio/bandeja), empaquetado simple en un solo archivo |
| C++ + Qt | ❌ | Baja eficiencia de desarrollo, licencia compleja (LGPL), requiere mucho código escrito a mano para integrarse con la pila de medios de Windows |
| Tauri / Electron | ❌ | Alto consumo de memoria (difícil lograr <150 MB en residencia), arranque lento, incumple el requisito de «bajo uso de recursos y arranque rápido» |
| WinUI 3 | ❌ | Empaquetado/implementación más complejo que WPF (requiere Windows App SDK), y el soporte SMTC para aplicaciones de escritorio no empaquetadas es igual que WPF |

**Puntos clave**:
- `Windows.Media.Control.GlobalSystemMediaTransportControlsSessionManager` está disponible directamente a través de la proyección del SDK de Windows de .NET 8 (CsWinRT), sin necesidad de identidad de empaquetado UWP.
- Aparte de las proyecciones WPF/WinForms/SDK de Windows integradas en el sistema, **cero dependencias de terceros en tiempo de ejecución** (ver [THIRD_PARTY.md](THIRD_PARTY.md)).
- Efecto acrílico: tanto en Win10 como en Win11 se implementa con `SetWindowCompositionAttribute` (`ACCENT_ENABLE_ACRYLICBLURBEHIND`) y se recortan las esquinas redondeadas con `SetWindowRgn` para que el desenfoque siga la forma de la cápsula.

---

## Descripción general de la arquitectura

```
src/WinIsland/
├── App.xaml(.cs)              # Raíz de composición: instancia única, captura de excepciones, bandeja, ciclo de vida de la ventana
├── Services/
│   ├── MediaModels.cs         # Modelo unificado de instantánea de medios (TrackInfo / MediaSnapshot)
│   ├── SmtcMediaProvider.cs   # Sesión de medios global de Windows (impulsada por eventos + envío con throttling)
│   ├── CiderClient.cs         # Envoltorio de la API local de Cider (V3 + LegacyV2, escaneo de puertos, análisis tolerante)
│   ├── CiderMediaProvider.cs  # Capa de sesión de Cider (ciclo de vida de la conexión)
│   ├── WindowTitleMediaProvider.cs # Respaldo: reconocimiento del título de la ventana
│   ├── MediaCoordinator.cs    # Despacho central: Cider > SMTC > título de ventana, caché de portadas, volumen adicional
│   ├── LrcParser.cs           # Análisis LRC (múltiples marcas de tiempo, offset, formatos de duración)
│   ├── LyricsService.cs       # Análisis de letras (.lrc local → Cider → en línea)
│   ├── OnlineLyricsService.cs # Letras en línea (interfaces no oficiales de NetEase/QQ Music, activadas por defecto con interruptor de un clic)
│   ├── ArtworkCache.cs        # Descarga/caché de portadas (portada remota de Cider → archivo local)
│   ├── SystemVolume.cs        # Volumen del sistema CoreAudio (COM P/Invoke)
│   ├── AppSettings.cs         # Lectura/escritura de configuración JSON (%APPDATA%\WinIsland\settings.json)
│   ├── SingleInstance.cs      # Mutex con nombre + pipe con nombre (el segundo inicio muestra la Isla)
│   ├── AutoStart.cs           # Clave de inicio automático HKCU Run
│   ├── GlobalHotkeyService.cs # Atajos globales (Win32 RegisterHotKey)
│   ├── NotificationService.cs # Banner de notificación de vidrio en la esquina superior derecha
│   ├── NotificationHistoryService.cs # Historial de notificaciones (últimas 50, persistencia JSON)
│   ├── BluetoothMonitor.cs    # Monitoreo de conexión/desconexión de dispositivos Bluetooth
│   ├── SystemNotificationMonitor.cs # Toma de control de notificaciones de Windows (espejo por automatización de UI)
│   ├── MediaAppRegistry.cs    # Registro de programas de medios (habilitar/deshabilitar/ordenar)
│   ├── AudioWaveService.cs    # Ondas de audio (muestreo del volumen del sistema, impulsa la vibración de las ondas)
│   ├── KeyboardIndicatorMonitor.cs # Indicadores de teclado (monitoreo del estado de CapsLock)
│   ├── ClipboardHistoryService.cs # Historial del portapapeles
│   ├── TodoService.cs         # Lista de tareas pendientes
│   ├── PomodoroService.cs     # Temporizador Pomodoro
│   ├── ScheduleService.cs     # Recordatorios de agenda
│   ├── IcsCalendar.cs       # Análisis de calendario .ics (eventos / VALARM)
│   ├── MeetingMonitor.cs    # Detección de ventanas de reunión (asistente de silencio en reuniones)
│   ├── PrivacyDeviceMonitor.cs # Estado de uso de micrófono/cámara (sondeo del registro de privacidad)
│   ├── RssMailService.cs    # Suscripciones RSS + correo (POP3)
│   ├── ScreenCaptureMonitor.cs # Detección de captura/grabación de pantalla
│   ├── IslandApiServer.cs   # API de la Isla (v1 + v3 HTTP / WebSocket)
│   ├── IslandPushModels.cs  # Modelos de tarjeta de la Isla (imagen/progreso dinámico/latido)
│   ├── DoNotDisturb.cs        # Modo No molestar (manual/por franjas)
│   ├── UpdaterService.cs      # Comprobación de actualizaciones de GitHub
│   ├── ProfileService.cs      # Perfiles de configuración (cambio entre varios conjuntos)
│   ├── WeatherService.cs      # Componente de clima (Open-Meteo, requiere conexión)
│   ├── PlaybackStateStore.cs  # Persistencia de la posición de reproducción (restaurar al salir/pausar)
│   ├── CiderTokenAutoDetect.cs # Detección automática del token API de Cider
│   └── AppLogger.cs           # Registro ligero en archivos
├── UI/
│   ├── IslandWindow.xaml(.cs) # Ventana de la Isla (animaciones, acrílico, posicionamiento, interacción al pasar el ratón)
│   ├── IslandViewModel.cs     # Modelo de vista principal (interpolación de progreso, índice de letras, visibilidad)
│   ├── SettingsWindow.xaml(.cs) / SettingsViewModel.cs
│   ├── LyricsWindow.xaml(.cs) # Ventana independiente de letras
│   ├── ThemeService.cs        # Tema claro/oscuro + pinceles del color del tema
│   ├── WindowEffects.cs       # Acrílico / modo oscuro / zona de esquinas redondeadas
│   ├── ScreenHelper.cs        # Varios monitores + conversión DPI PerMonitorV2
│   ├── TrayIcon.cs            # Icono y menú de la bandeja
│   ├── ClipboardPanelWindow.xaml(.cs) # Panel del historial del portapapeles
│   ├── QuickLauncherWindow.xaml(.cs)  # Lanzador rápido (Ctrl+Space)
│   └── Localization.cs        # Tabla de textos chino/inglés
└── Diagnostics/DiagnosticsCommand.cs  # Información de diagnóstico --diagnose
tests/WinIsland.Tests/         # Pruebas unitarias xunit (análisis LRC/configuración/Cider/título de ventana)
build/
├── publish.ps1                # Publicación con un clic (autónoma o dependiente del framework + zip)
├── WinIsland.iss              # Script de instalación Inno Setup
└── make-icon.ps1 / IconGen.cs # Herramienta de generación de iconos
```

**Flujo de datos**: `MediaCoordinator` consulta cada Provider una vez por segundo (asíncrono, sin bloquear la UI) → genera un `MediaSnapshot` unificado (con ruta de portada local y volumen) → lo publica en `IslandViewModel` a través del Dispatcher → el interpolador de 200 ms avanza suavemente la barra de progreso y el resaltado de letras → renderizado por enlace WPF.

---

## Inicio rápido

> 💡 **Versión precompilada**: el directorio `releases/` proporciona ejecutables autónomos de un solo archivo por versión (p. ej., `releases/1.1.5/win-x64/WinIsland-1.1.5-win-x64.exe`, incluye el runtime de .NET 8, doble clic para ejecutar). Las versiones beta se conservan solo localmente; solo las versiones estables se publican en GitHub (incluye versiones portátiles win-x64 / win-arm64 y el instalador universal).

### Requisitos del entorno
- Windows 10 1809+ / Windows 11
- Máquina de compilación: SDK de .NET 8 (o un SDK superior especificando `net8.0-windows10.0.19041.0`)

### Compilación
```powershell
# Restaurar + compilar + probar
dotnet build WinIsland.slnx -c Release
dotnet test  WinIsland.slnx -c Release

# Ejecutar (Debug)
dotnet run --project src\WinIsland -c Debug
```

### Publicación con un clic
```powershell
# Autónoma (incluye el runtime de .NET 8, sin instalación, ~73 MB en un solo archivo)
.\build\publish.ps1

# Dependiente del framework (tamaño pequeño, requiere .NET 8 Desktop Runtime)
.\build\publish.ps1 -FrameworkDependent
```
Los artefactos están en `publish\win-x64\` (incluye `WinIsland.exe`); el zip es `publish\WinIsland-win-x64.zip`.

### Instalador (opcional)
Después de instalar [Inno Setup 6](https://jrsoftware.org/isinfo.php):
```powershell
iscc.exe build\release-1.1.5.iss
```
Genera `releases\<version>\WinIsland-Setup-<version>.exe` (instalador universal, compatible con x64 y ARM64, instala automáticamente según la arquitectura). Al publicar una versión estable, copie `release-<version>.iss` en `build\` y actualice el número de versión.

---

## Uso

1. Inicie `WinIsland.exe` (o active el inicio automático / marque el inicio automático en el instalador). Aparece el icono en la bandeja.
2. Reproduzca cualquier música:
   - NetEase Cloud Music, QQ Music, Spotify, Apple Music oficial, etc. → se muestra automáticamente a través de la sesión de medios del sistema;
   - Cider → ver [Integración con Cider](#integración-con-cider);
   - Otros reproductores → respaldo de reconocimiento del título de la ventana (solo visualización).
3. **Haga clic** en la Isla para expandir la tarjeta completa (al pasar el ratón no se expande): arrastre del progreso (seek), control de reproducción, volumen y letras sincronizadas; haga clic de nuevo para contraer (tras salir de la tarjeta se contrae automáticamente a los 700 ms).
4. Menú de la bandeja: mostrar/ocultar, ventana de letras independiente, iniciar con Windows, **No molestar** (marcar silencia las notificaciones), **Buscar actualizaciones**, **Ver registros**, configuración y salir. **Cerrar la ventana principal no cierra el proceso** (solo se minimiza a la bandeja).
5. Atajos globales: `Ctrl+Alt+P` reproducir/pausar · `Ctrl+Alt+←/→` anterior/siguiente · `Ctrl+Alt+I` mostrar/ocultar · `Ctrl+Alt+Space` expandir/contraer · `Ctrl+Space` lanzador rápido (buscar aplicaciones / escribir una URL y pulsar Enter) · `Ctrl+Alt+V` panel del historial del portapapeles (todos se pueden desactivar / personalizar).
6. Las notificaciones y avisos (Bluetooth / notificaciones de Windows / en reproducción / batería baja) aparecen por defecto como banners de vidrio en la esquina superior derecha; se pueden activar/desactivar en Configuración → Notificaciones; con **No molestar** activado no se muestran banners (el contador de la insignia sigue contando).
7. Parámetros comunes de línea de comandos:
   ```powershell
   WinIsland.exe --demo       # Modo demostración (vista previa de la interfaz + letras de ejemplo sin medios)
   WinIsland.exe --diagnose   # Genera un informe de diagnóstico en %APPDATA%\WinIsland\diagnostics.txt
   WinIsland.exe --settings   # Abre la configuración al iniciar
   ```

---

## Referencia de configuración

Archivo de configuración: `%APPDATA%\WinIsland\settings.json` (JSON; los cambios de la interfaz de ajustes se aplican al instante; se puede exportar/importar).

| Clave | Predeterminado | Descripción |
| --- | --- | --- |
| `Language` | `zh-CN` | `zh-CN` / `en-US` |
| `ThemePreset` | `Default` | Tema: `Default/Ocean/Forest/Sunset/Neon/Mono/Grape/Sky/Rose/Amber/Lime/Teal/Lavender/Crimson/Midnight/Coffee/Sakura/Aurora/Custom` (anula AccentColor) |
| `FontFamily` | `Segoe UI` | Fuente de la interfaz |
| `FontScale` | `1.0` | Escala de fuente 0.8–1.4 |
| `CornerRadius` | `28` | Radio de las esquinas de la cápsula 16–40 |
| `BadgeEnabled` | `true` | Insignia de notificaciones sin leer (punto rojo + número en la esquina superior derecha) |
| `CoverTintBackground` | `true` | El fondo expandido toma el color de la portada del álbum |
| `WaveVisualizerEnabled` | `true` | Ondas de audio a la izquierda de los botones de control al reproducir medios |
| `WaveStyle` | `Bars` | Estilo de onda: `Bars` (barras) / `Spectrum` (espectro) / `Ring` (anillo) / `Particles` (partículas) |
| `WaveSyncEnabled` | `true` | Las ondas siguen el ritmo de la música (impulsadas por el sonido de salida del sistema) |
| `WaveSensitivity` | `1.0` | Sensibilidad de las ondas 0.2–3.0 |
| `WaveHeight` | `1.0` | Altura de las ondas 0.4–1.6 |
| `WallpaperThemeColorEnabled` | `false` | Color del fondo de pantalla: extrae el color principal del fondo actual como color del tema (puramente local) |
| `MarqueeTextEnabled` | `false` | Marquesina: desplazamiento horizontal automático cuando el título/letras son demasiado anchos |
| `EdgeSnapEnabled` | `true` | Al soltar el arrastre desbloqueado, se adhiere automáticamente al borde/centro de la pantalla |
| `FullScreenAutoHideEnabled` | `true` | Ocultar automáticamente la Isla en pantalla completa (video/juego/presentación) |
| `RecordingDndEnabled` | `false` | No molestar automático al grabar la pantalla (sin banners de notificación) |
| `Theme` | `Auto` | `Auto` / `Light` / `Dark` |
| `AccentColor` | `#6C5CE7` | Color del tema (#RRGGBB) |
| `Position` | `Center` | `Center` centrada arriba / `Right` derecha arriba |
| `Monitor` | `Primary` | `Primary` pantalla principal / `All` todas / `Index` pantalla especificada |
| `MonitorIndex` | `0` | Número de pantalla cuando `Monitor=Index` |
| `OffsetX` / `OffsetY` | `0` / `16` | Desplazamiento en píxeles |
| `Opacity` | `0.92` | 0.3–1.0 |
| `HideWhenNoMedia` | `true` | Oculta la Isla sin reproducción |
| `ShowWhenPaused` | `true` | Sigue mostrando al pausar |
| `StartWithWindows` | `false` | Iniciar con Windows |
| `StartHidden` | `false` | Ocultar al iniciar |
| `CompactShowArt/Title/Progress` | `true/true/false` | Contenido del modo compacto |
| `IslandAlwaysVisible` | `false` | Isla permanente (muestra los componentes incluso sin medios) |
| `ShowMediaInfo` | `true` | Muestra la información de reproducción (título/portada/letras, etc.) |
| `ReduceMotion` | `false` | Reduce los efectos dinámicos (desactiva las animaciones de resorte; accesibilidad/ahorro de energía) |
| `GlobalHotkeysEnabled` | `true` | Interruptor de atajos globales |
| `LowBatteryThreshold` | `20` | Umbral de aviso de batería baja (%), 0 para desactivar |
| `ExpandedShowArtTitle/Progress/Controls/Lyrics` | `true` | Interruptores de las secciones de la tarjeta expandida (portada+título / barra de progreso / controles y volumen / letras) |
| `Components` | objeto | Selección de componentes: `Time/Weather/Date/Cpu/Gpu/Ram/Net/Battery/Volume/CapsLock/Clipboard/Todo/Timer/Schedule/Holiday/Meeting/Mic/Cam`, cada uno con dos columnas `WhenIdle`/`WhenPlaying`; `Cover/Title/Artist/Lyrics/Progress` se muestran durante la reproducción; el diccionario `ComponentBadges` rellena el texto de la insignia de cada componente |
| `WidgetOrder` | `Time,Weather,...` | Orden de los componentes (claves separadas por comas, incluye `Song`) |
| `MediaApps` | `[]` | Habilitación/deshabilitación y prioridad de los programas de medios (vacío = todos habilitados) |
| `CompactWidth` / `CompactHeight` | `360` / `72` | Ancho / alto compactos (el ajuste manual por arrastre desactiva el ajuste automático) |
| `CompactWidthAuto` / `CompactHeightAuto` | `true` | El tamaño compacto se ajusta automáticamente al contenido (activado por defecto) |
| `ExpandedWidthAuto` / `MaxExpandedHeightAuto` | `true` | Ajuste automático del tamaño expandido (activado por defecto) |
| `ExpandedWidth` / `MaxExpandedHeight` | `400` / `384` | Ancho expandido / altura máxima expandida |
| `BluetoothNotifyEnabled` | `false` | Aviso de conexión/desconexión Bluetooth |
| `NotificationTakeoverEnabled` | `false` | Toma de control de las notificaciones de Windows (best effort) |
| `NotificationTimeoutSeconds` | `6` | Duración del banner de notificación (segundos) |
| `NotificationPosition` | `TopRight` | Posición de las notificaciones (esquina superior derecha) |
| `DoNotDisturbEnabled` / `DoNotDisturbManual` | `false` | No molestar: automático por franjas / manual |
| `DoNotDisturbStartHour` / `DoNotDisturbEndHour` | `22` / `8` | Franja de No molestar (horas) |
| `DnDAllowlist` | `[]` | Lista blanca de No molestar (`QQ.exe,WeChat.exe`; dentro de la lista blanca sí se muestran las notificaciones) |
| `Rules` | `[]` | Lista de reglas de automatización (condición + acción) |
| `ClipboardHistoryEnabled` / `ClipboardHistoryMax` | `false` / `15` | Interruptor del historial del portapapeles y número máximo de entradas |
| `PomodoroEnabled` / `PomodoroWorkMinutes` / `PomodoroBreakMinutes` | `false` / `25` / `5` | Interruptor del Pomodoro y duración de trabajo/descanso (minutos) |
| `KeyIndicatorSeconds` | `3` | Duración del indicador de teclado (CapsLock) (segundos) |
| `VolumeTempIndicatorEnabled` / `VolumeTempIndicatorSeconds` | `true` / `4` | Interruptor y duración del indicador temporal de volumen/silencio en la Isla |
| `FileCopyNotifyEnabled` | `true` | Copia/movimiento de archivos en la Isla (reconocimiento local del título de la ventana) |
| `DownloadProgressEnabled` | `false` | Descarga en curso en la Isla (escanea archivos temporales de la carpeta de descargas; desactivado por defecto) |
| `UsageMergeEnabled` / `UsageMergeItems` | `false` / `Mic,Cam,Meeting,Recording` | Cápsula combinada «En uso» y componentes participantes (desactivada por defecto) |
| `AutoUpdateCheck` | `false` | Comprobar automáticamente nuevas versiones de GitHub (desactivado por defecto, requiere conexión) |
| `DoubleClickAction` | `PlayPause` | Acción al doble clic: `PlayPause` / `ToggleExpand` / `ShowDesktop` / `ToggleVisible` / `NextTrack` / `PrevTrack` / `OpenSettings` / `None` |
| `AnimationStyle` | `Spring` | Skin de animación: `Spring` / `Soft` / `Elastic` / `Fade` |
| `ThemeTint` | `""` | Color de fondo personalizado #RRGGBB (se aplica cuando el preset es Custom) |
| `ExpandedCardStyle` | `Classic` | Plantilla de tarjeta expandida: `Classic` / `Hero` |
| `NetCurveEnabled` | `true` | El componente de red muestra la mini curva de los últimos 32 segundos |
| `LowPowerMode` | `false` | Modo de bajo consumo (reduce la frecuencia de fotogramas de las ondas y simplifica las animaciones en reposo) |
| `MeetingAssistantEnabled` | `false` | Asistente de silencio en reuniones: detecta ventanas de reunión + No molestar automático |
| `MeetingAutoDnd` / `MeetingKeywords` | `true` / `""` | No molestar automático en reuniones / palabras clave personalizadas de reuniones |
| `ScreenCaptureNotifyEnabled` / `ScreenshotNotifyEnabled` / `RecordingNotifyEnabled` | `false` / `true` / `true` | Interruptor general y subelementos de los avisos de captura/grabación |
| `CalendarEnabled` / `CalendarIcsPath` / `CalendarAdvanceMinutes` | `false` / `""` / `10` | Interruptor de recordatorios de calendario .ics / ruta del archivo / minutos de antelación |
| `RssNotifyEnabled` / `RssUrls` / `RssIntervalMinutes` | `false` / `""` / `15` | Recordatorios RSS / direcciones de suscripción / intervalo de consulta (minutos) |
| `MailNotifyEnabled` / `MailPop3Server` / `MailPop3Port` / `MailUseSsl` / `MailUser` / `MailPassword` / `MailCheckMinutes` | `false` / `""` / `995` / `true` / `""` / `""` / `5` | Recordatorios de correo (POP3): interruptor, servidor, puerto, SSL, cuenta, código de autorización e intervalo de comprobación |
| `QuickLauncherEnabled` / `HotkeyLauncher` | `true` / `Ctrl+Space` | Interruptor del lanzador rápido y atajo |
| `ClipboardPanelEnabled` / `HotkeyClipboardPanel` | `true` / `Ctrl+Alt+V` | Interruptor del panel del historial del portapapeles y atajo |
| `HotkeyExpand` | `Ctrl+Alt+Space` | Atajo de expandir/contraer |
| `NotifyFoldEnabled` | `true` | Plegar notificaciones similares (misma fuente y mismo título solo muestra una) |
| `ActiveProfile` | `Default` | Nombre del perfil de configuración (cambio entre varios conjuntos) |

---

## Integración con Cider

Cider (cliente de terceros de Apple Music) proporciona una API HTTP local. WinIsland ya encapsula un módulo independiente (`CiderClient.cs`) que se adapta automáticamente a las diferencias de versión.

**Pasos para activar (importante)**:
1. Abra Cider: **Configuración → Conectividad → Permitir control externo (Manage External Application Access)**; al activarlo, Cider mostrará el token de API (si está vacío, haga clic para generarlo).
2. Copie el token en **Configuración de WinIsland → Cider → API Token** y guarde.
3. El puerto predeterminado es `10767` y WinIsland lo detecta automáticamente; el RPC antiguo es `10769`.

> ⚠️ Las nuevas versiones de Cider 2.x requieren **token en todas las solicitudes de API por defecto** (sin token devuelve `403 UNAUTHORIZED_APP_TOKEN`). Si los registros de diagnóstico indican que se necesita el token, rellénelo siguiendo los pasos anteriores; de lo contrario, las letras/controles de Cider no estarán disponibles (las pistas aún se pueden mostrar a través de SMTC).

> ⚠️ Si los registros muestran repetidamente HttpClient.Timeout (originalmente 2 s), suele deberse a que el software de seguridad/proxy local intercepta el HTTP de bucle local (la respuesta real de Cider es de unos 30 ms). Desde 1.0.1 el tiempo de espera de lectura de datos se amplió a 5 s; si sigue agotando el tiempo, compruebe si el antivirus bloquea la conexión de red de WinIsland.

**Capacidades de la API implementadas** (según la documentación de la comunidad de Cider / el crate `cider-api` verificado, versión 2026):
- `GET /api/v1/playback/active`, `GET /now-playing` (pista/portada/progreso/estado)
- `POST /api/v1/playback/play|pause|playpause|next|previous|seek`
- `GET|POST /api/v1/playback/volume`
- `GET /api/v1/lyrics` (incluye respaldo `?id=`)
- Encabezado de autenticación: `apptoken` (compatible con `apitoken`)
- Antiguo 10769: `/active`, `/currentPlayingSong`, `/playPause`, `/next`, `/previous`, `/seekto/{t}`, `/audio`

> ⚠️ La API de Cider no es oficial y cambia rápidamente; todas las solicitudes tienen un tiempo de espera de 2 segundos y, si fallan, se degradan automáticamente a SMTC / título de ventana, **sin afectar al flujo principal**. Mantenga WinIsland actualizado para adaptarse a las nuevas versiones.

---

## Letras

Prioridad:
1. **.lrc local**: se busca como `Canción.lrc` / `Artista - Canción.lrc` en los directorios de letras (por defecto `%APPDATA%\WinIsland\Lyrics`, `Música\Lyrics` y la raíz de `Música`);
2. **Letras karaoke carácter por carácter de AMLL** (línea de tiempo TTML carácter por carácter de la biblioteca de canciones de amll.dev, activadas por defecto);
3. **Interfaz de letras de Cider** (cuando la fuente es Cider);
4. **Letras en línea** (interfaces no oficiales de NetEase / QQ Music): **activadas por defecto**; con el clic derecho en la Isla se pueden activar/desactivar con un clic, o desactivar en la configuración.

> ⚠️ Las letras en línea usan interfaces no oficiales y son solo para aprendizaje personal; respete los derechos de autor; si el titular de los derechos lo exige, puede desactivar esta función en cualquier momento (sin conexión por completo tras desactivarla).

---

## Notificaciones y alertas (desde 1.0.2, mejorado en 1.0.3)

Todas las notificaciones son **banners de vidrio en la esquina superior derecha**, con animación de entrada de estilo macOS (entra desde la derecha + fundido) y de salida; la duración de visualización es configurable (3–15 segundos).

- **Aviso de conexión Bluetooth**: Configuración → Notificaciones; al activarlo, aparece cuando un dispositivo Bluetooth se conecta/desconecta.
- **Toma de control de notificaciones de Windows**: Configuración → Notificaciones; al activarlo, refleja mediante automatización de UI (best effort) el contenido del centro de notificaciones (como las notificaciones de QQ) en los banners de la esquina superior derecha.
  > ⚠️ Windows no proporciona una API pública para «interceptar notificaciones de otras aplicaciones»; esta función es best effort y algunas notificaciones pueden no capturarse; no afecta al flujo principal.
- **Notificación en reproducción**: al cambiar de canción aparece automáticamente el banner «En reproducción - Título» (desde 1.0.3).
- **Aviso de batería baja**: aparece cuando la batería baja del umbral (predeterminado 20%, ajustable 0–50), una vez por ciclo de carga (desde 1.0.3).
- **Historial de notificaciones**: registros de las últimas 50 notificaciones; la página Configuración → Notificaciones permite verlas / vaciarlas (desde 1.0.3).
- **Ajuste del tamaño de la Isla**: Configuración → Apariencia permite ajustar el largo/ancho compactos y el largo expandido.

---
## API de la Isla (enviar a la Isla Dinámica desde otras aplicaciones)

WinIsland incluye un servicio HTTP local; otras aplicaciones pueden enviar información a la Isla Dinámica en tiempo real (similar a la integración de apps de terceros en la Isla Dinámica de iOS). **Documentación para desarrolladores en [docs/IslandAPI.md](docs/IslandAPI.md)**.

| Interfaz | Descripción |
|---|---|
| `POST /v1/island/push` | Envía / actualiza una tarjeta de la Isla (desde v3 admite imágenes / progreso dinámico / latido) |
| `PATCH /v3/island/push/{id}` | Actualización parcial: solo sobrescribe los campos presentes en el cuerpo (conserva la caducidad / posición en la cola) |
| `DELETE /v1/island/push/{id}` | Elimina una tarjeta |
| `GET /v1/island/active` (o `/v3/island/active`) | Consulta la tarjeta activa actual |
| `GET /v3/ws` | Canal bidireccional WebSocket: el cliente envía `push/update/remove/ping`, el servidor difunde los eventos `push_updated/push_removed` |
| `GET /v1/health` | Comprobación de salud |

- Configuración → API de la Isla: interruptor de activación, puerto (predeterminado 9840), token opcional y duración de visualización predeterminada global
- Las notificaciones de la Isla **no cambian el largo/ancho de la Isla**; la tarjeta se muestra en una sola línea en estado compacto y no oculta otros componentes
- Los botones admiten «Abrir enlace / Iniciar programa»; el remitente puede personalizar la duración de visualización por entrada (anula el valor predeterminado global)
- Nuevo en v3: `image` (imagen data URI o http), `progress_from/progress_to/progress_duration_seconds` (progreso automático), `heartbeat_seconds` (renovación por latido; si no se renueva en más de 2 veces el intervalo, se elimina automáticamente), `theme` (tema de la tarjeta dark/light/auto), `action: "command"` (el botón ejecuta un comando local); documentación completa en [docs/IslandAPI.md](docs/IslandAPI.md)

---
## Restauración del estado de reproducción

- Al salir de la aplicación, pausar o cambiar de canción, se guarda «pista + posición de reproducción» en `%APPDATA%\WinIsland\state.json` (solo local).
- En el próximo inicio, si es la misma pista y el reproductor aún no devuelve el progreso real, se restaura primero la última posición para evitar el salto de «mostrar la línea 0 y luego saltar a la frase de pausa»; no se restaura tras más de 1 hora o si cambió la pista.

---
## Privacidad y seguridad

- **Sin telemetría, sin anuncios, sin envíos**. Aparte de las «letras en línea» activadas manualmente por el usuario, la aplicación no realiza ninguna solicitud de red.
- **Componente de clima**: solo cuando activa «Mostrar clima» y escribe una ciudad solicita a Open-Meteo (gratuito, sin clave, sin cuenta) el clima actual; si no está activado, funciona totalmente sin conexión.
- Únicos escenarios de conexión: descarga de portadas de Cider (`mzstatic.com`, URL pública de portada devuelta por la API local) y letras en línea activadas por el usuario.
- Todos los datos se almacenan localmente en `%APPDATA%\WinIsland\`.
- Los registros solo guardan información de ejecución local (`logs\app-*.log`).

---

## Métricas no funcionales

Medido en la máquina de prueba (Windows 11 24H2, 2560×1440 al 100 %) (Release autónomo):

| Métrica | Medido | Objetivo |
| --- | --- | --- |
| CPU en reposo (sin medios) | < 0.5 % (0.3 % medido en Debug) | ≈ 0% |
| Memoria residente (Private) | ~72 MB | ≤ 150 MB |
| Inicio | < 1 s (en frío) | ≤ 2 s |
| Cerrar ventana principal | No sale, solo minimiza a la bandeja | ✅ |
| Múltiples instancias | Solo una; el segundo inicio muestra la Isla | ✅ |
| Excepciones | Captura unificada y registro en archivo, sin cuadro de bloqueo | ✅ |

> Nota: el WorkingSet de la implementación autónoma (incluye páginas compartidas del runtime de .NET) es de unos 160 MB, pero **la memoria Private es de unos 72 MB**; con una implementación dependiente del framework, el WorkingSet será menor.

---

## Limitaciones conocidas

- **El karaoke carácter por carácter depende de la fuente de letras y del progreso**: con una línea de tiempo TTML/LRC carácter por carácter de AMLL se resalta carácter por carácter; sin una línea de tiempo carácter por carácter o si el reproductor no proporciona el progreso real, se degrada a resaltar la frase completa (avance con el reloj local).
- **Progreso que retrocede ocasionalmente** (p. ej., Cider/SMTC reporta 0 o una posición caducada en un instante): se ha implementado una protección de posición: el retroceso instantáneo se ignora y se mantiene el avance actual, sin devolver las letras/barra de progreso al inicio; solo tras un retroceso sostenido de unos 4 segundos se considera una reposición real o un seek del reproductor.
- **Aviso de llamada entrante**: no implementado (opcional en P2). Implementados: aviso de Bluetooth, toma de control de notificaciones de Windows (best effort), notificación en reproducción, aviso de batería baja y recordatorios de agenda.
- **Cobertura de SMTC**: depende de que el reproductor registre la sesión de medios global; algunos reproductores antiguos que no la registran solo se pueden cubrir con el título de la ventana (sin botones de control).
- **Cider 1.x (API antigua, puerto 9000)**: no adaptado; solo se admite 2.x y superiores.

---

## Guía de verificación (requiere probar con un reproductor real)

Los siguientes escenarios requieren verificación en un entorno real (se indica lo que ya se verificó automáticamente en el entorno de desarrollo de este repositorio):

| Escenario | Estado |
| --- | --- |
| Enumeración de sesiones SMTC (con `--diagnose` se ve la lista de sesiones) | ✅ Probado (detecta sesiones reales como Bilibili) |
| Mostrar/ocultar automático de la Isla, expandir/contraer con clic, interpolación de progreso | ✅ Probado (demo + sesión real pausada) |
| Reproducir/pausar/cambiar/seek (reproductor real) | ⚠️ Requiere pruebas (las rutas de código corresponden directamente a la API de control de SMTC) |
| Conexión y control de la API de Cider | ⚠️ Requiere instalar Cider en la máquina y activar el control externo |
| Desplazamiento sincronizado de letras .lrc locales | ✅ El análisis LRC está probado por unidades; el extremo a extremo requiere una canción real |
| Letras en línea | ✅ Integrado con NetEase/QQ Music; el efecto extremo a extremo requiere una canción real |

**Pasos de verificación sugeridos**:
1. `WinIsland.exe --diagnose` → confirme que `System media sessions` lista el reproductor;
2. Reproduzca cualquier canción de NetEase/QQ Music/Spotify → la Isla debe mostrar la pista y permitir el control;
3. Abra Cider y active el control externo → la fuente de la Isla debe mostrar `Cider`, con seek/volumen;
4. Coloque un `.lrc` con el mismo nombre en la carpeta de la canción → al expandir, las letras deben desplazarse y resaltarse con el progreso.

---

## Preguntas frecuentes

**P: ¿No aparece la Isla Dinámica?**
- Confirme que hay reproducción (al pausar sigue mostrándose por defecto); `HideWhenNoMedia` está activado por defecto; ocultarse sin medios es normal.
- Ejecute `--diagnose` para ver la lista de sesiones; si la lista está vacía, el reproductor no registró SMTC.

**P: Cider muestra «No conectado»?**
- Confirme que «Permitir control externo» está activado en la configuración de Cider; revise el puerto (predeterminado 10767); confirme que Cider está habilitado en la configuración de WinIsland.

**P: ¿No se cargan las letras en línea?**
- Las letras en línea están activadas por defecto (clic derecho en la Isla → Letras en línea para alternar con un clic); si aún no hay letras, confirme en Configuración → Letras que están activadas y compruebe la conectividad de red.

**P: ¿El icono de la bandeja sigue ahí tras salir?**
- Menú de la bandeja → Salir; cerrar directamente la ventana de la Isla solo la oculta (según el diseño de «residencia en la bandeja»).

---

## Licencia de código abierto

- Aplicación: MIT (ver [LICENSE](LICENSE))
- Componentes de terceros: ver [THIRD_PARTY.md](THIRD_PARTY.md)

---

## Français

## WinIsland – Île dynamique pour Windows

> Apportez l'Île dynamique d'iOS sur Windows : fenêtre flottante du bureau avec contrôle des médias, paroles synchronisées, composants personnalisables, centre de notifications et résidence permanente dans la barre d'état système.
> Basé sur **.NET 8 + WPF**, compatible avec Windows 11 (également Windows 10, 1809+).

---

> **Apportez l'Île dynamique d'iOS sur Windows | Une Île dynamique moderne et polyvalente.**

Apportez l'Île dynamique d'iOS sur Windows 11 / 10 : contrôle de la lecture des médias, paroles karaoké caractère par caractère, composants personnalisables, centre de notifications et API de l'île, le tout dans une seule capsule. Basé sur **.NET 8 + WPF**, gratuit et open source (MIT), **sans publicité · sans télémétrie**.

🌐 **Site web : https://WinIsland.JudeKwong.com**

---

## ✨Points forts

- **▶️ Contrôle de la lecture des médias** : se connecte nativement aux sessions de médias globales de Windows (SMTC), compatible avec Musique NetEase, QQ Music, Spotify, Apple Music, Groove, Films et TV, etc. ; prise en charge spécifique de l'API locale de Cider ; en cas d'échec de connexion, le titre de la fenêtre est utilisé comme solution de repli. Pochette d'album, glissement de la progression (seek), lecture/pause/changement de piste, tout est inclus ; lorsque plusieurs lecteurs sont ouverts, la source de contrôle peut être changée en un clic ; un clic sur la pochette ouvre un aperçu immersif en plein écran.
- **♪ Paroles karaoké caractère par caractère** : la carte étendue défile et met en surbrillance en synchronisation, illuminant les caractères un à un à la manière d'un karaoké ; trois niveaux de sources de paroles : `.lrc` local → interface de paroles du lecteur → paroles en ligne facultatives ; paroles bilingues, interrupteur de traduction, copie de la ligne courante en un clic ; le minutage des paroles peut être ajusté finement par piste, et la fenêtre de paroles indépendante permet de régler l'opacité et le verrouillage.
- **🧩 Système de composants personnalisables** : heure, météo, date (avec calendrier lunaire / termes solaires), CPU/GPU/mémoire/disque, vitesse du réseau, batterie, méthode de saisie, interrupteurs rapides (WiFi/Bluetooth/mode nuit/silence) et plus de 30 composants ; chaque composant peut avoir une icône personnalisée, une sélection par cases à cocher et un ordre par glisser-déposer, avec les modes une ligne ou plusieurs lignes à tout moment.
- **🔗 API de l'île** : interface locale HTTP / WebSocket permettant à tout logiciel tiers d'envoyer des informations à l'Île dynamique en temps réel (semblable à l'intégration d'applications tierces dans l'Île dynamique d'iOS). La v3 prend en charge les images, la progression dynamique, le renouvellement par pulsation (heartbeat) et les thèmes clair/sombre de la carte ; les notifications ne modifient pas la largeur/hauteur de l'Île ni ne masquent les autres composants ; les boutons prennent en charge l'ouverture de liens / le lancement de programmes / l'exécution de commandes locales, et le clic sur le bouton notify peut rappeler l'expéditeur via WebSocket.
- **🔂 Centre de notifications** : bannières en verre dans le coin supérieur droit, avec animations de glissement de style macOS : appareils Bluetooth, alertes d'appels vocaux/vidéo WeChat/QQ, prise en charge des notifications système, en cours de lecture, batterie faible/chargée, hors ligne/rétabli ; historique des notifications, pliage, liste blanche Ne pas déranger et automatisation par règles ; les bannières peuvent inclure des boutons d'action (tels que «Déconnecter» et «Paramètres» pour Bluetooth).
- **✨ Apparence et effets** : 18 thèmes, couleur d'accent et fond personnalisables, verre liquide givré, **couleur extraite du fond d'écran** (extrait automatiquement la couleur du thème depuis le fond d'écran actuel), **texte défilant** (les paroles/pistes longues défilent horizontalement), 4 styles de skin d'animation (ressort iOS, etc.), **4 styles d'ondes audio** (barres / spectre / anneau / particules, qui vibrent au rythme de la musique) ; animations de développement/repli avec easing non linéaire à 60 fps ; le fond extrait de la pochette peut «respirer» lentement (thème dynamique) ; haute DPI PerMonitorV2, sans décalage à 120/150/200 %.
- **🧠 Interaction et intelligence** : déverrouillage et glissement + **ajustement au bord** (s'accroche au bord/centre au relâchement), **masquage automatique en plein écran** (se replie lors de la lecture vidéo/jeu/présentation en plein écran), actions personnalisées au double-clic, **boutons d'action rapide** (verrouiller l'écran / couper le son / lecture-pause / capture / afficher le bureau, etc., avec ordre personnalisable), **glisser des fichiers vers l'Île**, **Ne pas déranger intelligent pendant l'enregistrement d'écran** ; réponse immédiate à l'appui de la souris (le clic a la priorité pour développer/replier).
- **⚙️ Outils de productivité et automatisation** : Pomodoro, tâches, historique du presse-papiers, lanceur rapide, rappels d'agenda ; assistant de silence en réunion, alertes d'enregistrement/capture d'écran, progression de copie/téléchargement de fichiers sur l'Île ; raccourcis globaux et moteur de règles (affichage/masquage automatique selon les conditions).
- **🔒 Confidentialité et sécurité** : sans télémétrie, sans publicité, sans envoi de données. Entièrement hors ligne, sauf les paroles en ligne et la météo activées manuellement par l'utilisateur ; toutes les configurations et données sont enregistrées uniquement localement dans `%APPDATA%\WinIsland`.

---

## 📥Téléchargement (dernière version stable 1.1.5)

| Plateforme | Téléchargement | Description |
| --- | --- | --- |
| Windows x64 | [Version portable x64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-x64.exe) | L'option principale pour PC 64 bits ; fichier unique, sans installation, s'exécute directement |
| Windows ARM64 | [Version portable ARM64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-arm64.exe) | Appareils ARM comme Surface Pro X / Snapdragon ; fichier unique, sans installation |
| Windows universel | [Installateur universel](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-Setup-1.1.5.exe) | Assistant d'installation Inno Setup ; installe automatiquement selon l'architecture x64 / ARM64 |

Toutes les versions précédentes et le journal des modifications complet se trouvent dans [GitHub Releases](https://github.com/DMP-Pig/WinIsland/releases).

---

## 📊Indicateurs de performance

| Indicateur | Valeur |
| --- | --- |
| Mémoire résidente (Private) | ~72 Mo |
| Démarrage à froid | < 1 s |
| CPU au repos | ≤ 0 % |
| Images d'animation | 60 fps fluides |
| Instances multiples | Instance unique, évite l'exécution dupliquée |
| Télémétrie | 0 télémétrie · aucun envoi · aucune publicité |

---

## Table des matières

- [Fonctionnalités](#fonctionnalités)
- [Choix technologique et justification](#choix-technologique-et-justification)
- [Vue d'ensemble de l'architecture](#vue-densemble-de-larchitecture)
- [Démarrage rapide](#démarrage-rapide)
- [Compilation et empaquetage](#démarrage-rapide)
- [Utilisation](#utilisation)
- [Référence de configuration](#référence-de-configuration)
- [Intégration avec Cider](#intégration-avec-cider)
- [Paroles](#paroles)
- [Confidentialité et sécurité](#confidentialité-et-sécurité)
- [Indicateurs non fonctionnels](#indicateurs-non-fonctionnels)
- [Limitations connues](#limitations-connues)
- [Guide de vérification (nécessite un lecteur réel)](#guide-de-vérification-nécessite-un-lecteur-réel)
- [Questions fréquentes](#questions-fréquentes)
- [Licence open source](#licence-open-source)

---

## Fonctionnalités

### P0 (implémentées)
- **UI flottante de l'Île dynamique (style iOS)** : centrée en haut par défaut (configurable à droite) ; capsule aux coins arrondis ; suit le thème clair/sombre du système ou la couleur de thème manuelle ; **animation de transformation** entre la capsule compacte → la carte complète (fenêtre fixe + mise à l'échelle/apparition d'un seul élément, pilotée par le thread de composition WPF à 60 fps, avec rebond élastique de style iOS) ; **clic pour développer/replier** (le survol ne développe pas), repli automatique à la sortie (tampon anti-touche accidentelle de 700 ms) ; les clics en dehors de la carte traversent la fenêtre.
- **Verrouillage et glissement** : verrouillée par défaut (impossible à déplacer) ; le menu contextuel permet de **déverrouiller** (glisser l'Île à la souris une fois déverrouillée), **centrer** (vertical identique, centrage horizontal) et de **reverrouiller**. Après re-verrouillage suite à un glissement, la **position est conservée** (ne revient pas à la valeur par défaut) ; au relâchement du glissement il y a **ajustement au bord** (s'accroche au bord/centre de l'écran, configurable dans Paramètres → Général).
- **Conception compacte** : titre/artiste/paroles alignés à gauche (collés à la pochette) et centrés verticalement.
- **Pochette d'album** : la capsule et la carte étendue affichent la pochette (64 px agrandi au développement ; icône de placeholder si pas de pochette) ; les vignettes SMTC et les pochettes Cider sont mises en cache automatiquement.
- **Contrôle de la lecture des médias** : affiche titre, artiste et album ; barre de progression glissable (seek) ; lecture/pause, précédent, suivant ; contrôle du volume si nécessaire (Cider utilise son API, les autres sources contrôlent le volume du système ; désactivable) ; le composant médias affiche le badge de la source actuelle (Spotify / Cider / Musique NetEase / QQ Music, etc.).
- **Mini-lecteur** : fenêtre flottante indépendante (configurable dans Paramètres → Médias), affiche la pochette / le titre / l'artiste / la barre de progression et les contrôles de lecture ; peut être glissée librement et mémorise sa position ; affichage/masquage automatique avec la lecture.
- **Changement de périphérique de sortie audio** : Paramètres → Médias permet d'énumérer et de changer le périphérique de sortie par défaut du système (redémarrage du lecteur recommandé après le changement).
- **Prise en charge de sources multiples** :
  1. Sessions de médias globales de Windows (`Windows.Media.Control` / SMTC) : Musique NetEase, QQ Music, Spotify, Apple Music officiel, Groove, Films et TV, etc. ;
  2. **Cider** : API HTTP locale (port 10767, compatible avec l'ancien RPC 10769, balayage automatique des ports + configuration manuelle, prise en charge de l'authentification `apptoken`) ;
  3. Repli : titre de la fenêtre + identification du processus (affichage des informations uniquement, sans capacité de contrôle).
- **Affichage des paroles (mode karaoké caractère par caractère)** : au développement, la zone des paroles s'affiche en mode karaoké : **les caractères de la ligne actuelle s'illuminent un à un** ; la progression de la surbrillance est une valeur continue et les caractères à la limite passent en douceur de la couleur de base à la couleur de surbrillance avec easing à 60 fps, coulant de gauche à droite selon l'ordre de lecture (également correct avec des paroles à sauts de ligne, sans illuminer plusieurs lignes à la fois) ; chaque ligne commence à 0 (le premier caractère ne s'illumine pas au début) ; à la pause, la surbrillance se fige au moment de la pause : quand Cider n'a pas d'état explicite, lecture/pause est déterminé selon si «la position bouge» (ne confond plus avec la lecture uniquement à cause de remainingTime>0), SMTC privilégie le suivi de la session Cider (évite que d'autres sessions actives comme Bilibili ne la capturent) ; à la sortie et au redémarrage, la dernière position de pause est restaurée automatiquement (ne revient pas au début) ; la ligne actuelle ne surligne que le texte (sans capsule de fond, pour éviter une double surbrillance ; taille grande de 20 px), les autres lignes s'assombrissent, **défilement doux avec centrage automatique** (se rapproche de la ligne actuelle image par image à 60 fps et la suit au développement) ; en état compact, la ligne actuelle s'affiche en temps réel alignée à gauche et s'illumine également caractère par caractère ; fenêtre de paroles flottante indépendante facultative.
  - **Synchronisation de la progression** : lit automatiquement le jeton de l'API locale de Cider (sans configuration) pour obtenir la progression réelle de lecture et synchroniser précisément le karaoké avec la chanson ; les lecteurs sans progression disponible utilisent l'horloge locale.
  - **Sources de paroles** : `.lrc` local (`%APPDATA%\WinIsland\Lyrics` ou dossier de musique) → paroles karaoké caractère par caractère AMLL → interface de paroles de Cider → paroles en ligne (interrupteur en un clic avec clic droit sur l'Île). Sans paroles, affiche «Sans paroles», sans erreur.
  - **Paroles bilingues** : combine automatiquement les lignes de traduction avec les horodatages adjacents ; désactivable dans les paramètres (aucun fichier de paroles supplémentaire nécessaire) ; interrupteur d'affichage/masquage de la traduction et «Copier la ligne actuelle» pour copier en un clic.
- **Barre d'état système** : icône permanente, menu contextuel (afficher/masquer, fenêtre de paroles indépendante, démarrer avec Windows, paramètres, quitter), double-clic pour basculer la visibilité.### P1 (implémentées)
- **Système de composants (contenu personnalisable de l'Île)** : Paramètres → Composants permet de cocher les composants à afficher «sans chanson / avec chanson» et d'ajuster l'ordre par glisser-déposer :
  - Heure, météo (Open-Meteo, nécessite une ville et une connexion), date (avec calendrier lunaire et termes solaires), utilisation du CPU, utilisation du GPU, utilisation de la mémoire, vitesse du réseau (peut afficher la mini-courbe des 32 dernières secondes), batterie, espace disque libre, état de la méthode de saisie (chinois / anglais + nom de l'IME), interrupteurs rapides (WiFi / Bluetooth / mode nuit / silence en un clic), volume, indicateur de clavier (CapsLock), presse-papiers, tâches, Pomodoro, agenda, compte à rebours des jours fériés, en réunion, microphone, caméra ;
  - Informations sur la chanson (pochette/titre/artiste/paroles/barre de progression, uniquement pendant la lecture, toujours présent dans la barre d'ordre).
  - La barre d'ordre n'affiche que les composants cochés ; la liste et la barre prennent en charge la molette de la souris et les barres de défilement ; chaque composant peut avoir une icône personnalisée (icônes MDL2 ou emoji, Paramètres → Composants).
  - Composants temporaires sur l'Île : changement de volume, capture / enregistrement d'écran, copie / déplacement de fichiers, téléchargement en cours (les deux derniers désactivés par défaut) : lorsque l'événement se produit, le composant correspondant s'affiche temporairement même si l'Île est masquée.
  - **Capsule combinée «En cours d'utilisation»** (Paramètres → Composants, désactivée par défaut) : une fois activée, «Microphone / Caméra / En réunion / Enregistrement» sélectionnés se combinent en une seule capsule d'état «En cours d'utilisation · N» et les éléments combinés ne s'affichent plus séparément.
  - **Mode une ligne** (Paramètres → Apparence, activé par défaut) : tous les composants sur une seule ligne en état compact ; sans développement, affiche également les informations de la chanson et la ligne de paroles actuelle (surbrillance karaoké caractère par caractère), tronquant automatiquement les paroles longues ; la barre de progression et la liste complète des paroles s'affichent dans la carte étendue.
- **Personnalisation du contenu de la carte étendue** : pochette + titre, barre de progression, boutons de contrôle et volume, et zone des paroles peuvent être activés/désactivés séparément.
- **Personnalisation de l'apparence (page de réglages de style Réglages Système macOS)** : navigation à gauche + contenu à droite, verre liquide arrondi ; **18 thèmes prédéfinis** (défaut / océan / forêt / coucher de soleil / néon / monochrome / raisin / ciel / rose / ambre / citron vert / sarcelle / lavande / cramoisi / minuit / café / sakura / aurore, plus personnalisé) ; **couleur extraite du fond d'écran** (extrait automatiquement la couleur principale du fond d'écran actuel comme couleur du thème, purement local) ; **texte défilant** (les paroles longues défilent automatiquement) ; **4 styles d'ondes audio** (barres / spectre / anneau / particules) et **4 skins d'animation** (ressort iOS / doux / élastique / fondu) ; **mode basse consommation** (réduit la fréquence d'images des ondes et simplifie les animations au repos) ; haute DPI PerMonitorV2.
- **Raccourcis globaux** : `Ctrl+Alt+P` lecture/pause · `Ctrl+Alt+←/→` précédent/suivant · `Ctrl+Alt+I` afficher/masquer · `Ctrl+Alt+Space` développer/replier · `Ctrl+Space` lanceur rapide · `Ctrl+Alt+V` panneau de l'historique du presse-papiers.
- **Réduire les effets dynamiques** (accessibilité / économie d'énergie) : désactive les animations à ressort en un clic, changement instantané.
- **Ajustement de la taille de l'Île** : Paramètres → Apparence, permet d'ajuster la largeur/hauteur compactes et la largeur étendue.
- **Île dynamique permanente** : toujours visible même sans lecture (affiche les composants configurés).
- **Plusieurs moniteurs** : écran principal / tous les écrans / numéro d'écran spécifié.
- **Haute DPI** : PerMonitorV2, sans décalage à 120/150/200 %.
- **Configuration personnalisée** : position, décalage, opacité, couleur du thème, contenu du mode compact, masquer sans médias, etc. ; les modifications s'appliquent instantanément.
- **Masquage automatique de l'Île sans lecture** (désactivable).
- **Ne pas déranger** : activation manuelle en un clic ou silence automatique par plage horaire (bascule en un clic dans le menu de la barre d'état système ; les plages se configurent dans les réglages).
- **Rechercher les mises à jour** : vérification manuelle des nouvelles versions de GitHub dans le menu de la barre d'état système / les réglages ; vérification automatique facultative (désactivée par défaut, nécessite une connexion).
- **Action rapide au double-clic** (Paramètres → Général) : peut être «Lecture / Pause» (défaut), «Développer / Replier», «Afficher le bureau», «Masquer / Afficher l'Île», «Précédent», «Suivant», «Ouvrir les paramètres» ou «Aucune action».
- **Assistant de silence en réunion (détection de réunion)** : identifie les fenêtres de réunion comme Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet, active automatiquement Ne pas déranger pendant la réunion et affiche le composant «En réunion» (heuristique purement locale, hors ligne).
- **Alertes d'enregistrement / capture d'écran** : à l'appui de `PrintScreen` / `Alt+PrintScreen`, une alerte s'affiche ; lors de la détection de logiciels d'enregistrement comme OBS, Bandicam, Fraps, Camtasia, XSplit, Streamlabs, Xbox Game Bar, l'alerte «Enregistrement de l'écran» s'affiche (détection locale des processus, hors ligne).
- **Ne pas déranger intelligent (enregistrement)** : lors de la détection d'un enregistrement d'écran en cours, les notifications sont automatiquement silencées (sans bannière) ; à la fin, restauration automatique ; configurable dans Paramètres → Notifications.
- **Masquage automatique en plein écran** : lors de la détection de vidéo / jeu / présentation en plein écran (comme PowerPoint), l'Île se masque/se replie automatiquement et se restaure à la sortie du plein écran ; configurable dans Paramètres → Général.
- **Glisser des fichiers vers l'Île** : glisser des fichiers/dossiers vers l'Île permet «Copier le chemin / Ouvrir le dossier contenant / Épingler sur l'Île», etc. (choix par clic droit sur l'Île ou dans le menu de glisser-déposer).
- **Rappels d'événements de calendrier (.ics)** : analyse les fichiers iCalendar locaux (Outlook / Google Calendar / exportés du téléphone) ; à l'heure de l'événement (avec N minutes d'avance configurables), une bannière s'affiche ; analyse purement locale, hors ligne.
- **Rappels d'abonnements RSS** : interroge périodiquement les flux RSS 2.0 / Atom (intervalle réglable) ; à l'apparition d'une nouvelle entrée, une bannière s'affiche ; se connecte uniquement aux adresses d'abonnement configurées.
- **Rappels de courrier (POP3)** : récupère périodiquement les en-têtes des messages ; avec un nouveau courrier, une bannière s'affiche (lecture des en-têtes uniquement, ne télécharge pas le corps ni ne téléverse des données ; un code d'autorisation est recommandé).
- **Lanceur rapide (style Spotlight)** : s'ouvre avec `Ctrl+Space`, recherche les applications installées / les programmes du menu Démarrer ou ouvre directement une URL ; le raccourci est personnalisable.
- **Panneau de l'historique du presse-papiers** : `Ctrl+Alt+V` ouvre une fenêtre indépendante de l'historique ; un clic sur un élément le recopie dans le presse-papiers ; vidable ; le raccourci est personnalisable.
- **Règles (automatisation)** : Paramètres → Règles combine des conditions (toujours / sans médias en lecture / en lecture / plage horaire / programme de médias spécifique) et des actions (masquer / replier de force / afficher de force) pour contrôler l'Île automatiquement ; masquer a la priorité, puis replier, et enfin afficher de force.
- **Mode basse consommation** : réduit la fréquence d'images des ondes et simplifie les animations au repos pour économiser l'énergie (Paramètres → Général).

### P2 (implémentées)
- Changement de langue de l'interface entre le chinois simplifié et l'anglais.
- Exporter / importer le fichier de configuration JSON.
- Intégration des notifications Windows (Bluetooth / prise en charge des notifications du système / en cours de lecture / batterie faible).
- En attente : alerte d'appel entrant (non implémentée) ; rappels d'agenda implémentés (composant + outil de productivité).

---

## Choix technologique et justification

| Option | Conclusion | Justification |
| --- | --- | --- |
| **C# + WPF (.NET 8)** | ✅ Adopté | Faible consommation de ressources et démarrage rapide (face à la WebView d'Electron/Tauri), plus grande capacité d'intégration avec le système (prise en charge native SMTC/CoreAudio/barre d'état), empaquetage simple en un seul fichier |
| C++ + Qt | ⚪ | Efficacité de développement faible, licence complexe (LGPL), beaucoup de code écrit à la main pour s'intégrer à la pile médias de Windows |
| Tauri / Electron | ⚪ | Consommation de mémoire élevée (difficile d'atteindre <150 Mo en résidence), démarrage lent, ne respecte pas l'exigence de «faible consommation de ressources et démarrage rapide» |
| WinUI 3 | ⚪ | Empaquetage/déploiement plus complexe que WPF (nécessite Windows App SDK), et la prise en charge SMTC pour les applications de bureau non empaquetées est identique à WPF |

**Points clés** :
- `Windows.Media.Control.GlobalSystemMediaTransportControlsSessionManager` est disponible directement via la projection du SDK Windows de .NET 8 (CsWinRT), sans nécessiter d'identité d'empaquetage UWP.
- Hormis les projections WPF/WinForms/SDK Windows intégrées au système, **zéro dépendance tierce au runtime** (voir [THIRD_PARTY.md](THIRD_PARTY.md)).
- Effet acrylique : sous Win10 comme sous Win11, il est implémenté avec `SetWindowCompositionAttribute` (`ACCENT_ENABLE_ACRYLICBLURBEHIND`) et les coins arrondis sont rognés avec `SetWindowRgn` pour que le flou suive la forme de la capsule.

---

## Vue d'ensemble de l'architecture

```
src/WinIsland/
├── App.xaml(.cs)              # Racine de composition : instance unique, capture des exceptions, barre d'état, cycle de vie de la fenêtre
├── Services/
│   ├── MediaModels.cs         # Modèle unifié d'instantané des médias (TrackInfo / MediaSnapshot)
│   ├── SmtcMediaProvider.cs   # Session de médias globale de Windows (pilotée par événements + envoi avec throttling)
│   ├── CiderClient.cs         # Wrapper de l'API locale de Cider (V3 + LegacyV2, balayage des ports, analyse tolérante)
│   ├── CiderMediaProvider.cs  # Couche de session Cider (cycle de vie de la connexion)
│   ├── WindowTitleMediaProvider.cs # Repli : reconnaissance du titre de la fenêtre
│   ├── MediaCoordinator.cs    # Répartition centrale : Cider > SMTC > titre de fenêtre, cache des pochettes, volume supplémentaire
│   ├── LrcParser.cs           # Analyse LRC (horodatages multiples, offset, formats de durée)
│   ├── LyricsService.cs       # Analyse des paroles (.lrc local → Cider → en ligne)
│   ├── OnlineLyricsService.cs # Paroles en ligne (interfaces non officielles de NetEase/QQ Music, activées par défaut avec interrupteur en un clic)
│   ├── ArtworkCache.cs        # Téléchargement/cache des pochettes (pochette distante de Cider → fichier local)
│   ├── SystemVolume.cs        # Volume système CoreAudio (COM P/Invoke)
│   ├── AppSettings.cs         # Lecture/écriture de la configuration JSON (%APPDATA%\WinIsland\settings.json)
│   ├── SingleInstance.cs      # Mutex nommé + pipe nommé (le second démarrage affiche l'Île)
│   ├── AutoStart.cs           # Clé de démarrage automatique HKCU Run
│   ├── GlobalHotkeyService.cs # Raccourcis globaux (Win32 RegisterHotKey)
│   ├── NotificationService.cs # Bannière de notification en verre dans le coin supérieur droit
│   ├── NotificationHistoryService.cs # Historique des notifications (50 dernières, persistance JSON)
│   ├── BluetoothMonitor.cs    # Surveillance de la connexion/déconnexion des appareils Bluetooth
│   ├── SystemNotificationMonitor.cs # Prise en charge des notifications Windows (miroir par automatisation de l'interface)
│   ├── MediaAppRegistry.cs    # Enregistrement des programmes de médias (activer/désactiver/ordonner)
│   ├── AudioWaveService.cs    # Ondes audio (échantillonnage du volume système, pilote la vibration des ondes)
│   ├── KeyboardIndicatorMonitor.cs # Indicateurs de clavier (surveillance de l'état de CapsLock)
│   ├── ClipboardHistoryService.cs # Historique du presse-papiers
│   ├── TodoService.cs         # Liste de tâches
│   ├── PomodoroService.cs     # Minuteur Pomodoro
│   ├── ScheduleService.cs     # Rappels d'agenda
│   ├── WeatherService.cs      # Météo (Open-Meteo)
│   ├── RssService.cs          # Rappels RSS
│   ├── MailService.cs         # Rappels de courrier POP3
│   ├── RuleEngine.cs          # Moteur de règles d'automatisation
│   ├── IslandViewModel.cs     # Modèle de vue principal (interpolation de la progression, index des paroles, visibilité)
│   ├── SettingsWindow.xaml(.cs) / SettingsViewModel.cs
│   ├── LyricsWindow.xaml(.cs) # Fenêtre de paroles indépendante
│   ├── ThemeService.cs        # Thème clair/sombre + pinceaux de la couleur du thème
│   ├── WindowEffects.cs       # Acrylique / mode sombre / zone de coins arrondis
│   ├── ScreenHelper.cs        # Plusieurs moniteurs + conversion DPI PerMonitorV2
│   ├── TrayIcon.cs            # Icône et menu de la barre d'état
│   ├── ClipboardPanelWindow.xaml(.cs) # Panneau de l'historique du presse-papiers
│   ├── QuickLauncherWindow.xaml(.cs)  # Lanceur rapide (Ctrl+Space)
│   └── Localization.cs        # Table de textes chinois/anglais
├── Diagnostics/DiagnosticsCommand.cs  # Informations de diagnostic --diagnose
tests/WinIsland.Tests/         # Tests unitaires xunit (analyse LRC/configuration/Cider/titre de fenêtre)
build/
├── publish.ps1                # Publication en un clic (autonome ou dépendante du framework + zip)
├── WinIsland.iss              # Script d'installation Inno Setup
└── make-icon.ps1 / IconGen.cs # Outil de génération d'icônes
```

**Flux de données** : `MediaCoordinator` interroge chaque Provider une fois par seconde (asynchrone, sans bloquer l'interface) → génère un `MediaSnapshot` unifié (avec chemin de pochette local et volume) → le publie dans `IslandViewModel` via le Dispatcher → l'interpolateur de 200 ms fait avancer en douceur la barre de progression et la surbrillance des paroles → rendu par liaison WPF.

---

## Démarrage rapide

> 📦 **Version précompilée** : le répertoire `releases/` fournit des exécutables autonomes à fichier unique par version (p. ex., `releases/1.1.5/win-x64/WinIsland-1.1.5-win-x64.exe`, inclut le runtime .NET 8, double-clic pour exécuter). Les versions bêta sont conservées uniquement localement ; seules les versions stables sont publiées sur GitHub (comprend les versions portables win-x64 / win-arm64 et l'installateur universel).

### Exigences d'environnement
- Windows 10 1809+ / Windows 11
- Machine de compilation : SDK .NET 8 (ou un SDK supérieur en spécifiant `net8.0-windows10.0.19041.0`)

### Compilation
```powershell
# Restaurer + compiler + tester
dotnet build WinIsland.slnx -c Release
dotnet test  WinIsland.slnx -c Release

# Exécuter (Debug)
dotnet run --project src\WinIsland -c Debug
```

### Publication en un clic
```powershell
# Autonome (inclut le runtime .NET 8, sans installation, ~73 Mo en un seul fichier)
.\build\publish.ps1

# Dépendante du framework (petite taille, nécessite .NET 8 Desktop Runtime)
.\build\publish.ps1 -FrameworkDependent
```
Les artefacts se trouvent dans `publish\win-x64\` (comprend `WinIsland.exe`) ; le zip est `publish\WinIsland-win-x64.zip`.

### Installateur (facultatif)
Après avoir installé [Inno Setup 6](https://jrsoftware.org/isinfo.php) :
```powershell
iscc.exe build\release-1.1.5.iss
```
Génère `releases\<version>\WinIsland-Setup-<version>.exe` (installateur universel, compatible x64 et ARM64, installe automatiquement selon l'architecture). Lors de la publication d'une version stable, copiez `release-<version>.iss` dans `build\` et mettez à jour le numéro de version.

---

## Utilisation

1. Démarrez `WinIsland.exe` (ou activez le démarrage automatique / cochez le démarrage automatique dans l'installateur). L'icône apparaît dans la barre d'état.
2. Lisez n'importe quelle musique :
   - Musique NetEase, QQ Music, Spotify, Apple Music officiel, etc. → s'affiche automatiquement via la session de médias du système ;
   - Cider → voir [Intégration avec Cider](#intégration-avec-cider) ;
   - Autres lecteurs → repli de reconnaissance du titre de la fenêtre (affichage uniquement).
3. **Cliquez** sur l'Île pour développer la carte complète (le survol ne développe pas) : glissement de la progression (seek), contrôle de la lecture, volume et paroles synchronisées ; recliquez pour replier (après avoir quitté la carte, repli automatique à 700 ms).
4. Menu de la barre d'état : afficher/masquer, fenêtre de paroles indépendante, démarrer avec Windows, **Ne pas déranger** (cocher silencie les notifications), **Rechercher les mises à jour**, **Voir les journaux**, paramètres et quitter. **Fermer la fenêtre principale ne ferme pas le processus** (seulement une réduction dans la barre d'état).
5. Raccourcis globaux : `Ctrl+Alt+P` lecture/pause · `Ctrl+Alt+←/→` précédent/suivant · `Ctrl+Alt+I` afficher/masquer · `Ctrl+Alt+Space` développer/replier · `Ctrl+Space` lanceur rapide (rechercher des applications / saisir une URL et appuyer sur Entrée) · `Ctrl+Alt+V` panneau de l'historique du presse-papiers (tous désactivables / personnalisables).
6. Les notifications et alertes (Bluetooth / notifications Windows / en cours de lecture / batterie faible) apparaissent par défaut comme des bannières en verre dans le coin supérieur droit ; activables/désactivables dans Paramètres → Notifications ; avec **Ne pas déranger** activé, aucune bannière ne s'affiche (le compteur du badge continue de compter).
7. Paramètres courants de la ligne de commande :
   ```powershell
   WinIsland.exe --demo       # Mode démonstration (aperçu de l'interface + paroles d'exemple sans médias)
   WinIsland.exe --diagnose   # Génère un rapport de diagnostic dans %APPDATA%\WinIsland\diagnostics.txt
   WinIsland.exe --settings   # Ouvre les paramètres au démarrage
   ```

---

## Référence de configuration

Fichier de configuration : `%APPDATA%\WinIsland\settings.json` (JSON ; les modifications de l'interface des réglages s'appliquent instantanément ; exportable/importable).

| Clé | Défaut | Description |
| --- | --- | --- |
| `Language` | `zh-CN` | `zh-CN` / `en-US` |
| `ThemePreset` | `Default` | Thème : `Default/Ocean/Forest/Sunset/Neon/Mono/Grape/Sky/Rose/Amber/Lime/Teal/Lavender/Crimson/Midnight/Coffee/Sakura/Aurora/Custom` (remplace AccentColor) |
| `FontFamily` | `Segoe UI` | Police de l'interface |
| `FontScale` | `1.0` | Échelle de police 0.8–1.4 |
| `CornerRadius` | `28` | Rayon des coins de la capsule 16–40 |
| `BadgeEnabled` | `true` | Badge de notifications non lues (point rouge + numéro dans le coin supérieur droit) |
| `CoverTintBackground` | `true` | Le fond étendu prend la couleur de la pochette de l'album |
| `WaveVisualizerEnabled` | `true` | Ondes audio à gauche des boutons de contrôle lors de la lecture de médias |
| `WaveStyle` | `Bars` | Style d'onde : `Bars` (barres) / `Spectrum` (spectre) / `Ring` (anneau) / `Particles` (particules) |
| `WaveSyncEnabled` | `true` | Les ondes suivent le rythme de la musique (pilotées par le son de sortie du système) |
| `WaveSensitivity` | `1.0` | Sensibilité des ondes 0.2–3.0 |
| `WaveHeight` | `1.0` | Hauteur des ondes 0.4–1.6 |
| `WallpaperThemeColorEnabled` | `false` | Couleur du fond d'écran : extrait la couleur principale du fond actuel comme couleur du thème (purement local) |
| `MarqueeTextEnabled` | `false` | Texte défilant : défilement horizontal automatique lorsque le titre/les paroles sont trop larges |
| `EdgeSnapEnabled` | `true` | Au relâchement du glissement déverrouillé, s'accroche automatiquement au bord/centre de l'écran |
| `FullScreenAutoHideEnabled` | `true` | Masquage automatique de l'Île en plein écran (vidéo/jeu/présentation) |
| `RecordingDndEnabled` | `false` | Ne pas déranger automatique lors de l'enregistrement d'écran (sans bannières de notification) |
| `Theme` | `Auto` | `Auto` / `Light` / `Dark` |
| `AccentColor` | `#6C5CE7` | Couleur du thème (#RRGGBB) |
| `Position` | `Center` | `Center` centrée en haut / `Right` en haut à droite |
| `Monitor` | `Primary` | `Primary` écran principal / `All` tous / `Index` écran spécifié |
| `MonitorIndex` | `0` | Numéro d'écran lorsque `Monitor=Index` |
| `OffsetX` / `OffsetY` | `0` / `16` | Décalage en pixels |
| `Opacity` | `0.92` | 0.3–1.0 |
| `HideWhenNoMedia` | `true` | Masque l'Île sans lecture |
| `ShowWhenPaused` | `true` | Continue d'afficher à la pause |
| `StartWithWindows` | `false` | Démarrer avec Windows |
| `StartHidden` | `false` | Masquée au démarrage |
| `CompactShowArt/Title/Progress` | `true/true/false` | Contenu du mode compact |
| `IslandAlwaysVisible` | `false` | Île permanente (affiche les composants même sans médias) |
| `ShowMediaInfo` | `true` | Affiche les informations de lecture (titre/pochette/paroles, etc.) |
| `ReduceMotion` | `false` | Réduit les effets dynamiques (désactive les animations à ressort ; accessibilité/économie d'énergie) |
| `GlobalHotkeysEnabled` | `true` | Interrupteur des raccourcis globaux |
| `LowBatteryThreshold` | `20` | Seuil d'alerte de batterie faible (%), 0 pour désactiver |
| `ExpandedShowArtTitle/Progress/Controls/Lyrics` | `true` | Interrupteurs des sections de la carte étendue (pochette+titre / barre de progression / contrôles et volume / paroles) |
| `Components` | objet | Sélection des composants : `Time/Weather/Date/Cpu/Gpu/Ram/Net/Battery/Volume/CapsLock/Clipboard/Todo/Timer/Schedule/Holiday/Meeting/Mic/Cam`, chacun avec deux colonnes `WhenIdle`/`WhenPlaying` ; `Cover/Title/Artist/Lyrics/Progress` s'affichent pendant la lecture ; le dictionnaire `ComponentBadges` remplit le texte du badge de chaque composant |
| `WidgetOrder` | `Time,Weather,...` | Ordre des composants (clés séparées par des virgules, inclut `Song`) |
| `MediaApps` | `[]` | Activation/désactivation et priorité des programmes de médias (vide = tous activés) |
| `CompactWidth` / `CompactHeight` | `360` / `72` | Largeur / hauteur compactes (le réglage manuel par glissement désactive le réglage automatique) |
| `CompactWidthAuto` / `CompactHeightAuto` | `true` | La taille compacte s'ajuste automatiquement au contenu (activé par défaut) |
| `ExpandedWidthAuto` / `MaxExpandedHeightAuto` | `true` | Ajustement automatique de la taille étendue (activé par défaut) |
| `ExpandedWidth` / `MaxExpandedHeight` | `400` / `384` | Largeur étendue / hauteur maximale étendue |
| `BluetoothNotifyEnabled` | `false` | Alerte de connexion/déconnexion Bluetooth |
| `NotificationTakeoverEnabled` | `false` | Prise en charge des notifications Windows (best effort) |
| `NotificationTimeoutSeconds` | `6` | Durée de la bannière de notification (secondes) |
| `NotificationPosition` | `TopRight` | Position des notifications (coin supérieur droit) |
| `DoNotDisturbEnabled` / `DoNotDisturbManual` | `false` | Ne pas déranger : automatique par plages / manuel |
| `DoNotDisturbStartHour` / `DoNotDisturbEndHour` | `22` / `8` | Plage de Ne pas déranger (heures) |
| `DnDAllowlist` | `[]` | Liste blanche de Ne pas déranger (`QQ.exe,WeChat.exe` ; dans la liste blanche, les notifications s'affichent) |
| `Rules` | `[]` | Liste des règles d'automatisation (condition + action) |
| `ClipboardHistoryEnabled` / `ClipboardHistoryMax` | `false` / `15` | Interrupteur de l'historique du presse-papiers et nombre maximal d'entrées |
| `PomodoroEnabled` / `PomodoroWorkMinutes` / `PomodoroBreakMinutes` | `false` / `25` / `5` | Interrupteur du Pomodoro et durée de travail/pause (minutes) |
| `KeyIndicatorSeconds` | `3` | Durée de l'indicateur de clavier (CapsLock) (secondes) |
| `VolumeTempIndicatorEnabled` / `VolumeTempIndicatorSeconds` | `true` / `4` | Interrupteur et durée de l'indicateur temporaire de volume/silence sur l'Île |
| `FileCopyNotifyEnabled` | `true` | Copie/déplacement de fichiers sur l'Île (reconnaissance locale du titre de la fenêtre) |
| `DownloadProgressEnabled` | `false` | Téléchargement en cours sur l'Île (analyse les fichiers temporaires du dossier de téléchargements ; désactivé par défaut) |
| `UsageMergeEnabled` / `UsageMergeItems` | `false` / `Mic,Cam,Meeting,Recording` | Capsule combinée «En cours d'utilisation» et composants participants (désactivée par défaut) |
| `AutoUpdateCheck` | `false` | Recherche automatique des nouvelles versions de GitHub (désactivée par défaut, nécessite une connexion) |
| `DoubleClickAction` | `PlayPause` | Action au double-clic : `PlayPause` / `ToggleExpand` / `ShowDesktop` / `ToggleVisible` / `NextTrack` / `PrevTrack` / `OpenSettings` / `None` |
| `AnimationStyle` | `Spring` | Skin d'animation : `Spring` / `Soft` / `Elastic` / `Fade` |
| `ThemeTint` | `""` | Couleur de fond personnalisée #RRGGBB (s'applique lorsque le préréglage est Custom) |
| `ExpandedCardStyle` | `Classic` | Modèle de carte étendue : `Classic` / `Hero` |
| `NetCurveEnabled` | `true` | Le composant réseau affiche la mini-courbe des 32 dernières secondes |
| `LowPowerMode` | `false` | Mode basse consommation (réduit la fréquence d'images des ondes et simplifie les animations au repos) |
| `MeetingAssistantEnabled` | `false` | Assistant de silence en réunion : détecte les fenêtres de réunion + Ne pas déranger automatique |
| `MeetingAutoDnd` / `MeetingKeywords` | `true` / `""` | Ne pas déranger automatique en réunion / mots-clés de réunion personnalisés |
| `ScreenCaptureNotifyEnabled` / `ScreenshotNotifyEnabled` / `RecordingNotifyEnabled` | `false` / `true` / `true` | Interrupteur général et sous-éléments des alertes de capture/enregistrement |
| `CalendarEnabled` / `CalendarIcsPath` / `CalendarAdvanceMinutes` | `false` / `""` / `10` | Interrupteur des rappels de calendrier .ics / chemin du fichier / minutes d'avance |
| `RssNotifyEnabled` / `RssUrls` / `RssIntervalMinutes` | `false` / `""` / `15` | Rappels RSS / adresses d'abonnement / intervalle d'interrogation (minutes) |
| `MailNotifyEnabled` / `MailPop3Server` / `MailPop3Port` / `MailUseSsl` / `MailUser` / `MailPassword` / `MailCheckMinutes` | `false` / `""` / `995` / `true` / `""` / `""` / `5` | Rappels de courrier (POP3) : interrupteur, serveur, port, SSL, compte, code d'autorisation et intervalle de vérification |
| `QuickLauncherEnabled` / `HotkeyLauncher` | `true` / `Ctrl+Space` | Interrupteur du lanceur rapide et raccourci |
| `ClipboardPanelEnabled` / `HotkeyClipboardPanel` | `true` / `Ctrl+Alt+V` | Interrupteur du panneau de l'historique du presse-papiers et raccourci |
| `HotkeyExpand` | `Ctrl+Alt+Space` | Raccourci développer/replier |
| `NotifyFoldEnabled` | `true` | Plier les notifications similaires (même source et même titre affiche une seule) |
| `ActiveProfile` | `Default` | Nom du profil de configuration (bascule entre plusieurs ensembles) |

---

## Intégration avec Cider

Cider (client tiers d'Apple Music) fournit une API HTTP locale. WinIsland encapsule déjà un module indépendant (`CiderClient.cs`) qui s'adapte automatiquement aux différences de version.

**Étapes d'activation (important)** :
1. Ouvrez Cider : **Paramètres → Connectivité → Autoriser le contrôle externe (Manage External Application Access)** ; à l'activation, Cider affiche le jeton d'API (s'il est vide, cliquez pour le générer).
2. Copiez le jeton dans **Paramètres de WinIsland → Cider → API Token** et enregistrez.
3. Le port par défaut est `10767` et WinIsland le détecte automatiquement ; l'ancien RPC est `10769`.

> 🟡 Les nouvelles versions de Cider 2.x exigent **un jeton sur toutes les requêtes API par défaut** (sans jeton, renvoie `403 UNAUTHORIZED_APP_TOKEN`). Si les journaux de diagnostic indiquent qu'un jeton est nécessaire, remplissez-le en suivant les étapes ci-dessus ; sinon, les paroles/contrôles de Cider ne seront pas disponibles (les pistes peuvent encore s'afficher via SMTC).

> 🟡 Si les journaux montrent des HttpClient.Timeout répétés (initialement 2 s), cela vient généralement d'un logiciel de sécurité/proxy local qui intercepte le HTTP de boucle locale (la réponse réelle de Cider est d'environ 30 ms). Depuis 1.0.1, le délai de lecture des données a été porté à 5 s ; s'il expire toujours, vérifiez si l'antivirus bloque la connexion réseau de WinIsland.

**Capacités de l'API implémentées** (selon la documentation communautaire de Cider / le crate `cider-api` vérifié, version 2026) :
- `GET /api/v1/playback/active`, `GET /now-playing` (piste/pochette/progression/état)
- `POST /api/v1/playback/play|pause|playpause|next|previous|seek`
- `GET|POST /api/v1/playback/volume`
- `GET /api/v1/lyrics` (avec repli `?id=`)
- En-tête d'authentification : `apptoken` (compatible avec `apitoken`)
- Ancien 10769 : `/active`, `/currentPlayingSong`, `/playPause`, `/next`, `/previous`, `/seekto/{t}`, `/audio`

> 🟡 L'API de Cider n'est pas officielle et change rapidement ; toutes les requêtes ont un délai de 2 secondes et, en cas d'échec, dégradent automatiquement vers SMTC / titre de fenêtre, **sans affecter le flux principal**. Gardez WinIsland à jour pour vous adapter aux nouvelles versions.

---

## Paroles

Priorité :
1. **.lrc local** : recherché comme `Chanson.lrc` / `Artiste - Chanson.lrc` dans les répertoires de paroles (par défaut `%APPDATA%\WinIsland\Lyrics`, `Musique\Lyrics` et la racine de `Musique`) ;
2. **Paroles karaoké caractère par caractère AMLL** (timeline TTML caractère par caractère de la bibliothèque de chansons amll.dev, activées par défaut) ;
3. **Interface de paroles de Cider** (lorsque la source est Cider) ;
4. **Paroles en ligne** (interfaces non officielles de NetEase / QQ Music) : **activées par défaut** ; avec le clic droit sur l'Île, activation/désactivation en un clic, ou désactivation dans les paramètres.

> 🟡 Les paroles en ligne utilisent des interfaces non officielles et sont uniquement pour un apprentissage personnel ; respectez les droits d'auteur ; si le titulaire des droits l'exige, vous pouvez désactiver cette fonction à tout moment (totalement hors ligne après désactivation).

---

## Notifications et alertes (depuis 1.0.2, améliorées en 1.0.3)

Toutes les notifications sont des **bannières en verre dans le coin supérieur droit**, avec une animation d'entrée de style macOS (entrée depuis la droite + fondu) et de sortie ; la durée d'affichage est configurable (3–15 secondes).

- **Alerte de connexion Bluetooth** : Paramètres → Notifications ; une fois activée, elle apparaît lorsqu'un appareil Bluetooth se connecte/se déconnecte.
- **Prise en charge des notifications Windows** : Paramètres → Notifications ; une fois activée, elle reflète par automatisation de l'interface (best effort) le contenu du centre de notifications (comme les notifications QQ) dans les bannières du coin supérieur droit.
  > 🟡 Windows ne fournit pas d'API publique pour «intercepter les notifications d'autres applications» ; cette fonction est best effort et certaines notifications peuvent ne pas être capturées ; elle n'affecte pas le flux principal.
- **Notification en cours de lecture** : au changement de piste, la bannière «En cours de lecture - Titre» apparaît automatiquement (depuis 1.0.3).
- **Alerte de batterie faible** : apparaît lorsque la batterie descend sous le seuil (défaut 20 %, réglable 0–100), une fois par cycle de charge (depuis 1.0.3).
- **Historique des notifications** : enregistrements des 50 dernières notifications ; la page Paramètres → Notifications permet de les voir / vider (depuis 1.0.3).
- **Ajustement de la taille de l'Île** : Paramètres → Apparence permet d'ajuster la longueur/largeur compactes et la longueur étendue.

---
## API de l'île (envoyer depuis d'autres applications vers l'Île dynamique)

WinIsland inclut un service HTTP local ; d'autres applications peuvent envoyer des informations à l'Île dynamique en temps réel (semblable à l'intégration d'applications tierces dans l'Île dynamique d'iOS). **Documentation pour développeurs dans [docs/IslandAPI.md](docs/IslandAPI.md)**.

| Interface | Description |
|---|---|
| `POST /v1/island/push` | Envoie / met à jour une carte de l'Île (depuis v3, prend en charge les images / la progression dynamique / le heartbeat) |
| `PATCH /v3/island/push/{id}` | Mise à jour partielle : ne réécrit que les champs présents dans le corps (conserve l'expiration / la position dans la file) |
| `DELETE /v1/island/push/{id}` | Supprime une carte |
| `GET /v1/island/active` (ou `/v3/island/active`) | Interroge la carte active actuelle |
| `GET /v3/ws` | Canal bidirectionnel WebSocket : le client envoie `push/update/remove/ping`, le serveur diffuse les événements `push_updated/push_removed` |
| `GET /v1/health` | Vérification de santé |

- Paramètres → API de l'île : interrupteur d'activation, port (défaut 9840), jeton facultatif et durée d'affichage globale par défaut
- Les notifications de l'Île **ne modifient pas la longueur/largeur de l'Île** ; la carte s'affiche sur une seule ligne en état compact et ne masque pas les autres composants
- Les boutons prennent en charge «Ouvrir un lien / Lancer un programme» ; l'expéditeur peut personnaliser la durée d'affichage par entrée (remplace la valeur globale par défaut)
- Nouveau en v3 : `image` (image data URI ou http), `progress_from/progress_to/progress_duration_seconds` (progression automatique), `heartbeat_seconds` (renouvellement par pulsation ; s'il n'est pas renouvelé au-delà de 2 fois l'intervalle, suppression automatique), `theme` (thème de la carte dark/light/auto), `action: "command"` (le bouton exécute une commande locale) ; documentation complète dans [docs/IslandAPI.md](docs/IslandAPI.md)

---
## Restauration de l'état de lecture

- À la sortie de l'application, à la pause ou au changement de piste, «piste + position de lecture» est enregistré dans `%APPDATA%\WinIsland\state.json` (uniquement local).
- Au prochain démarrage, si c'est la même piste et que le lecteur ne renvoie pas encore la progression réelle, la dernière position est d'abord restaurée pour éviter le saut de «afficher la ligne 0 puis sauter à la phrase de pause» ; aucune restauration après plus de 1 heure ou si la piste a changé.

---
## Confidentialité et sécurité

- **Sans télémétrie, sans publicité, sans envois**. En dehors des «paroles en ligne» activées manuellement par l'utilisateur, l'application n'effectue aucune requête réseau.
- **Composant météo** : uniquement lorsque «Afficher la météo» est activé et qu'une ville est saisie, il interroge Open-Meteo (gratuit, sans clé, sans compte) pour la météo actuelle ; s'il n'est pas activé, entièrement hors ligne.
- Seuls scénarios de connexion : téléchargement des pochettes de Cider (`mzstatic.com`, URL publique de pochette renvoyée par l'API locale) et paroles en ligne activées par l'utilisateur.
- Toutes les données sont stockées localement dans `%APPDATA%\WinIsland\`.
- Les journaux ne conservent que des informations d'exécution locales (`logs\app-*.log`).

---

## Indicateurs non fonctionnels

Mesurés sur la machine de test (Windows 11 24H2, 2560×1440 à 100 %) (Release autonome) :

| Indicateur | Mesuré | Objectif |
| --- | --- | --- |
| CPU au repos (sans médias) | < 0,5 % (0,3 % mesuré en Debug) | ≤ 0 % |
| Mémoire résidente (Private) | ~72 Mo | ≤ 150 Mo |
| Démarrage | < 1 s (à froid) | ≤ 2 s |
| Fermer la fenêtre principale | Ne quitte pas, réduit dans la barre d'état système | ✅ |
| Instances multiples | Une seule ; le second démarrage affiche l'Île | ✅ |
| Exceptions | Capture unifiée et journalisation dans un fichier, sans boîte de blocage | ✅ |

> Remarque : le WorkingSet de la version autonome (comprend les pages partagées du runtime .NET) est d'environ 160 Mo, mais **la mémoire Private est d'environ 72 Mo** ; avec une version dépendante du framework, le WorkingSet sera plus faible.

---

## Limitations connues

- **Le karaoké caractère par caractère dépend de la source des paroles et de la progression** : avec une timeline TTML/LRC caractère par caractère AMLL, la mise en surbrillance se fait caractère par caractère ; sans timeline caractère par caractère ou si le lecteur ne fournit pas la progression réelle, on dégrade en surbrillance de la phrase complète (avance avec l'horloge locale).
- **Progression qui recule occasionnellement** (p. ex., Cider/SMTC signale 0 ou une position expirée à un instant) : une protection de position a été implémentée : le recul instantané est ignoré et l'avance actuelle est maintenue, sans ramener les paroles/la barre de progression au début ; seul un recul soutenu d'environ 4 secondes est considéré comme un vrai repositionnement ou un seek du lecteur.
- **Alerte d'appel entrant** : non implémentée (optionnelle en P2). Implémentées : alerte Bluetooth, prise en charge des notifications Windows (best effort), notification en cours de lecture, alerte de batterie faible et rappels d'agenda.
- **Couverture SMTC** : dépend de l'enregistrement de la session de médias globale par le lecteur ; certains lecteurs anciens qui ne l'enregistrent pas ne peuvent être couverts que par le titre de la fenêtre (sans boutons de contrôle).
- **Cider 1.x (ancienne API, port 9000)** : non adapté ; seuls 2.x et supérieurs sont pris en charge.

---

## Guide de vérification (nécessite un lecteur réel)

Les scénarios suivants nécessitent une vérification dans un environnement réel (ce qui a déjà été vérifié automatiquement dans l'environnement de développement de ce référentiel est indiqué) :

| Scénario | État |
| --- | --- |
| Énumération des sessions SMTC (avec `--diagnose`, la liste des sessions s'affiche) | ✅ Testé (détecte de vraies sessions comme Bilibili) |
| Affichage/masquage automatique de l'Île, développement/repli au clic, interpolation de la progression | ✅ Testé (démo + session réelle en pause) |
| Lecture/pause/changement/seek (lecteur réel) | 🟡 À tester (les chemins de code correspondent directement à l'API de contrôle SMTC) |
| Connexion et contrôle de l'API de Cider | 🟡 Nécessite d'installer Cider sur la machine et d'activer le contrôle externe |
| Défilement synchronisé des paroles .lrc locales | ✅ L'analyse LRC est testée par unités ; l'extrémité à extrémité nécessite une vraie chanson |
| Paroles en ligne | ✅ Intégré avec NetEase/QQ Music ; l'effet de bout en bout nécessite une vraie chanson |

**Étapes de vérification suggérées** :
1. `WinIsland.exe --diagnose` → confirmez que `System media sessions` liste le lecteur ;
2. Lisez n'importe quelle chanson de NetEase/QQ Music/Spotify → l'Île doit afficher la piste et permettre le contrôle ;
3. Ouvrez Cider et activez le contrôle externe → la source de l'Île doit afficher `Cider`, avec seek/volume ;
4. Placez un `.lrc` avec le même nom dans le dossier de la chanson → au développement, les paroles doivent défiler et se surligner avec la progression.

---

## Questions fréquentes

**Q : L'Île dynamique n'apparaît pas ?**
- Confirmez qu'il y a une lecture (à la pause, elle continue de s'afficher par défaut) ; `HideWhenNoMedia` est activé par défaut ; se masquer sans médias est normal.
- Exécutez `--diagnose` pour voir la liste des sessions ; si la liste est vide, le lecteur n'a pas enregistré SMTC.

**Q : Cider affiche «Non connecté» ?**
- Confirmez que «Autoriser le contrôle externe» est activé dans les paramètres de Cider ; vérifiez le port (défaut 10767) ; confirmez que Cider est activé dans les paramètres de WinIsland.

**Q : Les paroles en ligne ne se chargent pas ?**
- Les paroles en ligne sont activées par défaut (clic droit sur l'Île → Paroles en ligne pour basculer en un clic) ; si toujours rien, confirmez dans Paramètres → Paroles qu'elles sont activées et vérifiez la connectivité réseau.

**Q : L'icône de la barre d'état système reste après la sortie ?**
- Menu de la barre d'état → Quitter ; fermer directement la fenêtre de l'Île ne fait que la masquer (selon la conception de la «résidence dans la barre d'état système»).

---

## Licence open source

- Application : MIT (voir [LICENSE](LICENSE))
- Composants tiers : voir [THIRD_PARTY.md](THIRD_PARTY.md)

---

## العربية

## WinIsland – الجزيرة الديناميكية لنظام Windows

> انقل الجزيرة الديناميكية من iOS إلى Windows: نافذة عائمة على سطح المكتب مع التحكم في الوسائط، كلمات الأغاني المتزامنة، مكونات قابلة للتخصيص، مركز إشعارات، وإقامة دائمة في علبة النظام.
> مبنية على **.NET 8 + WPF**، متوافقة مع Windows 11 (وكذلك Windows 10، 1809+).

---

> **انقل الجزيرة الديناميكية من iOS إلى Windows | جزيرة ديناميكية حديثة متعددة الوظائف.**

انقل الجزيرة الديناميكية من iOS إلى Windows 11 / 10: التحكم في تشغيل الوسائط، كلمات كاريوكي حرفًا بحرف، مكونات قابلة للتخصيص، مركز إشعارات، وواجهة برمجة الجزيرة، كل ذلك في كبسولة واحدة. مبنية على **.NET 8 + WPF**، مجانية ومفتوحة المصدر (MIT)، **بدون إعلانات · بدون تتبع**.

🌐 **الموقع: https://WinIsland.JudeKwong.com**

---

## ✨أبرز الميزات

- **▶️ التحكم في تشغيل الوسائط**: يتصل بشكل أصلي بجلسات الوسائط العامة في Windows (SMTC)، متوافق مع موسيقى نيت إيز وQQ Music وSpotify وApple Music وGroove والأفلام والتلفزيون وغيرها؛ كما يدعم واجهة Cider المحلية خصيصًا؛ إذا تعذر الاتصال، يُستخدم عنوان النافذة كبديل. غلاف الألبوم، سحب شريط التقدم (seek)، تشغيل/إيقاف/تبديل الأغنية، كل ذلك متضمن؛ عند فتح عدة مشغلات يمكن تبديل مصدر التحكم بنقرة واحدة؛ النقر على الغلاف يفتح معاينة غامرة بملء الشاشة.
- **♪ كلمات كاريوكي حرفًا بحرف**: البطاقة الموسعة تنزلق وتُبرز الكلمات بشكل متزامن، مضيئة حرفًا بحرف بأسلوب الكاريوكي؛ ثلاثة مستويات لمصادر الكلمات: `.lrc` محلي ← واجهة كلمات المشغل ← كلمات عبر الإنترنت اختيارية؛ كلمات ثنائية اللغة، مفتاح ترجمة، نسخ السطر الحالي بنقرة واحدة؛ يمكن ضبط توقيت الكلمات بدقة لكل أغنية، وتتيح نافذة الكلمات المستقلة ضبط الشفافية والقفل.
- **🧩 نظام مكونات قابل للتخصيص**: الساعة، الطقس، التاريخ (مع التقويم القمري / المصطلحات الشمسية)، المعالج/بطاقة الرسوميات/الذاكرة/القرص، سرعة الشبكة، البطارية، طريقة الإدخال، مفاتيح سريعة (WiFi/Bluetooth/الوضع الليلي/كتم الصوت) وأكثر من 30 مكونًا؛ يمكن لكل مكون أن يكون له أيقونة مخصصة، واختيار بخانات، وترتيب بالسحب، مع وضع سطر واحد أو عدة أسطر في أي وقت.
- **🔗 واجهة برمجة الجزيرة**: واجهة HTTP / WebSocket محلية تتيح لأي برنامج خارجي إرسال معلومات إلى الجزيرة الديناميكية في الوقت الفعلي (مشابهة لتكامل تطبيقات الطرف الثالث في الجزيرة الديناميكية في iOS). الإصدار v3 يدعم الصور، والتقدم الديناميكي، والتجديد بالنبض (heartbeat)، والوضع الفاتح/الداكن للبطاقة؛ الإشعارات لا تغير عرض/ارتفاع الجزيرة ولا تخفي المكونات الأخرى؛ تدعم الأزرار فتح الروابط / تشغيل البرامج / تنفيذ أوامر محلية، ويمكن للنقر على زر الإشعار استدعاء المرسل عبر WebSocket.
- **🔂 مركز الإشعارات**: لافتات زجاجية في الزاوية العلوية اليمنى، مع حركات انزلاق بأسلوب macOS: أجهزة Bluetooth، تنبيهات المكالمات الصوتية/المرئية WeChat/QQ، الاستيلاء على إشعارات النظام، أثناء التشغيل، انخفاض البطارية/اكتمال الشحن، قطع الاتصال/عودة الاتصال؛ سجل الإشعارات، الطي، القائمة البيضاء لعدم الإزعاج، وأتمتة بالقواعد؛ يمكن أن تتضمن اللافتات أزرار إجراءات (مثل «قطع الاتصال» و«الإعدادات» لـ Bluetooth).
- **✨ المظهر والتأثيرات**: 18 سمة، ولون تمييز وخلفية قابلان للتخصيص، زجاج سائل مصنفر، **لون مأخوذ من الخلفية** (يستخرج تلقائيًا لون السمة من الخلفية الحالية)، **نص متحرك** (الكلمات/الأغاني الطويلة تنزلق أفقيًا)، 4 أنماط لجلود الحركة (زنبرك iOS وغيرها)، **4 أنماط لموجات الصوت** (أعمدة / طيف / حلقة / جسيمات، تهتز مع إيقاع الموسيقى)؛ حركات توسيع/طي بتحكم غير خطي بمعدل 60 إطارًا في الثانية؛ خلفية الغلاف المستخرجة يمكنها «التنفس» ببطء (سمة ديناميكية)؛ دقة عالية PerMonitorV2، بدون اختلال عند 120/150/200%.
- **🧠 التفاعل والذكاء**: فتح القفل والسحب + **الالتصاق بالحافة** (يلتصق بالحافة/المركز عند الإفلات)، **الإخفاء التلقائي في ملء الشاشة** (ينطوي عند تشغيل فيديو/لعبة/عرض تقديمي بملء الشاشة)، إجراءات مخصصة عند النقر المزدوج، **أزرار إجراءات سريعة** (قفل الشاشة/كتم الصوت/تشغيل-إيقاف/لقطة/إظهار سطح المكتب وغيرها، بترتيب قابل للتخصيص)، **سحب الملفات إلى الجزيرة**، **عدم إزعاج ذكي أثناء تسجيل الشاشة**؛ استجابة فورية لضغط الماوس (النقر له الأولوية للتوسيع/الطي).
- **⚙️ أدوات الإنتاجية والأتمتة**: بومودورو، قائمة مهام، سجل الحافظة، مشغل سريع، تذكيرات الجدول؛ مساعد كتم الصوت في الاجتماعات، تنبيهات تسجيل/لقطة الشاشة، تقدم نسخ/تنزيل الملفات على الجزيرة؛ اختصارات عامة ومحرك قواعد (إظهار/إخفاء تلقائي حسب الشروط).
- **🔒 الخصوصية والأمان**: بدون تتبع، بدون إعلانات، بدون إرسال بيانات. يعمل دون اتصال بالكامل، باستثناء الكلمات عبر الإنترنت والطقس اللذين يفعّلهما المستخدم يدويًا؛ جميع الإعدادات والبيانات تُحفظ محليًا فقط في `%APPDATA%\WinIsland`.

---

## 📥التنزيل (آخر إصدار مستقر 1.1.5)

| النظام الأساسي | التنزيل | الوصف |
| --- | --- | --- |
| Windows x64 | [نسخة محمولة x64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-x64.exe) | الخيار الرئيسي لأجهزة الكمبيوتر 64 بت؛ ملف واحد، بدون تثبيت، يعمل مباشرة |
| Windows ARM64 | [نسخة محمولة ARM64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-arm64.exe) | أجهزة ARM مثل Surface Pro X / Snapdragon؛ ملف واحد، بدون تثبيت |
| Windows شامل | [مثبّت شامل](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-Setup-1.1.5.exe) | معالج تثبيت Inno Setup؛ يثبت تلقائيًا حسب البنية x64 / ARM64 |

جميع الإصدارات السابقة وسجل التغييرات الكامل موجودة في [GitHub Releases](https://github.com/DMP-Pig/WinIsland/releases).

---

## 📊مؤشرات الأداء

| المؤشر | القيمة |
| --- | --- |
| الذاكرة المقيمة (Private) | ~72 م.ب |
| بدء التشغيل على البارد | < 1 ثانية |
| المعالج عند الخمول | ≤ 0% |
| إطارات الحركة | 60 إطارًا في الثانية بسلاسة |
| تعدد الحالات | حالة واحدة فقط، يمنع التشغيل المكرر |
| التتبع | 0 تتبع · بدون إرسال · بدون إعلانات |

---

## جدول المحتويات

- [الميزات](#الميزات)
- [الاختيار التقني والأساس المنطقي](#الاختيار-التقني-والأساس-المنطقي)
- [نظرة عامة على البنية](#نظرة-عامة-على-البنية)
- [بدء سريع](#بدء-سريع)
- [الترجمة والتجميع](#بدء-سريع)
- [الاستخدام](#الاستخدام)
- [مرجع الإعدادات](#مرجع-الإعدادات)
- [التكامل مع Cider](#التكامل-مع-cider)
- [الكلمات](#الكلمات)
- [الخصوصية والأمان](#الخصوصية-والأمان)
- [المؤشرات غير الوظيفية](#المؤشرات-غير-الوظيفية)
- [القيود المعروفة](#القيود-المعروفة)
- [دليل التحقق (يتطلب اختبارًا مع مشغل وسائط حقيقي)](#دليل-التحقق-يتطلب-اختبارًا-مع-مشغل-وسائط-حقيقي)
- [الأسئلة الشائعة](#الأسئلة-الشائعة)
- [الترخيص مفتوح المصدر](#الترخيص-مفتوح-المصدر)

---

## الميزات

### P0 (مُنفذة)
- **واجهة الجزيرة الديناميكية العائمة (بأسلوب iOS)**: تتركز أعلى الشاشة افتراضيًا (قابلة للضبط إلى اليمين)؛ كبسولة بزوايا دائرية؛ تتبع السمة الفاتحة/الداكنة للنظام أو لون السمة اليدوي؛ **حركة تحويل** بين الكبسولة المدمجة ← البطاقة الكاملة (نافذة ثابتة + تحجيم/ظهور عنصر واحد، مدفوعة بخيط التأليف في WPF بمعدل 60 إطارًا في الثانية، مع ارتداد مرن بأسلوب iOS)؛ **نقرة للتوسيع/الطي** (المرور فوقها لا يوسعها)، طي تلقائي عند الخروج (حاجز 700 مللي ثانية ضد اللمس العرضي)؛ النقرات خارج البطاقة تخترق النافذة.
- **القفل والسحب**: مقفلة افتراضيًا (لا يمكن تحريكها)؛ تتيح القائمة السياقية **فتح القفل** (سحب الجزيرة بالماوس بعد فتح القفل)، **التوسيط** (عمودي متساوٍ، أفقي في المنتصف)، و**إعادة القفل**. عند إعادة القفل بعد السحب **يُحفظ الموضع** (لا يعود إلى الافتراضي)؛ عند إفلات السحب يوجد **التصاق بالحافة** (يلتصق بحافة/مركز الشاشة، قابل للضبط في الإعدادات ← عام).
- **التصميم المدمج**: العنوان/الفنان/الكلمات بمحاذاة يسرى (ملاصقة للغلاف) ومركزية عموديًا.
- **غلاف الألبوم**: كل من الكبسولة والبطاقة الموسعة تعرضان الغلاف (64 بكسل مكبرًا عند التوسيع؛ أيقونة بديلة إذا لم يوجد غلاف)؛ الصور المصغرة SMTC وأغلفة Cider تُخزَّن مؤقتًا تلقائيًا.
- **التحكم في تشغيل الوسائط**: يعرض العنوان والفنان والألبوم؛ شريط تقدم قابل للسحب (seek)؛ تشغيل/إيقاف، سابق، تالٍ؛ التحكم في الصوت عند الحاجة (Cider يستخدم واجهته، والمصادر الأخرى تتحكم في صوت النظام؛ قابل للتعطيل)؛ مكون الوسائط يعرض شارة المصدر الحالي (Spotify / Cider / موسيقى نيت إيز / QQ Music وغيرها).
- **المشغل المصغر**: نافذة عائمة مستقلة (قابلة للضبط في الإعدادات ← الوسائط)، تعرض الغلاف/العنوان/الفنان/شريط التقدم وأزرار التشغيل؛ يمكن سحبها بحرية وتتذكر موضعها؛ تُظهر/تُخفي تلقائيًا مع التشغيل.
- **تبديل جهاز إخراج الصوت**: الإعدادات ← الوسائط تتيح سرد جهاز الإخراج الافتراضي للنظام وتغييره (يُنصح بإعادة تشغيل المشغل بعد التغيير).
- **دعم مصادر متعددة**:
  1. جلسات الوسائط العامة في Windows (`Windows.Media.Control` / SMTC): موسيقى نيت إيز، QQ Music، Spotify، Apple Music الرسمي، Groove، الأفلام والتلفزيون وغيرها؛
  2. **Cider**: واجهة HTTP محلية (المنفذ 10767، متوافقة مع RPC القديم 10769، فحص تلقائي للمنافذ + إعداد يدوي، تدعم المصادقة `apptoken`)؛
  3. بديل: عنوان النافذة + تحديد العملية (يعرض المعلومات فقط، بدون قدرة تحكم).
- **عرض الكلمات (وضع كاريوكي حرفًا بحرف)**: عند التوسيع، تظهر منطقة الكلمات في وضع الكاريوكي: **أحرف السطر الحالي تُضاء واحدًا تلو الآخر**؛ تقدم الإبراز قيمة مستمرة والأحرف عند الحدود تنتقل بسلاسة من اللون الأساسي إلى لون الإبراز بتحكم بمعدل 60 إطارًا في الثانية، تتدفق من اليسار إلى اليمين حسب ترتيب القراءة (صحيحة أيضًا مع الكلمات ذات الأسطر المتقطعة، دون إضاءة عدة أسطر معًا)؛ كل سطر يبدأ من 0 (الحرف الأول لا يُضاء في البداية)؛ عند الإيقاف المؤقت، يتجمد الإبراز عند لحظة الإيقاف: عندما لا يكون لدى Cider حالة صريحة، يُحدد التشغيل/الإيقاف حسب «هل يتحرك الموضع» (لم يعد يخلط مع التشغيل فقط بسبب remainingTime>0)، وSMTC يعطي الأولوية لمتابعة جلسة Cider (يمنع الجلسات النشطة الأخرى مثل Bilibili من التقاطها)؛ عند الخروج وإعادة التشغيل يُستعاد آخر موضع إيقاف تلقائيًا (لا يعود إلى البداية)؛ السطر الحالي يُبرز النص فقط (بدون كبسولة خلفية، لتجنب الإبراز المزدوج؛ حجم كبير 20 بكسل)، بينما تخفت بقية الأسطر، **انزلاق سلس مع توسيط تلقائي** (يقترب من السطر الحالي إطارًا بإطار بمعدل 60 إطارًا في الثانية ويتبعه عند التوسيع)؛ في الوضع المدمج، يُعرض السطر الحالي في الوقت الفعلي بمحاذاة يسرى ويُضاء أيضًا حرفًا بحرف؛ نافذة كلمات عائمة مستقلة اختيارية.
  - **مزامنة التقدم**: يقرأ تلقائيًا رمز واجهة Cider المحلية (بدون إعداد) للحصول على تقدم التشغيل الفعلي ومزامنة الكاريوكي مع الأغنية بدقة؛ المشغلات التي لا توفر تقدمًا تستخدم الساعة المحلية.
  - **مصادر الكلمات**: `.lrc` محلي (`%APPDATA%\WinIsland\Lyrics` أو مجلد الموسيقى) ← كلمات كاريوكي حرفًا بحرف AMLL ← واجهة كلمات Cider ← كلمات عبر الإنترنت (مفتاح بنقرة واحدة بالنقر بزر الماوس الأيمن على الجزيرة). بدون كلمات تظهر «بدون كلمات»، دون أخطاء.
  - **كلمات ثنائية اللغة**: تجمع تلقائيًا أسطر الترجمة مع الطوابع الزمنية المجاورة؛ يمكن تعطيلها في الإعدادات (لا حاجة لملفات كلمات إضافية)؛ مفتاح إظهار/إخفاء الترجمة و«نسخ السطر الحالي» للنسخ بنقرة واحدة.
- **علبة النظام**: أيقونة دائمة، قائمة سياقية (إظهار/إخفاء، نافذة كلمات مستقلة، بدء التشغيل مع Windows، الإعدادات، خروج)، نقرة مزدوجة لتبديل الرؤية.
### P1 (مُنفذة)
- **نظام المكونات (محتوى الجزيرة القابل للتخصيص)**: الإعدادات ← المكونات تتيح تحديد المكونات المعروضة «بدون أغنية / مع أغنية» وضبط الترتيب بالسحب:
  - الساعة، الطقس (Open-Meteo، يتطلب مدينة واتصالًا)، التاريخ (مع التقويم القمري والمصطلحات الشمسية)، استخدام المعالج، استخدام بطاقة الرسوميات، استخدام الذاكرة، سرعة الشبكة (يمكنها عرض المنحنى المصغر لآخر 32 ثانية)، البطارية، المساحة الحرة على القرص، حالة طريقة الإدخال (صينية / إنجليزية + اسم IME)، مفاتيح سريعة (WiFi / Bluetooth / الوضع الليلي / كتم الصوت بنقرة واحدة)، الصوت، مؤشر لوحة المفاتيح (CapsLock)، الحافظة، قائمة المهام، بومودورو، الجدول، العد التنازلي للأعياد، في اجتماع، الميكروفون، الكاميرا؛
  - معلومات الأغنية (الغلاف/العنوان/الفنان/الكلمات/شريط التقدم، تظهر فقط أثناء التشغيل، دائمة الوجود في شريط الترتيب).
  - شريط الترتيب يعرض فقط المكونات المحددة؛ القائمة والشريط يدعمان عجلة الماوس وأشرطة التمرير؛ يمكن لكل مكون أن يكون له أيقونة مخصصة (أيقونات MDL2 أو رموز تعبيرية، الإعدادات ← المكونات).
  - مكونات مؤقتة على الجزيرة: تغيير الصوت، لقطة/تسجيل الشاشة، نسخ/نقل الملفات، تنزيل قيد التقدم (الآخران معطّلان افتراضيًا): عند حدوث الحدث، يظهر المكوّن المقابل مؤقتًا حتى لو كانت الجزيرة مخفية.
  - **كبسولة «قيد الاستخدام» المجمعة** (الإعدادات ← المكونات، معطلة افتراضيًا): عند تفعيلها، تتحد «الميكروفون / الكاميرا / في اجتماع / التسجيل» المحددة في كبسولة حالة واحدة «قيد الاستخدام · N» ولم تعد العناصر المجمعة تظهر منفصلة.
  - **وضع السطر الواحد** (الإعدادات ← المظهر، مفعل افتراضيًا): جميع المكونات في سطر واحد في الوضع المدمج؛ بدون توسيع يعرض أيضًا معلومات الأغنية وسطر الكلمات الحالي (إبراز كاريوكي حرفًا بحرف)، مع اقتطاع الكلمات الطويلة تلقائيًا؛ شريط التقدم وقائمة الكلمات الكاملة تظهران في البطاقة الموسعة.
- **تخصيص محتوى البطاقة الموسعة**: يمكن تفعيل/تعطيل الغلاف + العنوان، شريط التقدم، أزرار التحكم والصوت، ومنطقة الكلمات بشكل منفصل.
- **تخصيص المظهر (صفحة إعدادات بأسلوب إعدادات النظام في macOS)**: تنقل يسار + محتوى يمين، زجاج سائل دائري؛ **18 سمة جاهزة** (الافتراضية / المحيط / الغابة / الغروب / النيون / أحادي اللون / العنب / السماء / الوردي / الكهرماني / الليموني / الأخضر المزرق / الخزامى / القرمزي / منتصف الليل / القهوة / ساكورا / الشفق، بالإضافة إلى مخصصة)؛ **لون مأخوذ من الخلفية** (يستخرج تلقائيًا اللون الرئيسي من الخلفية الحالية كالون للمظهر، محلي بحت)؛ **نص متحرك** (الكلمات الطويلة تنزلق تلقائيًا)؛ **4 أنماط لموجات الصوت** (أعمدة / طيف / حلقة / جسيمات) و**4 جلود حركة** (زنبرك iOS / ناعم / مرن / تلاشي)؛ **وضع توفير الطاقة** (يقلل معدل إطارات الموجات ويبسط الحركات أثناء الخمول)؛ دقة عالية PerMonitorV2.
- **الاختصارات العامة**: `Ctrl+Alt+P` تشغيل/إيقاف · `Ctrl+Alt+←/→` سابق/تالٍ · `Ctrl+Alt+I` إظهار/إخفاء · `Ctrl+Alt+Space` توسيع/طي · `Ctrl+Space` المشغل السريع · `Ctrl+Alt+V` لوحة سجل الحافظة.
- **تقليل التأثيرات الحركية** (إمكانية الوصول / توفير الطاقة): يعطل حركات الزنبرك بنقرة واحدة، تغيير فوري.
- **ضبط حجم الجزيرة**: الإعدادات ← المظهر، يتيح ضبط العرض/الارتفاع المدمجين والعرض الموسع.
- **الجزيرة الديناميكية الدائمة**: مرئية دائمًا حتى بدون تشغيل (تعرض المكونات المحددة).
- **شاشات متعددة**: الشاشة الرئيسية / جميع الشاشات / رقم شاشة محدد.
- **دقة عالية**: PerMonitorV2، بدون اختلال عند 120/150/200%.
- **إعداد مخصص**: الموضع، الإزاحة، الشفافية، لون المظهر، محتوى الوضع المدمج، الإخفاء بدون وسائط، إلخ؛ التغييرات تُطبق فورًا.
- **الإخفاء التلقائي للجزيرة بدون تشغيل** (قابل للتعطيل).
- **عدم الإزعاج**: تفعيل يدوي بنقرة واحدة أو كتم تلقائي حسب الفترة الزمنية (تبديل بنقرة واحدة في قائمة علبة النظام؛ تُضبط الفترات في الإعدادات).
- **البحث عن التحديثات**: فحص يدوي للإصدارات الجديدة من GitHub في قائمة علبة النظام / الإعدادات؛ فحص تلقائي اختياري (معطل افتراضيًا، يتطلب اتصالًا).
- **إجراء سريع عند النقر المزدوج** (الإعدادات ← عام): يمكن أن يكون «تشغيل / إيقاف» (الافتراضي)، «توسيع / طي»، «إظهار سطح المكتب»، «إخفاء / إظهار الجزيرة»، «سابق»، «تالٍ»، «فتح الإعدادات» أو «بدون إجراء».
- **مساعد كتم الصوت في الاجتماعات (كشف الاجتماع)**: يتعرف على نوافذ الاجتماعات مثل Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet، ويفعّل عدم الإزعاج تلقائيًا أثناء الاجتماع ويعرض مكوّن «في اجتماع» (استدلال محلي بحت، دون اتصال).
- **تنبيهات تسجيل/لقطة الشاشة**: عند الضغط على `PrintScreen` / `Alt+PrintScreen` يظهر تنبيه؛ عند كشف برامج تسجيل مثل OBS وBandicam وFraps وCamtasia وXSplit وStreamlabs وXbox Game Bar، يظهر «جارٍ تسجيل الشاشة» (كشف محلي للعمليات، دون اتصال).
- **عدم إزعاج ذكي (التسجيل)**: عند كشف تسجيل شاشة قيد التشغيل، تُكتم الإشعارات تلقائيًا (بدون لافتة)؛ عند الانتهاء، تُستعاد تلقائيًا؛ قابل للضبط في الإعدادات ← الإشعارات.
- **الإخفاء التلقائي في ملء الشاشة**: عند كشف فيديو/لعبة/عرض تقديمي بملء الشاشة (مثل PowerPoint)، تُخفى/تتطوى الجزيرة تلقائيًا وتُستعاد عند الخروج من ملء الشاشة؛ قابل للضبط في الإعدادات ← عام.
- **سحب الملفات إلى الجزيرة**: سحب الملفات/المجلدات إلى الجزيرة يتيح «نسخ المسار / فتح المجلد المحتوي / تثبيت على الجزيرة» وغيرها (اختر بالنقر بزر الماوس الأيمن على الجزيرة أو في قائمة السحب والإفلات).
- **تذكيرات أحداث التقويم (.ics)**: تحلل ملفات iCalendar المحلية (Outlook / Google Calendar / مصدرة من الهاتف)؛ عند حلول موعد الحدث (بقدرة N دقيقة مسبقًا قابلة للضبط) تظهر لافتة؛ تحليل محلي بحت، دون اتصال.
- **تذكيرات اشتراكات RSS**: تستعلم دوريًا عن خلاصات RSS 2.0 / Atom (بفاصل زمني قابل للضبط)؛ عند ظهور مدخل جديد تظهر لافتة؛ تتصل فقط بعناوين الاشتراك المحددة.
- **تذكيرات البريد (POP3)**: تسترجع رؤوس الرسائل دوريًا؛ عند وجود بريد جديد تظهر لافتة (تقرأ الرؤوس فقط، لا تنزّل المحتوى ولا ترفع بيانات؛ يُنصح باستخدام رمز تفويض).
- **المشغل السريع (بأسلوب Spotlight)**: يُفتح بـ `Ctrl+Space`، يبحث في التطبيقات المثبتة / برامج قائمة ابدأ أو يفتح عنوان URL مباشرة؛ الاختصار قابل للتخصيص.
- **لوحة سجل الحافظة**: `Ctrl+Alt+V` تفتح نافذة مستقلة للسجل؛ النقر على عنصر ينسخه مرة أخرى إلى الحافظة؛ يمكن مسحها؛ الاختصار قابل للتخصيص.
- **القواعد (الأتمتة)**: الإعدادات ← القواعد تجمع الشروط (دائمًا / بدون وسائط قيد التشغيل / قيد التشغيل / فترة زمنية / برنامج وسائط محدد) والإجراءات (إخفاء / طي قسري / إظهار قسري) للتحكم بالجزيرة تلقائيًا؛ الإخفاء له الأولوية، ثم الطي، وأخيرًا الإظهار القسري.
- **وضع توفير الطاقة**: يقلل معدل إطارات الموجات ويبسط الحركات أثناء الخمول لتوفير الطاقة (الإعدادات ← عام).

### P2 (مُنفذة)
- تبديل لغة الواجهة بين الصينية المبسطة والإنجليزية.
- تصدير / استيراد ملف إعدادات JSON.
- تكامل إشعارات Windows (Bluetooth / الاستيلاء على إشعارات النظام / أثناء التشغيل / انخفاض البطارية).
- قيد الانتظار: تنبيه المكالمات الواردة (غير منفذ)؛ تذكيرات الجدول منفذة (مكوّن + أداة إنتاجية).

---

## الاختيار التقني والأساس المنطقي

| الخيار | الخلاصة | الأساس المنطقي |
| --- | --- | --- |
| **C# + WPF (.NET 8)** | ✅ معتمد | استهلاك موارد منخفض وبدء سريع (مقابل WebView في Electron/Tauri)، قدرة أكبر على التكامل مع النظام (دعم أصلي SMTC/CoreAudio/علبة النظام)، تغليف بسيط في ملف واحد |
| C++ + Qt | ⚪ | كفاءة تطوير منخفضة، ترخيص معقد (LGPL)، يحتاج الكثير من التعليمات البرمجية المكتوبة يدويًا للتكامل مع مجموعة وسائط Windows |
| Tauri / Electron | ⚪ | استهلاك ذاكرة مرتفع (يصعب تحقيق <150 م.ب مقيمة)، بدء بطيء، لا يحقق شرط «استهلاك موارد منخفض وبدء سريع» |
| WinUI 3 | ⚪ | تغليف/نشر أكثر تعقيدًا من WPF (يتطلب Windows App SDK)، ودعم SMTC لتطبيقات سطح المكتب غير المغلفة مماثل لـ WPF |

**نقاط أساسية**:
- `Windows.Media.Control.GlobalSystemMediaTransportControlsSessionManager` متاح مباشرة عبر إسقاط Windows SDK في .NET 8 (CsWinRT)، دون الحاجة إلى هوية تغليف UWP.
- بصرف النظر عن إسقاطات WPF/WinForms/Windows SDK المضمنة في النظام، **صفر اعتماديات خارجية في وقت التشغيل** (انظر [THIRD_PARTY.md](THIRD_PARTY.md)).
- تأثير الأكريليك: في Win10 وWin11 معًا يُنفذ بـ `SetWindowCompositionAttribute` (`ACCENT_ENABLE_ACRYLICBLURBEHIND`) وتُقص الزوايا الدائرية بـ `SetWindowRgn` ليتبع التمويه شكل الكبسولة.

---

## نظرة عامة على البنية

```
src/WinIsland/
├── App.xaml(.cs)              # جذر التركيب: حالة واحدة، التقاط الاستثناءات، علبة النظام، دورة حياة النافذة
├── Services/
│   ├── MediaModels.cs         # نموذج موحد للقطة الوسائط (TrackInfo / MediaSnapshot)
│   ├── SmtcMediaProvider.cs   # جلسة الوسائط العامة في Windows (مدفوعة بالأحداث + إرسال بتقنين)
│   ├── CiderClient.cs         # غلاف واجهة Cider المحلية (V3 + LegacyV2، فحص المنافذ، تحليل متسامح)
│   ├── CiderMediaProvider.cs  # طبقة جلسة Cider (دورة حياة الاتصال)
│   ├── WindowTitleMediaProvider.cs # بديل: التعرف على عنوان النافذة
│   ├── MediaCoordinator.cs    # التوزيع المركزي: Cider > SMTC > عنوان النافذة، ذاكرة تخزين الأغلفة، صوت إضافي
│   ├── LrcParser.cs           # تحليل LRC (طوابع زمنية متعددة، إزاحة، صيغ مدة)
│   ├── LyricsService.cs       # تحليل الكلمات (.lrc محلي ← Cider ← عبر الإنترنت)
│   ├── OnlineLyricsService.cs # كلمات عبر الإنترنت (واجهات غير رسمية لـ NetEase/QQ Music، مفعّلة افتراضيًا بمفتاح بنقرة واحدة)
│   ├── ArtworkCache.cs        # تنزيل/تخزين الأغلفة (غلاف Cider البعيد ← ملف محلي)
│   ├── SystemVolume.cs        # صوت النظام CoreAudio (COM P/Invoke)
│   ├── AppSettings.cs         # قراءة/كتابة إعدادات JSON (%APPDATA%\WinIsland\settings.json)
│   ├── SingleInstance.cs      # Mutex مسمى + أنبوب مسمى (التشغيل الثاني يعرض الجزيرة)
│   ├── AutoStart.cs           # مفتاح بدء التشغيل التلقائي HKCU Run
│   ├── GlobalHotkeyService.cs # اختصارات عامة (Win32 RegisterHotKey)
│   ├── NotificationService.cs # لافتة إشعار زجاجية في الزاوية العلوية اليمنى
│   ├── NotificationHistoryService.cs # سجل الإشعارات (آخر 50، حفظ JSON)
│   ├── BluetoothMonitor.cs    # مراقبة اتصال/فصل أجهزة Bluetooth
│   ├── SystemNotificationMonitor.cs # الاستيلاء على إشعارات Windows (انعكاس عبر أتمتة الواجهة)
│   ├── MediaAppRegistry.cs    # تسجيل برامج الوسائط (تفعيل/تعطيل/ترتيب)
│   ├── AudioWaveService.cs    # موجات الصوت (أخذ عينات من صوت النظام، يدفع اهتزاز الموجات)
│   ├── KeyboardIndicatorMonitor.cs # مؤشرات لوحة المفاتيح (مراقبة حالة CapsLock)
│   ├── ClipboardHistoryService.cs # سجل الحافظة
│   ├── TodoService.cs         # قائمة المهام
│   ├── PomodoroService.cs     # مؤقت بومودورو
│   ├── ScheduleService.cs     # تذكيرات الجدول
│   ├── WeatherService.cs      # الطقس (Open-Meteo)
│   ├── RssService.cs          # تذكيرات RSS
│   ├── MailService.cs         # تذكيرات البريد POP3
│   ├── RuleEngine.cs          # محرك قواعد الأتمتة
│   ├── IslandViewModel.cs     # نموذج العرض الرئيسي (استيفاء التقدم، فهرس الكلمات، الرؤية)
│   ├── SettingsWindow.xaml(.cs) / SettingsViewModel.cs
│   ├── LyricsWindow.xaml(.cs) # نافذة كلمات مستقلة
│   ├── ThemeService.cs        # سمة فاتحة/داكنة + فرش ألوان المظهر
│   ├── WindowEffects.cs       # أكريليك / وضع داكن / منطقة زوايا دائرية
│   ├── ScreenHelper.cs        # شاشات متعددة + تحويل DPI PerMonitorV2
│   ├── TrayIcon.cs            # أيقونة وقائمة علبة النظام
│   ├── ClipboardPanelWindow.xaml(.cs) # لوحة سجل الحافظة
│   ├── QuickLauncherWindow.xaml(.cs)  # المشغل السريع (Ctrl+Space)
│   └── Localization.cs        # جدول نصوص صينية/إنجليزية
├── Diagnostics/DiagnosticsCommand.cs  # معلومات التشخيص --diagnose
tests/WinIsland.Tests/         # اختبارات وحدة xunit (تحليل LRC/الإعدادات/Cider/عنوان النافذة)
build/
├── publish.ps1                # نشر بنقرة واحدة (مستقل أو معتمد على الإطار + zip)
├── WinIsland.iss              # سكريبت تثبيت Inno Setup
└── make-icon.ps1 / IconGen.cs # أداة توليد الأيقونات
```

**تدفق البيانات**: `MediaCoordinator` يستعلم كل مزود مرة كل ثانية (غير متزامن، دون حجب الواجهة) ← يولّد `MediaSnapshot` موحدًا (مع مسار غلاف محلي وصوت) ← ينشره إلى `IslandViewModel` عبر Dispatcher ← المُدرج الزمني 200 مللي ثانية يحرك بسلاسة شريط التقدم وإبراز الكلمات ← عرض عبر ربط WPF.

---

## بدء سريع

> 📦 **إصدار مُجمّع مسبقًا**: مجلد `releases/` يوفر ملفات تنفيذية مستقلة ملفًا واحدًا لكل إصدار (مثل `releases/1.1.5/win-x64/WinIsland-1.1.5-win-x64.exe`، يتضمن وقت تشغيل .NET 8، انقر مرتين للتشغيل). النسخ التجريبية تُحفظ محليًا فقط؛ تُنشر النسخ المستقرة فقط على GitHub (تشمل نسخ محمولة win-x64 / win-arm64 والمثبّت الشامل).

### متطلبات البيئة
- Windows 10 1809+ / Windows 11
- جهاز الترجمة: SDK .NET 8 (أو SDK أحدث مع تحديد `net8.0-windows10.0.19041.0`)

### الترجمة
```powershell
# استعادة + ترجمة + اختبار
dotnet build WinIsland.slnx -c Release
dotnet test  WinIsland.slnx -c Release

# تشغيل (Debug)
dotnet run --project src\WinIsland -c Debug
```

### النشر بنقرة واحدة
```powershell
# مستقل (يتضمن وقت تشغيل .NET 8، بدون تثبيت، ~73 م.ب في ملف واحد)
.\build\publish.ps1

# معتمد على الإطار (حجم صغير، يتطلب .NET 8 Desktop Runtime)
.\build\publish.ps1 -FrameworkDependent
```
الملفات الناتجة في `publish\win-x64\` (تشمل `WinIsland.exe`)؛ الملف المضغوط هو `publish\WinIsland-win-x64.zip`.

### المثبّت (اختياري)
بعد تثبيت [Inno Setup 6](https://jrsoftware.org/isinfo.php):
```powershell
iscc.exe build\release-1.1.5.iss
```
ينتج `releases\<version>\WinIsland-Setup-<version>.exe` (مثبّت شامل، متوافق مع x64 وARM64، يثبت تلقائيًا حسب البنية). عند نشر إصدار مستقر، انسخ `release-<version>.iss` إلى `build\` وحدّث رقم الإصدار.

---

## الاستخدام

1. شغّل `WinIsland.exe` (أو فعّل بدء التشغيل التلقائي / علّم بدء التشغيل التلقائي في المثبّت). تظهر الأيقونة في علبة النظام.
2. شغّل أي موسيقى:
   - موسيقى نيت إيز، QQ Music، Spotify، Apple Music الرسمي وغيرها ← تظهر تلقائيًا عبر جلسة وسائط النظام؛
   - Cider ← انظر [التكامل مع Cider](#التكامل-مع-cider)؛
   - مشغلات أخرى ← بديل التعرف على عنوان النافذة (عرض فقط).
3. **انقر** على الجزيرة لتوسيع البطاقة الكاملة (المرور فوقها لا يوسعها): سحب التقدم (seek)، التحكم في التشغيل، الصوت، والكلمات المتزامنة؛ انقر مرة أخرى للطي (بعد مغادرة البطاقة، طي تلقائي بعد 700 مللي ثانية).
4. قائمة علبة النظام: إظهار/إخفاء، نافذة كلمات مستقلة، بدء التشغيل مع Windows، **عدم الإزعاج** (العلامة تكتم الإشعارات)، **البحث عن التحديثات**، **عرض السجلات**، الإعدادات والخروج. **إغلاق النافذة الرئيسية لا يغلق العملية** (يقلص فقط إلى علبة النظام).
5. الاختصارات العامة: `Ctrl+Alt+P` تشغيل/إيقاف · `Ctrl+Alt+←/→` سابق/تالٍ · `Ctrl+Alt+I` إظهار/إخفاء · `Ctrl+Alt+Space` توسيع/طي · `Ctrl+Space` المشغل السريع (البحث عن التطبيقات / كتابة عنوان URL والضغط على Enter) · `Ctrl+Alt+V` لوحة سجل الحافظة (كلها قابلة للتعطيل / التخصيص).
6. الإشعارات والتنبيهات (Bluetooth / إشعارات Windows / أثناء التشغيل / انخفاض البطارية) تظهر افتراضيًا كلافتات زجاجية في الزاوية العلوية اليمنى؛ يمكن تفعيل/تعطيلها في الإعدادات ← الإشعارات؛ مع تفعيل **عدم الإزعاج** لا تظهر لافتات (عدّاد الشارة يستمر في العد).
7. معاملات سطر الأوامر الشائعة:
   ```powershell
   WinIsland.exe --demo       # وضع العرض التوضيحي (معاينة الواجهة + كلمات مثال بدون وسائط)
   WinIsland.exe --diagnose   # ينشئ تقرير تشخيص في %APPDATA%\WinIsland\diagnostics.txt
   WinIsland.exe --settings   # يفتح الإعدادات عند بدء التشغيل
   ```

---

## مرجع الإعدادات

ملف الإعدادات: `%APPDATA%\WinIsland\settings.json` (JSON؛ تغييرات واجهة الإعدادات تُطبق فورًا؛ قابل للتصدير/الاستيراد).

| المفتاح | الافتراضي | الوصف |
| --- | --- | --- |
| `Language` | `zh-CN` | `zh-CN` / `en-US` |
| `ThemePreset` | `Default` | المظهر: `Default/Ocean/Forest/Sunset/Neon/Mono/Grape/Sky/Rose/Amber/Lime/Teal/Lavender/Crimson/Midnight/Coffee/Sakura/Aurora/Custom` (يلغي AccentColor) |
| `FontFamily` | `Segoe UI` | خط الواجهة |
| `FontScale` | `1.0` | مقياس الخط 0.8–1.4 |
| `CornerRadius` | `28` | نصف قطر زوايا الكبسولة 16–40 |
| `BadgeEnabled` | `true` | شارة الإشعارات غير المقروءة (نقطة حمراء + رقم في الزاوية العلوية اليمنى) |
| `CoverTintBackground` | `true` | الخلفية الموسعة تأخذ لون غلاف الألبوم |
| `WaveVisualizerEnabled` | `true` | موجات صوتية يسار أزرار التحكم عند تشغيل الوسائط |
| `WaveStyle` | `Bars` | نمط الموجة: `Bars` (أعمدة) / `Spectrum` (طيف) / `Ring` (حلقة) / `Particles` (جسيمات) |
| `WaveSyncEnabled` | `true` | الموجات تتبع إيقاع الموسيقى (مدفوعة بصوت إخراج النظام) |
| `WaveSensitivity` | `1.0` | حساسية الموجات 0.2–3.0 |
| `WaveHeight` | `1.0` | ارتفاع الموجات 0.4–1.6 |
| `WallpaperThemeColorEnabled` | `false` | لون الخلفية: يستخرج اللون الرئيسي من الخلفية الحالية كلون للمظهر (محلي بحت) |
| `MarqueeTextEnabled` | `false` | نص متحرك: انزلاق أفقي تلقائي عندما يكون العنوان/الكلمات أعرض من اللازم |
| `EdgeSnapEnabled` | `true` | عند إفلات السحب غير المقفل، يلتصق تلقائيًا بحافة/مركز الشاشة |
| `FullScreenAutoHideEnabled` | `true` | إخفاء تلقائي للجزيرة في ملء الشاشة (فيديو/لعبة/عرض تقديمي) |
| `RecordingDndEnabled` | `false` | عدم إزعاج تلقائي عند تسجيل الشاشة (بدون لافتات إشعار) |
| `Theme` | `Auto` | `Auto` / `Light` / `Dark` |
| `AccentColor` | `#6C5CE7` | لون المظهر (#RRGGBB) |
| `Position` | `Center` | `Center` مركزية أعلى / `Right` أعلى اليمين |
| `Monitor` | `Primary` | `Primary` الشاشة الرئيسية / `All` كل الشاشات / `Index` شاشة محددة |
| `MonitorIndex` | `0` | رقم الشاشة عندما `Monitor=Index` |
| `OffsetX` / `OffsetY` | `0` / `16` | الإزاحة بالبكسل |
| `Opacity` | `0.92` | 0.3–1.0 |
| `HideWhenNoMedia` | `true` | إخفاء الجزيرة بدون تشغيل |
| `ShowWhenPaused` | `true` | الاستمرار في العرض عند الإيقاف |
| `StartWithWindows` | `false` | بدء التشغيل مع Windows |
| `StartHidden` | `false` | مخفية عند بدء التشغيل |
| `CompactShowArt/Title/Progress` | `true/true/false` | محتوى الوضع المدمج |
| `IslandAlwaysVisible` | `false` | جزيرة دائمة (تعرض المكونات حتى بدون وسائط) |
| `ShowMediaInfo` | `true` | يعرض معلومات التشغيل (العنوان/الغلاف/الكلمات وغيرها) |
| `ReduceMotion` | `false` | يقلل التأثيرات الحركية (يعطل حركات الزنبرك؛ إمكانية الوصول/توفير الطاقة) |
| `GlobalHotkeysEnabled` | `true` | مفتاح الاختصارات العامة |
| `LowBatteryThreshold` | `20` | حد تنبيه انخفاض البطارية (٪)، 0 للتعطيل |
| `ExpandedShowArtTitle/Progress/Controls/Lyrics` | `true` | مفاتيح أقسام البطاقة الموسعة (غلاف+عنوان / شريط تقدم / تحكم وصوت / كلمات) |
| `Components` | كائن | اختيار المكونات: `Time/Weather/Date/Cpu/Gpu/Ram/Net/Battery/Volume/CapsLock/Clipboard/Todo/Timer/Schedule/Holiday/Meeting/Mic/Cam`، كل منها بعمودين `WhenIdle`/`WhenPlaying`؛ `Cover/Title/Artist/Lyrics/Progress` تظهر أثناء التشغيل؛ قاموس `ComponentBadges` يملأ نص شارة كل مكون |
| `WidgetOrder` | `Time,Weather,...` | ترتيب المكونات (مفاتيح مفصولة بفواصل، تشمل `Song`) |
| `MediaApps` | `[]` | تفعيل/تعطيل وأولوية برامج الوسائط (فارغ = الكل مفعل) |
| `CompactWidth` / `CompactHeight` | `360` / `72` | العرض/الارتفاع المدمجان (الضبط اليدوي بالسحب يعطل الضبط التلقائي) |
| `CompactWidthAuto` / `CompactHeightAuto` | `true` | الحجم المدمج يضبط تلقائيًا على المحتوى (مفعل افتراضيًا) |
| `ExpandedWidthAuto` / `MaxExpandedHeightAuto` | `true` | ضبط تلقائي للحجم الموسع (مفعل افتراضيًا) |
| `ExpandedWidth` / `MaxExpandedHeight` | `400` / `384` | العرض الموسع / أقصى ارتفاع موسع |
| `BluetoothNotifyEnabled` | `false` | تنبيه اتصال/فصل Bluetooth |
| `NotificationTakeoverEnabled` | `false` | الاستيلاء على إشعارات Windows (best effort) |
| `NotificationTimeoutSeconds` | `6` | مدة لافتة الإشعار (بالثواني) |
| `NotificationPosition` | `TopRight` | موضع الإشعارات (الزاوية العلوية اليمنى) |
| `DoNotDisturbEnabled` / `DoNotDisturbManual` | `false` | عدم الإزعاج: تلقائي حسب الفترات / يدوي |
| `DoNotDisturbStartHour` / `DoNotDisturbEndHour` | `22` / `8` | فترة عدم الإزعاج (بالساعات) |
| `DnDAllowlist` | `[]` | القائمة البيضاء لعدم الإزعاج (`QQ.exe,WeChat.exe`؛ داخل القائمة البيضاء تظهر الإشعارات) |
| `Rules` | `[]` | قائمة قواعد الأتمتة (شرط + إجراء) |
| `ClipboardHistoryEnabled` / `ClipboardHistoryMax` | `false` / `15` | مفتاح سجل الحافظة والعدد الأقصى للعناصر |
| `PomodoroEnabled` / `PomodoroWorkMinutes` / `PomodoroBreakMinutes` | `false` / `25` / `5` | مفتاح بومودورو ومدة العمل/الراحة (بالدقائق) |
| `KeyIndicatorSeconds` | `3` | مدة مؤشر لوحة المفاتيح (CapsLock) (بالثواني) |
| `VolumeTempIndicatorEnabled` / `VolumeTempIndicatorSeconds` | `true` / `4` | مفتاح ومدة المؤشر المؤقت للصوت/الكتم على الجزيرة |
| `FileCopyNotifyEnabled` | `true` | نسخ/نقل الملفات على الجزيرة (تعرف محلي على عنوان النافذة) |
| `DownloadProgressEnabled` | `false` | تنزيل قيد التقدم على الجزيرة (يفحص الملفات المؤقتة في مجلد التنزيلات؛ معطل افتراضيًا) |
| `UsageMergeEnabled` / `UsageMergeItems` | `false` / `Mic,Cam,Meeting,Recording` | كبسولة «قيد الاستخدام» المجمعة والمكونات المشاركة (معطلة افتراضيًا) |
| `AutoUpdateCheck` | `false` | فحص تلقائي للإصدارات الجديدة من GitHub (معطل افتراضيًا، يتطلب اتصالًا) |
| `DoubleClickAction` | `PlayPause` | إجراء النقر المزدوج: `PlayPause` / `ToggleExpand` / `ShowDesktop` / `ToggleVisible` / `NextTrack` / `PrevTrack` / `OpenSettings` / `None` |
| `AnimationStyle` | `Spring` | جلد الحركة: `Spring` / `Soft` / `Elastic` / `Fade` |
| `ThemeTint` | `""` | لون خلفية مخصص #RRGGBB (يُطبق عندما يكون المظهر Custom) |
| `ExpandedCardStyle` | `Classic` | قالب البطاقة الموسعة: `Classic` / `Hero` |
| `NetCurveEnabled` | `true` | مكوّن الشبكة يعرض المنحنى المصغر لآخر 32 ثانية |
| `LowPowerMode` | `false` | وضع توفير الطاقة (يقلل معدل إطارات الموجات ويبسط الحركات أثناء الخمول) |
| `MeetingAssistantEnabled` | `false` | مساعد كتم الصوت في الاجتماعات: يكشف نوافذ الاجتماع + عدم إزعاج تلقائي |
| `MeetingAutoDnd` / `MeetingKeywords` | `true` / `""` | عدم إزعاج تلقائي في الاجتماعات / كلمات مفتاحية مخصصة للاجتماعات |
| `ScreenCaptureNotifyEnabled` / `ScreenshotNotifyEnabled` / `RecordingNotifyEnabled` | `false` / `true` / `true` | المفتاح العام والعناصر الفرعية لتنبيهات اللقطة/التسجيل |
| `CalendarEnabled` / `CalendarIcsPath` / `CalendarAdvanceMinutes` | `false` / `""` / `10` | مفتاح تذكيرات تقويم .ics / مسار الملف / الدقائق المسبقة |
| `RssNotifyEnabled` / `RssUrls` / `RssIntervalMinutes` | `false` / `""` / `15` | تذكيرات RSS / عناوين الاشتراك / فترة الاستعلام (بالدقائق) |
| `MailNotifyEnabled` / `MailPop3Server` / `MailPop3Port` / `MailUseSsl` / `MailUser` / `MailPassword` / `MailCheckMinutes` | `false` / `""` / `995` / `true` / `""` / `""` / `5` | تذكيرات البريد (POP3): مفتاح، خادم، منفذ، SSL، حساب، رمز تفويض وفترة الفحص |
| `QuickLauncherEnabled` / `HotkeyLauncher` | `true` / `Ctrl+Space` | مفتاح المشغل السريع والاختصار |
| `ClipboardPanelEnabled` / `HotkeyClipboardPanel` | `true` / `Ctrl+Alt+V` | مفتاح لوحة سجل الحافظة والاختصار |
| `HotkeyExpand` | `Ctrl+Alt+Space` | اختصار التوسيع/الطي |
| `NotifyFoldEnabled` | `true` | طي الإشعارات المتشابهة (نفس المصدر ونفس العنوان يعرض واحدًا فقط) |
| `ActiveProfile` | `Default` | اسم ملف الإعدادات (التبديل بين عدة مجموعات) |
---

## التكامل مع Cider

Cider (عميل تابع لجهة خارجية لـ Apple Music) يوفر واجهة HTTP محلية. WinIsland تغلف بالفعل وحدة مستقلة (`CiderClient.cs`) تتكيف تلقائيًا مع اختلافات الإصدارات.

**خطوات التفعيل (مهم)**:
1. افتح Cider: **الإعدادات ← الاتصال ← السماح بالتحكم الخارجي (Manage External Application Access)**؛ عند التفعيل، سيعرض Cider رمز الواجهة (إذا كان فارغًا، انقر لتوليده).
2. انسخ الرمز إلى **إعدادات WinIsland ← Cider ← API Token** واحفظ.
3. المنفذ الافتراضي هو `10767` وتكتشفه WinIsland تلقائيًا؛ RPC القديم هو `10769`.

> 🟡 إصدارات Cider 2.x الأحدث تتطلب **رمزًا في جميع طلبات الواجهة افتراضيًا** (بدون رمز تعيد `403 UNAUTHORIZED_APP_TOKEN`). إذا أشارت سجلات التشخيص إلى الحاجة إلى رمز، املأه باتباع الخطوات أعلاه؛ وإلا فلن تتوفر كلمات/أزرار Cider (يمكن عرض الأغاني عبر SMTC).

> 🟡 إذا أظهرت السجلات HttpClient.Timeout متكررًا (كان 2 ثانية في الأصل)، فعادةً بسبب برنامج أمان/وكيل محلي يعترض HTTP للحلقة المحلية (الاستجابة الفعلية لـ Cider حوالي 30 مللي ثانية). منذ 1.0.1، مُدد مهلة قراءة البيانات إلى 5 ثوانٍ؛ إذا استمر انتهاء المهلة، تحقق مما إذا كان برنامج مكافحة الفيروسات يحظر اتصال WinIsland بالشبكة.

**قدرات الواجهة المنفذة** (وفقًا لوثائق مجتمع Cider / حزمة `cider-api` المفحوصة، إصدار 2026):
- `GET /api/v1/playback/active`، `GET /now-playing` (الأغنية/الغلاف/التقدم/الحالة)
- `POST /api/v1/playback/play|pause|playpause|next|previous|seek`
- `GET|POST /api/v1/playback/volume`
- `GET /api/v1/lyrics` (مع بديل `?id=`)
- ترويسة المصادقة: `apptoken` (متوافقة مع `apitoken`)
- القديم 10769: `/active`, `/currentPlayingSong`, `/playPause`, `/next`, `/previous`, `/seekto/{t}`, `/audio`

> 🟡 واجهة Cider غير رسمية وتتغير بسرعة؛ جميع الطلبات لها مهلة ثانيتين، وعند الفشل تتحول تلقائيًا إلى SMTC / عنوان النافذة، **دون التأثير على التدفق الرئيسي**. حافظ على تحديث WinIsland للتكيف مع الإصدارات الجديدة.

---

## الكلمات

الأولوية:
1. **`.lrc` محلي**: يُبحث عنه كـ `الأغنية.lrc` / `الفنان - الأغنية.lrc` في مجلدات الكلمات (افتراضيًا `%APPDATA%\WinIsland\Lyrics` و`موسيقى\Lyrics` وجذر `موسيقى`)؛
2. **كلمات كاريوكي حرفًا بحرف AMLL** (خط زمني TTML حرفًا بحرف من مكتبة أغاني amll.dev، مفعّل افتراضيًا)؛
3. **واجهة كلمات Cider** (عندما يكون المصدر Cider)؛
4. **الكلمات عبر الإنترنت** (واجهات غير رسمية لـ NetEase / QQ Music): **مفعّلة افتراضيًا**؛ بالنقر بزر الماوس الأيمن على الجزيرة يمكن التبديل بنقرة واحدة، أو التعطيل في الإعدادات.

> 🟡 الكلمات عبر الإنترنت تستخدم واجهات غير رسمية وهي للتعلم الشخصي فقط؛ احترم حقوق النشر؛ إذا طلب صاحب الحقوق ذلك، يمكنك تعطيل هذه الميزة في أي وقت (دون اتصال تمامًا بعد التعطيل).

---

## الإشعارات والتنبيهات (منذ 1.0.2، محسّنة في 1.0.3)

جميع الإشعارات **لافتات زجاجية في الزاوية العلوية اليمنى**، مع حركة دخول بأسلوب macOS (دخول من اليمين + تلاشي) وخروج؛ مدة العرض قابلة للضبط (3–15 ثانية).

- **تنبيه اتصال Bluetooth**: الإعدادات ← الإشعارات؛ عند التفعيل، يظهر عند اتصال/فصل جهاز Bluetooth.
- **الاستيلاء على إشعارات Windows**: الإعدادات ← الإشعارات؛ عند التفعيل، يعكس عبر أتمتة الواجهة (best effort) محتوى مركز الإشعارات (مثل إشعارات QQ) في لافتات الزاوية العلوية اليمنى.
  > 🟡 لا توفر Windows واجهة عامة «لاعتراض إشعارات التطبيقات الأخرى»؛ هذه الميزة best effort وقد لا تُلتقط بعض الإشعارات؛ لا تؤثر على التدفق الرئيسي.
- **إشعار أثناء التشغيل**: عند تغيير الأغنية تظهر تلقائيًا لافتة «قيد التشغيل - العنوان» (منذ 1.0.3).
- **تنبيه انخفاض البطارية**: يظهر عندما تنخفض البطارية عن الحد (الافتراضي 20%، قابل للضبط 0–100)، مرة واحدة لكل دورة شحن (منذ 1.0.3).
- **سجل الإشعارات**: سجلات آخر 50 إشعارًا؛ صفحة الإعدادات ← الإشعارات تتيح عرضها/مسحها (منذ 1.0.3).
- **ضبط حجم الجزيرة**: الإعدادات ← المظهر يتيح ضبط الطول/العرض المدمجين والطول الموسع.

---
## واجهة برمجة الجزيرة (الإرسال إلى الجزيرة الديناميكية من تطبيقات أخرى)

تتضمن WinIsland خدمة HTTP محلية؛ يمكن للتطبيقات الأخرى إرسال معلومات إلى الجزيرة الديناميكية في الوقت الفعلي (مشابهة لتكامل تطبيقات الطرف الثالث في الجزيرة الديناميكية في iOS). **وثائق المطورين في [docs/IslandAPI.md](docs/IslandAPI.md)**.

| الواجهة | الوصف |
|---|---|
| `POST /v1/island/push` | يرسل/يحدّث بطاقة الجزيرة (منذ v3 يدعم الصور / التقدم الديناميكي / النبض) |
| `PATCH /v3/island/push/{id}` | تحديث جزئي: يكتب فقط الحقول الموجودة في الجسم (يحتفظ بانتهاء الصلاحية / الموضع في قائمة الانتظار) |
| `DELETE /v1/island/push/{id}` | يحذف بطاقة |
| `GET /v1/island/active` (أو `/v3/island/active`) | يستعلم عن البطاقة النشطة الحالية |
| `GET /v3/ws` | قناة WebSocket ثنائية الاتجاه: يرسل العميل `push/update/remove/ping`، ويبث الخادم أحداث `push_updated/push_removed` |
| `GET /v1/health` | فحص الصحة |

- الإعدادات ← واجهة برمجة الجزيرة: مفتاح التفعيل، المنفذ (الافتراضي 9840)، رمز اختياري، ومدة العرض الافتراضية العامة
- إشعارات الجزيرة **لا تغير طول/عرض الجزيرة**؛ تظهر البطاقة في سطر واحد في الوضع المدمج ولا تخفي المكونات الأخرى
- تدعم الأزرار «فتح رابط / تشغيل برنامج»؛ يمكن للمرسل تخصيص مدة العرض لكل إدخال (يلغي الافتراضي العام)
- جديد في v3: `image` (صورة data URI أو http)، `progress_from/progress_to/progress_duration_seconds` (تقدم تلقائي)، `heartbeat_seconds` (تجديد بالنبض؛ إذا لم يُجدد لأكثر من ضعفي الفاصل، يُحذف تلقائيًا)، `theme` (سمة البطاقة dark/light/auto)، `action: "command"` (الزر ينفذ أمرًا محليًا)؛ الوثائق الكاملة في [docs/IslandAPI.md](docs/IslandAPI.md)

---
## استعادة حالة التشغيل

- عند الخروج من التطبيق أو الإيقاف المؤقت أو تغيير الأغنية، يُحفظ «الأغنية + موضع التشغيل» في `%APPDATA%\WinIsland\state.json` (محلي فقط).
- عند بدء التشغيل التالي، إذا كانت الأغنية نفسها ولم يُعد المشغل التقدم الفعلي بعد، يُستعاد آخر موضع أولًا لتجنب قفزة «عرض السطر 0 ثم القفز إلى عبارة الإيقاف»؛ لا يُستعاد بعد أكثر من ساعة أو إذا تغيرت الأغنية.

---
## الخصوصية والأمان

- **بدون تتبع، بدون إعلانات، بدون إرسال**. بصرف النظر عن «الكلمات عبر الإنترنت» التي يفعّلها المستخدم يدويًا، لا يقوم التطبيق بأي طلب شبكة.
- **مكوّن الطقس**: فقط عند تفعيل «عرض الطقس» وكتابة مدينة يستعلم Open-Meteo (مجاني، بدون مفتاح، بدون حساب) عن الطقس الحالي؛ إذا لم يكن مفعلاً، يعمل دون اتصال تمامًا.
- سيناريوهات الاتصال الوحيدة: تنزيل أغلفة Cider (`mzstatic.com`، عنوان غلاف عام تعيده الواجهة المحلية) والكلمات عبر الإنترنت التي يفعّلها المستخدم.
- جميع البيانات تُخزن محليًا في `%APPDATA%\WinIsland\`.
- السجلات تحفظ معلومات تشغيل محلية فقط (`logs\app-*.log`).

---

## المؤشرات غير الوظيفية

مقاسة على جهاز الاختبار (Windows 11 24H2، 2560×1440 بنسبة 100%) (إصدار مستقل):

| المؤشر | المقاس | الهدف |
| --- | --- | --- |
| المعالج عند الخمول (بدون وسائط) | < 0.5% (0.3% مقاس في Debug) | ≤ 0% |
| الذاكرة المقيمة (Private) | ~72 م.ب | ≤ 150 م.ب |
| بدء التشغيل | < 1 ثانية (على البارد) | ≤ 2 ثانية |
| إغلاق النافذة الرئيسية | لا يخرج، يقلص فقط إلى علبة النظام | ✅ |
| تعدد الحالات | حالة واحدة فقط؛ التشغيل الثاني يعرض الجزيرة | ✅ |
| الاستثناءات | التقاط موحد وتسجيل في ملف، بدون صندوق تجميد | ✅ |

> ملاحظة: WorkingSet للنسخة المستقلة (يشمل الصفحات المشتركة لوقت تشغيل .NET) حوالي 160 م.ب، لكن **الذاكرة الخاصة (Private) حوالي 72 م.ب**؛ مع نسخة معتمدة على الإطار سيكون WorkingSet أصغر.

---

## القيود المعروفة

- **كاريوكي حرفًا بحرف يعتمد على مصدر الكلمات والتقدم**: عند وجود خط زمني TTML/LRC حرفًا بحرف من AMLL، يُضاء حرفًا بحرف؛ بدون خط زمني حرفًا بحرف أو إذا لم يوفر المشغل التقدم الفعلي، يتدهور إلى إبراز العبارة كاملة (يتقدم بالساعة المحلية).
- **تقدم يتراجع أحيانًا** (مثل أن يبلغ Cider/SMTC عن 0 أو موضع منتهي في لحظة): تم تنفيذ حماية للموضع: التراجع اللحظي يُتجاهل ويُحافظ على التقدم الحالي، دون إعادة الكلمات/شريط التقدم إلى البداية؛ فقط بعد تراجع مستمر نحو 4 ثوانٍ يُعتبر إعادة تحديد حقيقية أو سحب للمشغل.
- **تنبيه المكالمات الواردة**: غير منفذ (اختياري في P2). المنفذ: تنبيه Bluetooth، الاستيلاء على إشعارات Windows (best effort)، إشعار أثناء التشغيل، تنبيه انخفاض البطارية، وتذكيرات الجدول.
- **تغطية SMTC**: تعتمد على تسجيل المشغل لجلسة الوسائط العامة؛ بعض المشغلات القديمة التي لا تسجلها لا يمكن تغطيتها إلا بعنوان النافذة (بدون أزرار تحكم).
- **Cider 1.x (واجهة قديمة، المنفذ 9000)**: غير متكيف؛ يدعم فقط 2.x وما فوق.

---

## دليل التحقق (يتطلب اختبارًا مع مشغل وسائط حقيقي)

السيناريوهات التالية تتطلب تحققًا في بيئة حقيقية (المشار إليه يُظهر ما تم التحقق منه تلقائيًا في بيئة التطوير لهذا المستودع):

| السيناريو | الحالة |
| --- | --- |
| تعداد جلسات SMTC (مع `--diagnose` تظهر قائمة الجلسات) | ✅ تم الاختبار (يكتشف جلسات حقيقية مثل Bilibili) |
| إظهار/إخفاء تلقائي للجزيرة، توسيع/طي بالنقر، استيفاء التقدم | ✅ تم الاختبار (عرض توضيحي + جلسة حقيقية موقوفة) |
| تشغيل/إيقاف/تبديل/سحب (مشغل حقيقي) | 🟡 يتطلب اختبارًا (مسارات التعليمات البرمجية تقابل مباشرة واجهة تحكم SMTC) |
| الاتصال بواجهة Cider والتحكم بها | 🟡 يتطلب تثبيت Cider على الجهاز وتفعيل التحكم الخارجي |
| انزلاق كلمات .lrc المحلية المتزامن | ✅ تحليل LRC مختبر بالوحدات؛ النهاية إلى النهاية تتطلب أغنية حقيقية |
| الكلمات عبر الإنترنت | ✅ متكامل مع NetEase/QQ Music؛ تأثير النهاية إلى النهاية يتطلب أغنية حقيقية |

**خطوات التحقق المقترحة**:
1. `WinIsland.exe --diagnose` ← أكد أن `System media sessions` يسرد المشغل؛
2. شغّل أي أغنية من NetEase/QQ Music/Spotify ← يجب أن تعرض الجزيرة الأغنية وتتيح التحكم؛
3. افتح Cider وفعّل التحكم الخارجي ← يجب أن يعرض مصدر الجزيرة `Cider`، مع سحب/صوت؛
4. ضع `.lrc` بنفس الاسم في مجلد الأغنية ← عند التوسيع، يجب أن تنزلق الكلمات وتُبرز مع التقدم.

---

## الأسئلة الشائعة

**س: الجزيرة الديناميكية لا تظهر؟**
- أكد وجود تشغيل (عند الإيقاف تستمر في العرض افتراضيًا)؛ `HideWhenNoMedia` مفعل افتراضيًا؛ الإخفاء بدون وسائط أمر طبيعي.
- نفّذ `--diagnose` لمشاهدة قائمة الجلسات؛ إذا كانت القائمة فارغة، لم يسجل المشغل SMTC.

**س: Cider يعرض «غير متصل»؟**
- أكد أن «السماح بالتحكم الخارجي» مفعل في إعدادات Cider؛ تحقق من المنفذ (الافتراضي 10767)؛ أكد أن Cider مفعل في إعدادات WinIsland.

**س: الكلمات عبر الإنترنت لا تُحمَّل؟**
- الكلمات عبر الإنترنت مفعلة افتراضيًا (نقرة بزر الماوس الأيمن على الجزيرة ← كلمات عبر الإنترنت للتبديل بنقرة واحدة)؛ إذا لم تظهر بعد، أكد في الإعدادات ← الكلمات أنها مفعلة وتحقق من اتصال الشبكة.

**س: أيقونة علبة النظام تبقى بعد الخروج؟**
- قائمة علبة النظام ← خروج؛ إغلاق نافذة الجزيرة مباشرة يخفيها فقط (حسب تصميم «الإقامة في علبة النظام»).

---

## الترخيص مفتوح المصدر

- التطبيق: MIT (انظر [LICENSE](LICENSE))
- مكونات الطرف الثالث: انظر [THIRD_PARTY.md](THIRD_PARTY.md)

---

## Русский

## WinIsland – Динамический остров для Windows

> Перенесите Динамический остров из iOS на Windows: плавающее окно на рабочем столе с управлением медиа, синхронизированными текстами песен, настраиваемыми компонентами, центром уведомлений и постоянным проживанием в трее.
> На базе **.NET 8 + WPF**, совместимо с Windows 11 (также Windows 10, 1809+).

---

> **Перенесите Динамический остров из iOS на Windows | Современный многофункциональный Динамический остров.**

Перенесите Динамический остров из iOS на Windows 11 / 10: управление воспроизведением медиа, караоке-тексты посимвольно, настраиваемые компоненты, центр уведомлений и API острова — всё в одной капсуле. На базе **.NET 8 + WPF**, бесплатно и с открытым исходным кодом (MIT), **без рекламы · без телеметрии**.

🌐 **Веб-сайт: https://WinIsland.JudeKwong.com**

---

## ✨Основные возможности

- **▶️ Управление воспроизведением медиа**: нативно подключается к глобальным медиа-сессиям Windows (SMTC), совместимо с NetEase Music, QQ Music, Spotify, Apple Music, Groove, «Фильмы и ТВ» и др.; дополнительно поддерживает локальный API Cider; если подключиться не удаётся, в качестве запасного варианта используется заголовок окна. Обложка альбома, перетаскивание прогресса (seek), воспроизведение/пауза/переключение трека — всё включено; при нескольких открытых плеерах источник управления можно переключить одним кликом; клик по обложке открывает иммерсивный предпросмотр на весь экран.
- **♪ Караоке-тексты посимвольно**: развёрнутая карточка плавно прокручивается и подсвечивает текст синхронно, зажигая символы один за другим в стиле караоке; три уровня источников текста: локальный `.lrc` → интерфейс текста плеера → необязательные онлайн-тексты; двуязычные тексты, переключатель перевода, копирование текущей строки одним кликом; время текста можно точно подстроить для каждой песни, а отдельное окно текста позволяет настроить прозрачность и блокировку.
- **🧩 Система настраиваемых компонентов**: часы, погода, дата (с лунным календарём / солнечными терминами), ЦП/ГП/память/диск, скорость сети, батарея, метод ввода, быстрые переключатели (WiFi/Bluetooth/ночной режим/без звука) и более 30 компонентов; каждый компонент может иметь собственную иконку, выбор флажками и порядок перетаскиванием, с режимом одной или нескольких строк в любой момент.
- **🔗 API острова**: локальный интерфейс HTTP / WebSocket, позволяющий любому стороннему программному обеспечению отправлять информацию на Динамический остров в реальном времени (аналогично интеграции сторонних приложений в Динамический остров iOS). v3 поддерживает изображения, динамический прогресс, продление пульсом (heartbeat) и светлую/тёмную тему карточки; уведомления не изменяют ширину/высоту острова и не скрывают другие компоненты; кнопки поддерживают открытие ссылок / запуск программ / выполнение локальных команд, а клик по кнопке notify может вызвать отправителя через WebSocket.
- **🔂 Центр уведомлений**: стеклянные баннеры в правом верхнем углу с анимациями скольжения в стиле macOS: устройства Bluetooth, оповещения о голосовых/видеозвонках WeChat/QQ, перехват системных уведомлений, воспроизведение, низкий заряд/завершение зарядки, офлайн/восстановление; история уведомлений, сворачивание, белый список «Не беспокоить» и автоматизация по правилам; баннеры могут содержать кнопки действий (например, «Отключить» и «Настройки» для Bluetooth).
- **✨ Внешний вид и эффекты**: 18 тем, настраиваемые акцентный цвет и фон, матовое жидкое стекло, **цвет, извлечённый из обоев** (автоматически извлекает цвет темы из текущих обоев), **бегущая строка** (длинные тексты/песни прокручиваются горизонтально), 4 стиля анимации (пружина iOS и др.), **4 стиля звуковых волн** (столбцы / спектр / кольцо / частицы, вибрирующие в такт музыке); анимации разворачивания/сворачивания с нелинейным easing при 60 fps; извлечённый из обложки фон может медленно «дышать» (динамическая тема); высокий DPI PerMonitorV2, без смещений при 120/150/200 %.
- **🧠 Взаимодействие и интеллект**: разблокировка и перетаскивание + **прилипание к краю** (прилипает к краю/центру при отпускании), **автоскрытие в полноэкранном режиме** (сворачивается при полноэкранном воспроизведении видео/игры/презентации), настраиваемые действия по двойному клику, **кнопки быстрых действий** (блокировка экрана/без звука/воспроизведение-пауза/снимок/показать рабочий стол и др., с настраиваемым порядком), **перетаскивание файлов на остров**, **умный режим «Не беспокоить» при записи экрана**; мгновенный отклик на нажатие мыши (клик имеет приоритет для разворачивания/сворачивания).
- **⚙️ Инструменты продуктивности и автоматизации**: Pomodoro, список задач, история буфера обмена, быстрый лаунчер, напоминания о событиях; помощник беззвучия на собраниях, оповещения о записи/снимке экрана, прогресс копирования/загрузки файлов на острове; глобальные горячие клавиши и движок правил (автоматическое показать/скрыть по условиям).
- **🔒 Конфиденциальность и безопасность**: без телеметрии, без рекламы, без отправки данных. Полностью офлайн, кроме онлайн-текстов и погоды, которые пользователь включает вручную; все настройки и данные сохраняются только локально в `%APPDATA%\WinIsland`.

---

## 📥Загрузка (последняя стабильная версия 1.1.5)

| Платформа | Загрузка | Описание |
| --- | --- | --- |
| Windows x64 | [Портативная версия x64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-x64.exe) | Основной вариант для 64-битных ПК; один файл, без установки, запускается напрямую |
| Windows ARM64 | [Портативная версия ARM64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-arm64.exe) | Устройства ARM, такие как Surface Pro X / Snapdragon; один файл, без установки |
| Windows универсальный | [Универсальный установщик](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-Setup-1.1.5.exe) | Мастер установки Inno Setup; устанавливает автоматически по архитектуре x64 / ARM64 |

Все предыдущие версии и полный журнал изменений находятся в [GitHub Releases](https://github.com/DMP-Pig/WinIsland/releases).

---

## 📊Показатели производительности

| Показатель | Значение |
| --- | --- |
| Постоянная память (Private) | ~72 МБ |
| Холодный запуск | < 1 с |
| ЦП в простое | ≤ 0 % |
| Кадры анимации | 60 fps плавно |
| Несколько экземпляров | Один экземпляр, исключает повторный запуск |
| Телеметрия | 0 телеметрии · без отправки · без рекламы |

---

## Содержание

- [Возможности](#возможности)
- [Технологический выбор и обоснование](#технологический-выбор-и-обоснование)
- [Обзор архитектуры](#обзор-архитектуры)
- [Быстрый старт](#быстрый-старт)
- [Сборка и упаковка](#быстрый-старт)
- [Использование](#использование)
- [Справочник по настройкам](#справочник-по-настройкам)
- [Интеграция с Cider](#интеграция-с-cider)
- [Текст песен](#текст-песен)
- [Конфиденциальность и безопасность](#конфиденциальность-и-безопасность)
- [Нефункциональные показатели](#нефункциональные-показатели)
- [Известные ограничения](#известные-ограничения)
- [Руководство по проверке (требуется тестирование с реальным плеером)](#руководство-по-проверке-требуется-тестирование-с-реальным-плеером)
- [Часто задаваемые вопросы](#часто-задаваемые-вопросы)
- [Открытая лицензия](#открытая-лицензия)

---

## Возможности

### P0 (реализовано)
- **Плавающий интерфейс Динамического острова (в стиле iOS)**: по умолчанию по центру сверху (настраивается вправо); капсула с закруглёнными углами; следует светлой/тёмной теме системы или ручному цвету темы; **анимация трансформации** между компактной капсулой → полной карточкой (фиксированное окно + масштабирование/появление одного элемента, управляется потоком композиции WPF при 60 fps, с упругим отскоком в стиле iOS); **клик для разворачивания/сворачивания** (наведение не разворачивает), автоматическое сворачивание при выходе (буфер 700 мс против случайного касания); клики вне карточки проходят сквозь окно.
- **Блокировка и перетаскивание**: заблокировано по умолчанию (переместить нельзя); контекстное меню позволяет **разблокировать** (перетаскивать остров мышью после разблокировки), **отцентрировать** (вертикально одинаково, горизонтально по центру) и снова **заблокировать**. При повторной блокировке после перетаскивания **позиция сохраняется** (не возвращается к значению по умолчанию); при отпускании перетаскивания действует **прилипание к краю** (прилипает к краю/центру экрана, настраивается в Настройки → Общие).
- **Компактная компоновка**: заголовок/исполнитель/текст выровнены по левому краю (вплотную к обложке) и отцентрированы по вертикали.
- **Обложка альбома**: и капсула, и развёрнутая карточка показывают обложку (при разворачивании увеличенная до 64 px; если обложки нет — значок-заглушка); миниатюры SMTC и обложки Cider автоматически кэшируются.
- **Управление воспроизведением медиа**: показывает заголовок, исполнителя и альбом; перетаскиваемый прогресс (seek); воспроизведение/пауза, предыдущий, следующий; управление громкостью при необходимости (Cider использует свой API, остальные источники управляют системной громкостью; можно отключить); медиа-компонент показывает значок текущего источника (Spotify / Cider / NetEase Music / QQ Music и т. д.).
- **Мини-плеер**: отдельное плавающее окно (настраивается в Настройки → Медиа), показывает обложку / заголовок / исполнителя / прогресс и элементы управления; свободно перетаскивается и запоминает позицию; автоматически показывается/скрывается вместе с воспроизведением.
- **Переключение устройства вывода звука**: Настройки → Медиа позволяет перечислить и изменить устройство вывода по умолчанию (после изменения рекомендуется перезапустить плеер).
- **Поддержка нескольких источников**:
  1. Глобальные медиа-сессии Windows (`Windows.Media.Control` / SMTC): NetEase Music, QQ Music, Spotify, официальный Apple Music, Groove, «Фильмы и ТВ» и др.;
  2. **Cider**: локальный HTTP API (порт 10767, совместим со старым RPC 10769, автоматическое сканирование портов + ручная настройка, поддержка аутентификации `apptoken`);
  3. Запасной вариант: заголовок окна + идентификация процесса (только отображение информации, без возможности управления).
- **Отображение текста (режим караоке посимвольно)**: при разворачивании область текста показывается в режиме караоке: **символы текущей строки зажигаются один за другим**; прогресс подсветки — непрерывное значение, символы на границе плавно переходят из базового цвета в цвет подсветки с easing при 60 fps, текут слева направо в порядке чтения (корректно и для текста с переносами строк, без подсветки нескольких строк одновременно); каждая строка начинается с 0 (первый символ в начале не зажигается); при паузе подсветка замирает в момент паузы: когда у Cider нет явного состояния, воспроизведение/пауза определяется по тому, «двигается ли позиция» (больше не путается с воспроизведением только из-за remainingTime>0), SMTC отдаёт приоритет сессии Cider (не даёт другим активным сессиям, таким как Bilibili, перехватить её); при выходе и перезапуске последняя позиция паузы восстанавливается автоматически (не возвращается к началу); текущая строка подсвечивает только текст (без фоновой капсулы, чтобы избежать двойной подсветки; крупный размер 20 px), остальные строки затемняются, **плавная прокрутка с автоцентрированием** (приближается к текущей строке кадр за кадром при 60 fps и следует за ней при разворачивании); в компактном состоянии текущая строка отображается в реальном времени с выравниванием влево и также зажигается посимвольно; опционально отдельное плавающее окно текста.
  - **Синхронизация прогресса**: автоматически читает токен локального API Cider (без настройки) для получения реального прогресса воспроизведения и точной синхронизации караоке с песней; плееры без доступного прогресса используют локальные часы.
  - **Источники текста**: локальный `.lrc` (`%APPDATA%\WinIsland\Lyrics` или папка музыки) → Посимвольное караоке AMLL → интерфейс текста Cider → онлайн-тексты (переключатель одним кликом через правый клик по острову). Без текста показывает «Нет текста», без ошибок.
  - **Двуязычные тексты**: автоматически объединяет строки перевода с соседними временными метками; можно отключить в настройках (дополнительные файлы текста не нужны); переключатель показа/скрытия перевода и «Копировать текущую строку» для копирования одним кликом.
- **Трей**: постоянная иконка, контекстное меню (показать/скрыть, отдельное окно текста, запуск с Windows, настройки, выход), двойной клик для переключения видимости.
### P1 (реализовано)
- **Система компонентов (настраиваемое содержимое острова)**: Настройки → Компоненты позволяет отметить, какие компоненты показывать «без песни / с песней», и настроить порядок перетаскиванием:
  - Часы, погода (Open-Meteo, требуется город и подключение), дата (с лунным календарём и солнечными терминами), загрузка ЦП, загрузка ГП, загрузка памяти, скорость сети (может показывать мини-кривую за последние 32 секунды), батарея, свободное место на диске, состояние метода ввода (китайский / английский + имя IME), быстрые переключатели (WiFi / Bluetooth / ночной режим / без звука одним кликом), громкость, индикатор клавиатуры (CapsLock), буфер обмена, список задач, Pomodoro, расписание, обратный отсчёт праздников, на собрании, микрофон, камера;
  - Информация о песне (обложка/заголовок/исполнитель/текст/прогресс, только во время воспроизведения, всегда присутствует в панели порядка).
  - Панель порядка показывает только отмеченные компоненты; список и панель поддерживают колесо мыши и полосы прокрутки; каждый компонент может иметь свою иконку (иконки MDL2 или эмодзи, Настройки → Компоненты).
  - Временные компоненты на острове: изменение громкости, снимок / запись экрана, копирование / перемещение файлов, идущая загрузка (последние два выключены по умолчанию): при наступлении события соответствующий компонент временно отображается, даже если остров скрыт.
  - **Комбинированная капсула «Используется»** (Настройки → Компоненты, выключена по умолчанию): при включении выбранные «Микрофон / Камера / На собрании / Запись» объединяются в одну капсулу состояния «Используется · N», и объединённые элементы больше не показываются отдельно.
  - **Режим одной строки** (Настройки → Оформление, включён по умолчанию): все компоненты в одной строке в компактном состоянии; без разворачивания также показывает информацию о песне и текущую строку текста (подсветка караоке посимвольно), автоматически обрезая длинные строки; прогресс и полный список строк показываются в развёрнутой карточке.
- **Настройка содержимого развёрнутой карточки**: обложка + заголовок, прогресс, кнопки управления и громкость, область текста можно включать/выключать по отдельности.
- **Настройка внешнего вида (страница настроек в стиле Системных настроек macOS)**: навигация слева + содержимое справа, закруглённое жидкое стекло; **18 предустановленных тем** (по умолчанию / океан / лес / закат / неон / монохром / виноград / небо / розовый / янтарь / лайм / бирюза / лаванда / малиновый / полночь / кофе / сакура / аврора, плюс пользовательская); **цвет, извлечённый из обоев** (автоматически извлекает основной цвет текущих обоев как цвет темы, только локально); **бегущая строка** (длинные строки автоматически прокручиваются); **4 стиля звуковых волн** (столбцы / спектр / кольцо / частицы) и **4 стиля анимации** (пружина iOS / плавный / упругий / затухание); **энергосберегающий режим** (снижает частоту кадров волн и упрощает анимации в простое); высокий DPI PerMonitorV2.
- **Глобальные горячие клавиши**: `Ctrl+Alt+P` воспроизведение/пауза · `Ctrl+Alt+←/→` предыдущий/следующий · `Ctrl+Alt+I` показать/скрыть · `Ctrl+Alt+Space` развернуть/свернуть · `Ctrl+Space` быстрый лаунчер · `Ctrl+Alt+V` панель истории буфера обмена.
- **Уменьшение динамических эффектов** (доступность / экономия энергии): отключает пружинные анимации одним кликом, мгновенное переключение.
- **Настройка размера острова**: Настройки → Оформление, позволяет настроить компактные длину/ширину и развёрнутую длину.
- **Постоянный Динамический остров**: всегда виден, даже без воспроизведения (показывает настроенные компоненты).
- **Несколько мониторов**: основной экран / все экраны / указанный номер экрана.
- **Высокий DPI**: PerMonitorV2, без смещений при 120/150/200 %.
- **Индивидуальная настройка**: позиция, смещение, прозрачность, цвет темы, содержимое компактного режима, скрытие без медиа и т. д.; изменения применяются мгновенно.
- **Автоскрытие острова без воспроизведения** (можно отключить).
- **Не беспокоить**: включение вручную одним кликом или автоматическое беззвучие по временному интервалу (переключение одним кликом в меню трея; интервалы настраиваются в параметрах).
- **Проверка обновлений**: ручная проверка новых версий на GitHub в меню трея / настройках; опциональная автоматическая проверка (выключена по умолчанию, требует подключения).
- **Быстрое действие по двойному клику** (Настройки → Общие): может быть «Воспроизведение / Пауза» (по умолчанию), «Развернуть / Свернуть», «Показать рабочий стол», «Скрыть / Показать остров», «Предыдущий», «Следующий», «Открыть настройки» или «Без действия».
- **Помощник беззвучия на собраниях (определение собраний)**: распознаёт окна собраний, такие как Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet, автоматически включает «Не беспокоить» во время собрания и показывает компонент «На собрании» (чисто локальная эвристика, офлайн).
- **Оповещения о записи / снимке экрана**: при нажатии `PrintScreen` / `Alt+PrintScreen` появляется оповещение; при обнаружении программ записи, таких как OBS, Bandicam, Fraps, Camtasia, XSplit, Streamlabs, Xbox Game Bar, появляется «Идёт запись экрана» (локальное обнаружение процессов, офлайн).
- **Умный режим «Не беспокоить» (запись)**: при обнаружении идущей записи экрана уведомления автоматически замалчиваются (без баннера); по завершении автоматически восстанавливаются; настраивается в Настройки → Уведомления.
- **Автоскрытие в полноэкранном режиме**: при обнаружении полноэкранного видео / игры / презентации (например, PowerPoint) остров автоматически скрывается/сворачивается и восстанавливается при выходе из полноэкранного режима; настраивается в Настройки → Общие.
- **Перетаскивание файлов на остров**: перетаскивание файлов/папок на остров позволяет «Скопировать путь / Открыть содержащую папку / Закрепить на острове» и т. д. (выбор правым кликом по острову или в меню перетаскивания).
- **Напоминания о событиях календаря (.ics)**: анализирует локальные файлы iCalendar (Outlook / Google Calendar / экспортированные с телефона); при наступлении времени события (с настраиваемым уведомлением за N минут) появляется баннер; анализ чисто локальный, офлайн.
- **Напоминания по RSS-подпискам**: периодически опрашивает ленты RSS 2.0 / Atom (настраиваемый интервал); при появлении новой записи показывает баннер; подключается только к настроенным адресам подписки.
- **Напоминания о почте (POP3)**: периодически получает заголовки писем; при новой почте показывает баннер (читает только заголовки, не скачивает содержимое и не отправляет данные; рекомендуется использовать код авторизации).
- **Быстрый лаунчер (в стиле Spotlight)**: открывается по `Ctrl+Space`, ищет установленные приложения / программы меню «Пуск» или открывает URL напрямую; горячая клавиша настраивается.
- **Панель истории буфера обмена**: `Ctrl+Alt+V` открывает отдельное окно истории; клик по элементу снова копирует его в буфер обмена; можно очистить; горячая клавиша настраивается.
- **Правила (автоматизация)**: Настройки → Правила объединяют условия (всегда / без воспроизведения медиа / воспроизведение / временной интервал / конкретная медиа-программа) и действия (скрыть / свернуть принудительно / показать принудительно) для автоматического управления островом; приоритет у скрытия, затем сворачивания и наконец принудительного показа.
- **Энергосберегающий режим**: снижает частоту кадров волн и упрощает анимации в простое для экономии энергии (Настройки → Общие).

### P2 (реализовано)
- Переключение языка интерфейса между упрощённым китайским и английским.
- Экспорт / импорт файла настроек JSON.
- Интеграция уведомлений Windows (Bluetooth / перехват системных уведомлений / воспроизведение / низкий заряд батареи).
- В ожидании: оповещение о входящем звонке (не реализовано); напоминания о событиях реализованы (компонент + инструмент продуктивности).

---

## Технологический выбор и обоснование

| Вариант | Заключение | Обоснование |
| --- | --- | --- |
| **C# + WPF (.NET 8)** | ✅ Принят | Низкое потребление ресурсов и быстрый запуск (по сравнению с WebView в Electron/Tauri), наибольшая интеграция с системой (нативная поддержка SMTC/CoreAudio/трея), простая упаковка в один файл |
| C++ + Qt | ⚪ | Низкая эффективность разработки, сложная лицензия (LGPL), много рукописного кода для интеграции с медиа-стеком Windows |
| Tauri / Electron | ⚪ | Высокое потребление памяти (сложно добиться <150 МБ в резидентном режиме), медленный запуск, не соответствует требованию «низкое потребление ресурсов и быстрый запуск» |
| WinUI 3 | ⚪ | Упаковка/развёртывание сложнее, чем у WPF (требуется Windows App SDK), а поддержка SMTC для неупакованных настольных приложений такая же, как у WPF |

**Ключевые моменты**:
- `Windows.Media.Control.GlobalSystemMediaTransportControlsSessionManager` доступен напрямую через проекцию Windows SDK в .NET 8 (CsWinRT), без необходимости в identity упаковки UWP.
- Помимо встроенных в систему проекций WPF/WinForms/Windows SDK, **ноль сторонних зависимостей во время выполнения** (см. [THIRD_PARTY.md](THIRD_PARTY.md)).
- Акриловый эффект: и в Win10, и в Win11 реализуется через `SetWindowCompositionAttribute` (`ACCENT_ENABLE_ACRYLICBLURBEHIND`), а скруглённые углы вырезаются через `SetWindowRgn`, чтобы размытие повторяло форму капсулы.

---

## Обзор архитектуры

```
src/WinIsland/
├── App.xaml(.cs)              # Корень композиции: единственный экземпляр, перехват исключений, трей, жизненный цикл окна
├── Services/
│   ├── MediaModels.cs         # Единая модель снимка медиа (TrackInfo / MediaSnapshot)
│   ├── SmtcMediaProvider.cs   # Глобальная медиа-сессия Windows (событийная + отправка с троттлингом)
│   ├── CiderClient.cs         # Обёртка локального API Cider (V3 + LegacyV2, сканирование портов, толерантный парсинг)
│   ├── CiderMediaProvider.cs  # Слой сессии Cider (жизненный цикл соединения)
│   ├── WindowTitleMediaProvider.cs # Запасной вариант: распознавание заголовка окна
│   ├── MediaCoordinator.cs    # Центральная диспетчеризация: Cider > SMTC > заголовок окна, кэш обложек, доп. громкость
│   ├── LrcParser.cs           # Разбор LRC (несколько временных меток, offset, форматы длительности)
│   ├── LyricsService.cs       # Анализ текста (.lrc локально → Cider → онлайн)
│   ├── OnlineLyricsService.cs # Онлайн-тексты (неофициальные интерфейсы NetEase/QQ Music, включены по умолчанию с переключателем в один клик)
│   ├── ArtworkCache.cs        # Загрузка/кэш обложек (удалённая обложка Cider → локальный файл)
│   ├── SystemVolume.cs        # Системная громкость CoreAudio (COM P/Invoke)
│   ├── AppSettings.cs         # Чтение/запись настроек JSON (%APPDATA%\WinIsland\settings.json)
│   ├── SingleInstance.cs      # Именованный мьютекс + именованный канал (второй запуск показывает остров)
│   ├── AutoStart.cs           # Ключ автозапуска HKCU Run
│   ├── GlobalHotkeyService.cs # Глобальные горячие клавиши (Win32 RegisterHotKey)
│   ├── NotificationService.cs # Стеклянный баннер уведомления в правом верхнем углу
│   ├── NotificationHistoryService.cs # История уведомлений (последние 50, сохранение JSON)
│   ├── BluetoothMonitor.cs    # Мониторинг подключения/отключения устройств Bluetooth
│   ├── SystemNotificationMonitor.cs # Перехват уведомлений Windows (зеркалирование через UI-автоматизацию)
│   ├── MediaAppRegistry.cs    # Реестр медиа-программ (включить/выключить/упорядочить)
│   ├── AudioWaveService.cs    # Звуковые волны (сэмплирование системной громкости, управляет вибрацией волн)
│   ├── KeyboardIndicatorMonitor.cs # Индикаторы клавиатуры (мониторинг состояния CapsLock)
│   ├── ClipboardHistoryService.cs # История буфера обмена
│   ├── TodoService.cs         # Список задач
│   ├── PomodoroService.cs     # Таймер Pomodoro
│   ├── ScheduleService.cs     # Напоминания о событиях
│   ├── WeatherService.cs      # Погода (Open-Meteo)
│   ├── RssService.cs          # RSS-напоминания
│   ├── MailService.cs         # Напоминания о почте POP3
│   ├── RuleEngine.cs          # Движок правил автоматизации
│   ├── IslandViewModel.cs     # Главная модель представления (интерполяция прогресса, индекс текста, видимость)
│   ├── SettingsWindow.xaml(.cs) / SettingsViewModel.cs
│   ├── LyricsWindow.xaml(.cs) # Отдельное окно текста
│   ├── ThemeService.cs        # Светлая/тёмная тема + кисти цвета темы
│   ├── WindowEffects.cs       # Акрил / тёмный режим / область скруглённых углов
│   ├── ScreenHelper.cs        # Несколько мониторов + преобразование DPI PerMonitorV2
│   ├── TrayIcon.cs            # Иконка и меню трея
│   ├── ClipboardPanelWindow.xaml(.cs) # Панель истории буфера обмена
│   ├── QuickLauncherWindow.xaml(.cs)  # Быстрый лаунчер (Ctrl+Space)
│   └── Localization.cs        # Таблица текстов китайский/английский
├── Diagnostics/DiagnosticsCommand.cs  # Диагностическая информация --diagnose
tests/WinIsland.Tests/         # Модульные тесты xunit (разбор LRC/настройки/Cider/заголовок окна)
build/
├── publish.ps1                # Публикация в один клик (автономная или зависящая от framework + zip)
├── WinIsland.iss              # Скрипт установки Inno Setup
└── make-icon.ps1 / IconGen.cs # Инструмент генерации иконок
```

**Поток данных**: `MediaCoordinator` опрашивает каждый Provider раз в секунду (асинхронно, не блокируя UI) → формирует единый `MediaSnapshot` (с локальным путём обложки и громкостью) → публикует его в `IslandViewModel` через Dispatcher → интерполятор 200 мс плавно продвигает прогресс и подсветку текста → отрисовка через привязку WPF.

---

## Быстрый старт

> 📦 **Готовая сборка**: каталог `releases/` содержит автономные исполняемые файлы по одной версии (например, `releases/1.1.5/win-x64/WinIsland-1.1.5-win-x64.exe`, включает среду выполнения .NET 8, двойной клик для запуска). Бета-версии хранятся только локально; на GitHub публикуются только стабильные версии (включая портативные win-x64 / win-arm64 и универсальный установщик).

### Требования к окружению
- Windows 10 1809+ / Windows 11
- Сборочная машина: SDK .NET 8 (или более новая версия SDK с указанием `net8.0-windows10.0.19041.0`)

### Сборка
```powershell
# Восстановить + собрать + протестировать
dotnet build WinIsland.slnx -c Release
dotnet test  WinIsland.slnx -c Release

# Запуск (Debug)
dotnet run --project src\WinIsland -c Debug
```

### Публикация в один клик
```powershell
# Автономная (включает среду выполнения .NET 8, без установки, ~73 МБ в одном файле)
.\build\publish.ps1

# Зависящая от framework (малый размер, требуется .NET 8 Desktop Runtime)
.\build\publish.ps1 -FrameworkDependent
```
Артефакты находятся в `publish\win-x64\` (включая `WinIsland.exe`); zip-файл — `publish\WinIsland-win-x64.zip`.

### Установщик (необязательно)
После установки [Inno Setup 6](https://jrsoftware.org/isinfo.php):
```powershell
iscc.exe build\release-1.1.5.iss
```
Создаёт `releases\<version>\WinIsland-Setup-<version>.exe` (универсальный установщик, совместимый с x64 и ARM64, устанавливает автоматически по архитектуре). При публикации стабильной версии скопируйте `release-<version>.iss` в `build\` и обновите номер версии.

---

## Использование

1. Запустите `WinIsland.exe` (или включите автозапуск / отметьте автозапуск в установщике). В трее появляется иконка.
2. Воспроизведите любую музыку:
   - NetEase Music, QQ Music, Spotify, официальный Apple Music и т. д. → отображается автоматически через медиа-сессию системы;
   - Cider → см. [Интеграция с Cider](#интеграция-с-cider);
   - Другие плееры → запасное распознавание заголовка окна (только отображение).
3. **Кликните** по острову, чтобы развернуть полную карточку (наведение не разворачивает): перетаскивание прогресса (seek), управление воспроизведением, громкость и синхронизированный текст; кликните снова для сворачивания (после выхода из карточки автоматически сворачивается через 700 мс).
4. Меню трея: показать/скрыть, отдельное окно текста, запуск с Windows, **Не беспокоить** (отметка замалчивает уведомления), **Проверить обновления**, **Просмотреть журналы**, настройки и выход. **Закрытие главного окна не завершает процесс** (только сворачивает в трей).
5. Глобальные горячие клавиши: `Ctrl+Alt+P` воспроизведение/пауза · `Ctrl+Alt+←/→` предыдущий/следующий · `Ctrl+Alt+I` показать/скрыть · `Ctrl+Alt+Space` развернуть/свернуть · `Ctrl+Space` быстрый лаунчер (поиск приложений / ввод URL и Enter) · `Ctrl+Alt+V` панель истории буфера обмена (все можно отключить / настроить).
6. Уведомления и оповещения (Bluetooth / уведомления Windows / воспроизведение / низкий заряд) по умолчанию появляются как стеклянные баннеры в правом верхнем углу; включаются/выключаются в Настройки → Уведомления; при включённом **Не беспокоить** баннеры не показываются (счётчик значка продолжает считать).
7. Частые параметры командной строки:
   ```powershell
   WinIsland.exe --demo       # Демонстрационный режим (предпросмотр интерфейса + пример текста без медиа)
   WinIsland.exe --diagnose   # Создаёт диагностический отчёт в %APPDATA%\WinIsland\diagnostics.txt
   WinIsland.exe --settings   # Открывает настройки при запуске
   ```

---

## Справочник по настройкам

Файл настроек: `%APPDATA%\WinIsland\settings.json` (JSON; изменения в интерфейсе настроек применяются мгновенно; можно экспортировать/импортировать).

| Ключ | По умолчанию | Описание |
| --- | --- | --- |
| `Language` | `zh-CN` | `zh-CN` / `en-US` |
| `ThemePreset` | `Default` | Тема: `Default/Ocean/Forest/Sunset/Neon/Mono/Grape/Sky/Rose/Amber/Lime/Teal/Lavender/Crimson/Midnight/Coffee/Sakura/Aurora/Custom` (переопределяет AccentColor) |
| `FontFamily` | `Segoe UI` | Шрифт интерфейса |
| `FontScale` | `1.0` | Масштаб шрифта 0.8–1.4 |
| `CornerRadius` | `28` | Радиус углов капсулы 16–40 |
| `BadgeEnabled` | `true` | Значок непрочитанных уведомлений (красная точка + число в правом верхнем углу) |
| `CoverTintBackground` | `true` | Развёрнутый фон принимает цвет обложки альбома |
| `WaveVisualizerEnabled` | `true` | Звуковые волны слева от кнопок управления при воспроизведении медиа |
| `WaveStyle` | `Bars` | Стиль волн: `Bars` (столбцы) / `Spectrum` (спектр) / `Ring` (кольцо) / `Particles` (частицы) |
| `WaveSyncEnabled` | `true` | Волны следуют ритму музыки (управляются системной громкостью вывода) |
| `WaveSensitivity` | `1.0` | Чувствительность волн 0.2–3.0 |
| `WaveHeight` | `1.0` | Высота волн 0.4–1.6 |
| `WallpaperThemeColorEnabled` | `false` | Цвет обоев: извлекает основной цвет текущих обоев как цвет темы (только локально) |
| `MarqueeTextEnabled` | `false` | Бегущая строка: автоматическая горизонтальная прокрутка, когда заголовок/текст слишком широкие |
| `EdgeSnapEnabled` | `true` | При отпускании разблокированного перетаскивания автоматически прилипает к краю/центру экрана |
| `FullScreenAutoHideEnabled` | `true` | Автоскрытие острова в полноэкранном режиме (видео/игра/презентация) |
| `RecordingDndEnabled` | `false` | Автоматический режим «Не беспокоить» при записи экрана (без баннеров уведомлений) |
| `Theme` | `Auto` | `Auto` / `Light` / `Dark` |
| `AccentColor` | `#6C5CE7` | Цвет темы (#RRGGBB) |
| `Position` | `Center` | `Center` по центру сверху / `Right` справа сверху |
| `Monitor` | `Primary` | `Primary` основной экран / `All` все / `Index` указанный экран |
| `MonitorIndex` | `0` | Номер экрана, когда `Monitor=Index` |
| `OffsetX` / `OffsetY` | `0` / `16` | Смещение в пикселях |
| `Opacity` | `0.92` | 0.3–1.0 |
| `HideWhenNoMedia` | `true` | Скрывает остров без воспроизведения |
| `ShowWhenPaused` | `true` | Продолжает показывать при паузе |
| `StartWithWindows` | `false` | Запуск с Windows |
| `StartHidden` | `false` | Скрыт при запуске |
| `CompactShowArt/Title/Progress` | `true/true/false` | Содержимое компактного режима |
| `IslandAlwaysVisible` | `false` | Постоянный остров (показывает компоненты даже без медиа) |
| `ShowMediaInfo` | `true` | Показывает информацию о воспроизведении (заголовок/обложка/текст и т. д.) |
| `ReduceMotion` | `false` | Уменьшает динамические эффекты (отключает пружинные анимации; доступность/экономия энергии) |
| `GlobalHotkeysEnabled` | `true` | Переключатель глобальных горячих клавиш |
| `LowBatteryThreshold` | `20` | Порог предупреждения о низком заряде (%), 0 — отключить |
| `ExpandedShowArtTitle/Progress/Controls/Lyrics` | `true` | Переключатели разделов развёрнутой карточки (обложка+заголовок / прогресс / управление и громкость / текст) |
| `Components` | объект | Выбор компонентов: `Time/Weather/Date/Cpu/Gpu/Ram/Net/Battery/Volume/CapsLock/Clipboard/Todo/Timer/Schedule/Holiday/Meeting/Mic/Cam`, каждый с двумя столбцами `WhenIdle`/`WhenPlaying`; `Cover/Title/Artist/Lyrics/Progress` показываются во время воспроизведения; словарь `ComponentBadges` заполняет текст значка каждого компонента |
| `WidgetOrder` | `Time,Weather,...` | Порядок компонентов (ключи через запятую, включает `Song`) |
| `MediaApps` | `[]` | Включение/отключение и приоритет медиа-программ (пусто = все включены) |
| `CompactWidth` / `CompactHeight` | `360` / `72` | Компактная ширина / высота (ручная настройка перетаскиванием отключает автоматическую) |
| `CompactWidthAuto` / `CompactHeightAuto` | `true` | Компактный размер автоматически подстраивается под содержимое (включено по умолчанию) |
| `ExpandedWidthAuto` / `MaxExpandedHeightAuto` | `true` | Автоматическая подстройка развёрнутого размера (включено по умолчанию) |
| `ExpandedWidth` / `MaxExpandedHeight` | `400` / `384` | Развёрнутая ширина / максимальная развёрнутая высота |
| `BluetoothNotifyEnabled` | `false` | Оповещение о подключении/отключении Bluetooth |
| `NotificationTakeoverEnabled` | `false` | Перехват уведомлений Windows (best effort) |
| `NotificationTimeoutSeconds` | `6` | Длительность баннера уведомления (секунды) |
| `NotificationPosition` | `TopRight` | Позиция уведомлений (правый верхний угол) |
| `DoNotDisturbEnabled` / `DoNotDisturbManual` | `false` | Не беспокоить: автоматически по интервалам / вручную |
| `DoNotDisturbStartHour` / `DoNotDisturbEndHour` | `22` / `8` | Интервал «Не беспокоить» (часы) |
| `DnDAllowlist` | `[]` | Белый список «Не беспокоить» (`QQ.exe,WeChat.exe`; в белом списке уведомления показываются) |
| `Rules` | `[]` | Список правил автоматизации (условие + действие) |
| `ClipboardHistoryEnabled` / `ClipboardHistoryMax` | `false` / `15` | Переключатель истории буфера обмена и максимальное число записей |
| `PomodoroEnabled` / `PomodoroWorkMinutes` / `PomodoroBreakMinutes` | `false` / `25` / `5` | Переключатель Pomodoro и длительность работы/отдыха (минуты) |
| `KeyIndicatorSeconds` | `3` | Длительность индикатора клавиатуры (CapsLock) (секунды) |
| `VolumeTempIndicatorEnabled` / `VolumeTempIndicatorSeconds` | `true` / `4` | Переключатель и длительность временного индикатора громкости/беззвучия на острове |
| `FileCopyNotifyEnabled` | `true` | Копирование/перемещение файлов на острове (локальное распознавание заголовка окна) |
| `DownloadProgressEnabled` | `false` | Идущая загрузка на острове (сканирует временные файлы в папке загрузок; выключено по умолчанию) |
| `UsageMergeEnabled` / `UsageMergeItems` | `false` / `Mic,Cam,Meeting,Recording` | Комбинированная капсула «Используется» и участвующие компоненты (выключено по умолчанию) |
| `AutoUpdateCheck` | `false` | Автоматическая проверка новых версий на GitHub (выключено по умолчанию, требует подключения) |
| `DoubleClickAction` | `PlayPause` | Действие по двойному клику: `PlayPause` / `ToggleExpand` / `ShowDesktop` / `ToggleVisible` / `NextTrack` / `PrevTrack` / `OpenSettings` / `None` |
| `AnimationStyle` | `Spring` | Стиль анимации: `Spring` / `Soft` / `Elastic` / `Fade` |
| `ThemeTint` | `""` | Пользовательский цвет фона #RRGGBB (применяется, когда пресет Custom) |
| `ExpandedCardStyle` | `Classic` | Шаблон развёрнутой карточки: `Classic` / `Hero` |
| `NetCurveEnabled` | `true` | Сетевой компонент показывает мини-кривую за последние 32 секунды |
| `LowPowerMode` | `false` | Энергосберегающий режим (снижает частоту кадров волн и упрощает анимации в простое) |
| `MeetingAssistantEnabled` | `false` | Помощник беззвучия на собраниях: обнаруживает окна собраний + автоматический режим «Не беспокоить» |
| `MeetingAutoDnd` / `MeetingKeywords` | `true` / `""` | Автоматический режим «Не беспокоить» на собраниях / настраиваемые ключевые слова собраний |
| `ScreenCaptureNotifyEnabled` / `ScreenshotNotifyEnabled` / `RecordingNotifyEnabled` | `false` / `true` / `true` | Общий переключатель и подпункты оповещений о снимке/записи |
| `CalendarEnabled` / `CalendarIcsPath` / `CalendarAdvanceMinutes` | `false` / `""` / `10` | Переключатель напоминаний календаря .ics / путь к файлу / минут до события |
| `RssNotifyEnabled` / `RssUrls` / `RssIntervalMinutes` | `false` / `""` / `15` | RSS-напоминания / адреса подписки / интервал опроса (минуты) |
| `MailNotifyEnabled` / `MailPop3Server` / `MailPop3Port` / `MailUseSsl` / `MailUser` / `MailPassword` / `MailCheckMinutes` | `false` / `""` / `995` / `true` / `""` / `""` / `5` | Напоминания о почте (POP3): переключатель, сервер, порт, SSL, учётная запись, код авторизации и интервал проверки |
| `QuickLauncherEnabled` / `HotkeyLauncher` | `true` / `Ctrl+Space` | Переключатель быстрого лаунчера и горячая клавиша |
| `ClipboardPanelEnabled` / `HotkeyClipboardPanel` | `true` / `Ctrl+Alt+V` | Переключатель панели истории буфера обмена и горячая клавиша |
| `HotkeyExpand` | `Ctrl+Alt+Space` | Горячая клавиша развернуть/свернуть |
| `NotifyFoldEnabled` | `true` | Сворачивание похожих уведомлений (одинаковый источник и заголовок показывает одно) |
| `ActiveProfile` | `Default` | Имя профиля настроек (переключение между несколькими наборами) |---
## Интеграция с Cider

Cider (сторонний клиент Apple Music) предоставляет локальный HTTP API. WinIsland уже включает отдельный модуль (`CiderClient.cs`), который автоматически адаптируется к различиям версий.

**Шаги активации (важно)**:
1. Откройте Cider: **Настройки → Подключение → Разрешить внешнее управление (Manage External Application Access)**; при включении Cider покажет API-токен (если он пуст, нажмите, чтобы сгенерировать его).
2. Скопируйте токен в **настройки WinIsland → Cider → API Token** и сохраните.
3. Порт по умолчанию — `10767`, WinIsland определяет его автоматически; старый RPC — `10769`.

> ⚠️ В новых версиях Cider 2.x **по умолчанию требуется токен во всех API-запросах** (без токена возвращается `403 UNAUTHORIZED_APP_TOKEN`). Если диагностические журналы указывают на необходимость токена, заполните его, следуя шагам выше; в противном случае тексты песен/управление Cider будут недоступны (треки по-прежнему можно отображать через SMTC).

> ⚠️ Если в журналах повторно появляется HttpClient.Timeout (изначально 2 с), скорее всего, антивирус/локальный прокси перехватывает HTTP на локальной петле (реальный ответ Cider — около 30 мс). С версии 1.0.1 тайм-аут чтения данных увеличен до 5 с; если он всё ещё истекает, проверьте, не блокирует ли антивирус сетевое подключение WinIsland.

**Реализованные возможности API** (по документации сообщества Cider / проверенному крейту `cider-api`, версия 2026):
- `GET /api/v1/playback/active`, `GET /now-playing` (трек/обложка/прогресс/состояние)
- `POST /api/v1/playback/play|pause|playpause|next|previous|seek`
- `GET|POST /api/v1/playback/volume`
- `GET /api/v1/lyrics` (включая запасной вариант `?id=`)
- Заголовок аутентификации: `apptoken` (совместим с `apitoken`)
- Старый 10769: `/active`, `/currentPlayingSong`, `/playPause`, `/next`, `/previous`, `/seekto/{t}`, `/audio`

> ⚠️ API Cider неофициальный и быстро меняется; у всех запросов тайм-аут 2 секунды, при сбое выполняется автоматический откат на SMTC / заголовок окна, **не влияя на основной поток**. Регулярно обновляйте WinIsland, чтобы адаптироваться к новым версиям.

---

## Текст песен

Приоритет:
1. **Локальные .lrc**: ищутся как `Песня.lrc` / `Исполнитель - Песня.lrc` в каталогах текстов (по умолчанию `%APPDATA%\WinIsland\Lyrics`, `Музыка\Lyrics` и корень `Музыка`);
2. **Посимвольное караоке AMLL** (таймлайн TTML по символам из каталога песен amll.dev, включено по умолчанию);
3. **Интерфейс текстов Cider** (когда источник — Cider);
4. **Онлайн-тексты** (неофициальные интерфейсы NetEase / QQ Music): **включены по умолчанию**; правый клик по острову — онлайн-тексты для быстрого переключения, или можно отключить в настройках.

> ⚠️ Онлайн-тексты используют неофициальные интерфейсы и предназначены только для личного изучения; уважайте авторские права; по требованию правообладателя функцию можно отключить в любое время (после отключения — полный офлайн).---
## Уведомления и оповещения (с 1.0.2, улучшено в 1.0.3)

Все уведомления — **стеклянные баннеры в правом верхнем углу**, с анимацией появления/исчезновения в стиле macOS (выезд справа + затухание); длительность показа настраивается (3–15 секунд).

- **Уведомление о подключении Bluetooth**: Настройки → Уведомления; при включении появляется при подключении/отключении устройства Bluetooth.
- **Перехват уведомлений Windows**: Настройки → Уведомления; при включении через UI-автоматизацию (best effort) отражает содержимое центра уведомлений (например, уведомления QQ) в баннерах в правом верхнем углу.
  > ⚠️ Windows не предоставляет публичного API для «перехвата уведомлений других приложений»; эта функция best effort, некоторые уведомления могут не перехватываться; на основной поток не влияет.
- **Уведомление о воспроизведении**: при смене трека автоматически появляется баннер «Сейчас играет - Название» (с 1.0.3).
- **Предупреждение о низком заряде**: появляется, когда заряд опускается ниже порога (по умолчанию 20%, настраивается 0–50), один раз за цикл зарядки (с 1.0.3).
- **История уведомлений**: записи последних 50 уведомлений; на странице Настройки → Уведомления их можно просмотреть/очистить (с 1.0.3).
- **Настройка размера острова**: Настройки → Внешний вид позволяет регулировать компактную длину/ширину и расширенную длину.

---
## API острова (отправка с других приложений на Динамический остров)

WinIsland включает локальный HTTP-сервис; другие приложения могут отправлять информацию на Динамический остров в реальном времени (аналогично интеграции сторонних приложений с Dynamic Island в iOS). **Документация для разработчиков: [docs/IslandAPI.md](docs/IslandAPI.md)**.

| Интерфейс | Описание |
|---|---|
| `POST /v1/island/push` | Отправка / обновление карточки острова (с v3 поддерживаются изображения / динамический прогресс / heartbeat) |
| `PATCH /v3/island/push/{id}` | Частичное обновление: перезаписываются только поля, присутствующие в теле (сохраняются срок действия / позиция в очереди) |
| `DELETE /v1/island/push/{id}` | Удаление карточки |
| `GET /v1/island/active` (или `/v3/island/active`) | Запрос текущей активной карточки |
| `GET /v3/ws` | Двусторонний WebSocket-канал: клиент отправляет `push/update/remove/ping`, сервер рассылает события `push_updated/push_removed` |
| `GET /v1/health` | Проверка работоспособности |

- Настройки → API острова: переключатель включения, порт (по умолчанию 9840), необязательный токен и глобальная длительность показа по умолчанию
- Уведомления острова **не меняют длину/ширину острова**; в компактном состоянии карточка отображается в одну строку и не скрывает другие компоненты
- Кнопки поддерживают «Открыть ссылку / Запустить программу»; отправитель может задать длительность показа для конкретной записи (переопределяет глобальное значение по умолчанию)
- Новое в v3: `image` (изображение data URI или http), `progress_from/progress_to/progress_duration_seconds` (автоматический прогресс), `heartbeat_seconds` (продление heartbeat; если не обновлять дольше 2 интервалов — автоматическое удаление), `theme` (тема карточки dark/light/auto), `action: "command"` (кнопка выполняет локальную команду); полная документация в [docs/IslandAPI.md](docs/IslandAPI.md)

---
## Восстановление состояния воспроизведения

- При выходе из приложения, паузе или смене трека сохраняется «трек + позиция воспроизведения» в `%APPDATA%\WinIsland\state.json` (только локально).
- При следующем запуске, если это тот же трек и плеер ещё не вернул реальный прогресс, сначала восстанавливается последняя позиция, чтобы избежать скачка «показать строку 0 и затем перескочить на паузу»; после более 1 часа или при смене трека восстановление не выполняется.---
## Конфиденциальность и безопасность

- **Никакой телеметрии, никакой рекламы, ничего не отправляется**. Кроме «онлайн-текстов», включённых вручную пользователем, приложение не выполняет ни одного сетевого запроса.
- **Погодный компонент**: только когда вы включите «Показывать погоду» и введёте город, запрашивается текущая погода у Open-Meteo (бесплатно, без ключа, без аккаунта); если не включено — полностью офлайн.
- Единственные сценарии подключения: загрузка обложек Cider (`mzstatic.com`, публичный URL обложки, возвращаемый локальным API) и включённые пользователем онлайн-тексты.
- Все данные хранятся локально в `%APPDATA%\WinIsland\`.
- Журналы содержат только локальную информацию о работе (`logs\app-*.log`).

---

## Нефункциональные показатели

Измерено на тестовой машине (Windows 11 24H2, 2560×1440 при 100 %) (автономный Release):

| Показатель | Измерено | Цель |
| --- | --- | --- |
| CPU в простое (без медиа) | < 0,5 % (0,3 % в Debug) | ≈ 0 % |
| Резидентная память (Private) | ~72 МБ | ≤ 150 МБ |
| Запуск | < 1 с (холодный) | ≤ 2 с |
| Закрытие главного окна | Не завершает, сворачивает в трей | ✅ |
| Несколько экземпляров | Только один; второй запуск показывает остров | ✅ |
| Исключения | Единый перехват и запись в файл, без блокирующих окон | ✅ |

> Примечание: WorkingSet автономной сборки (включая общие страницы рантайма .NET) — около 160 МБ, но **Private-память — около 72 МБ**; у версии с зависимым рантаймом WorkingSet будет меньше.

---

## Известные ограничения

- **Посимвольное караоке зависит от источника текстов и прогресса**: при наличии посимвольной временной шкалы TTML/LRC AMLL подсветка идёт по символам; без посимвольной временной шкалы или если плеер не предоставляет реальный прогресс, эффект деградирует до подсветки всей строки (продвижение по локальным часам).
- **Иногда прогресс откатывается назад** (например, Cider/SMTC в один момент сообщает 0 или устаревшую позицию): реализована защита позиции — мгновенный откат игнорируется, сохраняется текущее продвижение, текст/прогресс-бар не возвращаются к началу; только после устойчивого отката примерно на 4 секунды это считается реальной перестановкой или перемоткой плеера.
- **Предупреждение о входящем звонке**: не реализовано (опционально в P2). Реализовано: уведомление Bluetooth, перехват уведомлений Windows (best effort), уведомление о воспроизведении, предупреждение о низком заряде и напоминания календаря.
- **Покрытие SMTC**: зависит от регистрации плеером глобальной медиа-сессии; некоторые старые плееры, не регистрирующие её, покрываются только по заголовку окна (без кнопок управления).
- **Cider 1.x (старый API, порт 9000)**: не адаптирован; поддерживаются только 2.x и новее.---

## Руководство по проверке (требуется тестирование с реальным плеером)

Следующие сценарии требуют проверки в реальной среде (указано, что уже проверено автоматически в среде разработки этого репозитория):

| Сценарий | Статус |
| --- | --- |
| Перечисление SMTC-сессий (с `--diagnose` виден список сессий) | ✅ Проверено (обнаруживает реальные сессии, например Bilibili) |
| Автоматический показ/скрытие острова, раскрытие/сворачивание по клику, интерполяция прогресса | ✅ Проверено (демо + реальная приостановленная сессия) |
| Воспроизведение/пауза/переключение/перемотка (реальный плеер) | ⚠️ Требует тестирования (пути кода напрямую соответствуют API управления SMTC) |
| Подключение и управление API Cider | ⚠️ Требуется установить Cider на машину и включить внешнее управление |
| Синхронизированная прокрутка локальных текстов .lrc | ✅ Разбор LRC покрыт юнит-тестами; сквозной сценарий требует реального трека |
| Онлайн-тексты | ✅ Интегрировано с NetEase/QQ Music; сквозной эффект требует реального трека |

**Рекомендуемые шаги проверки**:
1. `WinIsland.exe --diagnose` → убедитесь, что `System media sessions` показывает плеер;
2. Воспроизведите любой трек NetEase/QQ Music/Spotify → остров должен показать трек и позволять управление;
3. Откройте Cider и включите внешнее управление → источник острова должен показывать `Cider`, с перемоткой/громкостью;
4. Поместите `.lrc` с тем же именем в папку трека → при раскрытии текст должен прокручиваться и подсвечиваться по прогрессу.

---

## Часто задаваемые вопросы

**В: Не отображается Динамический остров?**
- Убедитесь, что есть воспроизведение (при паузе по умолчанию продолжает показываться); `HideWhenNoMedia` включён по умолчанию; скрытие без медиа — это нормально.
- Запустите `--diagnose`, чтобы увидеть список сессий; если список пуст, плеер не зарегистрировал SMTC.

**В: Cider показывает «Нет подключения»?**
- Убедитесь, что в настройках Cider включено «Разрешить внешнее управление»; проверьте порт (по умолчанию 10767); убедитесь, что Cider включён в настройках WinIsland.

**В: Не загружаются онлайн-тексты?**
- Онлайн-тексты включены по умолчанию (правый клик по острову → Онлайн-тексты для переключения одним кликом); если текстов всё ещё нет, проверьте в Настройках → Тексты, что они включены, и проверьте сетевое подключение.

**В: Значок в трее остаётся после выхода?**
- Меню в трее → Выход; простое закрытие окна острова только скрывает его (по дизайну «проживание в трее»).

---

## Открытая лицензия

- Приложение: MIT (см. [LICENSE](LICENSE))
- Сторонние компоненты: см. [THIRD_PARTY.md](THIRD_PARTY.md)

---

## Português

## WinIsland — Ilha Dinâmica para Windows

> Leve a Ilha Dinâmica do iOS para o Windows: janela flutuante da área de trabalho com controle de mídia, letras sincronizadas, componentes personalizáveis, central de notificações e residência permanente na bandeja do sistema.
> Baseado em **.NET 8 + WPF**, compatível com Windows 11 (também com Windows 10, 1809+).

---

> **Leve a Ilha Dinâmica do iOS para o Windows | Uma Ilha Dinâmica moderna e multifuncional.**

Leve a Ilha Dinâmica do iOS para o Windows 11 / 10: controle de reprodução de mídia, letras de karaokê caractere por caractere, componentes personalizáveis, central de notificações e API da Ilha, tudo em uma única cápsula. Baseado em **.NET 8 + WPF**, gratuito e de código aberto (MIT), **sem anúncios · sem telemetria**.

🌐 **Site: https://WinIsland.JudeKwong.com**

---

## ✨ Destaques

- **▶ Controle de reprodução de mídia**: conecta-se nativamente às sessões de mídia globais do Windows (SMTC), compatível com NetEase Cloud Music, QQ Music, Spotify, Apple Music, Groove, Filmes e TV, etc.; além disso, suporte específico para a API local do Cider; se não for possível conectar, usa o título da janela como fallback. Capa do álbum, arrastar a barra de progresso (seek), reproduzir/pausar/trocar de música, tudo incluído; com vários reprodutores abertos ao mesmo tempo, você pode trocar a fonte de controle com um clique; ao clicar na capa, abre uma visualização imersiva em tela cheia.
- **♪ Letras de karaokê caractere por caractere**: o cartão expandido rola e realça sincronizado, acendendo caractere por caractere no estilo karaokê; três níveis de fontes de letras: `.lrc` local → interface de letras do reprodutor → letras on-line opcionais; letras bilíngues, botão de tradução, copiar a linha atual com um clique; o tempo das letras pode ser ajustado finamente por música, e a janela independente de letras permite ajustar opacidade e bloqueio.
- **▦ Sistema de componentes personalizáveis**: hora, clima, data (com calendário lunar / termos solares), CPU/GPU/memória/disco, velocidade da rede, bateria, método de entrada, botões rápidos (WiFi/Bluetooth/modo noturno/silencioso) e mais de 30 componentes; cada componente pode ter um ícone personalizado, seleção por caixas de seleção e ordem por arrastar, com modos de uma linha ou várias linhas a qualquer momento.
- **⇪ API da Ilha**: interface local HTTP / WebSocket que permite que qualquer software de terceiros envie informações para a Ilha Dinâmica em tempo real (semelhante à integração de apps de terceiros na Ilha Dinâmica do iOS). A v3 suporta imagens, progresso dinâmico, renovação por heartbeat (batida) e temas claro/escuro do cartão; as notificações não alteram a largura/altura da Ilha nem ocultam outros componentes; os botões suportam abrir links / iniciar programas / executar comandos locais, e o clique no botão notify pode retornar a chamada ao remetente via WebSocket.
- **🔔 Central de notificações**: banners de vidro no canto superior direito, com animações de deslize estilo macOS: dispositivos Bluetooth, avisos de chamadas de voz/vídeo do WeChat/QQ, captura das notificações do sistema, em reprodução, bateria fraca/carga concluída, sem conexão/recuperada; histórico de notificações, recolhimento, lista branca de Não perturbe e automação por regras; os banners podem incluir botões de ação (como «Desconectar» e «Configurações» para Bluetooth).
- **✦ Aparência e efeitos**: 18 temas de pele, cor de destaque e fundo personalizáveis, vidro líquido fosco, **cor extraída do papel de parede** (extrai automaticamente a cor do tema do fundo atual), **texto em letreiro** (letras/músicas longas rolam horizontalmente), 4 estilos de skin de animação (mola iOS, etc.), **4 estilos de onda de áudio** (barras / espectro / anel / partículas, que vibram com o ritmo da música); animações de expandir/recolher com easing não linear a 60 fps; o fundo extraído da capa pode «respirar» lentamente (tema dinâmico); alta DPI PerMonitorV2, sem desalinhamentos em 120/150/200 %.
- **🖱 Interação e inteligência**: desbloquear e arrastar + **ajuste na borda** (adere à borda/centro ao soltar), **ocultar automaticamente em tela cheia** (recolhe ao reproduzir vídeo/jogo/apresentação em tela cheia), ações personalizadas de duplo clique, **botões de ação rápida** (bloquear tela/silenciar/reproduzir-pausar/capturar/mostrar área de trabalho, etc., com ordem personalizável), **arrastar arquivos para a Ilha**, **Não perturbe inteligente durante gravação de tela**; resposta imediata ao pressionar o mouse (o clique tem prioridade para expandir/recolher).
- **⚡ Ferramentas de produtividade e automação**: Pomodoro, tarefas pendentes, histórico da área de transferência, iniciador rápido, lembretes de agenda; assistente de silêncio em reuniões, avisos de gravação de tela/captura, progresso de cópia/download de arquivos na Ilha; atalhos globais e motor de regras (mostrar/ocultar automaticamente conforme condições).
- **🛡 Privacidade e segurança**: sem telemetria, sem anúncios, sem envio de dados. Totalmente off-line, exceto as letras on-line e o clima que o usuário ativa manualmente; todas as configurações e dados são salvos apenas localmente em `%APPDATA%\WinIsland`.

---

## 📥 Download (última versão estável 1.1.5)

| Plataforma | Download | Descrição |
| --- | --- | --- |
| Windows x64 | [Versão portátil x64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-x64.exe) | A opção principal para PCs de 64 bits; arquivo único, sem instalação, executa diretamente |
| Windows ARM64 | [Versão portátil ARM64](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-1.1.5-win-arm64.exe) | Dispositivos ARM como Surface Pro X / Snapdragon; arquivo único, sem instalação |
| Windows universal | [Instalador universal](https://github.com/DMP-Pig/WinIsland/releases/download/1.1.5/WinIsland-Setup-1.1.5.exe) | Assistente de instalação Inno Setup; instala automaticamente conforme a arquitetura x64 / ARM64 |

Todas as versões anteriores e o changelog completo estão em [GitHub Releases](https://github.com/DMP-Pig/WinIsland/releases).

---

## 📊 Métricas de desempenho

| Métrica | Valor |
| --- | --- |
| Memória residente (Private) | ~72 MB |
| Início a frio | < 1 s |
| CPU em repouso | ≈ 0% |
| Quadros de animação | 60 fps fluidos |
| Múltiplas instâncias | Instância única, evita a execução duplicada |
| Telemetria | 0 telemetria · sem envios · sem anúncios |

---

## Índice

- [Características](#características)
- [Escolha tecnológica e justificativa](#escolha-tecnológica-e-justificativa)
- [Visão geral da arquitetura](#visão-geral-da-arquitetura)
- [Início rápido](#início-rápido)
- [Compilação e empacotamento](#início-rápido)
- [Uso](#uso)
- [Referência de configuração](#referência-de-configuração)
- [Integração com o Cider](#integração-com-o-cider)
- [Letras](#letras)
- [Privacidade e segurança](#privacidade-e-segurança)
- [Métricas não funcionais](#métricas-não-funcionais)
- [Limitações conhecidas](#limitações-conhecidas)
- [Guia de verificação (requer testar com um reprodutor real)](#guia-de-verificação-requer-testar-com-um-reprodutor-real)
- [Perguntas frequentes](#perguntas-frequentes)
- [Licença de código aberto](#licença-de-código-aberto)

---

## Características

### P0 (implementadas)
- **UI flutuante da Ilha Dinâmica (estilo iOS)**: centralizada no topo por padrão (configurável à direita); cápsula de cantos arredondados; segue o tema claro/escuro do sistema ou a cor do tema manual; **animação de transformação** entre cápsula compacta ↔ cartão completo (janela fixa + escala/aparição de um único elemento, impulsionada pela thread de composição do WPF a 60 fps, com rebote elástico estilo iOS); **clique para expandir/recolher** (ao passar o mouse não expande), recolhimento automático ao sair (buffer anti-toque acidental de 700 ms); cliques fora do cartão atravessam a janela.
- **Bloqueio e arraste**: bloqueada por padrão (não pode mover); o menu de contexto permite **desbloquear** (arrastar a Ilha com o mouse após desbloquear), **centralizar** (mesma vertical, centralizada horizontal) e **bloquear** novamente. Ao rebloquear após arrastar, **mantém a posição** (não volta ao padrão); ao soltar o arraste há **ajuste na borda** (adere à borda/centro da tela, configurável em Configurações → Geral).
- **Layout compacto**: título/artista/letras alinhados à esquerda (colados na capa) e centralizados verticalmente.
- **Capa do álbum**: tanto a cápsula quanto o cartão expandido mostram a capa (64 px em tamanho grande ao expandir; ícone de espaço reservado se não houver capa); as miniaturas SMTC e as capas do Cider são armazenadas em cache automaticamente.
- **Controle de reprodução de mídia**: mostra título, artista e álbum; barra de progresso arrastável (seek); reproduzir/pausar, anterior, próxima; controle de volume quando necessário (Cider usa sua API, outras fontes controlam o volume do sistema; pode ser desativado); o componente de mídia mostra o emblema da fonte atual (Spotify / Cider / NetEase Cloud Music / QQ Music, etc.).
- **Mini reprodutor**: janela flutuante independente (configurável em Configurações → Mídia), mostra capa / título / artista / barra de progresso e controles de reprodução; pode ser arrastado livremente e lembra a posição; mostra/oculta automaticamente com a reprodução.
- **Troca do dispositivo de saída de áudio**: Configurações → Mídia permite listar e trocar o dispositivo de saída padrão do sistema (recomenda-se reiniciar o reprodutor após a troca).
- **Suporte a múltiplas fontes**:
  1. Sessões de mídia globais do Windows (`Windows.Media.Control` / SMTC): NetEase Cloud Music, QQ Music, Spotify, Apple Music oficial, Groove, Filmes e TV, etc.;
  2. **Cider**: API HTTP local (porta 10767, compatível com o RPC antigo 10769, varredura automática de portas + configuração manual, suporta autenticação `apptoken`);
  3. Fallback: título da janela + identificação de processo (apenas exibe informações, sem capacidade de controle).
- **Exibição de letras (modo karaokê caractere por caractere)**: ao expandir, a área de letras é exibida em modo karaokê: **os caracteres da linha atual são acesos um a um**; o progresso de realce é um valor contínuo e os caracteres no limite passam suavemente da cor base para a cor de realce com easing a 60 fps, fluindo da esquerda para a direita conforme a ordem de leitura (também correto com letras com quebras de linha, sem acender várias linhas ao mesmo tempo); cada linha começa em 0 (o primeiro caractere não acende no início); ao pausar, o realce congela no momento da pausa: quando o Cider não tem estado explícito, reproduzir/pausar é determinado por «a posição se move» (não confunde mais com reprodução apenas por remainingTime>0), o SMTC prioriza seguir a sessão do Cider (evita que outras sessões ativas como Bilibili a capturem); ao sair e reiniciar, a última posição de pausa é restaurada automaticamente (não volta ao início); a linha atual realça apenas o texto (sem cápsula de fundo, para evitar duplo realce; tamanho grande de 20 px), as demais linhas são esmaecidas, **rolagem suave com centralização automática** (aproxima-se da linha atual quadro a quadro a 60 fps e a acompanha ao expandir); no estado compacto, a linha atual é exibida em tempo real alinhada à esquerda e também acende caractere por caractere; janela flutuante independente de letras opcional.
  - **Sincronização de progresso**: lê automaticamente o token da API local do Cider (sem configuração) para obter o progresso real da reprodução e sincronizar o karaokê com precisão; reprodutores sem progresso disponível usam o relógio local.
  - **Fontes de letras**: `.lrc` local (`%APPDATA%\WinIsland\Lyrics` ou pasta de músicas) → Letras de karaokê caractere por caractere AMLL → interface de letras do Cider → letras on-line (botão de um clique com o botão direito na Ilha). Sem letras, mostra «Sem letras», sem erros.
  - **Letras bilíngues**: combina automaticamente as linhas de tradução com timestamps adjacentes; pode ser desativado nas configurações (não são necessários arquivos de letras adicionais); botão de mostrar/ocultar tradução e «Copiar linha atual» para copiar com um clique.
- **Bandeja do sistema**: ícone permanente, menu de contexto (mostrar/ocultar, janela independente de letras, iniciar com o Windows, configurações, sair), duplo clique para alternar a visibilidade.

### P1 (implementadas)
- **Sistema de componentes (conteúdo personalizável da Ilha)**: Configurações → Componentes permite marcar quais componentes exibir «sem música / com música» e ajustar a ordem arrastando:
  - Hora, clima (Open-Meteo, requer cidade e conexão), data (com calendário lunar e termos solares), uso de CPU, uso de GPU, uso de memória, velocidade da rede (pode mostrar a mini curva dos últimos 32 segundos), bateria, espaço livre em disco, estado do método de entrada (chinês / inglês + nome do IME), botões rápidos (WiFi / Bluetooth / modo noturno / silêncio com um clique), volume, indicador de teclado (CapsLock), área de transferência, tarefas pendentes, Pomodoro, agenda, contagem regressiva de feriados, em reunião, microfone, câmera;
  - Informações da música (capa/título/artista/letras/barra de progresso, apenas durante a reprodução, sempre presente na barra de ordem).
  - A barra de ordem mostra apenas os componentes marcados; a lista e a barra suportam roda do mouse e barras de rolagem; cada componente pode ter um ícone personalizado (ícones MDL2 ou emoji, Configurações → Componentes).
  - Componentes temporários na Ilha: troca de volume, captura / gravação de tela, cópia / movimentação de arquivos, download em andamento (os dois últimos desativados por padrão): quando o evento ocorre, o componente correspondente é exibido temporariamente mesmo se a Ilha estiver oculta.
  - **Cápsula combinada «Em uso»** (Configurações → Componentes, desativada por padrão): ao ativá-la, «Microfone / Câmera / Em reunião / Gravação» selecionados são combinados em uma única cápsula de estado «Em uso · …», e os itens combinados não são mais exibidos separadamente.
  - **Modo de uma linha** (Configurações → Aparência, ativado por padrão): todos os componentes em uma única linha no estado compacto; sem expandir também mostra as informações da música e a linha de letra atual (realce karaokê caractere por caractere), truncando letras longas automaticamente; a barra de progresso e a lista completa de letras são exibidas no cartão expandido.
- **Personalização do conteúdo do cartão expandido**: capa + título, barra de progresso, botões de controle e volume, e área de letras podem ser ativados/desativados separadamente.
- **Personalização da aparência (página de configurações estilo Configurações do Sistema do macOS)**: navegação esquerda + conteúdo direito, vidro líquido arredondado; **18 temas de pele predefinidos** (padrão / oceano / floresta / pôr do sol / neon / monocromático / uva / céu / rosa / âmbar / lima / verde-azulado / lavanda / carmesim / meia-noite / café / sakura / aurora, mais personalizado); **cor extraída do papel de parede** (extrai automaticamente a cor principal do fundo atual como cor do tema, puramente local); **letreiro** (letras longas rolam automaticamente); **4 estilos de onda de áudio** (barras / espectro / anel / partículas) e **4 skins de animação** (mola iOS / suave / elástico / desvanecido); **modo de baixo consumo** (reduz a taxa de quadros das ondas e simplifica as animações em repouso); PerMonitorV2 alta DPI.
- **Atalhos globais**: `Ctrl+Alt+P` reproduzir/pausar · `Ctrl+Alt+←/→` anterior/próxima · `Ctrl+Alt+I` mostrar/ocultar · `Ctrl+Alt+Space` expandir/recolher · `Ctrl+Space` iniciador rápido · `Ctrl+Alt+V` painel do histórico da área de transferência.
- **Reduzir efeitos dinâmicos** (acessibilidade / economia de energia): desativa as animações de mola com um clique, troca instantânea.
- **Ajuste do tamanho da Ilha**: Configurações → Aparência, permite ajustar comprimento/largura compactos e comprimento expandido.
- **Ilha Dinâmica permanente**: sempre visível mesmo sem reprodução (mostra os componentes configurados).
- **Vários monitores**: tela principal / todas as telas / número de tela especificado.
- **Alta DPI**: PerMonitorV2, sem desalinhamentos em 120/150/200 %.


- **Configuração personalizada**: posição, deslocamento, opacidade, cor do tema, conteúdo do modo compacto, ocultar sem mídia, etc.; as alterações são aplicadas instantaneamente.
- **Ocultar automaticamente a Ilha sem reprodução** (pode ser desativado).
- **Não perturbe**: ativação manual com um clique ou silêncio automático por faixa horária (alternar com um clique no menu da bandeja; as faixas são definidas nas configurações).
- **Buscar atualizações**: verificação manual de novas versões no GitHub pelo menu da bandeja / configurações; verificação automática opcional (desativada por padrão, requer conexão).
- **Ação rápida de duplo clique** (Configurações → Geral): pode ser «Reproduzir / Pausar» (padrão), «Expandir / Recolher», «Mostrar área de trabalho», «Ocultar / Mostrar a Ilha», «Anterior», «Próxima», «Abrir configurações» ou «Sem ação».
- **Assistente de silêncio em reuniões (detecção de reuniões)**: identifica janelas de reuniões como Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet, ativa automaticamente o Não perturbe durante a reunião e mostra o componente «Em reunião» (heurística puramente local, sem conexão).
- **Avisos de gravação de tela / captura**: ao pressionar `PrintScreen` / `Alt+PrintScreen` aparece um aviso; ao detectar software de gravação como OBS, Bandicam, Fraps, Camtasia, XSplit, Streamlabs, Xbox Game Bar, aparece «Gravando tela» (detecção local de processos, sem conexão).
- **Não perturbe inteligente (gravação)**: ao detectar gravação de tela em andamento, as notificações são silenciadas automaticamente (sem banner); ao terminar, restaura automaticamente; configurável em Configurações → Notificações.
- **Ocultar automaticamente em tela cheia**: ao detectar vídeo / jogo / apresentação em tela cheia (como PowerPoint), a Ilha é ocultada/recolhida automaticamente e restaurada ao sair da tela cheia; configurável em Configurações → Geral.
- **Arrastar arquivos para a Ilha**: arrastar arquivos/pastas para a Ilha permite «Copiar caminho / Abrir pasta contêiner / Fixar na Ilha», etc. (escolha com o botão direito na Ilha ou no menu de arrastar e soltar).
- **Lembretes de eventos do calendário (.ics)**: analisa arquivos iCalendar locais (Outlook / Google Calendar / exportados do celular); quando chega a hora do evento (com N minutos de antecedência configurável) aparece um banner; análise puramente local, sem conexão.
- **Lembretes de assinaturas RSS**: consulta periodicamente fontes RSS 2.0 / Atom (intervalo ajustável); ao aparecer uma nova entrada, mostra um banner; conecta-se apenas aos endereços de assinatura configurados.
- **Lembretes de e-mail (POP3)**: recupera periodicamente os cabeçalhos das mensagens; com e-mail novo, mostra um banner (apenas lê cabeçalhos, não baixa o corpo nem envia dados; recomenda-se usar um código de autorização).
- **Iniciador rápido (estilo Spotlight)**: abre com `Ctrl+Space`, busca aplicativos instalados / programas do menu Iniciar ou abre diretamente uma URL; o atalho é personalizável.
- **Painel do histórico da área de transferência**: `Ctrl+Alt+V` abre uma janela independente do histórico; ao clicar em um item, copia novamente para a área de transferência; pode ser esvaziado; o atalho é personalizável.
- **Regras (automação)**: Configurações → Regras combina condições (sempre / sem mídia em reprodução / reproduzindo / faixa horária / programa de mídia específico) e ações (ocultar / recolher forçadamente / mostrar forçadamente) para controlar a Ilha automaticamente; ocultar tem prioridade, depois recolher e por último mostrar forçadamente.
- **Modo de baixo consumo**: reduz a taxa de quadros das ondas e simplifica as animações em repouso para economizar energia (Configurações → Geral).

### P2 (implementadas)
- Troca do idioma da interface entre chinês simplificado e inglês.
- Exportar / importar arquivo de configuração JSON.
- Integração com notificações do Windows (Bluetooth / captura das notificações do sistema / em reprodução / bateria fraca).
- Pendente: aviso de chamada recebida (não implementado); lembretes de agenda implementados (componente + ferramenta de produtividade).

---

## Escolha tecnológica e justificativa

| Opção | Conclusão | Justificativa |
| --- | --- | --- |
| **C# + WPF (.NET 8)** | ✅ Adotado | Baixo uso de recursos e inicialização rápida (em comparação ao WebView do Electron/Tauri), maior capacidade de integração com o sistema (suporte nativo a SMTC/CoreAudio/bandeja), empacotamento simples em um único arquivo |
| C++ + Qt | ❌ | Baixa eficiência de desenvolvimento, licença complexa (LGPL), exige muito código escrito à mão para se integrar à pilha de mídia do Windows |
| Tauri / Electron | ❌ | Alto consumo de memória (difícil ficar abaixo de 150 MB em residência), inicialização lenta, não atende ao requisito de «baixo uso de recursos e inicialização rápida» |
| WinUI 3 | ❌ | Empacotamento/distribuição mais complexo que o WPF (requer Windows App SDK), e o suporte a SMTC para aplicativos de desktop não empacotados é igual ao do WPF |

**Pontos-chave**:
- `Windows.Media.Control.GlobalSystemMediaTransportControlsSessionManager` está disponível diretamente por meio da projeção do SDK do Windows do .NET 8 (CsWinRT), sem necessidade de identidade de empacotamento UWP.
- Além das projeções WPF/WinForms/SDK do Windows integradas ao sistema, **zero dependências de terceiros em tempo de execução** (veja [THIRD_PARTY.md](THIRD_PARTY.md)).
- Efeito acrílico: tanto no Win10 quanto no Win11 é implementado com `SetWindowCompositionAttribute` (`ACCENT_ENABLE_ACRYLICBLURBEHIND`) e os cantos arredondados são recortados com `SetWindowRgn` para que o desfoque acompanhe a forma da cápsula.---

## Visão geral da arquitetura

```
src/WinIsland/
├── App.xaml(.cs)              # Raiz de composição: instância única, captura de exceções, bandeja, ciclo de vida da janela
├── Services/
│   ├── MediaModels.cs         # Modelo unificado de instantâneo de mídia (TrackInfo / MediaSnapshot)
│   ├── SmtcMediaProvider.cs   # Sessão de mídia global do Windows (orientada a eventos + envio com throttling)
│   ├── CiderClient.cs         # Wrapper da API local do Cider (V3 + LegacyV2, varredura de portas, análise tolerante)
│   ├── CiderMediaProvider.cs  # Camada de sessão do Cider (ciclo de vida da conexão)
│   ├── WindowTitleMediaProvider.cs # Fallback: reconhecimento do título da janela
│   ├── MediaCoordinator.cs    # Despacho central: Cider > SMTC > título da janela, cache de capas, volume adicional
│   ├── LrcParser.cs           # Análise LRC (vários timestamps, offset, formatos de duração)
│   ├── LyricsService.cs       # Análise de letras (.lrc local → Cider → on-line)
│   ├── OnlineLyricsService.cs # Letras on-line (interfaces não oficiais do NetEase/QQ Music, ativadas por padrão com botão de um clique)
│   ├── ArtworkCache.cs        # Download/cache de capas (capa remota do Cider → arquivo local)
│   ├── SystemVolume.cs        # Volume do sistema CoreAudio (COM P/Invoke)
│   ├── AppSettings.cs         # Leitura/gravação de configuração JSON (%APPDATA%\WinIsland\settings.json)
│   ├── SingleInstance.cs      # Mutex nomeado + pipe nomeado (a segunda inicialização mostra a Ilha)
│   ├── AutoStart.cs           # Chave de inicialização automática HKCU Run
│   ├── GlobalHotkeyService.cs # Atalhos globais (Win32 RegisterHotKey)
│   ├── NotificationService.cs # Banner de notificação de vidro no canto superior direito
│   ├── NotificationHistoryService.cs # Histórico de notificações (últimas 50, persistência JSON)
│   ├── BluetoothMonitor.cs    # Monitoramento de conexão/desconexão de dispositivos Bluetooth
│   ├── SystemNotificationMonitor.cs # Captura das notificações do Windows (espelhamento por automação de UI)
│   ├── MediaAppRegistry.cs    # Registro de programas de mídia (habilitar/desabilitar/ordenar)
│   ├── AudioWaveService.cs    # Ondas de áudio (amostragem do volume do sistema, impulsiona a vibração das ondas)
│   ├── KeyboardIndicatorMonitor.cs # Indicadores de teclado (monitoramento do estado do CapsLock)
│   ├── ClipboardHistoryService.cs # Histórico da área de transferência
│   ├── TodoService.cs         # Lista de tarefas pendentes
│   ├── PomodoroService.cs     # Temporizador Pomodoro
│   ├── ScheduleService.cs     # Lembretes de agenda
│   ├── IcsCalendar.cs       # Análise de calendário .ics (eventos / VALARM)
│   ├── MeetingMonitor.cs    # Detecção de janelas de reunião (assistente de silêncio em reuniões)
│   ├── PrivacyDeviceMonitor.cs # Estado de uso de microfone/câmera (consulta ao registro de privacidade)
│   ├── RssMailService.cs    # Assinaturas RSS + e-mail (POP3)
│   ├── ScreenCaptureMonitor.cs # Detecção de captura/gravação de tela
│   ├── IslandApiServer.cs   # API da Ilha (v1 + v3 HTTP / WebSocket)
│   ├── IslandPushModels.cs  # Modelos de cartão da Ilha (imagem/progresso dinâmico/heartbeat)
│   ├── DoNotDisturb.cs        # Modo Não perturbe (manual/por faixas)
│   ├── UpdaterService.cs      # Verificação de atualizações do GitHub
│   ├── ProfileService.cs      # Perfis de configuração (troca entre vários conjuntos)
│   ├── WeatherService.cs      # Componente de clima (Open-Meteo, requer conexão)
│   ├── PlaybackStateStore.cs  # Persistência da posição de reprodução (restaurar ao sair/pausar)
│   ├── CiderTokenAutoDetect.cs # Detecção automática do token da API do Cider
│   └── AppLogger.cs           # Registro leve em arquivos
├── UI/
│   ├── IslandWindow.xaml(.cs) # Janela da Ilha (animações, acrílico, posicionamento, interação ao passar o mouse)
│   ├── IslandViewModel.cs     # Modelo de visão principal (interpolação de progresso, índice de letras, visibilidade)
│   ├── SettingsWindow.xaml(.cs) / SettingsViewModel.cs
│   ├── LyricsWindow.xaml(.cs) # Janela independente de letras
│   ├── ThemeService.cs        # Tema claro/escuro + pincéis da cor do tema
│   ├── WindowEffects.cs       # Acrílico / modo escuro / zona de cantos arredondados
│   ├── ScreenHelper.cs        # Vários monitores + conversão de DPI PerMonitorV2
│   ├── TrayIcon.cs            # Ícone e menu da bandeja
│   ├── ClipboardPanelWindow.xaml(.cs) # Painel do histórico da área de transferência
│   ├── QuickLauncherWindow.xaml(.cs)  # Iniciador rápido (Ctrl+Space)
│   └── Localization.cs        # Tabela de textos chinês/inglês
└── Diagnostics/DiagnosticsCommand.cs  # Informações de diagnóstico --diagnose
tests/WinIsland.Tests/         # Testes unitários xunit (análise LRC/configuração/Cider/título da janela)
build/
├── publish.ps1                # Publicação com um clique (autônoma ou dependente do framework + zip)
├── WinIsland.iss              # Script de instalação Inno Setup
└── make-icon.ps1 / IconGen.cs # Ferramenta de geração de ícones
```

**Fluxo de dados**: `MediaCoordinator` consulta cada Provider uma vez por segundo (assíncrono, sem bloquear a UI) → gera um `MediaSnapshot` unificado (com caminho da capa local e volume) → publica no `IslandViewModel` por meio do Dispatcher → o interpolador de 200 ms avança suavemente a barra de progresso e o realce das letras → renderização por binding do WPF.---

## Início rápido

> 💡 **Versão pré-compilada**: o diretório `releases/` fornece executáveis autônomos de arquivo único por versão (p. ex., `releases/1.1.5/win-x64/WinIsland-1.1.5-win-x64.exe`, inclui o runtime do .NET 8, execute com duplo clique). As versões beta são mantidas apenas localmente; somente as versões estáveis são publicadas no GitHub (inclui versões portáteis win-x64 / win-arm64 e o instalador universal).

### Requisitos do ambiente
- Windows 10 1809+ / Windows 11
- Máquina de compilação: SDK do .NET 8 (ou um SDK superior especificando `net8.0-windows10.0.19041.0`)

### Compilação
```powershell
# Restaurar + compilar + testar
dotnet build WinIsland.slnx -c Release
dotnet test  WinIsland.slnx -c Release

# Executar (Debug)
dotnet run --project src\WinIsland -c Debug
```

### Publicação com um clique
```powershell
# Autônoma (inclui o runtime do .NET 8, sem instalação, ~73 MB em um único arquivo)
.\build\publish.ps1

# Dependente do framework (tamanho pequeno, requer .NET 8 Desktop Runtime)
.\build\publish.ps1 -FrameworkDependent
```
Os artefatos ficam em `publish\win-x64\` (inclui `WinIsland.exe`); o zip é `publish\WinIsland-win-x64.zip`.

### Instalador (opcional)
Depois de instalar o [Inno Setup 6](https://jrsoftware.org/isinfo.php):
```powershell
iscc.exe build\release-1.1.5.iss
```
Gera `releases\<version>\WinIsland-Setup-<version>.exe` (instalador universal, compatível com x64 e ARM64, instala automaticamente conforme a arquitetura). Ao publicar uma versão estável, copie `release-<version>.iss` para `build\` e atualize o número da versão.

---

## Uso

1. Inicie o `WinIsland.exe` (ou ative a inicialização automática / marque a inicialização automática no instalador). O ícone aparece na bandeja.
2. Reproduza qualquer música:
   - NetEase Cloud Music, QQ Music, Spotify, Apple Music oficial, etc. → é exibido automaticamente por meio da sessão de mídia do sistema;
   - Cider → veja [Integração com o Cider](#integração-com-o-cider);
   - Outros reprodutores → fallback de reconhecimento do título da janela (somente visualização).
3. **Clique** na Ilha para expandir o cartão completo (ao passar o mouse, não expande): arrastar o progresso (seek), controle de reprodução, volume e letras sincronizadas; clique novamente para recolher (ao sair do cartão, recolhe automaticamente após 700 ms).
4. Menu da bandeja: mostrar/ocultar, janela independente de letras, iniciar com o Windows, **Não perturbe** (marcar silencia as notificações), **Buscar atualizações**, **Ver registros**, configurações e sair. **Fechar a janela principal não encerra o processo** (apenas minimiza para a bandeja).
5. Atalhos globais: `Ctrl+Alt+P` reproduzir/pausar · `Ctrl+Alt+←/→` anterior/próxima · `Ctrl+Alt+I` mostrar/ocultar · `Ctrl+Alt+Space` expandir/recolher · `Ctrl+Space` iniciador rápido (buscar aplicativos / digitar uma URL e pressionar Enter) · `Ctrl+Alt+V` painel do histórico da área de transferência (todos podem ser desativados / personalizados).
6. As notificações e avisos (Bluetooth / notificações do Windows / em reprodução / bateria fraca) aparecem por padrão como banners de vidro no canto superior direito; podem ser ativados/desativados em Configurações → Notificações; com o **Não perturbe** ativado, nenhum banner é exibido (o contador do emblema continua contando).
7. Parâmetros comuns de linha de comando:
   ```powershell
   WinIsland.exe --demo       # Modo demonstração (pré-visualização da interface + letras de exemplo sem mídia)
   WinIsland.exe --diagnose   # Gera um relatório de diagnóstico em %APPDATA%\WinIsland\diagnostics.txt
   WinIsland.exe --settings   # Abre as configurações ao iniciar---

## Referência de configuração

Arquivo de configuração: `%APPDATA%\WinIsland\settings.json` (JSON; as alterações da interface de configurações são aplicadas instantaneamente; pode ser exportado/importado).

| Chave | Padrão | Descrição |
| --- | --- | --- |
| `Language` | `zh-CN` | `zh-CN` / `en-US` |
| `ThemePreset` | `Default` | Tema: `Default/Ocean/Forest/Sunset/Neon/Mono/Grape/Sky/Rose/Amber/Lime/Teal/Lavender/Crimson/Midnight/Coffee/Sakura/Aurora/Custom` (substitui AccentColor) |
| `FontFamily` | `Segoe UI` | Fonte da interface |
| `FontScale` | `1.0` | Escala da fonte 0.8–1.4 |
| `CornerRadius` | `28` | Raio dos cantos da cápsula 16–40 |
| `BadgeEnabled` | `true` | Emblema de notificações não lidas (ponto vermelho + número no canto superior direito) |
| `CoverTintBackground` | `true` | O fundo expandido assume a cor da capa do álbum |
| `WaveVisualizerEnabled` | `true` | Ondas de áudio à esquerda dos botões de controle ao reproduzir mídia |
| `WaveStyle` | `Bars` | Estilo de onda: `Bars` (barras) / `Spectrum` (espectro) / `Ring` (anel) / `Particles` (partículas) |
| `WaveSyncEnabled` | `true` | As ondas seguem o ritmo da música (impulsionadas pelo som de saída do sistema) |
| `WaveSensitivity` | `1.0` | Sensibilidade das ondas 0.2–3.0 |
| `WaveHeight` | `1.0` | Altura das ondas 0.4–1.6 |
| `WallpaperThemeColorEnabled` | `false` | Cor do papel de parede: extrai a cor principal do fundo atual como cor do tema (puramente local) |
| `MarqueeTextEnabled` | `false` | Letreiro: rolagem horizontal automática quando o título/letras são largos demais |
| `EdgeSnapEnabled` | `true` | Ao soltar o arraste desbloqueado, adere automaticamente à borda/centro da tela |
| `FullScreenAutoHideEnabled` | `true` | Ocultar automaticamente a Ilha em tela cheia (vídeo/jogo/apresentação) |
| `RecordingDndEnabled` | `false` | Não perturbe automático ao gravar a tela (sem banners de notificação) |
| `Theme` | `Auto` | `Auto` / `Light` / `Dark` |
| `AccentColor` | `#6C5CE7` | Cor do tema (#RRGGBB) |
| `Position` | `Center` | `Center` centralizada no topo / `Right` direita no topo |
| `Monitor` | `Primary` | `Primary` tela principal / `All` todas / `Index` tela especificada |
| `MonitorIndex` | `0` | Número da tela quando `Monitor=Index` |
| `OffsetX` / `OffsetY` | `0` / `16` | Deslocamento em pixels |
| `Opacity` | `0.92` | 0.3–1.0 |
| `HideWhenNoMedia` | `true` | Oculta a Ilha sem reprodução |
| `ShowWhenPaused` | `true` | Continua exibindo ao pausar |
| `StartWithWindows` | `false` | Iniciar com o Windows |
| `StartHidden` | `false` | Ocultar ao iniciar |
| `CompactShowArt/Title/Progress` | `true/true/false` | Conteúdo do modo compacto |
| `IslandAlwaysVisible` | `false` | Ilha permanente (mostra os componentes mesmo sem mídia) |
| `ShowMediaInfo` | `true` | Mostra as informações de reprodução (título/capa/letras, etc.) |
| `ReduceMotion` | `false` | Reduz os efeitos dinâmicos (desativa as animações de mola; acessibilidade/economia de energia) |
| `GlobalHotkeysEnabled` | `true` | Botão dos atalhos globais |
| `LowBatteryThreshold` | `20` | Limite de aviso de bateria fraca (%), 0 para desativar |
| `ExpandedShowArtTitle/Progress/Controls/Lyrics` | `true` | Botões das seções do cartão expandido (capa+título / barra de progresso / controles e volume / letras) |
| `Components` | objeto | Seleção de componentes: `Time/Weather/Date/Cpu/Gpu/Ram/Net/Battery/Volume/CapsLock/Clipboard/Todo/Timer/Schedule/Holiday/Meeting/Mic/Cam`, cada um com duas colunas `WhenIdle`/`WhenPlaying`; `Cover/Title/Artist/Lyrics/Progress` são exibidos durante a reprodução; o dicionário `ComponentBadges` preenche o texto do emblema de cada componente |
| `WidgetOrder` | `Time,Weather,...` | Ordem dos componentes (chaves separadas por vírgula, inclui `Song`) |
| `MediaApps` | `[]` | Habilitar/desabilitar e prioridade dos programas de mídia (vazio = todos habilitados) |
| `CompactWidth` / `CompactHeight` | `360` / `72` | Largura / altura compactas (o ajuste manual por arraste desativa o ajuste automático) |
| `CompactWidthAuto` / `CompactHeightAuto` | `true` | O tamanho compacto ajusta-se automaticamente ao conteúdo (ativado por padrão) |
| `ExpandedWidthAuto` / `MaxExpandedHeightAuto` | `true` | Ajuste automático do tamanho expandido (ativado por padrão) |
| `ExpandedWidth` / `MaxExpandedHeight` | `400` / `384` | Largura expandida / altura máxima expandida |
| `BluetoothNotifyEnabled` | `false` | Aviso de conexão/desconexão Bluetooth |
| `NotificationTakeoverEnabled` | `false` | Captura das notificações do Windows (best effort) |
| `NotificationTimeoutSeconds` | `6` | Duração do banner de notificação (segundos) || `NotificationPosition` | `TopRight` | Posição das notificações (canto superior direito) |
| `DoNotDisturbEnabled` / `DoNotDisturbManual` | `false` | Não perturbe: automático por faixas / manual |
| `DoNotDisturbStartHour` / `DoNotDisturbEndHour` | `22` / `8` | Faixa de Não perturbe (horas) |
| `DnDAllowlist` | `[]` | Lista branca de Não perturbe (`QQ.exe,WeChat.exe`; dentro da lista branca, as notificações são exibidas) |
| `Rules` | `[]` | Lista de regras de automação (condição + ação) |
| `ClipboardHistoryEnabled` / `ClipboardHistoryMax` | `false` / `15` | Botão do histórico da área de transferência e número máximo de entradas |
| `PomodoroEnabled` / `PomodoroWorkMinutes` / `PomodoroBreakMinutes` | `false` / `25` / `5` | Botão do Pomodoro e duração de trabalho/descanso (minutos) |
| `KeyIndicatorSeconds` | `3` | Duração do indicador de teclado (CapsLock) (segundos) |
| `VolumeTempIndicatorEnabled` / `VolumeTempIndicatorSeconds` | `true` / `4` | Botão e duração do indicador temporário de volume/silêncio na Ilha |
| `FileCopyNotifyEnabled` | `true` | Cópia/movimentação de arquivos na Ilha (reconhecimento local do título da janela) |
| `DownloadProgressEnabled` | `false` | Download em andamento na Ilha (escaneia arquivos temporários da pasta de downloads; desativado por padrão) |
| `UsageMergeEnabled` / `UsageMergeItems` | `false` / `Mic,Cam,Meeting,Recording` | Cápsula combinada «Em uso» e componentes participantes (desativada por padrão) |
| `AutoUpdateCheck` | `false` | Verificar automaticamente novas versões do GitHub (desativado por padrão, requer conexão) |
| `DoubleClickAction` | `PlayPause` | Ação de duplo clique: `PlayPause` / `ToggleExpand` / `ShowDesktop` / `ToggleVisible` / `NextTrack` / `PrevTrack` / `OpenSettings` / `None` |
| `AnimationStyle` | `Spring` | Skin de animação: `Spring` / `Soft` / `Elastic` / `Fade` |
| `ThemeTint` | `""` | Cor de fundo personalizada #RRGGBB (aplicada quando o preset é Custom) |
| `ExpandedCardStyle` | `Classic` | Modelo do cartão expandido: `Classic` / `Hero` |
| `NetCurveEnabled` | `true` | O componente de rede mostra a mini curva dos últimos 32 segundos |
| `LowPowerMode` | `false` | Modo de baixo consumo (reduz a taxa de quadros das ondas e simplifica as animações em repouso) |
| `MeetingAssistantEnabled` | `false` | Assistente de silêncio em reuniões: detecta janelas de reunião + Não perturbe automático |
| `MeetingAutoDnd` / `MeetingKeywords` | `true` / `""` | Não perturbe automático em reuniões / palavras-chave personalizadas de reuniões |
| `ScreenCaptureNotifyEnabled` / `ScreenshotNotifyEnabled` / `RecordingNotifyEnabled` | `false` / `true` / `true` | Botão geral e subitens dos avisos de captura/gravação |
| `CalendarEnabled` / `CalendarIcsPath` / `CalendarAdvanceMinutes` | `false` / `""` / `10` | Botão de lembretes de calendário .ics / caminho do arquivo / minutos de antecedência |
| `RssNotifyEnabled` / `RssUrls` / `RssIntervalMinutes` | `false` / `""` / `15` | Lembretes RSS / endereços de assinatura / intervalo de consulta (minutos) |
| `MailNotifyEnabled` / `MailPop3Server` / `MailPop3Port` / `MailUseSsl` / `MailUser` / `MailPassword` / `MailCheckMinutes` | `false` / `""` / `995` / `true` / `""` / `""` / `5` | Lembretes de e-mail (POP3): botão, servidor, porta, SSL, conta, código de autorização e intervalo de verificação |
| `QuickLauncherEnabled` / `HotkeyLauncher` | `true` / `Ctrl+Space` | Botão do iniciador rápido e atalho |
| `ClipboardPanelEnabled` / `HotkeyClipboardPanel` | `true` / `Ctrl+Alt+V` | Botão do painel do histórico da área de transferência e atalho |
| `HotkeyExpand` | `Ctrl+Alt+Space` | Atalho de expandir/recolher |
| `NotifyFoldEnabled` | `true` | Recolher notificações semelhantes (mesma fonte e mesmo título mostra apenas uma) |
| `ActiveProfile` | `Default` | Nome do perfil de configuração (troca entre vários conjuntos) |

---

## Integração com o Cider

O Cider (cliente de terceiros do Apple Music) fornece uma API HTTP local. O WinIsland já encapsula um módulo independente (`CiderClient.cs`) que se adapta automaticamente às diferenças de versão.

**Passos para ativar (importante)**:
1. Abra o Cider: **Configurações → Conectividade → Permitir controle externo (Manage External Application Access)**; ao ativar, o Cider mostrará o token da API (se estiver vazio, clique para gerá-lo).
2. Copie o token em **Configurações do WinIsland → Cider → API Token** e salve.
3. A porta padrão é `10767` e o WinIsland a detecta automaticamente; o RPC antigo é `10769`.

> ⚠️ As novas versões do Cider 2.x exigem **token em todas as solicitações de API por padrão** (sem token retorna `403 UNAUTHORIZED_APP_TOKEN`). Se os registros de diagnóstico indicarem que o token é necessário, preencha-o seguindo os passos acima; caso contrário, as letras/controles do Cider não estarão disponíveis (as faixas ainda podem ser exibidas via SMTC).

> ⚠️ Se os registros mostrarem repetidamente HttpClient.Timeout (originalmente 2 s), geralmente é porque o software de segurança/proxy local intercepta o HTTP de loopback (a resposta real do Cider leva cerca de 30 ms). Desde a 1.0.1, o tempo limite de leitura de dados foi ampliado para 5 s; se continuar expirando, verifique se o antivírus bloqueia a conexão de rede do WinIsland.

**Capacidades da API implementadas** (de acordo com a documentação da comunidade do Cider / crate `cider-api` verificado, versão 2026):
- `GET /api/v1/playback/active`, `GET /now-playing` (faixa/capa/progresso/estado)
- `POST /api/v1/playback/play|pause|playpause|next|previous|seek`
- `GET|POST /api/v1/playback/volume`
- `GET /api/v1/lyrics` (inclui fallback `?id=`)
- Cabeçalho de autenticação: `apptoken` (compatível com `apitoken`)
- Antigo 10769: `/active`, `/currentPlayingSong`, `/playPause`, `/next`, `/previous`, `/seekto/{t}`, `/audio`

> ⚠️ A API do Cider não é oficial e muda rapidamente; todas as solicitações têm tempo limite de 2 segundos e, em caso de falha, degradam automaticamente para SMTC / título da janela, **sem afetar o fluxo principal**. Mantenha o WinIsland atualizado para se adaptar às novas versões.
---

## Letras

Prioridade:
1. **.lrc local**: procura `Música.lrc` / `Artista - Música.lrc` nos diretórios de letras (por padrão `%APPDATA%\WinIsland\Lyrics`, `Música\Lyrics` e a raiz de `Música`);
2. **Letras de karaokê caractere por caractere AMLL** (linha do tempo TTML caractere por caractere da biblioteca de músicas da amll.dev, ativadas por padrão);
3. **Interface de letras do Cider** (quando a fonte é o Cider);
4. **Letras on-line** (interfaces não oficiais do NetEase / QQ Music): **ativadas por padrão**; com o botão direito na Ilha, podem ser ativadas/desativadas com um clique, ou desativadas nas configurações.

> ⚠️ As letras on-line usam interfaces não oficiais e são apenas para aprendizado pessoal; respeite os direitos autorais; se o titular dos direitos exigir, você pode desativar esse recurso a qualquer momento (ficando totalmente off-line após desativar).

---

## Notificações e alertas (desde 1.0.2, melhorado na 1.0.3)

Todas as notificações são **banners de vidro no canto superior direito**, com animação de entrada estilo macOS (entra pela direita + fade) e de saída; a duração de exibição é configurável (3–15 segundos).

- **Aviso de conexão Bluetooth**: Configurações → Notificações; ao ativar, aparece quando um dispositivo Bluetooth conecta/desconecta.
- **Captura das notificações do Windows**: Configurações → Notificações; ao ativar, espelha via automação de UI (best effort) o conteúdo da central de notificações (como as notificações do QQ) nos banners do canto superior direito.
  > ⚠️ O Windows não fornece uma API pública para «interceptar notificações de outros aplicativos»; esse recurso é best effort e algumas notificações podem não ser capturadas; não afeta o fluxo principal.
- **Notificação em reprodução**: ao trocar de música, o banner «Em reprodução - Título» aparece automaticamente (desde 1.0.3).
- **Aviso de bateria fraca**: aparece quando a bateria fica abaixo do limite (padrão 20%, ajustável 0–50), uma vez por ciclo de carga (desde 1.0.3).
- **Histórico de notificações**: registros das últimas 50 notificações; a página Configurações → Notificações permite visualizá-las / esvaziá-las (desde 1.0.3).
- **Ajuste do tamanho da Ilha**: Configurações → Aparência permite ajustar o comprimento/largura compactos e o comprimento expandido.

---
## API da Ilha (enviar para a Ilha Dinâmica a partir de outros aplicativos)

O WinIsland inclui um serviço HTTP local; outros aplicativos podem enviar informações para a Ilha Dinâmica em tempo real (semelhante à integração de apps de terceiros na Ilha Dinâmica do iOS). **Documentação para desenvolvedores em [docs/IslandAPI.md](docs/IslandAPI.md)**.

| Interface | Descrição |
|---|---|
| `POST /v1/island/push` | Envia / atualiza um cartão da Ilha (desde a v3 suporta imagens / progresso dinâmico / heartbeat) |
| `PATCH /v3/island/push/{id}` | Atualização parcial: sobrescreve apenas os campos presentes no corpo (preserva a expiração / posição na fila) |
| `DELETE /v1/island/push/{id}` | Remove um cartão |
| `GET /v1/island/active` (ou `/v3/island/active`) | Consulta o cartão ativo atual |
| `GET /v3/ws` | Canal bidirecional WebSocket: o cliente envia `push/update/remove/ping`, o servidor transmite os eventos `push_updated/push_removed` |
| `GET /v1/health` | Verificação de saúde |

- Configurações → API da Ilha: botão de ativação, porta (padrão 9840), token opcional e duração de exibição padrão global
- As notificações da Ilha **não alteram o comprimento/largura da Ilha**; o cartão é exibido em uma única linha no estado compacto e não oculta outros componentes
- Os botões suportam «Abrir link / Iniciar programa»; o remetente pode personalizar a duração de exibição por entrada (substitui o padrão global)
- Novo na v3: `image` (imagem data URI ou http), `progress_from/progress_to/progress_duration_seconds` (progresso automático), `heartbeat_seconds` (renovação por heartbeat; se não for renovado em mais de 2 vezes o intervalo, é removido automaticamente), `theme` (tema do cartão dark/light/auto), `action: "command"` (o botão executa um comando local); documentação completa em [docs/IslandAPI.md](docs/IslandAPI.md)

---
## Restauração do estado de reprodução

- Ao sair do aplicativo, pausar ou trocar de música, salva «faixa + posição de reprodução» em `%APPDATA%\WinIsland\state.json` (somente local).
- Na próxima inicialização, se for a mesma faixa e o reprodutor ainda não retornar o progresso real, a última posição é restaurada primeiro para evitar o salto de «mostrar a linha 0 e depois pular para a frase da pausa»; não restaura após mais de 1 hora ou se a faixa mudou.

---
## Privacidade e segurança

- **Sem telemetria, sem anúncios, sem envios**. Além das «letras on-line» ativadas manualmente pelo usuário, o aplicativo não faz nenhuma solicitação de rede.- **Componente de clima**: somente quando ativa «Mostrar clima» e digita uma cidade, consulta o Open-Meteo (gratuito, sem chave, sem conta) para o clima atual; se não estiver ativado, funciona totalmente off-line.
- Únicos cenários de conexão: download de capas do Cider (`mzstatic.com`, URL pública da capa retornada pela API local) e letras on-line ativadas pelo usuário.
- Todos os dados são armazenados localmente em `%APPDATA%\WinIsland\`.
- Os registros guardam apenas informações de execução local (`logs\app-*.log`).

---

## Métricas não funcionais

Medido na máquina de teste (Windows 11 24H2, 2560×1440 a 100 %) (Release autônoma):

| Métrica | Medido | Objetivo |
| --- | --- | --- |
| CPU em repouso (sem mídia) | < 0,5 % (0,3 % medido em Debug) | ≈ 0% |
| Memória residente (Private) | ~72 MB | ≤ 150 MB |
| Inicialização | < 1 s (a frio) | ≤ 2 s |
| Fechar a janela principal | Não sai, apenas minimiza para a bandeja | ✅ |
| Múltiplas instâncias | Apenas uma; a segunda inicialização mostra a Ilha | ✅ |
| Exceções | Captura unificada e registro em arquivo, sem caixa de bloqueio | ✅ |

> Nota: o WorkingSet da implementação autônoma (inclui páginas compartilhadas do runtime do .NET) é de cerca de 160 MB, mas **a memória Private é de cerca de 72 MB**; com implementação dependente do framework, o WorkingSet será menor.

---

## Limitações conhecidas

- **O karaokê caractere por caractere depende da fonte de letras e do progresso**: com uma linha do tempo TTML/LRC caractere por caractere da AMLL, o realce é feito caractere por caractere; sem linha do tempo caractere por caractere ou se o reprodutor não fornecer o progresso real, degrada para realçar a frase completa (avanço com o relógio local).
- **Progresso que retrocede ocasionalmente** (p. ex., Cider/SMTC relata 0 ou uma posição expirada em um instante): uma proteção de posição foi implementada: o retrocesso instantâneo é ignorado e o avanço atual é mantido, sem devolver as letras/barra de progresso ao início; somente após um retrocesso sustentado de cerca de 4 segundos é considerado uma reposição real ou um seek do reprodutor.
- **Aviso de chamada recebida**: não implementado (opcional na P2). Implementados: aviso de Bluetooth, captura das notificações do Windows (best effort), notificação em reprodução, aviso de bateria fraca e lembretes de agenda.
- **Cobertura do SMTC**: depende de o reprodutor registrar a sessão de mídia global; alguns reprodutores antigos que não a registram só podem ser cobertos pelo título da janela (sem botões de controle).
- **Cider 1.x (API antiga, porta 9000)**: não adaptado; apenas 2.x e superiores são suportados.

---

## Guia de verificação (requer testar com um reprodutor real)

Os cenários a seguir exigem verificação em um ambiente real (indicando o que já foi verificado automaticamente no ambiente de desenvolvimento deste repositório):

| Cenário | Status |
| --- | --- |
| Enumeração de sessões SMTC (com `--diagnose` a lista de sessões é exibida) | ✅ Testado (detecta sessões reais como Bilibili) |
| Mostrar/ocultar automático da Ilha, expandir/recolher com clique, interpolação de progresso | ✅ Testado (demo + sessão real pausada) |
| Reproduzir/pausar/trocar/seek (reprodutor real) | ⚠️ Requer testes (os caminhos de código correspondem diretamente à API de controle do SMTC) |
| Conexão e controle da API do Cider | ⚠️ Requer instalar o Cider na máquina e ativar o controle externo |
| Rolagem sincronizada de letras .lrc locais | ✅ A análise LRC é testada por unidades; o ponta a ponta requer uma música real |
| Letras on-line | ✅ Integrado com NetEase/QQ Music; o efeito ponta a ponta requer uma música real |

**Passos de verificação sugeridos**:
1. `WinIsland.exe --diagnose` → confirme que `System media sessions` lista o reprodutor;
2. Reproduza qualquer música do NetEase/QQ Music/Spotify → a Ilha deve mostrar a faixa e permitir o controle;
3. Abra o Cider e ative o controle externo → a fonte da Ilha deve mostrar `Cider`, com seek/volume;
4. Coloque um `.lrc` com o mesmo nome na pasta da música → ao expandir, as letras devem rolar e ser realçadas conforme o progresso.
---

## Perguntas frequentes

**P: A Ilha Dinâmica não aparece?**
- Confirme que há reprodução (ao pausar, continua exibindo por padrão); `HideWhenNoMedia` está ativado por padrão; ocultar-se sem mídia é normal.
- Execute `--diagnose` para ver a lista de sessões; se a lista estiver vazia, o reprodutor não registrou o SMTC.

**P: O Cider mostra «Não conectado»?**
- Confirme que «Permitir controle externo» está ativado nas configurações do Cider; verifique a porta (padrão 10767); confirme que o Cider está habilitado nas configurações do WinIsland.

**P: As letras on-line não carregam?**
- As letras on-line estão ativadas por padrão (botão direito na Ilha → Letras on-line para alternar com um clique); se ainda não houver letras, confirme em Configurações → Letras que estão ativadas e verifique a conectividade de rede.

**P: O ícone da bandeja continua lá após sair?**
- Menu da bandeja → Sair; fechar diretamente a janela da Ilha apenas a oculta (conforme o design de «residência na bandeja»).

---

## Licença de código aberto

- Aplicativo: MIT (veja [LICENSE](LICENSE))
- Componentes de terceiros: veja [THIRD_PARTY.md](THIRD_PARTY.md)

---


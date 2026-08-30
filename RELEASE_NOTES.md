<div align="center">

**🌐 选择语言 / Select Language**

[简体中文](#简体中文) · [繁體中文](#繁體中文) · [English](#english) · [Español](#español) · [Français](#français) · [العربية](#العربية) · [Русский](#русский) · [Português](#português)

</div>

> **说明 / Note**: 以简体中文为标准 · Simplified Chinese is the standard reference.

---

## 简体中文

## WinIsland 1.1.5（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- **AMLL 逐字歌词（真·卡拉OK）**：接入 amll.dev 的 Apple Music 风格 TTML 曲库，设置 → 歌词「AMLL 逐字歌词」开关（默认开启）；来源优先级：本地 LRC → AMLL TTML → Cider → 在线歌词
- **逐字高亮引擎重写**：60fps 墙钟连续推进 + 非线性缓动；整行均分兜底；双语歌词按时间轴匹配
- **暂停高亮稳定**：暂停后冻结在暂停时刻，退出重启不跳动；仅活动行驱动动画，空闲近 0 CPU
- **紧凑态歌词间距修复**：歌词与右侧按钮保持固定间距；空间不足自动扩大岛宽（720→800）
- **稳定性**：AMLL 5 秒超时 + 优雅降级；控件重新可见校准墙钟基准

## WinIsland 1.1.4（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- **快捷操作按钮**：展开灵动岛卡片底部新增一排可自定义的快捷按钮（锁屏 / 静音 / 播放暂停 / 截图 / 显示桌面 / 任务管理器 / 计算器 / 睡眠 / 音量±）；设置 → 快捷操作 可勾选与 ↑↓ 排序，修改即时生效
- **来电提醒**：检测微信 / QQ 的语音、视频通话窗口，右上角弹出提醒（区分「来电」与「通话中」）；设置 → 通知 可开关并自定义检测应用；仅本机检测，不上传数据
- **上岛命令动作**：第三方上岛推送按钮新增 `action: "command"`，可在本地执行命令行（仅本机回环 API，可配 Token）
- **上岛卡片主题**：第三方推送可携带 `theme: dark / light / auto`，推送卡片自动切换深浅色玻璃样式
- **动画提速**：四种动效皮肤整体时长缩短约 20%，保持 iOS 弹簧缓动与 60fps 丝滑不跳帧
- **后台占用优化**：键盘指示灯仅在组件启用时轮询、全屏监控降频、音频波纹空闲降频
- **稳定性修复**：修复启动时对剪贴板已有内容误弹「已复制」提示（启动基线）；修复日历 .ics 标题转义（\, \; \n）显示为反斜杠的问题
- **性能与稳定性**：蓝牙 / SMTC / 天气接口日志降噪，天气限流指数退避；构建通过，单元与集成测试 104 项全部通过

## WinIsland 1.1.3（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- **多播放器切换**：同时打开多个播放器时，展开卡可一键切换当前控制的媒体来源（音符图标 + 来源名 + 下拉箭头），不再只能控制最后启动的播放器
- **封面沉浸**：点击展开卡的专辑封面 / 大图即可打开全屏封面预览，点击 / Esc / 右键淡入淡出关闭
- **歌词时间微调**：展开卡歌词区增设 +0.5s / -0.5s 按钮，按歌曲记忆时间偏移，让歌词与音乐完美对齐
- **桌面歌词增强**：独立歌词小窗支持不透明度调节（默认 0.85）与「锁定」开关（锁定后鼠标穿透、不可拖动）
- **动态主题呼吸**：开启封面取色背景时，展开卡背景色随封面颜色缓慢「呼吸」起伏（约 18 秒一周期），不再是静止的平板色块
- **点击抢先**：按下鼠标立即切换展开 / 收起，不再等待松开才响应，手感更跟手
- **通知操作按钮**：通知横幅支持操作按钮（蓝牙连接提示现带「断开」「设置」按钮，点击立即执行并收起）
- **上岛按钮回调**：第三方上岛的 notify 动作按钮被点击时，通过 WebSocket 向推送方广播 push_button 事件（含 push_id 与按钮文字），推送方自行处理回调
- **性能与稳定性**：动态主题变为按需订阅合成帧（空闲 0 CPU）、渐变画刷缓存复用降低 GC 压力；修复新版本与旧版本实例同时运行互斥体冲突导致无法启动的问题


## WinIsland 1.1.1（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- **电量提醒**：低电量提醒（阈值可调），连接电源且充到设定阈值（默认 100%）时弹出「充电完成」提醒，均为本地检测、可开关
- **网络提醒**：断网 / 网络恢复时弹出提示（本地网络状态检测，可开关）
- **新组件**：磁盘剩余空间（系统盘）、输入法状态（中 / 英 + 输入法名称）
- **农历与节气**：日期组件可附加显示农历日期与节气（默认开启，可在设置中关闭）
- **快捷开关组件**：WiFi / 蓝牙 / 夜间模式 / 静音 一键切换（走本地 API，无联网；Radio 状态 2 秒缓存，避免开销）
- **播放来源徽标**：媒体组件上显示当前播放来源（Spotify / Cider / 网易云 / QQ音乐等），一眼可知来自哪个播放器
- **歌词增强**：歌词翻译显示 / 隐藏开关，「复制当前行」按钮一键复制当前歌词
- **组件图标自定义**：每个组件可单独自定义图标（MDL2 图标或 Emoji），不设置则使用默认字形
- **修复歌词缩放跳动**：移除卡拉OK逐字回弹位移导致的歌词「放大又缩小」抖动；展开态当前行歌词的字号 / 透明度改为 300ms 平滑过渡，滚动更丝滑稳定
## WinIsland 1.1.0（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- **音量 / 静音临时上岛**：系统音量变化、静音 / 取消静音时，灵动岛短暂显示音量指示（显示时长可调，可在设置中开关）
- **文件复制 / 移动上岛**：检测到资源管理器正在复制 / 移动文件时，灵动岛显示「正在复制文件…」提示（纯本地窗口标题识别，可开关）
- **下载进度上岛**：检测下载目录中的浏览器临时文件（.crdownload / .part / .download 等），显示「正在下载 N 个文件」（默认关闭，可在设置中开启）
- **「使用中」合并胶囊**：设置 → 组件可开启（默认关闭），把「麦克风 / 摄像头 / 会议中 / 录屏」合并为单个「使用中 · …」状态胶囊；可勾选哪些组件参与合并，参与合并的项不再单独显示
- **番茄钟增强**：点击灵动岛上的番茄钟组件可暂停 / 继续计时
- **截图 / 录屏临时上岛**：截图或开始录制时，灵动岛临时显示对应指示（灵动岛隐藏时也能触发）
- **卡拉OK逐字点亮回弹**：每句歌词从第一个字开始平滑点亮，带轻微回弹动效，更流畅自然
- 内部轮询架构优化：隐藏状态下也能触发音量 / 复制 / 下载 / 截图等临时上岛事件

## WinIsland 1.0.9（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- **新组件**：GPU 占用、麦克风 / 摄像头使用中、节假日倒计时、会议中；网络组件可显示最近 32 秒迷你曲线
- **双击快捷动作**：设置 → 通用可设双击灵动岛动作为「播放 / 暂停」「打开设置」或「无动作」
- **开会静音助手**：识别会议窗口（Teams / Zoom / 腾讯会议 / 钉钉 / 飞书 / Webex / Slack / Discord / Google Meet），会议中自动勿扰（纯本地启发式）
- **屏幕录制 / 截图提示**：PrintScreen 截图提示 + 录制软件（OBS / Bandicam / Xbox Game Bar 等）检测提示
- **日历事件提醒（.ics）**：解析本地 iCalendar 文件，事件到点（可提前 N 分钟）弹横幅，纯本地
- **RSS 订阅提醒**：轮询 RSS 2.0 / Atom，新条目弹横幅
- **邮件提醒（POP3）**：只读邮件头，新邮件弹横幅，建议使用授权码
- **快速启动器（Spotlight 风格）**：`Ctrl+Space` 搜索应用 / 输入网址打开
- **剪贴板历史面板**：`Ctrl+Alt+V` 独立窗口，点击复制回剪贴板
- **规则（自动化）**：条件（始终 / 未播放 / 播放中 / 时间段 / 指定媒体程序）× 动作（隐藏 / 强制收起 / 强制显示）
- **上岛 API v3**：图片（data URI / http）、动态进度（from/to/duration 自动推进）、心跳续期（heartbeat_seconds）、PATCH 部分更新、WebSocket 通道（/v3/ws）
- **外观**：18 种主题皮肤预设、自定义背景色、4 种动效皮肤、低功耗模式
- 设置页改为 macOS System Settings 风格（左侧导航 + 右侧内容），所有改动即时生效

- **修复深色模式黑字**：为设置界面所有自定义控件模板（按钮 / 复选框 / 输入框 / 下拉框 / 下拉项 / 页签 / 左侧导航等）统一绑定前景色，并新增运行时兜底扫描——深色模式下不再出现个别选项（界面语言、双击动作等下拉框）显示黑字看不清的问题，浅色模式自动恢复深色文字
- **修复设置界面无法打开**：移除重复的 XAML 前景色行导致的 BAML 加载失败
- **动画性能优化**：卡拉OK逐字复用 Run 对象（消除逐帧布局）、稳定 60fps 故事板、日志批量刷新，动画更丝滑
- **移除组件上的红色角标圆点**（按用户要求）

## WinIsland 1.0.8（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容

**设置界面全面重构**
- 采用类 macOS 系统设置布局：左侧导航列表 + 右侧内容区，共 13 个分类（通用 / 外观 / 组件 / 媒体 / 媒体信息展示 / 歌词 / Cider / 上岛 API / 效率工具 / 更新 / 关于 / 通知 / 规则）
- 深色 / 浅色模式下所有设置文字颜色自动适配：深色模式白字、浅色模式黑字，不再出现看不清的问题
- 左侧导航文字改为高对比白色，去掉右侧白色分隔线，强化悬停与选中反馈

**媒体播放**
- 新增迷你播放器：独立悬浮小窗，展示专辑封面 / 歌名 / 歌手 / 进度条与播放控制，可自由拖动并记忆位置，随媒体播放自动显示 / 隐藏（可在设置中开启）
- 新增音频输出设备切换：设置 → 媒体 可枚举并切换系统默认播放设备（切换后建议重启播放器生效）
- 播放器来源底层增强：支持枚举全部 SMTC 媒体会话与 Cider 会话，可切换媒体来源

**歌词**
- 新增双语歌词：自动合并相邻时间戳的翻译行（可在设置中关闭）

**外观与动效**
- 新增动效皮肤：4 种动画风格（iOS 弹簧（默认）/ 柔和弹簧 / 弹性回弹 / 简洁渐隐），展开 / 收起使用非线性缓动
- 新增低功耗模式：空闲时降低波纹渲染帧率、简化动画，更省电

**全局快捷键**
- 5 个可自定义组合键：显示 / 隐藏、播放 / 暂停、上一首、下一首、展开 / 收起
- 支持 Ctrl / Alt / Shift / Win + 字母、数字、F1–F24、方向键

**智能规则引擎（设置 → 规则）**
- 按条件自动控制灵动岛显示：始终生效 / 未播放媒体时 / 正在播放媒体时 / 指定时间段 / 指定媒体程序播放时
- 动作：隐藏灵动岛 / 强制收起 / 强制显示；优先级：隐藏 > 折叠 > 强制显示

**通知**
- 通知历史支持：未读红点标记、全部已读、单条删除、点击条目打开来源应用、清空历史
- 新增通知折叠：同来源同标题的重复通知复用同一横幅并累加数量
- 新增勿扰白名单：白名单内的来源（逗号分隔 exe 名）不受勿扰影响，仍正常弹出横幅
- 移除灵动岛上的未读红点角标

**效率工具**
- 复制文本时弹出「已复制」提示
- 自动识别短信验证码并高亮提示
- 大文本复制显示进度动画（按长度估算推进，完成后再显示结果）

**组件**
- 新增节假日倒计时组件：内置 2026–2027 年节假日表（元旦 / 春节 / 清明 / 劳动节 / 端午 / 中秋 / 国庆），显示「XX N 天后」或「今日 XX」，可在组件设置中开关

**上岛 API v2**
- 新增字段：subtitle（副标题）、type（info / success / warning / error）、priority（high / normal / low）、accent（自定义强调色）、click（整卡点击回跳）
- 推送队列：多条推送按优先级高 → 低、先入先出排列；同 id 重复推送保留原队列位置与过期时间
- POST 响应新增 position 字段
- 新增可直接运行的示例脚本 docs/sdk-examples/（push.bat / pull.bat / push.ps1 / push.py / pull.py）

**修复**
- 修复设置窗口无法打开 / 双击无法运行的问题（导航与标签页切换增加空值保护并显式初始化）
- 新增 69 个自动化测试（上岛 API、通知折叠与白名单、规则引擎、验证码识别、LRC 歌词解析），全部通过

### 资产 Assets
- Windows x64 / arm64 便携版（单文件自包含，免安装直接运行）
- Windows 通用安装包（Inno Setup，同时支持 x64 与 ARM64，自动按架构安装）

> 便携版为独立 exe 文件，不再提供 ZIP 压缩包。
## WinIsland 1.0.7（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- 「声音波纹」升级为**跟随音乐节奏**：通过 WASAPI 环回实时采集系统正在播放的真实音频，节拍强时浪高、安静时浪低，不再是固定音量条
- **60fps 连贯渲染**：起音 25ms / 释放 140ms 指数平滑，波纹起伏连贯、不生硬、不卡顿
- 新增设置（设置 → 外观 → 声音波纹）：跟随音乐节奏开关、灵敏度 0.2–3.0、波纹高度 0.4–1.6，改动即时生效
- 无音频设备 / 音频服务异常时自动降级为节拍模拟，并每 8 秒自动重试恢复实时采集，不卡死、不堆积线程

## WinIsland 1.0.6（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- 新增 6 个灵动岛组件：音量、键盘指示灯（CapsLock）、剪贴板、待办、番茄钟、日程，均支持「无歌曲 / 有歌曲」双列勾选与可拖拽排序
- 新增「效率工具」设置页：剪贴板历史、番茄钟计时、待办列表、日程提醒
- 新增「声音波纹」：播放媒体时，控制按钮左侧随系统音量实时抖动（设置 → 外观可开关）
- 新增 7 种主题预设：默认 / 海洋 / 森林 / 日落 / 霓虹 / 单色 / 葡萄紫
- 外观个性化：自定义字体、字号缩放（0.8–1.4）、胶囊圆角半径（16–40）、展开背景随专辑封面取色、未读通知角标
- 托盘菜单新增：勿扰模式（手动 / 按时段自动静默通知）、检查更新、查看日志
- 设置页现代化改版（圆角 + 液态玻璃），选项改动即时生效，无需手动保存
- 新增更新检查（托盘 / 设置手动检查，可选自动检查，默认关闭）


### 资产 Assets
- Windows x64 / arm64 便携版（单文件自包含，免安装直接运行）
- Windows 通用安装包（Inno Setup，同时支持 x64 与 ARM64，自动按架构安装）

> 便携版为独立 exe 文件，不再提供 ZIP 压缩包。

## WinIsland 1.0.5（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- 新增「上岛 API」：其他软件可通过本地 HTTP 接口把信息推送到灵动岛（类似 iOS 灵动岛第三方 App 集成），**开发文档见 docs/IslandAPI.md**
  - `POST /v1/island/push` 推送/更新 · `DELETE /v1/island/push/{id}` 移除 · `GET /v1/island/active` 查询 · `GET /v1/health`
  - 支持图标、标题、正文、进度、按钮（打开链接/启动程序）、按条自定义显示时长
  - 设置页提供启用 / 端口 / 可选 Token / 全局默认时长
- 上岛卡片在紧凑态单行展示、不遮挡其它组件、**不影响灵动岛长宽**（自动 / 手动尺寸恒定）
- 尺寸「自动调整」：按内容自适应，手动拖动滑杆会自动关闭对应自动项
- 展开内容支持滚轮滚动（隐藏滚动条）
- 组件上下对齐统一；启动布局 / 字体修复（强制 PerMonitorV2，启动即正常大小）
- 播放媒体不再弹「正在播放」通知

- 修复：展开灵动岛后（约 1~2 秒）卡片回退到紧凑尺寸导致整体黑屏的缺陷
  - 展开内容改为与紧凑行重叠交叉淡入淡出，动画全程无背景透出
  - 展开/收起动画完成后显式写回最终卡片尺寸，展开态稳定不缩回
  - 同步修复展开态点击第三方上岛按钮黑屏的问题


### 资产 Assets
- Windows x64 / arm64 便携版（单文件自包含，免安装直接运行）
- Windows 通用安装包（Inno Setup，同时支持 x64 与 ARM64，自动按架构安装）

> 便携版为独立 exe 文件，不再提供 ZIP 压缩包。

---

## 繁體中文

## WinIsland 1.1.5（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容
- **AMLL 逐字歌詞（真·卡拉OK）**：接入 amll.dev 的 Apple Music 風格 TTML 曲庫，設定 → 歌詞「AMLL 逐字歌詞」開關（預設開啟）；來源優先順序：本地 LRC → AMLL TTML → Cider → 線上歌詞
- **逐字高亮引擎重寫**：60fps 牆鐘連續推進 + 非線性緩動；整行均分兜底；雙語歌詞按時間軸匹配
- **暫停高亮穩定**：暫停後凍結在暫停時刻，退出重啟不跳動；僅活動行驅動動畫，閒置近 0 CPU
- **緊湊態歌詞間距修復**：歌詞與右側按鈕保持固定間距；空間不足自動擴大島寬（720→800）
- **穩定性**：AMLL 5 秒逾時 + 優雅降級；控制項重新可見時校準牆鐘基準

## WinIsland 1.1.4（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容
- **快捷操作按鈕**：展開動態島卡片底部新增一排可自訂的快捷按鈕（鎖定螢幕 / 靜音 / 播放暫停 / 截圖 / 顯示桌面 / 工作管理員 / 計算機 / 睡眠 / 音量±）；設定 → 快捷操作 可勾選與 ↑↓ 排序，修改即時生效
- **來電提醒**：偵測微信 / QQ 的語音、視訊通話視窗，右上角彈出提醒（區分「來電」與「通話中」）；設定 → 通知 可開關並自訂偵測應用程式；僅本機偵測，不上傳資料
- **上島指令動作**：第三方上島推播按鈕新增 `action: "command"`，可在本機執行命令列（僅本機回環 API，可配 Token）
- **上島卡片主題**：第三方推播可攜帶 `theme: dark / light / auto`，推播卡片自動切換深淺色玻璃樣式
- **動畫加速**：四種動效皮膚整體時長縮短約 20%，保持 iOS 彈簧緩動與 60fps 流暢不跳幀
- **背景佔用最佳化**：鍵盤指示燈僅在元件啟用時輪詢、全螢幕監控降頻、音訊波紋閒置降頻
- **穩定性修復**：修復啟動時對剪貼簿既有內容誤彈「已複製」提示（啟動基線）；修復行事曆 .ics 標題跳脫（\, \; \n）顯示為反斜線的問題
- **效能與穩定性**：藍牙 / SMTC / 天氣介面日誌降噪，天氣限流指數退避；建置通過，單元與整合測試 104 項全部通過

## WinIsland 1.1.3（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容
- **多播放器切換**：同時開啟多個播放器時，展開卡可一鍵切換目前控制的媒體來源（音符圖示 + 來源名稱 + 下拉箭頭），不再只能控制最後啟動的播放器
- **封面沉浸**：點擊展開卡的專輯封面 / 大圖即可開啟全螢幕封面預覽，點擊 / Esc / 右鍵淡入淡出關閉
- **歌詞時間微調**：展開卡歌詞區新增 +0.5s / -0.5s 按鈕，按歌曲記憶時間偏移，讓歌詞與音樂完美對齊
- **桌面歌詞增強**：獨立歌詞小視窗支援不透明度調整（預設 0.85）與「鎖定」開關（鎖定後滑鼠穿透、不可拖曳）
- **動態主題呼吸**：開啟封面取色背景時，展開卡背景色隨封面顏色緩慢「呼吸」起伏（約 18 秒一週期），不再是靜止的平板色塊
- **點擊搶先**：按下滑鼠立即切換展開 / 收起，不再等待放開才回應，手感更跟手
- **通知操作按鈕**：通知橫幅支援操作按鈕（藍牙連線提示現帶「中斷連線」「設定」按鈕，點擊立即執行並收起）
- **上島按鈕回呼**：第三方上島的 notify 動作按鈕被點擊時，透過 WebSocket 向推播方廣播 push_button 事件（含 push_id 與按鈕文字），推播方自行處理回呼
- **效能與穩定性**：動態主題改為按需訂閱合成幀（閒置 0 CPU）、漸層畫筆快取重用降低 GC 壓力；修復新版本與舊版本執行個體同時執行互斥體衝突導致無法啟動的問題


## WinIsland 1.1.1（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容
- **電量提醒**：低電量提醒（閾值可調），連接電源且充到設定閾值（預設 100%）時彈出「充電完成」提醒，均為本機偵測、可開關
- **網路提醒**：斷網 / 網路恢復時彈出提示（本機網路狀態偵測，可開關）
- **新元件**：磁碟剩餘空間（系統磁碟）、輸入法狀態（中 / 英 + 輸入法名稱）
- **農曆與節氣**：日期元件可附加顯示農曆日期與節氣（預設開啟，可在設定中關閉）
- **快捷開關元件**：WiFi / 藍牙 / 夜間模式 / 靜音 一鍵切換（走本地 API，無連網；Radio 狀態 2 秒快取，避免開銷）
- **播放來源徽標**：媒體元件上顯示目前播放來源（Spotify / Cider / 網易雲 / QQ音樂等），一眼可知來自哪個播放器
- **歌詞增強**：歌詞翻譯顯示 / 隱藏開關，「複製目前列」按鈕一鍵複製目前歌詞
- **元件圖示自訂**：每個元件可單獨自訂圖示（MDL2 圖示或 Emoji），不設定則使用預設字形
- **修復歌詞縮放跳動**：移除卡拉OK逐字回彈位移導致的歌詞「放大又縮小」抖動；展開態目前列歌詞的字號 / 透明度改為 300ms 平滑過渡，捲動更流暢穩定
## WinIsland 1.1.0（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容
- **音量 / 靜音臨時上島**：系統音量變化、靜音 / 取消靜音時，動態島短暫顯示音量指示（顯示時長可調，可在設定中開關）
- **檔案複製 / 移動上島**：偵測到檔案總管正在複製 / 移動檔案時，動態島顯示「正在複製檔案…」提示（純本地視窗標題辨識，可開關）
- **下載進度上島**：偵測下載目錄中的瀏覽器暫存檔（.crdownload / .part / .download 等），顯示「正在下載 N 個檔案」（預設關閉，可在設定中開啟）
- **「使用中」合併膠囊**：設定 → 元件可開啟（預設關閉），把「麥克風 / 攝影機 / 會議中 / 錄製螢幕」合併為單一「使用中 · …」狀態膠囊；可勾選哪些元件參與合併，參與合併的項目不再單獨顯示
- **番茄鐘增強**：點擊動態島上的番茄鐘元件可暫停 / 繼續計時
- **截圖 / 錄製螢幕臨時上島**：截圖或開始錄製時，動態島暫時顯示對應指示（動態島隱藏時也能觸發）
- **卡拉OK逐字點亮回彈**：每句歌詞從第一個字開始平滑點亮，帶輕微回彈動效，更流暢自然
- 內部輪詢架構最佳化：隱藏狀態下也能觸發音量 / 複製 / 下載 / 截圖等臨時上島事件

## WinIsland 1.0.9（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容
- **新元件**：GPU 佔用、麥克風 / 攝影機使用中、假日倒數、會議中；網路元件可顯示最近 32 秒迷你曲線
- **雙擊快捷動作**：設定 → 一般可設雙擊動態島動作為「播放 / 暫停」「開啟設定」或「無動作」
- **開會靜音助手**：辨識會議視窗（Teams / Zoom / 騰訊會議 / 釘釘 / 飛書 / Webex / Slack / Discord / Google Meet），會議中自動勿擾（純本地啟發式）
- **螢幕錄製 / 截圖提示**：PrintScreen 截圖提示 + 錄製軟體（OBS / Bandicam / Xbox Game Bar 等）偵測提示
- **行事曆事件提醒（.ics）**：解析本地 iCalendar 檔案，事件到點（可提前 N 分鐘）彈出橫幅，純本地
- **RSS 訂閱提醒**：輪詢 RSS 2.0 / Atom，新條目彈出橫幅
- **郵件提醒（POP3）**：唯讀郵件標頭，新郵件彈出橫幅，建議使用授權碼
- **快速啟動器（Spotlight 風格）**：`Ctrl+Space` 搜尋應用程式 / 輸入網址開啟
- **剪貼簿歷史面板**：`Ctrl+Alt+V` 獨立視窗，點擊複製回剪貼簿
- **規則（自動化）**：條件（永遠 / 未播放 / 播放中 / 時間段 / 指定媒體程式）× 動作（隱藏 / 強制收起 / 強制顯示）
- **上島 API v3**：圖片（data URI / http）、動態進度（from/to/duration 自動推進）、心跳續期（heartbeat_seconds）、PATCH 部分更新、WebSocket 通道（/v3/ws）
- **外觀**：18 種主題皮膚預設、自訂背景色、4 種動效皮膚、低功耗模式
- 設定頁改為 macOS System Settings 風格（左側導覽 + 右側內容），所有變更即時生效

- **修復深色模式黑字**：為設定介面所有自訂控制項範本（按鈕 / 核取方塊 / 輸入方塊 / 下拉方塊 / 下拉項目 / 頁籤 / 左側導覽等）統一繫結前景色，並新增執行階段兜底掃描——深色模式下不再出現個別選項（介面語言、雙擊動作等下拉方塊）顯示黑字看不清楚的問題，淺色模式自動恢復深色文字
- **修復設定介面無法開啟**：移除重複的 XAML 前景色行導致的 BAML 載入失敗
- **動畫效能最佳化**：卡拉OK逐字重用 Run 物件（消除逐幀版面）、穩定 60fps Storyboard、日誌批次重新整理，動畫更流暢
- **移除元件上的紅色角標圓點**（依使用者要求）

## WinIsland 1.0.8（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容

**設定介面全面重構**
- 採用類 macOS 系統設定版面：左側導覽清單 + 右側內容區，共 13 個分類（一般 / 外觀 / 元件 / 媒體 / 媒體資訊顯示 / 歌詞 / Cider / 上島 API / 效率工具 / 更新 / 關於 / 通知 / 規則）
- 深色 / 淺色模式下所有設定文字顏色自動適應：深色模式白字、淺色模式黑字，不再出現看不清楚的問題
- 左側導覽文字改為高對比白色，移除右側白色分隔線，強化懸停與選取回饋

**媒體播放**
- 新增迷你播放器：獨立浮動小視窗，展示專輯封面 / 歌名 / 歌手 / 進度列與播放控制，可自由拖曳並記憶位置，隨媒體播放自動顯示 / 隱藏（可在設定中開啟）
- 新增音訊輸出裝置切換：設定 → 媒體 可列舉並切換系統預設播放裝置（切換後建議重新啟動播放器生效）
- 播放器來源底層增強：支援列舉全部 SMTC 媒體工作階段與 Cider 工作階段，可切換媒體來源

**歌詞**
- 新增雙語歌詞：自動合併相鄰時間戳記的翻譯列（可在設定中關閉）

**外觀與動效**
- 新增動效皮膚：4 種動畫風格（iOS 彈簧（預設）/ 柔和彈簧 / 彈性回彈 / 簡潔漸隱），展開 / 收起使用非線性緩動
- 新增低功耗模式：閒置時降低波紋渲染幀率、簡化動畫，更省電

**全域快速鍵**
- 5 個可自訂組合鍵：顯示 / 隱藏、播放 / 暫停、上一首、下一首、展開 / 收起
- 支援 Ctrl / Alt / Shift / Win + 字母、數字、F1–F24、方向鍵

**智慧規則引擎（設定 → 規則）**
- 依條件自動控制動態島顯示：永遠生效 / 未播放媒體時 / 正在播放媒體時 / 指定時間段 / 指定媒體程式播放時
- 動作：隱藏動態島 / 強制收起 / 強制顯示；優先順序：隱藏 > 摺疊 > 強制顯示

**通知**
- 通知歷史支援：未讀紅點標記、全部標為已讀、單筆刪除、點擊項目開啟來源應用程式、清空歷史
- 新增通知摺疊：同來源同標題的重複通知共用同一橫幅並累加數量
- 新增勿擾白名單：白名單內的來源（逗號分隔 exe 名稱）不受勿擾影響，仍正常彈出橫幅
- 移除動態島上的未讀紅點角標

**效率工具**
- 複製文字時彈出「已複製」提示
- 自動辨識簡訊驗證碼並高亮提示
- 大量文字複製顯示進度動畫（依長度估算推進，完成後再顯示結果）

**元件**
- 新增假日倒數元件：內建 2026–2027 年假日表（元旦 / 春節 / 清明 / 勞動節 / 端午 / 中秋 / 國慶），顯示「XX N 天後」或「今日 XX」，可在元件設定中開關

**上島 API v2**
- 新增欄位：subtitle（副標題）、type（info / success / warning / error）、priority（high / normal / low）、accent（自訂強調色）、click（整卡點擊回跳）
- 推播佇列：多條推播依優先順序高 → 低、先入先出排列；同 id 重複推播保留原佇列位置與到期時間
- POST 回應新增 position 欄位
- 新增可直接執行的範例腳本 docs/sdk-examples/（push.bat / pull.bat / push.ps1 / push.py / pull.py）

**修復**
- 修復設定視窗無法開啟 / 雙擊無法執行的問題（導覽與頁籤切換增加空值保護並明確初始化）
- 新增 69 個自動化測試（上島 API、通知摺疊與白名單、規則引擎、驗證碼辨識、LRC 歌詞解析），全部通過

### 資產 Assets
- Windows x64 / arm64 攜帶版（單一檔案自包含，免安裝直接執行）
- Windows 通用安裝套件（Inno Setup，同時支援 x64 與 ARM64，自動依架構安裝）

> 攜帶版為獨立 exe 檔案，不再提供 ZIP 壓縮檔。
## WinIsland 1.0.7（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容
- 「聲音波紋」升級為**跟隨音樂節奏**：透過 WASAPI 環迴即時擷取系統正在播放的真實音訊，節拍強時浪高、安靜時浪低，不再是固定音量列
- **60fps 連貫渲染**：起音 25ms / 釋放 140ms 指數平滑，波紋起伏連貫、不生硬、不卡頓
- 新增設定（設定 → 外觀 → 聲音波紋）：跟隨音樂節奏開關、靈敏度 0.2–3.0、波紋高度 0.4–1.6，變更即時生效
- 無音訊裝置 / 音訊服務異常時自動降級為節拍模擬，並每 8 秒自動重試恢復即時擷取，不卡死、不堆積執行緒

## WinIsland 1.0.6（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容
- 新增 6 個動態島元件：音量、鍵盤指示燈（CapsLock）、剪貼簿、待辦、番茄鐘、行程，均支援「無歌曲 / 有歌曲」雙欄勾選與可拖曳排序
- 新增「效率工具」設定頁：剪貼簿歷史、番茄鐘計時、待辦清單、行程提醒
- 新增「聲音波紋」：播放媒體時，控制按鈕左側隨系統音量即時抖動（設定 → 外觀可開關）
- 新增 7 種主題預設：預設 / 海洋 / 森林 / 日落 / 霓虹 / 單色 / 葡萄紫
- 外觀個人化：自訂字型、字型大小縮放（0.8–1.4）、膠囊圓角半徑（16–40）、展開背景隨專輯封面取色、未讀通知角標
- 系統匣功能表新增：勿擾模式（手動 / 依時段自動靜音通知）、檢查更新、檢視紀錄
- 設定頁現代化改版（圓角 + 液態玻璃），選項變更即時生效，無需手動儲存
- 新增更新檢查（系統匣 / 設定手動檢查，可選自動檢查，預設關閉）


### 資產 Assets
- Windows x64 / arm64 攜帶版（單一檔案自包含，免安裝直接執行）
- Windows 通用安裝套件（Inno Setup，同時支援 x64 與 ARM64，自動依架構安裝）

> 攜帶版為獨立 exe 檔案，不再提供 ZIP 壓縮檔。

## WinIsland 1.0.5（正式版 / Stable）

一款現代化、多功能的 Windows 動態島元件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新內容
- 新增「上島 API」：其他軟體可透過本地 HTTP 介面把資訊推送到動態島（類似 iOS 動態島第三方 App 整合），**開發文件見 docs/IslandAPI.md**
  - `POST /v1/island/push` 推送/更新 · `DELETE /v1/island/push/{id}` 移除 · `GET /v1/island/active` 查詢 · `GET /v1/health`
  - 支援圖示、標題、內文、進度、按鈕（開啟連結/啟動程式）、依條目自訂顯示時長
  - 設定頁提供啟用 / 連接埠 / 選用 Token / 全域預設時長
- 上島卡片在緊湊態單行顯示、不遮擋其他元件、**不影響動態島長寬**（自動 / 手動尺寸恆定）
- 尺寸「自動調整」：依內容自適應，手動拖曳滑桿會自動關閉對應的自動項目
- 展開內容支援滾輪捲動（隱藏捲軸）
- 元件上下對齊統一；啟動版面 / 字型修復（強制 PerMonitorV2，啟動即正常大小）
- 播放媒體不再彈出「正在播放」通知

- 修復：展開動態島後（約 1~2 秒）卡片回退到緊湊尺寸導致整體黑屏的缺陷
  - 展開內容改為與緊湊列重疊交叉淡入淡出，動畫全程無背景透出
  - 展開/收起動畫完成後明確寫回最終卡片尺寸，展開態穩定不縮回
  - 同步修復展開態點擊第三方上島按鈕黑屏的問題


### 資產 Assets
- Windows x64 / arm64 攜帶版（單一檔案自包含，免安裝直接執行）
- Windows 通用安裝套件（Inno Setup，同時支援 x64 與 ARM64，自動依架構安裝）

> 攜帶版為獨立 exe 檔案，不再提供 ZIP 壓縮檔。

---

## English

## WinIsland 1.1.5 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New
- **AMLL word-by-word lyrics (true karaoke)**: connects to the Apple Music-style TTML library at amll.dev; Settings → Lyrics, "AMLL word-by-word lyrics" toggle (on by default); source priority: local LRC → AMLL TTML → Cider → online lyrics
- **Word-by-word highlight engine rewritten**: continuous 60fps wall-clock progression + non-linear easing; fallback evenly divides each line; bilingual lyrics matched by timeline
- **Stable pause highlighting**: after pausing, the highlight freezes at the pause moment and doesn't jump on exit or restart; only the active line drives the animation, near 0 CPU when idle
- **Compact-state lyrics spacing fix**: the lyrics keep a fixed gap from the buttons on the right; when space runs short, the Island width automatically expands (720→800)
- **Stability**: 5-second AMLL timeout + graceful degradation; the wall-clock baseline is recalibrated when the control becomes visible again

## WinIsland 1.1.4 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New
- **Quick action buttons**: A customizable row of quick buttons has been added to the bottom of the expanded Dynamic Island card (lock screen / mute / play-pause / screenshot / show desktop / task manager / calculator / sleep / volume±); go to Settings → Quick Actions to check items and reorder with ↑↓, and changes take effect instantly
- **Incoming call alerts**: Detects WeChat / QQ voice and video call windows and pops up an alert in the top-right corner (distinguishes "incoming call" from "in call"); Settings → Notifications lets you toggle this and customize which apps are detected; detection is local-only — no data is uploaded
- **Island command actions**: Third-party Island push buttons now support `action: "command"` to run a command line locally (local loopback API only, Token configurable)
- **Island card themes**: Third-party pushes can carry `theme: dark / light / auto`, and the pushed card automatically switches between dark and light glass styles
- **Faster animations**: The total duration of all four animation skins is shortened by about 20%, keeping the iOS spring easing and smooth 60fps rendering with no dropped frames
- **Background usage optimization**: Keyboard indicators are polled only when the widget is enabled, fullscreen monitoring runs at a lower frequency, and audio ripples are throttled when idle
- **Stability fixes**: Fixed a false "Copied" toast for pre-existing clipboard content at startup (startup baseline); fixed calendar .ics title escaping (\, \; \n) being displayed as backslashes
- **Performance & stability**: Reduced log noise for the Bluetooth / SMTC / weather APIs, with exponential backoff for weather rate limiting; the build passes and all 104 unit and integration tests pass

## WinIsland 1.1.3 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New
- **Multi-player switching**: When multiple players are open, the expanded card can switch the currently controlled media source with one click (note icon + source name + dropdown arrow); you're no longer limited to controlling only the last launched player
- **Immersive album art**: Click the album art / large image on the expanded card to open a fullscreen cover preview; click / Esc / right-click to close with a fade
- **Fine-tune lyrics timing**: The lyrics area of the expanded card now has +0.5s / -0.5s buttons that remember a per-song time offset so the lyrics align perfectly with the music
- **Enhanced desktop lyrics**: The standalone lyrics window supports opacity adjustment (default 0.85) and a "Lock" toggle (once locked, the mouse passes through and the window can't be dragged)
- **Breathing dynamic theme**: When the cover-color-derived background is enabled, the expanded card background slowly "breathes" with the cover color (about an 18-second cycle) instead of being a static flat color block
- **Instant click response**: Pressing the mouse immediately toggles expand / collapse instead of waiting for release, for a more responsive feel
- **Notification action buttons**: Notification banners support action buttons (the Bluetooth connection prompt now has "Disconnect" and "Settings" buttons; clicking executes immediately and collapses the banner)
- **Island button callbacks**: When a notify action button from a third-party Island push is clicked, a push_button event (with push_id and the button text) is broadcast to the pusher over WebSocket; the pusher handles the callback itself
- **Performance & stability**: Dynamic themes now subscribe to composition frames on demand (0 CPU when idle) and the gradient brush cache is reused to reduce GC pressure; fixed the mutex conflict that prevented startup when new and old instances ran simultaneously


## WinIsland 1.1.1 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New
- **Battery alerts**: Low-battery alerts (adjustable threshold); when plugged in and charged to the set threshold (default 100%), a "Charging complete" alert pops up; all detection is local and can be toggled
- **Network alerts**: Shows a notification when the network disconnects / reconnects (local network status detection, toggleable)
- **New widgets**: Disk free space (system drive), input method status (CN / EN + input method name)
- **Lunar calendar and solar terms**: The date widget can additionally show the lunar date and solar terms (enabled by default, can be turned off in settings)
- **Quick toggle widgets**: One-click toggles for WiFi / Bluetooth / night mode / mute (uses the local API, no network needed; Radio state is cached for 2 seconds to avoid overhead)
- **Playback source badges**: The media widget shows the current playback source (Spotify / Cider / NetEase Cloud Music / QQ Music, etc.) so you can tell at a glance which player it comes from
- **Lyrics enhancements**: A toggle to show / hide lyrics translations, plus a "Copy current line" button to copy the current lyric in one click
- **Custom widget icons**: Each widget can have its own custom icon (MDL2 icon or Emoji); the default glyph is used when none is set
- **Fixed lyrics scaling jitter**: Removed the "enlarge and shrink" jitter of lyrics caused by the karaoke word-by-word bounce offset; the font size / opacity of the current lyric line in the expanded state now transitions smoothly over 300ms, making scrolling smoother and more stable
## WinIsland 1.1.0 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New
- **Temporary volume / mute Island**: When the system volume changes or mute is toggled on / off, the Dynamic Island briefly shows a volume indicator (display duration adjustable; toggleable in settings)
- **File copy / move Island**: When File Explorer is detected copying / moving files, the Dynamic Island shows a "Copying files…" indicator (pure local window-title detection, toggleable)
- **Download progress Island**: Detects browser temporary files in the download directory (.crdownload / .part / .download, etc.) and shows "Downloading N files" (off by default; can be enabled in settings)
- **"In Use" merged capsule**: Can be enabled under Settings → Widgets (off by default); merges "Microphone / Camera / In a meeting / Screen recording" into a single "In Use · …" status capsule; you can choose which widgets join the merge, and merged items no longer show separately
- **Pomodoro enhancements**: Click the Pomodoro widget on the Dynamic Island to pause / resume the timer
- **Temporary screenshot / screen recording Island**: When you take a screenshot or start recording, the Dynamic Island briefly shows the corresponding indicator (it triggers even when the Island is hidden)
- **Karaoke word-by-word lighting with bounce**: Each lyric line lights up smoothly from the first character, with a slight bounce effect, more fluid and natural
- Internal polling architecture optimization: temporary Island events such as volume / copy / download / screenshot can now also trigger while hidden

## WinIsland 1.0.9 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New
- **New widgets**: GPU usage, microphone / camera in use, holiday countdown, in a meeting; the network widget can show a mini graph of the last 32 seconds
- **Double-click quick actions**: Settings → General lets you set the double-click action of the Dynamic Island to "Play / Pause", "Open Settings", or "No action"
- **Meeting mute assistant**: Detects meeting windows (Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet) and automatically enables Do Not Disturb during meetings (purely local heuristics)
- **Screen recording / screenshot alerts**: PrintScreen screenshot prompts + detection prompts for recording software (OBS / Bandicam / Xbox Game Bar, etc.)
- **Calendar event reminders (.ics)**: Parses local iCalendar files and shows a banner when an event starts (can be up to N minutes early); purely local
- **RSS subscription alerts**: Polls RSS 2.0 / Atom and shows a banner for new entries
- **Email alerts (POP3)**: Reads only email headers and shows a banner for new mail; using an authorization code is recommended
- **Quick launcher (Spotlight-style)**: Press `Ctrl+Space` to search apps / type a URL to open
- **Clipboard history panel**: `Ctrl+Alt+V` opens a standalone window; click to copy back to the clipboard
- **Rules (automation)**: Conditions (always / not playing / playing / time range / specific media program) × actions (hide / force collapse / force show)
- **Island API v3**: Images (data URI / http), dynamic progress (from/to/duration advances automatically), heartbeat renewal (heartbeat_seconds), PATCH partial updates, WebSocket channel (/v3/ws)
- **Appearance**: 18 theme skin presets, custom background color, 4 animation skins, low-power mode
- The settings page now uses a macOS System Settings style (left navigation + right content), and all changes take effect instantly

- **Fixed black text in dark mode**: Unified the foreground-color binding for all custom control templates in the settings UI (buttons / checkboxes / input boxes / dropdowns / dropdown items / tabs / left navigation, etc.) and added a runtime fallback scan — individual options (UI language, double-click action, and other dropdowns) no longer show unreadable black text in dark mode, and light mode automatically restores dark text
- **Fixed the settings window not opening**: Removed duplicate XAML foreground-color rows that caused BAML loading failures
- **Animation performance optimization**: The karaoke word-by-word effect reuses Run objects (eliminating per-frame layout), with a stable 60fps Storyboard and batched log refresh for smoother animations
- **Removed the red badge dot on widgets** (per user request)

## WinIsland 1.0.8 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New

**Complete settings UI redesign**
- macOS-like system settings layout: left navigation list + right content area, with 13 categories (General / Appearance / Widgets / Media / Media Info Display / Lyrics / Cider / Island API / Productivity Tools / Updates / About / Notifications / Rules)
- All settings text colors adapt automatically in dark / light mode: white text in dark mode, black text in light mode — no more readability problems
- Left navigation text changed to high-contrast white, the white divider on the right removed, and hover / selection feedback strengthened

**Media playback**
- New mini player: a standalone floating window showing album art / song title / artist / progress bar and playback controls; freely draggable with remembered position, auto-shows / auto-hides with media playback (can be enabled in settings)
- New audio output device switching: Settings → Media can enumerate and switch the system's default playback device (restarting the player after switching is recommended for it to take effect)
- Enhanced player source layer: supports enumerating all SMTC media sessions and Cider sessions, and switching media sources

**Lyrics**
- New bilingual lyrics: translation lines with adjacent timestamps are automatically merged (can be turned off in settings)

**Appearance & animations**
- New animation skins: 4 animation styles (iOS spring (default) / soft spring / elastic bounce / simple fade), with non-linear easing for expand / collapse
- New low-power mode: reduces the ripple rendering frame rate and simplifies animations when idle to save more power

**Global shortcuts**
- 5 customizable key combinations: show / hide, play / pause, previous track, next track, expand / collapse
- Supports Ctrl / Alt / Shift / Win + letters, numbers, F1–F24, arrow keys

**Smart rule engine (Settings → Rules)**
- Automatically controls the Dynamic Island display based on conditions: always active / when no media is playing / when media is playing / during a specified time range / when a specified media program is playing
- Actions: hide the Dynamic Island / force collapse / force show; priority: hide > collapse > force show

**Notifications**
- Notification history support: unread red-dot markers, mark all as read, delete individual entries, click an entry to open the source app, clear history
- New notification collapsing: duplicate notifications with the same source and title reuse the same banner and accumulate a count
- New Do Not Disturb whitelist: sources in the whitelist (comma-separated exe names) are not affected by Do Not Disturb and still show banners normally
- Removed the unread red-dot badge on the Dynamic Island

**Productivity tools**
- Shows a "Copied" toast when copying text
- Automatically recognizes SMS verification codes and highlights them
- Large text copies show a progress animation (estimated by length; the result is shown when complete)

**Widgets**
- New holiday countdown widget: includes a 2026–2027 holiday table (New Year's Day / Spring Festival / Qingming / Labor Day / Dragon Boat Festival / Mid-Autumn Festival / National Day), showing "XX in N days" or "Today XX"; toggleable in widget settings

**Island API v2**
- New fields: subtitle, type (info / success / warning / error), priority (high / normal / low), accent (custom accent color), click (whole-card click callback)
- Push queue: multiple pushes are ordered by priority high → low, first in first out; pushing again with the same id keeps the original queue position and expiration time
- The POST response now includes a position field
- New runnable example scripts at docs/sdk-examples/ (push.bat / pull.bat / push.ps1 / push.py / pull.py)

**Fixes**
- Fixed the settings window not opening / not launching on double-click (added null guards and explicit initialization for navigation and tab switching)
- Added 69 automated tests (Island API, notification collapsing and whitelist, rule engine, verification code recognition, LRC lyric parsing), all passing

### Assets
- Windows x64 / arm64 portable builds (single self-contained file, runs without installation)
- Windows universal installer (Inno Setup, supports both x64 and ARM64, installs automatically by architecture)

> The portable build is a standalone exe file; ZIP archives are no longer provided.
## WinIsland 1.0.7 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New
- "Sound ripple" upgraded to **follow the music beat**: it captures the real audio currently playing on the system in real time via WASAPI loopback — waves are taller on strong beats and lower when quiet, no longer a fixed volume bar
- **60fps continuous rendering**: exponential smoothing with 25ms attack / 140ms release for fluid, natural wave motion without jank
- New settings (Settings → Appearance → Sound ripple): follow-the-music-beat toggle, sensitivity 0.2–3.0, ripple height 0.4–1.6; changes take effect instantly
- When there's no audio device / the audio service is abnormal, it automatically falls back to beat simulation and retries real-time capture every 8 seconds — no freezing, no thread buildup

## WinIsland 1.0.6 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New
- 6 new Dynamic Island widgets: volume, keyboard indicators (CapsLock), clipboard, to-dos, Pomodoro, schedule — all support "no song / with song" dual-column checkboxes and drag-to-reorder
- New "Productivity Tools" settings page: clipboard history, Pomodoro timer, to-do list, schedule reminders
- New "Sound ripple": while media is playing, the area left of the control buttons shakes in real time with the system volume (toggleable under Settings → Appearance)
- 7 new theme presets: Default / Ocean / Forest / Sunset / Neon / Monochrome / Grape Purple
- Appearance personalization: custom font, font-size scaling (0.8–1.4), capsule corner radius (16–40), expanded background tinted from the album art, unread notification badge
- Tray menu additions: Do Not Disturb mode (manual / auto-silence notifications by time period), check for updates, view logs
- Modernized settings pages (rounded corners + liquid glass); option changes take effect instantly, no manual saving needed
- New update check (manual check from tray / settings, optional automatic check, off by default)


### Assets
- Windows x64 / arm64 portable builds (single self-contained file, runs without installation)
- Windows universal installer (Inno Setup, supports both x64 and ARM64, installs automatically by architecture)

> The portable build is a standalone exe file; ZIP archives are no longer provided.

## WinIsland 1.0.5 (Stable Release)

A modern, multi-functional Dynamic Island widget for Windows.

### What's New
- New "Island API": other software can push information to the Dynamic Island through a local HTTP interface (similar to third-party app integrations with the iOS Dynamic Island); **developer docs at docs/IslandAPI.md**
  - `POST /v1/island/push` push/update · `DELETE /v1/island/push/{id}` remove · `GET /v1/island/active` query · `GET /v1/health`
  - Supports icons, title, body, progress, buttons (open link / launch program), and per-item custom display duration
  - The settings page provides enable / port / optional Token / global default duration
- Island cards display on a single line in compact mode, don't cover other widgets, and **don't affect the Dynamic Island's dimensions** (constant size in auto / manual modes)
- "Auto-adjust" sizing: adapts to the content; manually dragging the slider automatically turns off the corresponding auto option
- Expanded content supports wheel scrolling (scrollbar hidden)
- Uniform vertical alignment of widgets; startup layout / font fixes (forced PerMonitorV2, correct size from startup)
- Playing media no longer pops up a "Now Playing" notification

- Fix: the card falling back to compact size about 1~2 seconds after expanding, causing a fully black screen
  - Expanded content now cross-fades with the overlapping compact row, with no background showing through during the animation
  - After the expand/collapse animation completes, the final card size is explicitly written back, keeping the expanded state stable without shrinking back
  - Also fixed the black screen when clicking third-party Island buttons in the expanded state


### Assets
- Windows x64 / arm64 portable builds (single self-contained file, runs without installation)
- Windows universal installer (Inno Setup, supports both x64 and ARM64, installs automatically by architecture)

> The portable build is a standalone exe file; ZIP archives are no longer provided.

---

## Español

## WinIsland 1.1.5 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades
- **Letras palabra por palabra de AMLL (auténtico karaoke)**: se conecta a la biblioteca TTML de amll.dev con estilo de Apple Music; Ajustes → Letras, interruptor «AMLL letras palabra por palabra» (activado por defecto); prioridad de fuentes: LRC local → AMLL TTML → Cider → letras en línea
- **Motor de resaltado palabra por palabra reescrito**: avance continuo de reloj de pared a 60 fps + easing no lineal; respaldo de reparto uniforme por línea; letras bilingües emparejadas según la línea de tiempo
- **Resaltado de pausa estable**: al pausar, el resaltado se congela en el momento de la pausa y no salta al salir ni al reiniciar; solo la línea activa impulsa la animación, cerca de 0 CPU en reposo
- **Corrección del espaciado de letras en estado compacto**: las letras mantienen una separación fija con los botones de la derecha; si falta espacio, el ancho de la isla se amplía automáticamente (720→800)
- **Estabilidad**: tiempo de espera de AMLL de 5 segundos + degradación elegante; se recalibra la base del reloj de pared cuando el control vuelve a ser visible

## WinIsland 1.1.4 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades
- **Botones de acción rápida**: se añade una fila personalizable de botones rápidos en la parte inferior de la tarjeta expandida de la isla dinámica (bloquear pantalla / silencio / reproducir-pausar / captura de pantalla / mostrar escritorio / administrador de tareas / calculadora / suspensión / volumen±); Ajustes → Acciones rápidas permite marcarlos y ordenarlos con ↑↓, y los cambios surten efecto al instante
- **Avisos de llamadas entrantes**: detecta las ventanas de llamadas de voz y vídeo de WeChat / QQ y muestra un aviso en la esquina superior derecha (distingue «llamada entrante» de «en llamada»); Ajustes → Notificaciones permite activarlo/desactivarlo y personalizar las aplicaciones detectadas; la detección es solo local, no se suben datos
- **Acciones de comando en la isla**: los botones de push de terceros en la isla ahora admiten `action: "command"` para ejecutar una línea de comandos localmente (solo API de loopback local, Token configurable)
- **Temas de las tarjetas de la isla**: los push de terceros pueden incluir `theme: dark / light / auto` y la tarjeta enviada cambia automáticamente entre estilos de cristal claro y oscuro
- **Animaciones más rápidas**: la duración total de los cuatro skins de animación se reduce alrededor de un 20 %, manteniendo la suavidad del resorte de iOS y los 60 fps sin saltos de fotogramas
- **Optimización del uso en segundo plano**: los indicadores de teclado solo se sondean cuando el componente está habilitado, la supervisión de pantalla completa se reduce de frecuencia y las ondas de audio se reducen de frecuencia en reposo
- **Correcciones de estabilidad**: se corrige el aviso falso de «Copiado» al inicio cuando el portapapeles ya contiene contenido (base de inicio); se corrige que los escapes del título .ics del calendario (\, \; \n) se mostraran como barras invertidas
- **Rendimiento y estabilidad**: se reduce el ruido de los registros de las interfaces de Bluetooth / SMTC / clima, con retroceso exponencial para la limitación de velocidad del clima; la compilación pasa y las 104 pruebas unitarias y de integración se superan todas

## WinIsland 1.1.3 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades
- **Cambio entre varios reproductores**: con varios reproductores abiertos a la vez, la tarjeta expandida permite cambiar de un solo clic la fuente de medios controlada actualmente (icono de nota + nombre de la fuente + flecha desplegable); ya no estás limitado a controlar solo el último reproductor iniciado
- **Carátula envolvente**: al hacer clic en la carátula del álbum / la imagen grande de la tarjeta expandida se abre una vista previa a pantalla completa; clic / Esc / clic derecho la cierran con fundido
- **Ajuste fino de la sincronización de letras**: el área de letras de la tarjeta expandida incluye ahora botones +0.5s / -0.5s que recuerdan un desplazamiento por canción para alinear las letras perfectamente con la música
- **Letras de escritorio mejoradas**: la ventana de letras independiente admite ajuste de opacidad (por defecto 0.85) y un interruptor de «Bloqueo» (al bloquearse, el ratón la atraviesa y no se puede arrastrar)
- **Tema dinámico respirante**: con el fondo basado en el color de la carátula activado, el fondo de la tarjeta expandida «respira» lentamente siguiendo el color de la carátula (ciclo de unos 18 segundos), en lugar de un bloque de color plano y estático
- **Respuesta inmediata al clic**: al pulsar el ratón se alterna inmediatamente expandir / contraer, sin esperar a soltar el botón; la respuesta es más ágil
- **Botones de acción en notificaciones**: los banners de notificación admiten botones de acción (el aviso de conexión Bluetooth ahora incluye botones «Desconectar» y «Ajustes»; al hacer clic se ejecuta la acción y se contrae el banner)
- **Devoluciones de llamada de los botones de la isla**: al hacer clic en un botón de acción notify de un push de terceros en la isla, se emite por WebSocket al remitente un evento push_button (con push_id y el texto del botón); el remitente gestiona la devolución de llamada
- **Rendimiento y estabilidad**: el tema dinámico ahora se suscribe a los fotogramas de composición solo cuando es necesario (0 CPU en reposo) y se reutiliza la caché de pinceles degradados para reducir la presión del GC; se corrige el conflicto de exclusión mutua que impedía el inicio cuando convivían instancias de versiones nuevas y antiguas


## WinIsland 1.1.1 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades
- **Avisos de batería**: avisos de batería baja (umbral ajustable); al conectar la alimentación y cargar hasta el umbral configurado (por defecto 100 %), aparece un aviso de «Carga completada»; toda la detección es local y se puede activar o desactivar
- **Avisos de red**: aparece un aviso cuando la red se desconecta / se restablece (detección local del estado de la red, se puede activar o desactivar)
- **Nuevos componentes**: espacio libre del disco (disco del sistema), estado del método de entrada (CH / EN + nombre del método de entrada)
- **Calendario lunar y términos solares**: el componente de fecha puede mostrar además la fecha lunar y los términos solares (activado por defecto, se puede desactivar en los ajustes)
- **Componentes de conmutación rápida**: conmutación con un clic de WiFi / Bluetooth / modo nocturno / silencio (a través de la API local, sin conexión; el estado de Radio se cachea durante 2 segundos para evitar gastos innecesarios)
- **Insignias de fuente de reproducción**: el componente de medios muestra la fuente de reproducción actual (Spotify / Cider / NetEase Cloud Music / QQ Music, etc.), para saber de un vistazo de qué reproductor proviene
- **Mejoras de letras**: interruptor para mostrar / ocultar las traducciones de letras y botón «Copiar línea actual» para copiar la letra actual con un clic
- **Iconos de componentes personalizados**: cada componente puede tener un icono propio (icono MDL2 o Emoji); si no se configura, se usa el glifo predeterminado
- **Corregidas las sacudidas del zoom de letras**: se elimina la vibración de «agrandar y encoger» de las letras causada por el desplazamiento de rebote palabra por palabra del karaoke; el tamaño de fuente / la opacidad de la línea actual en estado expandido pasan a transicionar suavemente en 300 ms, con un desplazamiento más fluido y estable
## WinIsland 1.1.0 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades
- **Subida temporal de volumen / silencio**: cuando cambia el volumen del sistema o se activa / desactiva el silencio, la isla dinámica muestra brevemente un indicador de volumen (duración de visualización ajustable, se puede activar o desactivar en los ajustes)
- **Subida de copia / movimiento de archivos**: al detectar que el Explorador de archivos está copiando / moviendo archivos, la isla dinámica muestra el aviso «Copiando archivos…» (detección local pura por título de ventana, se puede activar o desactivar)
- **Subida de progreso de descarga**: detecta los archivos temporales del navegador en el directorio de descargas (.crdownload / .part / .download, etc.) y muestra «Descargando N archivos» (desactivado por defecto, se puede activar en los ajustes)
- **Cápsula combinada «En uso»**: se puede activar en Ajustes → Componentes (desactivada por defecto); combina «Micrófono / Cámara / En reunión / Grabación de pantalla» en una única cápsula de estado «En uso · …»; puedes marcar qué componentes participan en la combinación, y los elementos combinados ya no se muestran por separado
- **Mejoras de la técnica Pomodoro**: al hacer clic en el componente Pomodoro de la isla dinámica se puede pausar / reanudar el temporizador
- **Subida temporal de capturas / grabación de pantalla**: al hacer una captura o empezar a grabar, la isla dinámica muestra temporalmente el indicador correspondiente (también se activa cuando la isla está oculta)
- **Iluminación palabra por palabra del karaoke con rebote**: cada línea de letras se ilumina suavemente desde la primera letra, con un ligero efecto de rebote, más fluida y natural
- Optimización de la arquitectura de sondeo interna: los eventos temporales de la isla (volumen / copia / descarga / captura, etc.) también se pueden activar en estado oculto

## WinIsland 1.0.9 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades
- **Nuevos componentes**: uso de GPU, micrófono / cámara en uso, cuenta atrás de días festivos, en reunión; el componente de red puede mostrar una minicurva de los últimos 32 segundos
- **Acciones rápidas de doble clic**: Ajustes → General permite configurar la acción del doble clic sobre la isla dinámica como «Reproducir / Pausar», «Abrir ajustes» o «Sin acción»
- **Asistente de silencio en reuniones**: reconoce las ventanas de reuniones (Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet) y activa automáticamente el modo No molestar durante las reuniones (heurística puramente local)
- **Avisos de grabación / captura de pantalla**: aviso de captura con PrintScreen + avisos de detección de software de grabación (OBS / Bandicam / Xbox Game Bar, etc.)
- **Recordatorios de eventos de calendario (.ics)**: analiza archivos iCalendar locales y muestra un banner cuando llega un evento (se puede adelantar N minutos); puramente local
- **Avisos de suscripciones RSS**: sondea RSS 2.0 / Atom y muestra un banner para las nuevas entradas
- **Avisos de correo (POP3)**: solo lee las cabeceras del correo y muestra un banner ante correo nuevo; se recomienda usar un código de autorización
- **Lanzador rápido (estilo Spotlight)**: `Ctrl+Space` para buscar aplicaciones / escribir una URL y abrirla
- **Panel de historial del portapapeles**: `Ctrl+Alt+V` abre una ventana independiente; haz clic para copiar de vuelta al portapapeles
- **Reglas (automatización)**: condiciones (siempre / sin reproducción / reproduciendo / tramo horario / programa multimedia específico) × acciones (ocultar / forzar contracción / forzar visualización)
- **Island API v3**: imágenes (data URI / http), progreso dinámico (from/to/duration avanzan automáticamente), renovación por latido (heartbeat_seconds), actualizaciones parciales PATCH, canal WebSocket (/v3/ws)
- **Apariencia**: 18 ajustes predefinidos de skins de tema, color de fondo personalizado, 4 skins de animación, modo de bajo consumo
- La página de ajustes pasa al estilo de Ajustes del sistema de macOS (navegación izquierda + contenido derecho); todos los cambios surten efecto al instante

- **Corregido el texto negro en modo oscuro**: se unifica la vinculación del color de primer plano en todas las plantillas de controles personalizados de la interfaz de ajustes (botones / casillas / campos de entrada / listas desplegables / elementos desplegables / pestañas / navegación izquierda, etc.) y se añade un escaneo de respaldo en tiempo de ejecución — en modo oscuro ya no aparecen opciones concretas (lista desplegable del idioma de la interfaz, acción de doble clic, etc.) con texto negro ilegible; el modo claro restaura automáticamente el texto oscuro
- **Corregido que la interfaz de ajustes no se abriera**: se eliminan las filas duplicadas de color de primer plano XAML que provocaban fallos de carga de BAML
- **Optimización del rendimiento de las animaciones**: el efecto palabra por palabra del karaoke reutiliza objetos Run (elimina el diseño fotograma a fotograma), con un Storyboard estable a 60 fps y refresco de registros por lotes para animaciones más suaves
- **Eliminado el punto indicador rojo de los componentes** (a petición del usuario)

## WinIsland 1.0.8 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades

**Rediseño integral de la interfaz de ajustes**
- Disposición similar a los Ajustes del sistema de macOS: lista de navegación izquierda + área de contenido derecha, con 13 categorías (General / Apariencia / Componentes / Medios / Visualización de información de medios / Letras / Cider / Island API / Herramientas de eficiencia / Actualizaciones / Acerca de / Notificaciones / Reglas)
- Los colores del texto de los ajustes se adaptan automáticamente en modo oscuro / claro: texto blanco en modo oscuro y negro en modo claro, sin problemas de legibilidad
- El texto de la navegación izquierda pasa a blanco de alto contraste, se elimina el divisor blanco de la derecha y se refuerza la retroalimentación de hover y selección

**Reproducción de medios**
- Nuevo mini reproductor: una ventana flotante independiente que muestra la carátula del álbum / el título de la canción / el artista / la barra de progreso y los controles de reproducción; se puede arrastrar libremente y recuerda su posición, y se muestra / oculta automáticamente con la reproducción (se puede activar en los ajustes)
- Nuevo cambio de dispositivo de salida de audio: Ajustes → Medios permite enumerar y cambiar el dispositivo de reproducción predeterminado del sistema (se recomienda reiniciar el reproductor tras el cambio para que surta efecto)
- Mejora de la capa de fuentes del reproductor: admite enumerar todas las sesiones multimedia SMTC y las sesiones de Cider, y cambiar la fuente de medios

**Letras**
- Nuevas letras bilingües: las líneas de traducción con marcas de tiempo adyacentes se combinan automáticamente (se puede desactivar en los ajustes)

**Apariencia y animaciones**
- Nuevos skins de animación: 4 estilos de animación (resorte de iOS (predeterminado) / resorte suave / rebote elástico / fundido simple), con easing no lineal para expandir / contraer
- Nuevo modo de bajo consumo: reduce la frecuencia de fotogramas del renderizado de ondas y simplifica las animaciones en reposo para ahorrar más energía

**Atajos globales**
- 5 combinaciones de teclas personalizables: mostrar / ocultar, reproducir / pausar, anterior, siguiente, expandir / contraer
- Compatible con Ctrl / Alt / Shift / Win + letras, números, F1–F24, teclas de dirección

**Motor de reglas inteligentes (Ajustes → Reglas)**
- Controla automáticamente la visualización de la isla dinámica según condiciones: siempre activo / cuando no se reproducen medios / cuando se están reproduciendo medios / durante un tramo horario específico / cuando se reproduce un programa multimedia específico
- Acciones: ocultar la isla dinámica / forzar contracción / forzar visualización; prioridad: ocultar > contraer > forzar visualización

**Notificaciones**
- Historial de notificaciones: marcador de punto rojo de no leídas, marcar todas como leídas, eliminar una a una, hacer clic en una entrada para abrir la aplicación de origen, vaciar el historial
- Nuevo plegado de notificaciones: las notificaciones repetidas de la misma fuente y título reutilizan el mismo banner y acumulan la cantidad
- Nueva lista blanca de No molestar: las fuentes de la lista blanca (nombres exe separados por comas) no se ven afectadas por No molestar y siguen mostrando banners con normalidad
- Eliminado el distintivo de punto rojo de no leídas de la isla dinámica

**Herramientas de eficiencia**
- Al copiar texto se muestra el aviso «Copiado»
- Reconoce automáticamente los códigos de verificación por SMS y los resalta
- La copia de textos grandes muestra una animación de progreso (avanza según la longitud estimada; el resultado se muestra al terminar)

**Componentes**
- Nuevo componente de cuenta atrás de días festivos: incluye una tabla de días festivos 2026–2027 (Año Nuevo / Fiesta de la Primavera / Qingming / Día del Trabajo / Festival del Barco Dragón / Festival del Medio Otoño / Fiesta Nacional), que muestra «XX en N días» o «Hoy XX»; se puede activar o desactivar en los ajustes del componente

**Island API v2**
- Nuevos campos: subtitle (subtítulo), type (info / success / warning / error), priority (high / normal / low), accent (color de acento personalizado), click (devolución de llamada al hacer clic en toda la tarjeta)
- Cola de push: varios push se ordenan por prioridad de mayor a menor, primero en entrar primero en salir; volver a enviar con el mismo id conserva la posición original en la cola y el tiempo de caducidad
- La respuesta POST incluye ahora un campo position
- Nuevos scripts de ejemplo listos para ejecutar en docs/sdk-examples/ (push.bat / pull.bat / push.ps1 / push.py / pull.py)

**Correcciones**
- Corregido que la ventana de ajustes no se abriera / no se ejecutara con doble clic (se añade protección de valores nulos e inicialización explícita para la navegación y el cambio de pestañas)
- Se añaden 69 pruebas automatizadas (Island API, plegado de notificaciones y lista blanca, motor de reglas, reconocimiento de códigos de verificación, análisis de letras LRC), todas superadas

### Recursos
- Versiones portables de Windows x64 / arm64 (archivo único autocontenido, se ejecuta sin instalación)
- Instalador universal de Windows (Inno Setup, compatible con x64 y ARM64, instala automáticamente según la arquitectura)

> La versión portable es un archivo exe independiente; ya no se proporcionan paquetes ZIP.
## WinIsland 1.0.7 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades
- «Ondas de sonido» mejorado para **seguir el ritmo de la música**: captura en tiempo real el audio real que reproduce el sistema mediante lazo de retorno WASAPI: las ondas suben con ritmos fuertes y bajan en silencio, en lugar de una barra de volumen fija
- **Renderizado continuo a 60 fps**: suavizado exponencial con ataque de 25 ms / liberación de 140 ms para unas ondas fluidas y naturales, sin rigidez ni tirones
- Nuevos ajustes (Ajustes → Apariencia → Ondas de sonido): interruptor de seguimiento del ritmo musical, sensibilidad 0.2–3.0, altura de las ondas 0.4–1.6; los cambios surten efecto al instante
- Cuando no hay dispositivo de audio / el servicio de audio es anómalo, degrada automáticamente a simulación de ritmo y reintenta la captura en tiempo real cada 8 segundos, sin congelarse ni acumular hilos

## WinIsland 1.0.6 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades
- 6 nuevos componentes de la isla dinámica: volumen, indicadores de teclado (CapsLock), portapapeles, tareas pendientes, Pomodoro y agenda; todos admiten casillas de doble columna «sin canción / con canción» y ordenación por arrastre
- Nueva página de ajustes «Herramientas de eficiencia»: historial del portapapeles, temporizador Pomodoro, lista de tareas, recordatorios de la agenda
- Nueva «Ondas de sonido»: al reproducir medios, la zona izquierda de los botones de control vibra en tiempo real con el volumen del sistema (se puede activar o desactivar en Ajustes → Apariencia)
- 7 nuevos ajustes predefinidos de tema: Predeterminado / Océano / Bosque / Atardecer / Neón / Monocromo / Uva morada
- Personalización de la apariencia: fuente personalizada, escala del tamaño de fuente (0.8–1.4), radio de las esquinas de la cápsula (16–40), fondo expandido teñido con el color de la carátula del álbum, distintivo de notificaciones no leídas
- Añadidos al menú de la bandeja: modo No molestar (manual / silenciar notificaciones automáticamente por tramo horario), comprobar actualizaciones, ver registros
- Páginas de ajustes modernizadas (esquinas redondeadas + cristal líquido); los cambios de opciones surten efecto al instante, sin guardado manual
- Nueva comprobación de actualizaciones (comprobación manual desde la bandeja / los ajustes, comprobación automática opcional, desactivada por defecto)


### Recursos
- Versiones portables de Windows x64 / arm64 (archivo único autocontenido, se ejecuta sin instalación)
- Instalador universal de Windows (Inno Setup, compatible con x64 y ARM64, instala automáticamente según la arquitectura)

> La versión portable es un archivo exe independiente; ya no se proporcionan paquetes ZIP.

## WinIsland 1.0.5 (Versión estable)

Un widget moderno y multifuncional de Dynamic Island para Windows.

### Novedades
- Nueva «Island API»: otros programas pueden enviar información a la isla dinámica mediante una interfaz HTTP local (similar a la integración de apps de terceros con la isla dinámica de iOS); **documentación de desarrollo en docs/IslandAPI.md**
  - `POST /v1/island/push` enviar/actualizar · `DELETE /v1/island/push/{id}` eliminar · `GET /v1/island/active` consultar · `GET /v1/health`
  - Admite iconos, título, cuerpo, progreso, botones (abrir enlace / iniciar programa) y duración de visualización personalizada por elemento
  - La página de ajustes ofrece activar / puerto / Token opcional / duración global predeterminada
- Las tarjetas de la isla se muestran en una sola línea en estado compacto, no tapan otros componentes y **no afectan al largo y ancho de la isla dinámica** (tamaño constante en modo automático / manual)
- Tamaño de «ajuste automático»: se adapta al contenido; arrastrar manualmente el control deslizante desactiva automáticamente la opción automática correspondiente
- El contenido expandido admite desplazamiento con la rueda (barra de desplazamiento oculta)
- Alineación vertical uniforme de los componentes; correcciones de diseño / fuente al inicio (PerMonitorV2 forzado, tamaño correcto desde el arranque)
- Reproducir medios ya no muestra la notificación «En reproducción»

- Corrección: el defecto de que la tarjeta volvía al tamaño compacto unos 1~2 segundos después de expandir la isla dinámica y provocaba una pantalla negra total
  - El contenido expandido ahora se funde de forma cruzada superponiéndose a la fila compacta; la animación no deja ver el fondo en ningún momento
  - Tras completarse la animación de expandir/contraer, el tamaño final de la tarjeta se escribe explícitamente; el estado expandido se mantiene estable sin encogerse
  - También se corrige la pantalla negra al hacer clic en los botones de terceros de la isla en estado expandido


### Recursos
- Versiones portables de Windows x64 / arm64 (archivo único autocontenido, se ejecuta sin instalación)
- Instalador universal de Windows (Inno Setup, compatible con x64 y ARM64, instala automáticamente según la arquitectura)

> La versión portable es un archivo exe independiente; ya no se proporcionan paquetes ZIP.

---

## Français

## WinIsland 1.1.5 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés
- **Paroles mot à mot AMLL (véritable karaoké)** : connexion à la bibliothèque TTML d'amll.dev au style Apple Music ; Réglages → Paroles, interrupteur « Paroles mot à mot AMLL » (activé par défaut) ; priorité des sources : LRC local → AMLL TTML → Cider → paroles en ligne
- **Moteur de surbrillance mot à mot réécrit** : progression continue horloge murale 60 fps + easing non linéaire ; repli par répartition égale sur la ligne ; paroles bilingues appariées selon la chronologie
- **Surbrillance de pause stable** : après une pause, la surbrillance se fige au moment de la pause et ne saute pas à la sortie ni au redémarrage ; seule la ligne active pilote l'animation, ~0 CPU au repos
- **Correction de l'espacement des paroles en état compact** : les paroles gardent un espacement fixe avec les boutons de droite ; si l'espace manque, la largeur de l'île s'élargit automatiquement (720→800)
- **Stabilité** : délai d'expiration AMLL de 5 secondes + dégradation élégante ; recalibrage de la base d'horloge murale lorsque le contrôle redevient visible

## WinIsland 1.1.4 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés
- **Boutons d'action rapide** : une rangée personnalisable de boutons rapides a été ajoutée au bas de la carte dépliée de l'île dynamique (verrouiller l'écran / muet / lecture-pause / capture d'écran / afficher le bureau / gestionnaire des tâches / calculatrice / veille / volume±) ; Réglages → Actions rapides permet de les cocher et de les réordonner avec ↑↓, les modifications s'appliquent instantanément
- **Alertes d'appel entrant** : détecte les fenêtres d'appels vocaux et vidéo de WeChat / QQ et affiche une alerte dans le coin supérieur droit (en distinguant « appel entrant » de « en appel ») ; Réglages → Notifications permet d'activer/désactiver et de personnaliser les applications détectées ; la détection est locale uniquement, aucune donnée n'est téléversée
- **Actions de commande de l'île** : les boutons de push tiers de l'île prennent désormais en charge `action: "command"` pour exécuter une ligne de commande localement (API de bouclage local uniquement, Token configurable)
- **Thèmes des cartes de l'île** : les push tiers peuvent transporter `theme: dark / light / auto` ; la carte envoyée bascule automatiquement entre les styles de verre clair et sombre
- **Animations plus rapides** : la durée totale des quatre skins d'animation est réduite d'environ 20 %, en conservant l'easing ressort d'iOS et le rendu fluide à 60 fps sans image sautée
- **Optimisation de l'utilisation en arrière-plan** : les indicateurs de clavier ne sont interrogés que lorsque le composant est activé, la surveillance plein écran est réduite en fréquence et les ondulations audio sont réduites en fréquence au repos
- **Correctifs de stabilité** : correction de la notification « Copié » affichée à tort au démarrage lorsque le presse-papiers contenait déjà du texte (référence de démarrage) ; correction de l'affichage des échappements du titre .ics du calendrier (\, \; \n) comme barres obliques inverses
- **Performances et stabilité** : réduction du bruit des journaux des interfaces Bluetooth / SMTC / météo, avec repli exponentiel pour la limitation de débit de la météo ; la compilation passe et les 104 tests unitaires et d'intégration sont tous réussis

## WinIsland 1.1.3 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés
- **Commutation multi-lecteur** : lorsque plusieurs lecteurs sont ouverts, la carte dépliée permet de changer d'un clic la source multimédia actuellement contrôlée (icône de note + nom de la source + flèche déroulante) ; vous n'êtes plus limité au dernier lecteur lancé
- **Pochette immersive** : cliquer sur la pochette d'album / la grande image de la carte dépliée ouvre un aperçu plein écran ; un clic / Échap / clic droit ferment en fondu
- **Réglage fin de la synchronisation des paroles** : la zone de paroles de la carte dépliée dispose désormais de boutons +0,5 s / -0,5 s qui mémorisent un décalage par chanson pour aligner parfaitement les paroles sur la musique
- **Paroles de bureau améliorées** : la fenêtre de paroles autonome prend en charge le réglage de l'opacité (par défaut 0,85) et un interrupteur « Verrouiller » (une fois verrouillée, la souris la traverse et la fenêtre ne peut plus être déplacée)
- **Thème dynamique respirant** : lorsque le fond à dominante de couleur de pochette est activé, l'arrière-plan de la carte dépliée « respire » lentement en suivant la couleur de la pochette (cycle d'environ 18 secondes) au lieu d'un bloc de couleur plat et statique
- **Réponse instantanée** : appuyer sur la souris bascule immédiatement l'extension / la réduction, sans attendre le relâchement, pour une sensation plus réactive
- **Boutons d'action des notifications** : les bandeaux de notification prennent en charge des boutons d'action (l'alerte de connexion Bluetooth comporte désormais des boutons « Déconnecter » et « Réglages » qui exécutent l'action immédiatement et replient le bandeau)
- **Rappels des boutons de l'île** : lorsqu'un bouton d'action notify d'un push tiers de l'île est cliqué, un événement push_button (avec push_id et le texte du bouton) est diffusé à l'expéditeur via WebSocket ; celui-ci gère lui-même le rappel
- **Performances et stabilité** : le thème dynamique s'abonne désormais aux images de composition à la demande (0 CPU au repos) et le cache des pinceaux en dégradé est réutilisé pour réduire la pression du GC ; correction du conflit de mutex qui empêchait le démarrage lorsque des instances de versions récente et ancienne tournaient simultanément


## WinIsland 1.1.1 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés
- **Alertes de batterie** : alertes de batterie faible (seuil réglable) ; lorsqu'il est branché et chargé jusqu'au seuil défini (par défaut 100 %), une alerte « Charge terminée » s'affiche ; toute la détection est locale et peut être activée/désactivée
- **Alertes réseau** : une notification s'affiche lorsque le réseau se déconnecte / se rétablit (détection locale de l'état du réseau, activable/désactivable)
- **Nouveaux composants** : espace libre du disque (disque système), état de la méthode de saisie (CH / EN + nom de la méthode de saisie)
- **Calendrier lunaire et termes solaires** : le composant date peut en plus afficher la date lunaire et les termes solaires (activé par défaut, désactivable dans les réglages)
- **Composants de bascule rapide** : bascule en un clic du WiFi / Bluetooth / mode nuit / muet (via l'API locale, sans réseau ; l'état Radio est mis en cache pendant 2 secondes pour éviter les frais)
- **Badges de source de lecture** : le composant média affiche la source de lecture actuelle (Spotify / Cider / NetEase Cloud Music / QQ Music, etc.) pour savoir d'un coup d'œil de quel lecteur elle provient
- **Améliorations des paroles** : interrupteur d'affichage / masquage des traductions de paroles et bouton « Copier la ligne actuelle » pour copier la parole en cours en un clic
- **Icônes de composant personnalisées** : chaque composant peut avoir sa propre icône (icône MDL2 ou Emoji) ; le glyphe par défaut est utilisé si aucune n'est définie
- **Correction des sauts de zoom des paroles** : suppression de la vibration « agrandir puis réduire » des paroles due au décalage de rebond mot à mot du karaoké ; la taille de police / l'opacité de la ligne de paroles active en état déplié passent désormais en transition douce sur 300 ms, pour un défilement plus fluide et stable
## WinIsland 1.1.0 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés
- **Île temporaire volume / muet** : lorsque le volume système change ou que le mode muet est activé / désactivé, l'île dynamique affiche brièvement un indicateur de volume (durée d'affichage réglable, activable/désactivable dans les réglages)
- **Île copie / déplacement de fichiers** : lorsqu'il détecte que l'Explorateur de fichiers copie / déplace des fichiers, l'île dynamique affiche l'indication « Copie des fichiers… » (détection locale uniquement par titre de fenêtre, activable/désactivable)
- **Île progression de téléchargement** : détecte les fichiers temporaires du navigateur dans le dossier de téléchargements (.crdownload / .part / .download, etc.) et affiche « Téléchargement de N fichiers » (désactivé par défaut, activable dans les réglages)
- **Capsule combinée « En cours d'utilisation »** : activable dans Réglages → Composants (désactivée par défaut) ; elle combine « Microphone / Caméra / En réunion / Enregistrement d'écran » en une seule capsule d'état « En cours d'utilisation · … » ; vous pouvez cocher les composants concernés, et les éléments combinés ne s'affichent plus séparément
- **Améliorations du Pomodoro** : cliquer sur le composant Pomodoro de l'île dynamique permet de mettre en pause / reprendre le minuteur
- **Île temporaire capture / enregistrement d'écran** : lors d'une capture d'écran ou du début d'un enregistrement, l'île dynamique affiche temporairement l'indication correspondante (elle se déclenche même lorsque l'île est masquée)
- **Allumage mot à mot du karaoké avec rebond** : chaque ligne de paroles s'allume en douceur depuis le premier caractère, avec un léger effet de rebond, plus fluide et naturel
- Optimisation de l'architecture de sondage interne : les événements temporaires de l'île (volume / copie / téléchargement / capture, etc.) peuvent aussi se déclencher à l'état masqué

## WinIsland 1.0.9 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés
- **Nouveaux composants** : utilisation du GPU, micro / caméra en cours d'utilisation, compte à rebours des jours fériés, en réunion ; le composant réseau peut afficher une mini-courbe des 32 dernières secondes
- **Actions rapides au double-clic** : Réglages → Général permet de définir l'action du double-clic sur l'île dynamique comme « Lecture / Pause », « Ouvrir les réglages » ou « Aucune action »
- **Assistant muet en réunion** : détecte les fenêtres de réunion (Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet) et active automatiquement Ne pas déranger pendant les réunions (heuristique purement locale)
- **Alertes d'enregistrement / capture d'écran** : alerte de capture avec Impr. écran + alertes de détection des logiciels d'enregistrement (OBS / Bandicam / Xbox Game Bar, etc.)
- **Rappels d'événements du calendrier (.ics)** : analyse les fichiers iCalendar locaux et affiche un bandeau au début d'un événement (jusqu'à N minutes à l'avance) ; purement local
- **Alertes d'abonnements RSS** : interroge RSS 2.0 / Atom et affiche un bandeau pour les nouveaux articles
- **Alertes e-mail (POP3)** : ne lit que les en-têtes des e-mails et affiche un bandeau pour les nouveaux messages ; un code d'autorisation est recommandé
- **Lanceur rapide (style Spotlight)** : `Ctrl+Space` pour rechercher des applications / saisir une URL et l'ouvrir
- **Panneau d'historique du presse-papiers** : `Ctrl+Alt+V` ouvre une fenêtre indépendante ; un clic recopie dans le presse-papiers
- **Règles (automatisation)** : conditions (toujours / pas de lecture / en lecture / période de temps / application multimédia spécifique) × actions (masquer / replier de force / afficher de force)
- **Island API v3** : images (data URI / http), progression dynamique (from/to/duration avance automatiquement), renouvellement par battement de cœur (heartbeat_seconds), mises à jour partielles PATCH, canal WebSocket (/v3/ws)
- **Apparence** : 18 préréglages de skins de thème, couleur d'arrière-plan personnalisée, 4 skins d'animation, mode basse consommation
- La page de réglages adopte le style Réglages Système de macOS (navigation à gauche + contenu à droite) ; tous les changements s'appliquent instantanément

- **Correction du texte noir en mode sombre** : liaison unifiée de la couleur de premier plan sur tous les modèles de contrôles personnalisés de l'interface de réglages (boutons / cases à cocher / champs de saisie / listes déroulantes / éléments déroulants / onglets / navigation à gauche, etc.) et ajout d'un balayage de secours à l'exécution — en mode sombre, certaines options (liste déroulante de la langue de l'interface, action de double-clic, etc.) n'affichent plus de texte noir illisible ; le mode clair restaure automatiquement le texte sombre
- **Correction de l'impossibilité d'ouvrir l'interface de réglages** : suppression des lignes XAML de couleur de premier plan en double qui provoquaient des échecs de chargement BAML
- **Optimisation des performances des animations** : l'effet mot à mot du karaoké réutilise les objets Run (suppression de la mise en page image par image), avec un Storyboard stable à 60 fps et une actualisation par lots des journaux pour des animations plus fluides
- **Suppression du point de badge rouge sur les composants** (à la demande des utilisateurs)

## WinIsland 1.0.8 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés

**Refonte complète de l'interface des réglages**
- Disposition de type Réglages Système de macOS : liste de navigation à gauche + zone de contenu à droite, avec 13 catégories (Général / Apparence / Composants / Média / Affichage des informations média / Paroles / Cider / Island API / Outils de productivité / Mises à jour / À propos / Notifications / Règles)
- Les couleurs du texte des réglages s'adaptent automatiquement en mode sombre / clair : texte blanc en mode sombre, texte noir en mode clair — plus de problèmes de lisibilité
- Le texte de la navigation de gauche passe en blanc à contraste élevé, le séparateur blanc à droite est supprimé et le retour visuel au survol / à la sélection est renforcé

**Lecture multimédia**
- Nouveau mini-lecteur : une fenêtre flottante indépendante affichant la pochette d'album / le titre / l'artiste / la barre de progression et les contrôles de lecture ; déplaçable librement avec mémorisation de la position, affichage / masquage automatique selon la lecture (activable dans les réglages)
- Nouvelle commutation de périphérique de sortie audio : Réglages → Média permet d'énumérer et de changer le périphérique de lecture par défaut du système (un redémarrage du lecteur est recommandé après le changement)
- Amélioration de la couche de sources du lecteur : énumération de toutes les sessions média SMTC et des sessions Cider, avec changement de source média

**Paroles**
- Nouvelles paroles bilingues : les lignes de traduction aux horodatages adjacents sont fusionnées automatiquement (désactivable dans les réglages)

**Apparence et animations**
- Nouveaux skins d'animation : 4 styles d'animation (ressort iOS (par défaut) / ressort doux / rebond élastique / fondu simple), avec easing non linéaire pour l'extension / la réduction
- Nouveau mode basse consommation : réduction du taux de trames du rendu des ondulations et simplification des animations au repos pour économiser davantage d'énergie

**Raccourcis globaux**
- 5 combinaisons de touches personnalisables : afficher / masquer, lecture / pause, précédent, suivant, étendre / replier
- Prend en charge Ctrl / Alt / Shift / Win + lettres, chiffres, F1–F24, touches fléchées

**Moteur de règles intelligentes (Réglages → Règles)**
- Contrôle automatiquement l'affichage de l'île dynamique selon des conditions : toujours actif / quand aucun média ne joue / quand un média joue / pendant une période donnée / quand un programme multimédia précis joue
- Actions : masquer l'île dynamique / replier de force / afficher de force ; priorité : masquer > replier > afficher de force

**Notifications**
- Historique des notifications : marqueur de point rouge non lu, tout marquer comme lu, suppression individuelle, clic sur un élément pour ouvrir l'application source, effacer l'historique
- Nouveau repli des notifications : les notifications en double de la même source et du même titre réutilisent le même bandeau et cumulent un compteur
- Nouvelle liste blanche Ne pas déranger : les sources de la liste blanche (noms exe séparés par des virgules) ne sont pas affectées par Ne pas déranger et affichent toujours leurs bandeaux normalement
- Suppression du badge à point rouge non lu sur l'île dynamique

**Outils de productivité**
- Affiche une notification « Copié » lors de la copie de texte
- Reconnaît automatiquement les codes de vérification par SMS et les met en surbrillance
- La copie de grands textes affiche une animation de progression (estimée selon la longueur ; le résultat s'affiche à la fin)

**Composants**
- Nouveau composant de compte à rebours des jours fériés : table des jours fériés 2026–2027 intégrée (Nouvel An / Fête du Printemps / Qingming / Fête du Travail / Fête des bateaux-dragons / Fête de la mi-automne / Fête nationale), affichant « XX dans N jours » ou « Aujourd'hui XX » ; activable/désactivable dans les réglages du composant

**Island API v2**
- Nouveaux champs : subtitle (sous-titre), type (info / success / warning / error), priority (high / normal / low), accent (couleur d'accent personnalisée), click (rappel au clic sur toute la carte)
- File d'attente de push : plusieurs push sont ordonnés par priorité décroissante, premier entré premier sorti ; renvoyer un push avec le même id conserve la position d'origine dans la file et la date d'expiration
- La réponse POST inclut désormais un champ position
- Nouveaux scripts d'exemple directement exécutables dans docs/sdk-examples/ (push.bat / pull.bat / push.ps1 / push.py / pull.py)

**Correctifs**
- Correction de l'impossibilité d'ouvrir la fenêtre des réglages / de l'échec du lancement au double-clic (protection contre les valeurs nulles et initialisation explicite ajoutées pour la navigation et le changement d'onglets)
- Ajout de 69 tests automatisés (Island API, repli des notifications et liste blanche, moteur de règles, reconnaissance des codes de vérification, analyse des paroles LRC), tous réussis

### Ressources
- Versions portables Windows x64 / arm64 (fichier unique autonome, s'exécute sans installation)
- Installateur universel Windows (Inno Setup, prend en charge x64 et ARM64, installation automatique selon l'architecture)

> La version portable est un fichier exe autonome ; les archives ZIP ne sont plus fournies.
## WinIsland 1.0.7 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés
- « Ondulation sonore » améliorée pour **suivre le rythme de la musique** : capture en temps réel de l'audio réellement diffusé par le système via la boucle de retour WASAPI — les vagues montent sur les temps forts et descendent au calme, au lieu d'une barre de volume fixe
- **Rendu continu à 60 fps** : lissage exponentiel avec attaque 25 ms / relâchement 140 ms pour des ondulations fluides et naturelles, sans à-coups
- Nouveaux réglages (Réglages → Apparence → Ondulation sonore) : interrupteur de suivi du rythme musical, sensibilité 0.2–3.0, hauteur d'ondulation 0.4–1.6 ; les changements s'appliquent instantanément
- En l'absence de périphérique audio / en cas d'anomalie du service audio, repli automatique sur une simulation de rythme, avec nouvelle tentative de capture en temps réel toutes les 8 secondes — sans gel ni accumulation de threads

## WinIsland 1.0.6 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés
- 6 nouveaux composants de l'île dynamique : volume, indicateurs de clavier (CapsLock), presse-papiers, tâches à faire, Pomodoro et agenda ; tous prennent en charge les cases à cocher à double colonne « sans chanson / avec chanson » et le tri par glisser-déposer
- Nouvelle page de réglages « Outils de productivité » : historique du presse-papiers, minuteur Pomodoro, liste de tâches, rappels d'agenda
- Nouvelle « Ondulation sonore » : pendant la lecture de médias, la zone à gauche des boutons de commande vibre en temps réel avec le volume du système (activable/désactivable dans Réglages → Apparence)
- 7 nouveaux préréglages de thème : Par défaut / Océan / Forêt / Coucher de soleil / Néon / Monochrome / Violet raisin
- Personnalisation de l'apparence : police personnalisée, échelle de taille de police (0.8–1.4), rayon des coins de la capsule (16–40), fond déplié teinté depuis la couleur de la pochette, badge de notifications non lues
- Ajouts au menu de la zone de notification : mode Ne pas déranger (manuel / silencer les notifications automatiquement par période), rechercher des mises à jour, voir les journaux
- Pages de réglages modernisées (coins arrondis + verre liquide) ; les modifications des options s'appliquent instantanément, sans enregistrement manuel
- Nouvelle vérification des mises à jour (vérification manuelle depuis la zone de notification / les réglages, vérification automatique facultative, désactivée par défaut)


### Ressources
- Versions portables Windows x64 / arm64 (fichier unique autonome, s'exécute sans installation)
- Installateur universel Windows (Inno Setup, prend en charge x64 et ARM64, installation automatique selon l'architecture)

> La version portable est un fichier exe autonome ; les archives ZIP ne sont plus fournies.

## WinIsland 1.0.5 (Version stable)

Un widget Dynamic Island moderne et multifonctionnel pour Windows.

### Nouveautés
- Nouvelle « Island API » : d'autres logiciels peuvent pousser des informations vers l'île dynamique via une interface HTTP locale (comparable à l'intégration d'apps tierces avec l'île dynamique d'iOS) ; **documentation développeur dans docs/IslandAPI.md**
  - `POST /v1/island/push` envoyer/mettre à jour · `DELETE /v1/island/push/{id}` supprimer · `GET /v1/island/active` interroger · `GET /v1/health`
  - Prend en charge les icônes, le titre, le corps, la progression, les boutons (ouvrir un lien / lancer un programme) et une durée d'affichage personnalisée par élément
  - La page de réglages fournit l'activation / le port / un Token facultatif / la durée par défaut globale
- Les cartes de l'île s'affichent sur une seule ligne en état compact, ne masquent pas les autres composants et **n'affectent pas les dimensions de l'île dynamique** (taille constante en modes automatique / manuel)
- Taille « ajustement automatique » : s'adapte au contenu ; faire glisser manuellement le curseur désactive automatiquement l'option automatique correspondante
- Le contenu déplié prend en charge le défilement à la molette (barre de défilement masquée)
- Alignement vertical uniforme des composants ; corrections de disposition / police au démarrage (PerMonitorV2 forcé, taille correcte dès le lancement)
- Lire un média n'affiche plus la notification « En cours de lecture »

- Correction : le défaut où la carte revenait à la taille compacte environ 1~2 secondes après l'extension de l'île dynamique, provoquant un écran entièrement noir
  - Le contenu déplié se fond désormais en croisé avec la rangée compacte qui se superpose ; aucun fond ne transparaît pendant l'animation
  - Après la fin de l'animation d'extension/réduction, la taille finale de la carte est explicitement réécrite ; l'état déplié reste stable sans se réduire
  - Correction également du problème d'écran noir en cliquant sur les boutons tiers de l'île à l'état déplié


### Ressources
- Versions portables Windows x64 / arm64 (fichier unique autonome, s'exécute sans installation)
- Installateur universel Windows (Inno Setup, prend en charge x64 et ARM64, installation automatique selon l'architecture)

> La version portable est un fichier exe autonome ; les archives ZIP ne sont plus fournies.

---

## العربية

## WinIsland 1.1.5 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد
- **كلمات AMLL حرفًا بحرف (كاريوكي حقيقي)**: الاتصال بمكتبة TTML بأسلوب Apple Music من amll.dev؛ الإعدادات ← الكلمات، مفتاح «كلمات AMLL حرفًا بحرف» (مفعّل افتراضيًا)؛ أولوية المصادر: LRC محلي ← TTML من AMLL ← Cider ← كلمات عبر الإنترنت
- **إعادة كتابة محرك الإضاءة حرفًا بحرف**: تقدم مستمر بساعة حائط بمعدل 60 إطارًا في الثانية + تخفيف غير خطي؛ تقسيم السطر كاملًا كحل احتياطي؛ الكلمات ثنائية اللغة مطابقة عبر الخط الزمني
- **ثبات إضاءة الإيقاف المؤقت**: بعد الإيقاف المؤقت تُجمد الإضاءة عند لحظة الإيقاف ولا تقفز عند الخروج أو إعادة التشغيل؛ السطر النشط فقط يحرك الرسم المتحرك، والمعالج شبه صفر عند الخمول
- **إصلاح تباعد الكلمات في الحالة المضغوطة**: تحافظ الكلمات على مسافة ثابتة من الأزرار على اليمين؛ وعند ضيق المساحة يتسع عرض الجزيرة تلقائيًا (720←800)
- **الاستقرار**: مهلة AMLL 5 ثوانٍ + تدهور أنيق؛ وتُعاد معايرة أساس ساعة الحائط عند عودة العنصر للظهور

## WinIsland 1.1.4 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد
- **أزرار الإجراءات السريعة**: صف جديد قابل للتخصيص من الأزرار السريعة في أسفل بطاقة الجزيرة الموسعة (قفل الشاشة / كتم الصوت / تشغيل-إيقاف / لقطة شاشة / إظهار سطح المكتب / إدارة المهام / الآلة الحاسبة / السكون / رفع وخفض الصوت)؛ الإعدادات ← الإجراءات السريعة للاختيار والترتيب بـ ↑↓، وتُطبق التغييرات فورًا
- **تنبيهات المكالمات الواردة**: كشف نوافذ المكالمات الصوتية والمرئية في WeChat / QQ وعرض تنبيه في الزاوية العلوية اليمنى (يميز بين «مكالمة واردة» و«في مكالمة»)؛ الإعدادات ← الإشعارات للتحكم وتخصيص التطبيقات المرصودة؛ الكشف محلي فقط، دون رفع أي بيانات
- **إجراء الأوامر للجزيرة**: أزرار الدفع من طرف ثالث تدعم الآن `action: "command"` لتنفيذ سطر أوامر محليًا (واجهة محلية فقط، مع إمكانية ضبط Token)
- **ثيمات بطاقات الجزيرة**: يمكن أن يحمل الدفع من طرف ثالث `theme: dark / light / auto`، وتتحول البطاقة تلقائيًا بين النمطين الزجاجيين الداكن والفاتح
- **تسريع الرسوم المتحركة**: تقصير المدة الإجمالية للجلود الأربعة بنحو 20% مع الحفاظ على تخفيف الزنبرك بأسلوب iOS وسلاسة 60 إطارًا في الثانية دون قفزات
- **تحسين استهلاك الخلفية**: مؤشر لوحة المفاتيح يُستقصى فقط عند تفعيل المكوّن، ومراقبة ملء الشاشة بتردد أقل، وموجات الصوت تُخفف عند الخمول
- **إصلاحات الاستقرار**: إصلاح ظهور إشعار «تم النسخ» الخاطئ للمحتوى الموجود في الحافظة عند بدء التشغيل (خط الأساس عند البدء)؛ وإصلاح مشكلة عرض ترميز عناوين ملفات التقويم .ics (\, \; \n) كشرطات مائلة عكسية
- **الأداء والاستقرار**: تقليل ضوضاء سجلات واجهات Bluetooth / SMTC / الطقس، مع تراجع أسي للحد من معدل الطقس؛ البناء ناجح وكل اختبارات الوحدات والتكامل البالغ عددها 104 تمر بنجاح

## WinIsland 1.1.3 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد
- **التبديل بين عدة مشغلات**: عند فتح عدة مشغلات معًا، يمكن للبطاقة الموسعة تبديل مصدر الوسائط المتحكم به حاليًا بنقرة واحدة (أيقونة نغمة + اسم المصدر + سهم قائمة منسدلة)، ولم يعد مقتصرًا على التحكم في آخر مشغل شُغّل
- **غلاف الألبوم بملء الشاشة**: انقر على غلاف الألبوم / الصورة الكبيرة في البطاقة الموسعة لفتح معاينة بملء الشاشة؛ وأغلقها بالنقر / Esc / الزر الأيمن مع تلاشي
- **ضبط دقيق لتوقيت الكلمات**: منطقة الكلمات في البطاقة الموسعة تكتسب زري +0.5 ثانية / -0.5 ثانية يحفظان إزاحة زمنية لكل أغنية لمحاذاة الكلمات مع الموسيقى تمامًا
- **تحسين كلمات سطح المكتب**: نافذة الكلمات المستقلة تدعم ضبط الشفافية (افتراضي 0.85) ومفتاح «قفل» (عند القفل يخترق الماوس ولا يمكن سحب النافذة)
- **الثيم الديناميكي المتنفس**: عند تفعيل خلفية ألوان الغلاف، تتنفس خلفية البطاقة الموسعة ببطء مع لون الغلاف (دورة نحو 18 ثانية) بدلًا من كتلة لون ثابتة
- **استجابة فورية للنقر**: الضغط على الماوس يبدّل التوسيع / الطي فورًا دون انتظار الرفع، لإحساس أكثر استجابة
- **أزرار إجراءات الإشعارات**: لافتات الإشعارات تدعم أزرار إجراءات (تنبيه اتصال Bluetooth يحمل الآن زري «قطع الاتصال» و«الإعدادات»، يُنفَّذ فورًا ويُطوى عند النقر)
- **استدعاء أزرار الجزيرة**: عند النقر على زر إجراء notify في دفعات الجزيرة من طرف ثالث، يُبث حدث push_button عبر WebSocket إلى جهة الدفع (يتضمن push_id ونص الزر)، وتعالج جهة الدفع الاستدعاء بنفسها
- **الأداء والاستقرار**: الثيم الديناميكي صار يشترك في إطارات التركيب عند الحاجة فقط (0 CPU عند الخمول)، مع إعادة استخدام مخبأ الفرش المتدرجة لتقليل ضغط GC؛ وإصلاح تعارض الكائن المتبادل بين الإصدار الجديد والقديم عند التشغيل المتزامن الذي منع بدء التشغيل

## WinIsland 1.1.1 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد
- **تنبيه البطارية**: تنبيه انخفاض البطارية (عتبة قابلة للضبط)، وعند توصيل الطاقة والوصول إلى العتبة المحددة (افتراضي 100%) يظهر تنبيه «اكتمل الشحن»، وكلاهما كشف محلي وقابل للتعطيل
- **تنبيه الشبكة**: إشعار عند انقطاع الشبكة / استعادتها (كشف حالة الشبكة محليًا، قابل للتعطيل)
- **مكونات جديدة**: المساحة المتبقية من القرص (قرص النظام)، وحالة طريقة الإدخال (صينية / إنجليزية + اسم طريقة الإدخال)
- **التقويم القمري والمصطلحات الشمسية**: مكون التاريخ يمكن أن يعرض التاريخ القمري والمصطلحات الشمسية إضافيًا (مفعّل افتراضيًا، يمكن إيقافه في الإعدادات)
- **مكون مفاتيح سريعة**: WiFi / Bluetooth / الوضع الليلي / كتم الصوت بتبديل بنقرة واحدة (عبر واجهة محلية دون اتصال؛ حالة Radio مخزنة مؤقتًا ثانيتين لتجنب التكلفة)
- **شارة مصدر التشغيل**: يعرض مكون الوسائط مصدر التشغيل الحالي (Spotify / Cider / NetEase / QQ Music وغيرها) لتعرف من أي مشغل يأتي
- **تحسين الكلمات**: مفتاح إظهار / إخفاء ترجمة الكلمات، وزر «نسخ السطر الحالي» لنسخ الكلمات الحالية بنقرة واحدة
- **تخصيص أيقونات المكونات**: يمكن تخصيص أيقونة كل مكون على حدة (أيقونة MDL2 أو Emoji)، وعند عدم التعيين تُستخدم الأشكال الافتراضية
- **إصلاح قفز تكبير الكلمات**: إزالة الاهتزاز الناتج عن إزاحة الارتداد حرفًا بحرف التي جعلت الكلمات «تكبر ثم تصغر»؛ وحجم خط السطر النشط / شفافيته في الحالة الموسعة صارا يتغيران بانتقال سلس 300ms لتمرير أنعم وأكثر استقرارًا

## WinIsland 1.1.0 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد
- **مؤشر الصوت / كتم مؤقت على الجزيرة**: عند تغيير صوت النظام أو الكتم / إلغاء الكتم، تعرض الجزيرة مؤشرًا صوتيًا لفترة وجيزة (مدة العرض قابلة للضبط، وقابلة للتعطيل في الإعدادات)
- **نسخ / نقل الملفات على الجزيرة**: عند اكتشاف أن مستكشف الملفات ينسخ / ينقل ملفات، تعرض الجزيرة «جارٍ نسخ الملفات…» (تحديد عبر عنوان نافذة محلي فقط، قابل للتعطيل)
- **تقدم التنزيل على الجزيرة**: اكتشاف الملفات المؤقتة في مجلد التنزيل (.crdownload / .part / .download وغيرها) وعرض «جارٍ تنزيل N من الملفات» (مغلق افتراضيًا، يمكن تفعيله في الإعدادات)
- **كبسولة دمج «قيد الاستخدام»**: الإعدادات ← المكونات يمكن تفعيلها (مغلقة افتراضيًا) لدمج «ميكروفون / كاميرا / في اجتماع / تسجيل الشاشة» في كبسولة حالة واحدة «قيد الاستخدام · …»؛ يمكن اختيار المكونات المشاركة في الدمج، ولا تظهر العناصر المدمجة منفردة بعد الآن
- **تحسين مؤقت بومودورو**: النقر على مكوّن البومودورو في الجزيرة يوقف / يستأنف المؤقت
- **مؤشر مؤقت للقطة / تسجيل الشاشة**: عند التقاط لقطة أو بدء تسجيل، تعرض الجزيرة المؤشر المناسب مؤقتًا (يعمل حتى عند إخفاء الجزيرة)
- **ارتداد الإضاءة حرفًا بحرف في الكاريوكي**: كل سطر كلمات يُضاء بسلاسة من الحرف الأول مع ارتداد خفيف، أكثر سلاسة وطبيعية
- تحسين بنية الاستقصاء الداخلي: أحداث مؤقتة مثل الصوت / النسخ / التنزيل / اللقطة تعمل أيضًا في الحالة المخفية

## WinIsland 1.0.9 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد
- **مكونات جديدة**: استخدام GPU، استخدام الميكروفون / الكاميرا، العد التنازلي للعطلات، في اجتماع؛ ومكوّن الشبكة يمكنه عرض منحنى مصغر لآخر 32 ثانية
- **إجراء نقر مزدوج سريع**: الإعدادات ← عام يمكن ضبط النقر المزدوج على الجزيرة ليكون «تشغيل / إيقاف» أو «فتح الإعدادات» أو «بدون إجراء»
- **مساعد كتم الاجتماعات**: التعرف على نوافذ الاجتماعات (Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet) مع إشعار عدم الإزعاج تلقائيًا أثناء الاجتماع (استدلال محلي فقط)
- **إشعارات تسجيل / لقطة الشاشة**: إشعار لقطة شاشة PrintScreen + كشف برامج التسجيل (OBS / Bandicam / Xbox Game Bar وغيرها)
- **تذكيرات أحداث التقويم (.ics)**: تحليل ملفات iCalendar المحلية؛ عند حلول الحدث (يمكن قبله بعدة دقائق) تظهر لافتة، محلي بالكامل
- **تذكيرات اشتراك RSS**: استقصاء RSS 2.0 / Atom وعرض لافتة عند الإدخالات الجديدة
- **تنبيه البريد (POP3)**: قراءة رؤوس البريد فقط، لافتة عند وجود بريد جديد، ويُنصح باستخدام رمز التفويض
- **مشغل سريع (بأسلوب Spotlight)**: `Ctrl+Space` للبحث عن التطبيقات / إدخال رابط وفتحه
- **لوحة سجل الحافظة**: `Ctrl+Alt+V` نافذة مستقلة، النقر ينسخ مرة أخرى إلى الحافظة
- **القواعد (الأتمتة)**: الشروط (دائمًا / غير مشغّل / قيد التشغيل / فترة زمنية / برنامج وسائط محدد) × الإجراءات (إخفاء / طي قسري / عرض قسري)
- **واجهة الجزيرة API v3**: صور (data URI / http)، تقدم ديناميكي (from/to/duration تلقائي)، تجديد بالقلب (heartbeat_seconds)، تحديث جزئي PATCH، قناة WebSocket (/v3/ws)
- **المظهر**: 18 ثيمًا جاهزًا، لون خلفية مخصص، 4 جلود رسوم متحركة، وضع الطاقة المنخفضة
- صفحة الإعدادات صارت بأسلوب إعدادات نظام macOS (تنقل يساري + محتوى يميني)، وكل التغييرات تُطبق فورًا

- **إصلاح النص الأسود في الوضع الداكن**: ربط لون المقدمة بشكل موحد لجميع قوالب عناصر التحكم المخصصة في واجهة الإعدادات (أزرار / خانات اختيار / حقول إدخال / قوائم منسدلة / عناصر القوائم / علامات التبويب / التنقل الأيسر إلخ) مع فحص احتياطي وقت التشغيل — لم تعد بعض الخيارات (لغة الواجهة، إجراء النقر المزدوج إلخ) تظهر بنص أسود غير مقروء في الوضع الداكن، ويعود النص الداكن تلقائيًا في الوضع الفاتح
- **إصلاح عدم فتح واجهة الإعدادات**: إزالة أسطر XAML المكررة للون المقدمة التي سببت فشل تحميل BAML
- **تحسين أداء الرسوم المتحركة**: إعادة استخدام كائنات Run في الكاريوكي حرفًا بحرف (إلغاء التخطيط إطارًا بإطار)، وقصص مستقرة بمعدل 60 إطارًا في الثانية، وتحديث السجلات بالدفعات، لرسوم أكثر سلاسة
- **إزالة النقطة الحمراء للشارة** على المكونات (بناءً على طلب المستخدمين)

## WinIsland 1.0.8 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد

**إعادة بناء شاملة لواجهة الإعدادات**
- تخطيط بأسلوب إعدادات macOS: قائمة تنقل يسارية + منطقة محتوى يمينية، بإجمالي 13 فئة (عام / مظهر / مكونات / وسائط / عرض معلومات الوسائط / كلمات / Cider / واجهة الجزيرة API / أدوات الإنتاجية / تحديث / حول / إشعارات / قواعد)
- ألوان نص الإعدادات تتكيف تلقائيًا في الوضعين الداكن / الفاتح: نص أبيض في الداكن وأسود في الفاتح، دون مشاكل قراءة
- نص التنقل الأيسر صار أبيض عالي التباين، مع إزالة خط الفصل الأبيض الأيمن وتعزيز استجابات التمرير والتحديد

**تشغيل الوسائط**
- مشغل مصغر جديد: نافذة عائمة مستقلة تعرض غلاف الألبوم / اسم الأغنية / الفنان / شريط التقدم وأزرار التحكم، قابلة للسحب الحر مع تذكر الموقع، تظهر وتختفي تلقائيًا مع تشغيل الوسائط (يمكن تفعيلها في الإعدادات)
- تبديل جهاز إخراج الصوت: الإعدادات ← الوسائط يمكنها تعداد أجهزة التشغيل الافتراضية للنظام وتبديلها (يُنصح بإعادة تشغيل المشغل بعد التبديل ليُطبق)
- تحسين أساسي لمصادر المشغل: دعم تعداد جميع جلسات SMTC للوسائط وجلسات Cider مع إمكانية تبديل مصدر الوسائط

**الكلمات**
- كلمات ثنائية اللغة جديدة: دمج أسطر الترجمة ذات الطوابع الزمنية المتجاورة تلقائيًا (يمكن إيقافه في الإعدادات)

**المظهر والحركة**
- جلود حركة جديدة: 4 أنماط حركة (زنبرك iOS (افتراضي) / زنبرك ناعم / ارتداد مرن / تلاشي بسيط)، مع تخفيف غير خطي في التوسيع / الطي
- وضع الطاقة المنخفضة الجديد: خفض معدل إطارات الموجات وتخفيف الرسوم عند الخمول لتوفير الطاقة

**الاختصارات العامة**
- 5 مجموعات مفاتيح قابلة للتخصيص: إظهار / إخفاء، تشغيل / إيقاف، التالي، السابق، توسيع / طي
- يدعم Ctrl / Alt / Shift / Win + حروف أو أرقام أو F1–F24 أو مفاتيح الأسهم

**محرك القواعد الذكي (الإعدادات ← القواعد)**
- التحكم التلقائي في عرض الجزيرة حسب الشروط: دائمًا مفعل / عند عدم تشغيل وسائط / عند تشغيل وسائط / فترة زمنية محددة / عند تشغيل برنامج وسائط محدد
- الإجراءات: إخفاء الجزيرة / طي قسري / عرض قسري؛ الأولوية: إخفاء > طي > عرض قسري

**الإشعارات**
- سجل الإشعارات: علامة أحمر غير مقروء، تحديد الكل كمقروء، حذف مفرد، النقر على عنصر لفتح التطبيق المصدر، مسح السجل
- طي الإشعارات الجديد: الإشعارات المتكررة من نفس المصدر والعنوان تشترك في نفس اللافتة مع تجميع العدد
- قائمة بيضاء لعدم الإزعاج: المصادر في القائمة البيضاء (أسماء exe مفصولة بفواصل) لا تخضع لعدم الإزعاج وتستمر في عرض اللافتات
- إزالة علامة النقطة الحمراء غير المقروءة من الجزيرة

**أدوات الإنتاجية**
- إشعار «تم النسخ» عند نسخ النص
- التعرف التلقائي على رموز التحقق القصيرة وإبرازها
- نسخ النصوص الكبيرة...

### الأصول
- الإصدار المحمول Windows x64 / arm64 (ملف واحد قائم بذاته، يعمل مباشرة دون تثبيت)
- حزمة التثبيت العامة Windows (Inno Setup، تدعم x64 و ARM64، تثبيت تلقائي حسب البنية)

> الإصدار المحمول ملف exe مستقل، ولم يعد يُقدم حزمة ZIP.

## WinIsland 1.0.7 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد
- ترقية «موجات الصوت» إلى**الاستجابة لإيقاع الموسيقى**: عبر WASAPI Loopback تُلتقط الأصوات الحقيقية التي يشغلها النظام لحظيًا؛ الموجة ترتفع مع الإيقاع القوي وتنخفض مع الهدوء، بدلًا من شريط صوت ثابت
- **تصيير متصل بمعدل 60 إطارًا في الثانية**: نعومة أسية 25ms للبداية / 140ms للتحرر، تموجات متصلة وغير خشنة أو متقطعة
- إعدادات جديدة (الإعدادات ← المظهر ← موجات الصوت): مفتاح الاستجابة لإيقاع الموسيقى، الحساسية 0.2–3.0، ارتفاع الموجة 0.4–1.6، وتُطبق فورًا
- عند غياب أجهزة الصوت / خلل خدمة الصوت، ينخفض تلقائيًا إلى محاكاة الإيقاع مع إعادة محاولة كل 8 ثوانٍ لاستعادة الالتقاط الفعلي، دون تجمد أو تراكم الخيوط

## WinIsland 1.0.6 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد
- إضافة 6 مكونات للجزيرة: الصوت، مؤشر لوحة المفاتيح (CapsLock)، الحافظة، المهام، مؤقت بومودورو، الجدول — جميعها تدعم اختيارًا مزدوجًا «بدون أغنية / مع أغنية» وترتيبًا بالسحب
- صفحة «أدوات الإنتاجية» الجديدة في الإعدادات: سجل الحافظة، مؤقت بومودورو، قائمة المهام، تذكيرات الجدول
- «موجات الصوت» الجديدة: عند تشغيل الوسائط، تنبض الموجة يسار أزرار التحكم مع صوت النظام لحظيًا (الإعدادات ← المظهر للتفعيل)
- 7 ثيمات جديدة: افتراضي / محيط / غابة / غروب / نيون / أحادي اللون / بنفسجي عنبي
- تخصيص المظهر: خط مخصص، تكبير الخط (0.8–1.4)، نصف قطر زوايا الكبسولة (16–40)، خلفية التوسيع بألوان غلاف الألبوم، شارة الإشعارات غير المقروءة
- قائمة علبة النظام الجديدة: وضع عدم الإزعاج (يدوي / كتم تلقائي حسب الفترة)، التحقق من التحديثات، عرض السجلات
- إعادة تصميم حديثة لصفحة الإعدادات (زوايا دائرية + زجاج سائل)، تطبيق فوري للخيارات دون حفظ يدوي
- فحص التحديثات الجديد (يدوي من العلبة / الإعدادات، مع خيار تلقائي، مغلق افتراضيًا)

### الأصول
- الإصدار المحمول Windows x64 / arm64 (ملف واحد قائم بذاته، يعمل مباشرة دون تثبيت)
- حزمة التثبيت العامة Windows (Inno Setup، تدعم x64 و ARM64، تثبيت تلقائي حسب البنية)

> الإصدار المحمول ملف exe مستقل، ولم يعد يُقدم حزمة ZIP.

## WinIsland 1.0.5 (إصدار مستقر)

أداة Windows Dynamic Island حديثة ومتعددة الوظائف.

### ما الجديد
- «واجهة الجزيرة API» الجديدة: يمكن للبرامج الأخرى دفع المعلومات إلى الجزيرة عبر واجهة HTTP محلية (مثل تكامل تطبيقات طرف ثالث مع Dynamic Island في iOS)، **وثائق التطوير في docs/IslandAPI.md**
  - `POST /v1/island/push` دفع/تحديث · `DELETE /v1/island/push/{id}` إزالة · `GET /v1/island/active` استعلام · `GET /v1/health`
  - دعم الأيقونات والعناوين والنص والتقدم والأزرار (فتح رابط / تشغيل برنامج) ومدة عرض مخصصة لكل عنصر
  - صفحة الإعدادات توفر تفعيل / منفذ / Token اختياري / مدة افتراضية عامة
- بطاقات الجزيرة تُعرض في سطر واحد في الحالة المضغوطة دون حجب المكونات الأخرى و**دون التأثير على أبعاد الجزيرة** (الأبعاد التلقائية / اليدوية ثابتة)
- «الضبط التلقائي» للحجم: تكيف حسب المحتوى، وسحب شريط التمرير يدويًا يغلق عنصر الضبط التلقائي المقابل
- المحتوى الموسع يدعم التمرير بالعجلة (مع إخفاء شريط التمرير)
- محاذاة موحدة للمكونات عموديًا؛ إصلاح تخطيط / خط التشغيل (فرض PerMonitorV2، حجم طبيعي عند البدء)
- لم تعد وسائط التشغيل تظهر إشعار «قيد التشغيل»

- الإصلاح: بعد توسيع الجزيرة (بنحو 1-2 ثانية) كانت البطاقة تعود إلى الحجم المضغوط مسببة شاشة سوداء
  - أصبح المحتوى الموسع يتقاطع مع السطر المضغوط بتلاشي متقاطع، دون كشف الخلفية أثناء الرسم
  - بعد اكتمال رسم التوسيع / الطي تُكتب أبعاد البطاقة النهائية صراحة، فلا تعود الحالة الموسعة إلى الانكماش
  - إصلاح مشكلة الشاشة السوداء عند النقر على أزرار طرف ثالث في الحالة الموسعة

### الأصول
- الإصدار المحمول Windows x64 / arm64 (ملف واحد قائم بذاته، يعمل مباشرة دون تثبيت)
- حزمة التثبيت العامة Windows (Inno Setup، تدعم x64 و ARM64، تثبيت تلقائي حسب البنية)

> الإصدار المحمول ملف exe مستقل، ولم يعد يُقدم حزمة ZIP.

---

## Русский

## WinIsland 1.1.5 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового
- **Посимвольные тексты AMLL (настоящее караоке)**: подключение к библиотеке TTML в стиле Apple Music на amll.dev; Настройки → Тексты, переключатель «Посимвольные тексты AMLL» (включён по умолчанию); приоритет источников: локальные LRC → AMLL TTML → Cider → онлайн-тексты
- **Переписан движок посимвольной подсветки**: непрерывное продвижение по настенным часам 60fps + нелинейное сглаживание; запасной вариант — равномерное деление всей строки; двуязычные тексты сопоставляются по таймлайну
- **Стабильная подсветка при паузе**: после паузы подсветка замирает на моменте паузы и не прыгает при выходе или перезапуске; анимацию ведёт только активная строка, в простое почти 0% CPU
- **Исправлены отступы текста в компактном состоянии**: текст сохраняет фиксированный отступ от кнопок справа; при нехватке места ширина острова автоматически расширяется (720→800)
- **Стабильность**: тайм-аут AMLL 5 секунд + изящная деградация; при повторном появлении элемента база настенных часов калибруется заново

## WinIsland 1.1.4 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового
- **Кнопки быстрых действий**: внизу развёрнутой карточки острова добавлен настраиваемый ряд быстрых кнопок (блокировка / без звука / воспроизведение-пауза / скриншот / показать рабочий стол / диспетчер задач / калькулятор / сон / громкость±); Настройки → Быстрые действия позволяют отмечать и сортировать их с помощью ↑↓, изменения применяются мгновенно
- **Оповещения о входящих звонках**: обнаружение окон голосовых и видеозвонков WeChat / QQ с всплывающим уведомлением в правом верхнем углу (различает «входящий звонок» и «в разговоре»); Настройки → Уведомления позволяют включать и настраивать отслеживаемые приложения; обнаружение только локальное — данные не отправляются
- **Командные действия острова**: кнопки сторонних push-уведомлений теперь поддерживают `action: "command"` для локального выполнения командной строки (только локальный loopback API, Token настраивается)
- **Темы карточек острова**: сторонние push-уведомления могут передавать `theme: dark / light / auto`, и карточка автоматически переключается между тёмным и светлым стеклянным стилем
- **Ускорение анимаций**: общая длительность четырёх скинов анимации сокращена примерно на 20% с сохранением пружинного сглаживания в стиле iOS и плавных 60fps без пропуска кадров
- **Оптимизация фонового расхода**: индикатор клавиатуры опрашивается только при включённом виджете, мониторинг полноэкранного режима реже, звуковые волны замедляются в простое
- **Исправления стабильности**: исправлено ложное уведомление «Скопировано» для содержимого, уже находившегося в буфере обмена при запуске (базовая линия запуска); исправлено отображение экранирования в заголовках календаря .ics (\, \; \n) в виде обратных слешей
- **Производительность и стабильность**: снижен шум логов API Bluetooth / SMTC / погоды, экспоненциальная задержка при ограничении частоты погоды; сборка проходит, все 104 модульных и интеграционных теста пройдены

## WinIsland 1.1.3 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового
- **Переключение между несколькими плеерами**: при нескольких открытых плеерах развёрнутая карточка может одним кликом переключать текущий управляемый источник медиа (иконка ноты + имя источника + стрелка выпадающего списка); больше не нужно управлять только последним запущенным плеером
- **Обложка во весь экран**: нажмите на обложку альбома / большое изображение на развёрнутой карточке, чтобы открыть полноэкранный предпросмотр; закрытие кликом / Esc / правой кнопкой с плавным затуханием
- **Точная подстройка тайминга текстов**: в области текстов развёрнутой карточки добавлены кнопки +0,5s / -0,5s, запоминающие смещение для каждой песни, чтобы тексты идеально совпадали с музыкой
- **Улучшенные тексты на рабочем столе**: отдельное окно текстов поддерживает регулировку прозрачности (по умолчанию 0,85) и переключатель «Блокировка» (при блокировке мышь проходит насквозь, окно нельзя перетаскивать)
- **Дышащая динамическая тема**: при включённом фоне из цветов обложки фон развёрнутой карточки медленно «дышит» вместе с цветом обложки (цикл около 18 секунд) вместо статичного плоского блока
- **Мгновенный отклик на клик**: нажатие мыши сразу переключает разворот / сворачивание, не дожидаясь отпускания — отклик более быстрый
- **Кнопки действий в уведомлениях**: баннеры уведомлений поддерживают кнопки действий (уведомление о подключении Bluetooth теперь имеет кнопки «Отключить» и «Настройки», при нажатии выполняются сразу и баннер сворачивается)
- **Callback кнопок острова**: при нажатии кнопки действия notify в сторонних push-уведомлениях через WebSocket транслируется событие push_button (с push_id и текстом кнопки), и сторона-отправитель сама обрабатывает callback
- **Производительность и стабильность**: динамическая тема стала подписываться на кадры композиции по требованию (0% CPU в простое), кэш градиентных кистей переиспользуется для снижения нагрузки на GC; исправлен конфликт мьютекса между новой и старой версиями при одновременном запуске, мешавший старту

## WinIsland 1.1.1 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового
- **Оповещение о заряде батареи**: уведомление о низком заряде (порог настраивается), при подключении питания и достижении заданного порога (по умолчанию 100%) показывается «Зарядка завершена»; оба — локальное обнаружение, можно отключить
- **Сетевые уведомления**: уведомление при отключении / восстановлении сети (локальная проверка состояния сети, отключается)
- **Новые виджеты**: свободное место на диске (системный диск), состояние метода ввода (китайский / английский + название метода ввода)
- **Лунный календарь и сезонные термины**: виджет даты может дополнительно показывать дату по лунному календарю и сезонные термины (включено по умолчанию, отключается в настройках)
- **Виджет быстрых переключателей**: WiFi / Bluetooth / ночной режим / без звука одним кликом (через локальный API, без интернета; состояние Radio кэшируется на 2 секунды для снижения нагрузки)
- **Значок источника воспроизведения**: виджет медиа показывает текущий источник (Spotify / Cider / NetEase / QQ Music и др.), чтобы сразу было видно, из какого плеера идёт музыка
- **Улучшение текстов**: переключатель показа/скрытия перевода текстов и кнопка «Скопировать текущую строку» для копирования текущего текста одним кликом
- **Настройка иконок виджетов**: каждый виджет можно снабдить собственной иконкой (MDL2 или Emoji); при отсутствии настройки используется стандартный глиф
- **Исправление прыжков масштаба текста**: убрана дрожь от смещения посимвольного отскока, из-за которой текст «увеличивался и уменьшался»; размер шрифта / прозрачность активной строки в развёрнутом состоянии теперь плавно меняются за 300мс — прокрутка более плавная и стабильная

## WinIsland 1.1.0 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового
- **Временный индикатор громкости / беззвучия на острове**: при изменении системной громкости или включении/выключении беззвучия остров кратковременно показывает индикатор громкости (длительность настраивается, можно отключить в настройках)
- **Копирование / перемещение файлов на острове**: при обнаружении копирования/перемещения файлов в проводнике остров показывает «Копирование файлов…» (определение только по заголовку окна, отключается)
- **Прогресс загрузки на острове**: обнаружение временных файлов браузера (.crdownload / .part / .download и др.) в папке загрузок с показом «Загружается N файлов» (по умолчанию выключено, можно включить в настройках)
- **Капсула объединения «В использовании»**: Настройки → Виджеты можно включить (по умолчанию выключено), чтобы объединить «микрофон / камера / в совещании / запись экрана» в одну капсулу состояния «В использовании · …»; можно выбрать участвующие виджеты, объединённые больше не показываются отдельно
- **Улучшение Pomodoro**: клик по виджету Pomodoro на острове приостанавливает / возобновляет таймер
- **Временный индикатор скриншота / записи**: при скриншоте или начале записи остров временно показывает соответствующий индикатор (срабатывает даже при скрытом острове)
- **Отскок посимвольной подсветки караоке**: каждая строка текста плавно подсвечивается с первого символа с лёгким отскоком — плавнее и естественнее
- Оптимизация внутренней архитектуры опроса: временные события (громкость / копирование / загрузка / скриншот и т. п.) срабатывают и в скрытом состоянии

## WinIsland 1.0.9 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового
- **Новые виджеты**: использование GPU, использование микрофона / камеры, обратный отсчёт до праздников, в совещании; виджет сети может показывать мини-график за последние 32 секунды
- **Быстрое действие по двойному клику**: Настройки → Общие позволяют задать двойной клик по острову как «воспроизведение / пауза», «открыть настройки» или «без действия»
- **Ассистент отключения звука на совещаниях**: распознавание окон совещаний (Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet) с автоматическим «Не беспокоить» во время совещания (только локальная эвристика)
- **Уведомления о записи / скриншоте экрана**: уведомление о скриншоте PrintScreen + обнаружение программ записи (OBS / Bandicam / Xbox Game Bar и др.)
- **Напоминания о событиях календаря (.ics)**: анализ локальных файлов iCalendar; при наступлении события (можно заранее, за N минут) показывается баннер — полностью локально
- **Напоминания RSS-подписок**: опрос RSS 2.0 / Atom, баннер при новых записях
- **Почтовые уведомления (POP3)**: чтение только заголовков писем, баннер при новом письме; рекомендуется код авторизации
- **Быстрый запуск (в стиле Spotlight)**: `Ctrl+Space` для поиска приложений / ввода URL для открытия
- **Панель истории буфера обмена**: `Ctrl+Alt+V` отдельное окно, клик копирует обратно в буфер обмена
- **Правила (автоматизация)**: условия (всегда / не воспроизводится / воспроизводится / период времени / конкретная медиапрограмма) × действия (скрыть / принудительно свернуть / принудительно показать)
- **Island API v3**: изображения (data URI / http), динамический прогресс (from/to/duration автоматически), продление по heartbeat (heartbeat_seconds), частичное обновление PATCH, канал WebSocket (/v3/ws)
- **Внешний вид**: 18 готовых тем, пользовательский цвет фона, 4 скина анимации, режим низкого энергопотребления
- Страница настроек переделана в стиле System Settings macOS (левая навигация + правый контент), все изменения применяются мгновенно

- **Исправление чёрного текста в тёмном режиме**: единая привязка цвета текста для всех пользовательских шаблонов элементов управления в интерфейсе настроек (кнопки / флажки / поля ввода / выпадающие списки / элементы списков / вкладки / левая навигация и т. д.) плюс резервное сканирование во время выполнения — в тёмном режиме отдельные варианты (язык интерфейса, действие двойного клика и т. п.) больше не отображаются чёрным нечитаемым текстом; в светлом режиме тёмный текст возвращается автоматически
- **Исправление невозможности открыть окно настроек**: удалены повторяющиеся строки XAML с цветом текста, вызывавшие сбой загрузки BAML
- **Оптимизация производительности анимаций**: переиспользование объектов Run в посимвольном караоке (устранение покадрового layout), стабильные storyboard 60fps, пакетное обновление логов — анимации плавнее
- **Убрана красная точка-бейдж на виджетах** (по просьбе пользователей)

## WinIsland 1.0.8 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового

**Полная переработка интерфейса настроек**
- Макет в стиле System Settings macOS: левый список навигации + правая область контента, всего 13 категорий (Общие / Внешний вид / Виджеты / Медиа / Отображение медиаинформации / Тексты / Cider / Island API / Инструменты продуктивности / Обновление / О программе / Уведомления / Правила)
- Цвет текста настроек автоматически адаптируется в тёмном / светлом режиме: белый в тёмном, чёрный в светлом — больше никаких проблем с читаемостью
- Текст левой навигации стал белым с высоким контрастом, убрана правая белая разделительная линия, усилена реакция на наведение и выбор

**Воспроизведение медиа**
- Новый мини-плеер: отдельное плавающее окно с обложкой альбома / названием / исполнителем / полосой прогресса и управлением воспроизведением; свободно перетаскивается и запоминает позицию; автоматически показывается / скрывается при воспроизведении (можно включить в настройках)
- Переключение устройства вывода звука: Настройки → Медиа позволяют перечислить и переключить системное устройство воспроизведения по умолчанию (после переключения рекомендуется перезапустить плеер)
- Улучшена основа источников плеера: поддержка перечисления всех медиасессий SMTC и сессий Cider с переключением источника медиа

**Тексты**
- Новые двуязычные тексты: автоматическое объединение строк перевода с соседними временными метками (можно отключить в настройках)

**Внешний вид и анимация**
- Новые скины анимации: 4 стиля (пружина iOS (по умолчанию) / мягкая пружина / упругий отскок / простое затухание), с нелинейным сглаживанием при развороте / сворачивании
- Новый режим низкого энергопотребления: снижение частоты кадров волн и упрощение анимаций в простое — экономия энергии

**Глобальные горячие клавиши**
- 5 настраиваемых комбинаций: показать / скрыть, воспроизведение / пауза, следующий трек, предыдущий трек, развернуть / свернуть
- Поддержка Ctrl / Alt / Shift / Win + буквы, цифры, F1–F24, клавиши стрелок

**Умный движок правил (Настройки → Правила)**
- Автоматическое управление отображением острова по условиям: всегда действует / когда медиа не воспроизводится / когда медиа воспроизводится / заданный период времени / при воспроизведении заданной медиапрограммы
- Действия: скрыть остров / принудительно свернуть / принудительно показать; приоритет: скрыть > свернуть > принудительно показать

**Уведомления**
- История уведомлений: отметка непрочитанных красной точкой, отметить все прочитанными, удаление отдельной записи, клик по записи открывает исходное приложение, очистка истории
- Новое сворачивание уведомлений: повторные уведомления одного источника и заголовка используют один баннер с накоплением количества
- Белый список «Не беспокоить»: источники из белого списка (имена exe через запятую) не подпадают под «Не беспокоить» и продолжают показывать баннеры
- Убрана точка-бейдж непрочитанных на острове

**Инструменты продуктивности**
- Появление уведомления «Скопировано» при копировании текста
- Автоматическое распознавание кодов подтверждения из SMS с подсветкой
- Копирование больших текстов...

### Ресурсы
- Портативная версия Windows x64 / arm64 (один автономный файл, запускается без установки)
- Универсальный установщик Windows (Inno Setup, поддерживает x64 и ARM64, автоматическая установка по архитектуре)

> Портативная версия — это отдельный exe-файл, ZIP-архив больше не предоставляется.

## WinIsland 1.0.7 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового
- «Звуковые волны» обновлены до**реагирования на ритм музыки**: через WASAPI Loopback в реальном времени захватывается реальный звук системы; волна выше при сильном бите и ниже в тишине, вместо фиксированной полосы громкости
- **Плавный рендеринг 60fps**: экспоненциальное сглаживание 25мс атаки / 140мс отпускания, волны непрерывные, без рывков и задержек
- Новые настройки (Настройки → Внешний вид → Звуковые волны): переключатель реакции на ритм музыки, чувствительность 0.2–3.0, высота волны 0.4–1.6, применяются мгновенно
- При отсутствии аудиоустройств / сбое аудиосервиса автоматически переходит на имитацию бита и каждые 8 секунд пытается восстановить реальный захват — без зависаний и накопления потоков

## WinIsland 1.0.6 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового
- Добавлены 6 виджетов острова: громкость, индикатор клавиатуры (CapsLock), буфер обмена, задачи, Pomodoro, расписание — все поддерживают двойной выбор «без песни / с песней» и перетаскивание для сортировки
- Новая страница «Инструменты продуктивности» в настройках: история буфера обмена, таймер Pomodoro, список задач, напоминания расписания
- Новые «Звуковые волны»: при воспроизведении медиа волна слева от кнопок управления вибрирует в реальном времени вместе с системной громкостью (Настройки → Внешний вид для включения)
- Добавлены 7 тем: по умолчанию / океан / лес / закат / неон / монохром / виноград
- Кастомизация внешнего вида: пользовательский шрифт, масштаб шрифта (0.8–1.4), радиус углов капсулы (16–40), фон разворота из цветов обложки альбома, бейдж непрочитанных уведомлений
- Новое в меню трея: режим «Не беспокоить» (вручную / автоматически по расписанию), проверка обновлений, просмотр журналов
- Современный редизайн страницы настроек (скруглённые углы + жидкое стекло), изменения применяются мгновенно без ручного сохранения
- Новая проверка обновлений (вручную из трея / настроек, опционально автоматически, по умолчанию выключена)

### Ресурсы
- Портативная версия Windows x64 / arm64 (один автономный файл, запускается без установки)
- Универсальный установщик Windows (Inno Setup, поддерживает x64 и ARM64, автоматическая установка по архитектуре)

> Портативная версия — это отдельный exe-файл, ZIP-архив больше не предоставляется.

## WinIsland 1.0.5 (Стабильный выпуск)

Современный многофункциональный виджет Dynamic Island для Windows.

### Что нового
- Новый «Island API»: другие программы могут отправлять информацию на остров через локальный HTTP-интерфейс (аналогично интеграции сторонних приложений с Dynamic Island в iOS), **документация разработчика в docs/IslandAPI.md**
  - `POST /v1/island/push` отправка/обновление · `DELETE /v1/island/push/{id}` удаление · `GET /v1/island/active` запрос · `GET /v1/health`
  - Поддержка иконок, заголовков, текста, прогресса, кнопок (открыть ссылку / запустить программу), индивидуальной длительности показа
  - Страница настроек: включение / порт / необязательный Token / глобальная длительность по умолчанию
- Карточки острова в компактном состоянии показываются в одну строку, не перекрывают другие виджеты и **не влияют на размеры острова** (автоматические / ручные размеры постоянны)
- «Автоматическая подстройка» размера: адаптация под содержимое; ручное перетаскивание ползунка автоматически отключает соответствующую автоопцию
- Развёрнутый контент поддерживает прокрутку колесом (полоса прокрутки скрыта)
- Единое вертикальное выравнивание виджетов; исправление макета / шрифта запуска (принудительный PerMonitorV2, нормальный размер с первого запуска)
- При воспроизведении медиа больше не показывается уведомление «Сейчас воспроизводится»

- Исправление: после разворота острова (примерно через 1–2 секунды) карточка возвращалась к компактному размеру, вызывая чёрный экран
  - Развёрнутый контент теперь перекрывается с компактной строкой перекрёстным затуханием, фон не просвечивает во время анимации
  - После завершения анимации разворота / сворачивания итоговый размер карточки явно записывается, развёрнутое состояние стабильно не схлопывается
  - Заодно исправлена проблема чёрного экрана при клике по сторонним кнопкам острова в развёрнутом состоянии

### Ресурсы
- Портативная версия Windows x64 / arm64 (один автономный файл, запускается без установки)
- Универсальный установщик Windows (Inno Setup, поддерживает x64 и ARM64, автоматическая установка по архитектуре)

> Портативная версия — это отдельный exe-файл, ZIP-архив больше не предоставляется.

---

## Português

## WinIsland 1.1.5 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades
- **Letras palavra por palavra AMLL (karaokê de verdade)**: conexão à biblioteca TTML no estilo Apple Music da amll.dev; Configurações → Letras, alternância «Letras palavra por palavra AMLL» (ativada por padrão); prioridade de fontes: LRC local → AMLL TTML → Cider → letras on-line
- **Mecanismo de destaque palavra por palavra reescrito**: progressão contínua de relógio de parede a 60fps + easing não linear; fallback divide a linha inteira uniformemente; letras bilíngues combinadas pela linha do tempo
- **Destaque estável ao pausar**: após pausar, o destaque congela no momento da pausa e não pula ao sair ou reiniciar; apenas a linha ativa conduz a animação, CPU quase 0% em repouso
- **Correção de espaçamento das letras no estado compacto**: as letras mantêm um espaçamento fixo em relação aos botões à direita; quando o espaço é insuficiente, a largura da Ilha se expande automaticamente (720→800)
- **Estabilidade**: timeout AMLL de 5 segundos + degradação elegante; a base do relógio de parede é recalibrada quando o controle fica visível novamente

## WinIsland 1.1.4 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades
- **Botões de ação rápida**: uma linha personalizável de botões rápidos foi adicionada à parte inferior do cartão expandido da Ilha (bloquear tela / mudo / play-pause / captura de tela / mostrar área de trabalho / gerenciador de tarefas / calculadora / dormir / volume±); Configurações → Ações rápidas permite marcar e reordenar com ↑↓, e as alterações têm efeito imediato
- **Alertas de chamadas recebidas**: detecta janelas de chamadas de voz e vídeo do WeChat / QQ e exibe um alerta no canto superior direito (distingue «chamada recebida» de «em chamada»); Configurações → Notificações permite ativar e personalizar os aplicativos detectados; a detecção é apenas local — nenhum dado é enviado
- **Ações de comando na Ilha**: botões de push de terceiros agora suportam `action: "command"` para executar uma linha de comando localmente (somente API de loopback local, Token configurável)
- **Temas de cartões da Ilha**: pushes de terceiros podem carregar `theme: dark / light / auto`, e o cartão alterna automaticamente entre os estilos de vidro escuro e claro
- **Animações mais rápidas**: a duração total dos quatro skins de animação foi reduzida em cerca de 20%, mantendo o easing de mola do iOS e os suaves 60fps sem quadros perdidos
- **Otimização de uso em segundo plano**: o indicador do teclado é consultado apenas com o widget ativado, o monitoramento de tela cheia roda com frequência menor e as ondas de áudio são reduzidas em repouso
- **Correções de estabilidade**: corrigido o aviso falso de «Copiado» para conteúdo já presente na área de transferência ao iniciar (linha de base de inicialização); corrigido o escape de títulos do calendário .ics (\, \; \n) sendo exibido como barras invertidas
- **Desempenho e estabilidade**: menos ruído nos logs das APIs de Bluetooth / SMTC / clima, com backoff exponencial para limite de taxa do clima; a compilação passa e todos os 104 testes unitários e de integração passam

## WinIsland 1.1.3 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades
- **Alternância entre vários players**: com vários players abertos, o cartão expandido pode alternar a fonte de mídia atualmente controlada com um clique (ícone de nota + nome da fonte + seta do menu suspenso); você não está mais limitado a controlar apenas o último player iniciado
- **Capa do álbum imersiva**: clique na capa do álbum / imagem grande no cartão expandido para abrir uma prévia em tela cheia; feche com clique / Esc / botão direito com fade
- **Ajuste fino do tempo das letras**: a área de letras do cartão expandido agora tem botões +0,5s / -0,5s que memorizam um deslocamento por música para alinhar perfeitamente as letras à música
- **Letras de área de trabalho aprimoradas**: a janela independente de letras suporta ajuste de opacidade (padrão 0,85) e uma alternância de «Bloquear» (quando bloqueada, o mouse atravessa e a janela não pode ser arrastada)
- **Tema dinâmico respirando**: com o fundo derivado da cor da capa ativado, o fundo do cartão expandido «respira» lentamente junto com a cor da capa (ciclo de cerca de 18 segundos) em vez de um bloco de cor estático
- **Resposta instantânea ao clique**: pressionar o mouse alterna imediatamente expandir / recolher, sem esperar o soltar, para uma sensação mais responsiva
- **Botões de ação em notificações**: banners de notificação suportam botões de ação (o alerta de conexão Bluetooth agora tem botões «Desconectar» e «Configurações», executados imediatamente ao clicar e recolhidos)
- **Callback de botões da Ilha**: ao clicar no botão de ação notify em pushes de terceiros, um evento push_button é transmitido via WebSocket ao remetente (incluindo push_id e o texto do botão), e o remetente processa o callback
- **Desempenho e estabilidade**: o tema dinâmico agora assina quadros de composição sob demanda (0% CPU em repouso), o cache de pincéis de gradiente é reutilizado para reduzir a pressão do GC; corrigido o conflito de mutex entre instâncias novas e antigas em execução simultânea que impedia a inicialização

## WinIsland 1.1.1 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades
- **Alerta de bateria**: alerta de bateria fraca (limiar ajustável); ao conectar a energia e atingir o limiar definido (padrão 100%), aparece o alerta «Carregamento concluído»; ambos são detecção local e podem ser desativados
- **Alertas de rede**: aviso ao desconectar / reconectar a rede (detecção local do estado da rede, desativável)
- **Novos widgets**: espaço livre em disco (disco do sistema), status do método de entrada (chinês / inglês + nome do método de entrada)
- **Calendário lunar e termos solares**: o widget de data pode exibir adicionalmente a data do calendário lunar e os termos solares (ativado por padrão, desativável nas configurações)
- **Widget de alternância rápida**: WiFi / Bluetooth / modo noturno / mudo com um clique (via API local, sem internet; estado do rádio em cache por 2 segundos para evitar custo)
- **Crachá da fonte de reprodução**: o widget de mídia mostra a fonte de reprodução atual (Spotify / Cider / NetEase / QQ Music etc.) para você saber de qual player vem
- **Letras aprimoradas**: alternância de mostrar/ocultar tradução das letras e botão «Copiar linha atual» para copiar a letra atual com um clique
- **Personalização de ícones dos widgets**: cada widget pode ter um ícone próprio (ícone MDL2 ou Emoji); sem configuração, usa o glifo padrão
- **Correção do salto de escala das letras**: removida a vibração causada pelo deslocamento do rebote palavra por palavra, que fazia as letras «aumentarem e diminuírem»; o tamanho da fonte / opacidade da linha ativa no estado expandido agora tem transição suave de 300ms — rolagem mais suave e estável

## WinIsland 1.1.0 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades
- **Indicador temporário de volume / mudo na Ilha**: ao alterar o volume do sistema ou ativar/desativar o mudo, a Ilha mostra brevemente um indicador de volume (duração ajustável, desativável nas configurações)
- **Copiar / mover arquivos na Ilha**: ao detectar que o Explorador de Arquivos está copiando / movendo arquivos, a Ilha mostra «Copiando arquivos…» (identificação apenas pelo título da janela, desativável)
- **Progresso de download na Ilha**: detecção de arquivos temporários do navegador (.crdownload / .part / .download etc.) na pasta de downloads com exibição de «Baixando N arquivos» (desativado por padrão, ativável nas configurações)
- **Cápsula de mesclagem «Em uso»**: Configurações → Widgets pode ser ativada (desativada por padrão) para mesclar «microfone / câmera / em reunião / gravação de tela» em uma única cápsula de status «Em uso · …»; você pode escolher quais widgets participam, e os mesclados não aparecem mais separadamente
- **Pomodoro aprimorado**: clicar no widget Pomodoro na Ilha pausa / retoma o temporizador
- **Indicador temporário de captura / gravação de tela**: ao capturar a tela ou iniciar uma gravação, a Ilha mostra temporariamente o indicador correspondente (funciona mesmo com a Ilha oculta)
- **Rebote da iluminação palavra por palavra do karaokê**: cada linha de letras é iluminada suavemente do primeiro caractere, com um leve rebote — mais fluida e natural
- Otimização da arquitetura interna de polling: eventos temporários (volume / cópia / download / captura etc.) também disparam no estado oculto
## WinIsland 1.0.9 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades
- **Novos widgets**: uso de GPU, microfone / câmera em uso, contagem regressiva de feriados, em reunião; o widget de rede pode mostrar um mini gráfico dos últimos 32 segundos
- **Ações rápidas de clique duplo**: Configurações → Geral permite definir a ação de clique duplo da Ilha como "Reproduzir / Pausar", "Abrir Configurações" ou "Nenhuma ação"
- **Assistente de mudo em reuniões**: detecta janelas de reunião (Teams / Zoom / Tencent Meeting / DingTalk / Feishu / Webex / Slack / Discord / Google Meet) e ativa automaticamente o Não perturbe durante reuniões (heurística puramente local)
- **Avisos de gravação de tela / captura de tela**: avisos de captura do PrintScreen + detecção de software de gravação (OBS / Bandicam / Xbox Game Bar etc.)
- **Lembretes de eventos do calendário (.ics)**: analisa arquivos iCalendar locais e mostra um banner quando um evento começa (pode ser até N minutos antes); puramente local
- **Alertas de assinatura RSS**: consulta RSS 2.0 / Atom e mostra um banner para novas entradas
- **Alertas de e-mail (POP3)**: lê apenas os cabeçalhos do e-mail e mostra um banner para novas mensagens; recomenda-se usar um código de autorização
- **Inicializador rápido (estilo Spotlight)**: pressione `Ctrl+Space` para pesquisar aplicativos / digitar um URL para abrir
- **Painel de histórico da área de transferência**: `Ctrl+Alt+V` abre uma janela independente; clique para copiar de volta para a área de transferência
- **Regras (automação)**: condições (sempre / não reproduzindo / reproduzindo / intervalo de tempo / programa de mídia específico) × ações (ocultar / recolher à força / mostrar à força)
- **Island API v3**: imagens (data URI / http), progresso dinâmico (from/to/duration avança automaticamente), renovação por heartbeat (heartbeat_seconds), atualizações parciais PATCH, canal WebSocket (/v3/ws)
- **Aparência**: 18 predefinições de temas, cor de fundo personalizada, 4 skins de animação, modo de baixo consumo
- A página de configurações agora usa o estilo System Settings do macOS (navegação esquerda + conteúdo à direita), e todas as alterações têm efeito imediato

- **Corrigido texto preto no modo escuro**: unificada a vinculação da cor de primeiro plano de todos os modelos de controle personalizados na interface de configurações (botões / caixas de seleção / caixas de entrada / menus suspensos / itens de menu suspenso / guias / navegação esquerda etc.) e adicionada uma varredura de fallback em tempo de execução — opções individuais (idioma da interface, ação de clique duplo e outros menus suspensos) não mostram mais texto preto ilegível no modo escuro, e o modo claro restaura automaticamente o texto escuro
- **Corrigido que a janela de configurações não abria**: removidas linhas duplicadas de cor de primeiro plano XAML que causavam falhas de carregamento BAML
- **Otimização de desempenho da animação**: o efeito palavra por palavra do karaokê reutiliza objetos Run (eliminando o layout quadro a quadro), com uma Storyboard estável a 60fps e atualização em lote do log, para animações mais suaves
- **Removido o ponto vermelho de notificação nos widgets** (a pedido dos usuários)

## WinIsland 1.0.8 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades

**Redesenho completo da interface de configurações**
- Layout semelhante às configurações do sistema macOS: lista de navegação esquerda + área de conteúdo à direita, com 13 categorias (Geral / Aparência / Widgets / Mídia / Exibição de informações de mídia / Letras / Cider / Island API / Ferramentas de produtividade / Atualizações / Sobre / Notificações / Regras)
- Todas as cores de texto das configurações se adaptam automaticamente nos modos escuro / claro: texto branco no modo escuro, texto preto no modo claro — sem mais problemas de legibilidade
- Texto da navegação esquerda alterado para branco de alto contraste, removida a divisória branca à direita e fortalecidos os feedbacks de foco e seleção

**Reprodução de mídia**
- Novo mini player: uma janela flutuante independente mostrando capa do álbum / título da música / artista / barra de progresso e controles de reprodução; livremente arrastável com posição memorizada, aparece / some automaticamente com a reprodução de mídia (pode ser ativado nas configurações)
- Novo recurso de troca de dispositivo de saída de áudio: Configurações → Mídia pode enumerar e trocar o dispositivo de reprodução padrão do sistema (recomenda-se reiniciar o player após a troca para que tenha efeito)
- Camada de fonte do player aprimorada: suporta enumerar todas as sessões de mídia SMTC e sessões do Cider, além de trocar a fonte de mídia

**Letras**
- Novas letras bilíngues: linhas de tradução com carimbos de tempo adjacentes são mescladas automaticamente (pode ser desativado nas configurações)

**Aparência e animações**
- Novas skins de animação: 4 estilos (mola iOS (padrão) / mola suave / rebote elástico / fade simples), com easing não linear para expandir / recolher
- Novo modo de baixo consumo: reduz a taxa de quadros da renderização da ondulação e simplifica as animações quando ocioso para economizar mais energia

**Atalhos globais**
- 5 combinações de teclas personalizáveis: mostrar / ocultar, reproduzir / pausar, faixa anterior, faixa seguinte, expandir / recolher
- Suporta Ctrl / Alt / Shift / Win + letras, números, F1–F24, teclas de seta

**Mecanismo de regras inteligente (Configurações → Regras)**
- Controla automaticamente a exibição da Dynamic Island com base em condições: sempre ativa / quando nenhuma mídia está reproduzindo / quando a mídia está reproduzindo / durante um intervalo de tempo especificado / quando um programa de mídia especificado está reproduzindo
- Ações: ocultar a Dynamic Island / recolher à força / mostrar à força; prioridade: ocultar > recolher > mostrar à força

**Notificações**
- Suporte a histórico de notificações: marcadores de ponto vermelho não lido, marcar tudo como lido, excluir itens individuais, clicar em um item para abrir o aplicativo de origem, limpar histórico
- Novo dobramento de notificações: notificações duplicadas com a mesma origem e título reutilizam o mesmo banner e acumulam uma contagem
- Nova lista de permissões do Não perturbe: origens na lista de permissões (nomes de exe separados por vírgula) não são afetadas pelo Não perturbe e continuam mostrando banners normalmente
- Removido o distintivo de ponto vermelho não lido na Dynamic Island

**Ferramentas de produtividade**
- Mostra um aviso "Copiado" ao copiar texto
- Reconhece automaticamente códigos de verificação SMS e os destaca
- Cópias de texto longo mostram uma animação de progresso (estimada pelo comprimento; o resultado é mostrado ao final)

**Widgets**
- Novo widget de contagem regressiva de feriados: inclui uma tabela de feriados 2026–2027 (Ano Novo / Festival da Primavera / Qingming / Dia do Trabalho / Festival do Barco-Dragão / Festival do Meio do Outono / Dia Nacional), mostrando "Faltam N dias para XX" ou "Hoje é XX"; pode ser ativado nas configurações de widgets

**Island API v2**
- Novos campos: subtítulo, tipo (info / sucesso / aviso / erro), prioridade (alta / normal / baixa), cor de destaque (cor de destaque personalizada), clique (callback de clique no cartão inteiro)
- Fila de envio: vários envios são ordenados por prioridade alta → baixa, primeiro a entrar, primeiro a sair; enviar novamente com o mesmo id mantém a posição original na fila e o tempo de expiração
- A resposta do POST agora inclui um campo de posição
- Novos scripts de exemplo executáveis em docs/sdk-examples/ (push.bat / pull.bat / push.ps1 / push.py / pull.py)

**Correções**
- Corrigido que a janela de configurações não abria / não iniciava ao clicar duas vezes (adicionadas verificações nulas e inicialização explícita para navegação e troca de guias)
- Adicionados 69 testes automatizados (Island API, dobramento e lista de permissões de notificações, mecanismo de regras, reconhecimento de código de verificação, análise de letras LRC), todos passando

### Recursos
- Versão portátil Windows x64 / arm64 (arquivo único autônomo, executa sem instalação)
- Instalador universal Windows (Inno Setup, suporta x64 e ARM64, instalação automática por arquitetura)

> A versão portátil é um arquivo exe independente; o pacote ZIP não é mais fornecido.

## WinIsland 1.0.7 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades
- A "ondulação sonora" foi atualizada para **seguir o ritmo da música**: captura em tempo real o áudio atualmente reproduzido no sistema via WASAPI loopback — as ondas ficam mais altas nas batidas fortes e mais baixas quando silencioso, não é mais uma barra de volume fixa
- **Renderização contínua a 60fps**: suavização exponencial com 25ms de ataque / 140ms de liberação para um movimento de onda fluido e natural, sem engasgos
- Novas configurações (Configurações → Aparência → Ondulação sonora): alternância de seguir o ritmo da música, sensibilidade 0.2–1.0, altura da ondulação 0.4–0.6; as alterações têm efeito imediato
- Quando não há dispositivo de áudio / o serviço de áudio está anormal, ele volta automaticamente para a simulação de batida e tenta novamente a captura em tempo real a cada 8 segundos — sem travamentos, sem acúmulo de threads

## WinIsland 1.0.6 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades
- 6 novos widgets da Dynamic Island: volume, indicadores de teclado (CapsLock), área de transferência, tarefas, Pomodoro, agenda — todos suportam caixas de seleção de duas colunas "sem música / com música" e reordenação por arrastar
- Nova página de configurações "Ferramentas de produtividade": histórico da área de transferência, timer Pomodoro, lista de tarefas, lembretes de agenda
- Nova "Ondulação sonora": durante a reprodução de mídia, a área à esquerda dos botões de controle vibra em tempo real com o volume do sistema (ativável em Configurações → Aparência)
- 7 novos temas predefinidos: Padrão / Oceano / Floresta / Pôr do sol / Neon / Monocromático / Roxo uva
- Personalização da aparência: fonte personalizada, escala do tamanho da fonte (0.8–1.4), raio dos cantos da cápsula (16–40), cor de fundo expandida extraída da capa do álbum, distintivo de notificação não lida
- Novidades no menu da bandeja: modo Não perturbe (manual / silenciar notificações automaticamente por período), verificar atualizações, ver logs
- Páginas de configurações modernizadas (cantos arredondados + vidro líquido); as alterações de opções têm efeito imediato, sem necessidade de salvar manualmente
- Nova verificação de atualizações (verificação manual pela bandeja / configurações, verificação automática opcional, desativada por padrão)

### Recursos
- Versão portátil Windows x64 / arm64 (arquivo único autônomo, executa sem instalação)
- Instalador universal Windows (Inno Setup, suporta x64 e ARM64, instalação automática por arquitetura)

> A versão portátil é um arquivo exe independente; o pacote ZIP não é mais fornecido.

## WinIsland 1.0.5 (Versão estável)

Um widget moderno e multifuncional de Dynamic Island para Windows.

### Novidades
- Nova "Island API": outros softwares podem enviar informações para a Dynamic Island por meio de uma interface HTTP local (semelhante às integrações de aplicativos de terceiros com a Dynamic Island do iOS); **documentação para desenvolvedores em docs/IslandAPI.md**
  - `POST /v1/island/push` enviar / atualizar · `DELETE /v1/island/push/{id}` remover · `GET /v1/island/active` consultar · `GET /v1/health`
  - Suporta ícones, título, corpo, progresso, botões (abrir link / iniciar programa) e duração de exibição personalizada por item
  - A página de configurações oferece ativar / porta / Token opcional / duração padrão global
- Os cartões da Ilha são exibidos em uma única linha no modo compacto, não cobrem outros widgets e **não afetam as dimensões da Dynamic Island** (tamanho constante nos modos automático / manual)
- Tamanho "ajuste automático": adapta-se ao conteúdo; arrastar manualmente o controle deslizante desativa automaticamente a opção automática correspondente
- O conteúdo expandido suporta rolagem com a roda do mouse (barra de rolagem oculta)
- Alinhamento vertical uniforme dos widgets; correções de layout / fonte na inicialização (PerMonitorV2 forçado, tamanho correto desde o início)
- A reprodução de mídia não exibe mais uma notificação "Reproduzindo agora"

- Correção: o cartão voltava ao tamanho compacto cerca de 1 a 2 segundos após expandir, causando uma tela totalmente preta
  - O conteúdo expandido agora faz cross-fade com a linha compacta sobreposta, sem fundo aparecendo durante a animação
  - Após a conclusão da animação de expandir / recolher, o tamanho final do cartão é gravado explicitamente, mantendo o estado expandido estável sem encolher de volta
  - Também corrigida a tela preta ao clicar em botões da Ilha de terceiros no estado expandido

### Recursos
- Versão portátil Windows x64 / arm64 (arquivo único autônomo, executa sem instalação)
- Instalador universal Windows (Inno Setup, suporta x64 e ARM64, instalação automática por arquitetura)

> A versão portátil é um arquivo exe independente; o pacote ZIP não é mais fornecido.

---


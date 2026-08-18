## WinIsland 1.0.4（正式版 / Stable）

一款现代化、多功能的 Windows 灵动岛组件。A modern, multi-functional Dynamic Island widget for Windows.

### 更新内容
- 新增「单行模式」：设置 → 外观 可开启，紧凑态所有组件（歌曲信息、时间、天气、日期、CPU、内存、网络、电量等）以一行显示
  - 单行模式下歌曲信息只显示「封面 + 歌名 - 歌手」，歌词与进度条在展开卡片中仍完整显示
- 修复：组件顺序拖动后界面不刷新的问题（返回新 List 实例触发 ItemsSource 更新）
- 修复：媒体程序排序同样不刷新的问题

### 资产 Assets
- Windows x64 / arm64 便携版（单文件自包含，免安装直接运行）
- Windows 通用安装包（Inno Setup，同时支持 x64 与 ARM64，自动按架构安装）

> 便携版为独立 exe 文件，不再提供 ZIP 压缩包。

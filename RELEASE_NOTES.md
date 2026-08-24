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

### 资产 Assets
- Windows x64 / arm64 便携版（单文件自包含，免安装直接运行）
- Windows 通用安装包（Inno Setup，同时支持 x64 与 ARM64，自动按架构安装）

> 便携版为独立 exe 文件，不再提供 ZIP 压缩包。

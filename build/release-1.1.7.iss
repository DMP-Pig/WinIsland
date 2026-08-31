; WinIsland - Universal Inno Setup installer for the 1.1.7 stable release (x64 + ARM64)
; Installs the matching binary for the current architecture automatically.
#define MyAppName "WinIsland"
#define MyAppVersion "1.1.7"
#define MyAppPublisher "WinIsland"
#define MyAppExeName "WinIsland.exe"

[Setup]
AppId={{D8E63E7C-4B2A-4F5E-9C4A-2F6B0C1D9E3A}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
UninstallDisplayIcon={app}\{#MyAppExeName}
OutputDir=..\..\releases\1.1.7
OutputBaseFilename=WinIsland-Setup-{#MyAppVersion}
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64compatible arm64
ArchitecturesInstallIn64BitMode=x64compatible arm64
PrivilegesRequired=lowest

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"
Name: "autostart"; Description: "Start WinIsland automatically with Windows"; GroupDescription: "Startup:"

[Files]
Source: "..\..\releases\1.1.7\win-x64\WinIsland-1.1.7-win-x64.exe"; DestDir: "{app}"; DestName: "WinIsland.exe"; Flags: ignoreversion; Check: IsX64OS
Source: "..\..\releases\1.1.7\win-arm64\WinIsland-1.1.7-win-arm64.exe"; DestDir: "{app}"; DestName: "WinIsland.exe"; Flags: ignoreversion; Check: IsARM64

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#MyAppName}}"; Flags: nowait postinstall skipifsilent

[Registry]
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "WinIsland"; ValueData: """{app}\{#MyAppExeName}"""; Flags: uninsdeletevalue; Tasks: autostart

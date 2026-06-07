<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

<br />

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · QQ Group: 467608841 / 233840761

<br />

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## Language

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## Features

- Typed key-value storage: `bool`, `int`, `float`, `string`, serializable objects (via JSON)
- Two built-in storage backends:
  - **PlayerPrefsSettingHelper** (default) — uses Unity PlayerPrefs, with platform adapters for Douyin, WeChat, Kuaishou, and Alipay mini-games
  - **DefaultSettingHelper** — file-based binary storage at `Application.persistentDataPath`
- Pluggable helper architecture — implement `ISettingHelper` for custom backends
- Auto-load on start, auto-save on shutdown
- Safe parsing with `TryParse` — corrupted values return defaults with a warning log instead of crashing

## Architecture

```
SettingComponent (MonoBehaviour)
  └─ SettingManager (ISettingManager)
       └─ ISettingHelper
            ├─ PlayerPrefsSettingHelper (default)
            └─ DefaultSettingHelper
```

## Quick Start

### Installation

Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:

```json
{
  "scopedRegistries": [
    {
      "name": "GameFrameX",
      "url": "https://gameframex.upm.alianblank.uk",
      "scopes": [
        "com.gameframex"
      ]
    }
  ]
}
```

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity": "1.1.1",
    "com.gameframex.unity.setting": "1.5.1"
  }
}
```

`scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

### Basic Usage

```csharp
// The SettingComponent is automatically available after adding to a GameObject.
// Access it via GameFramework entry or GetComponent.

SettingComponent setting = ...;

// Write settings
setting.SetBool("FullScreen", true);
setting.SetInt("ResolutionWidth", 1920);
setting.SetFloat("Volume", 0.8f);
setting.SetString("PlayerName", "PlayerOne");

// Persist to storage
setting.Save();
```

### Read Settings

```csharp
// With default fallback (returns default if key is missing or value is corrupted)
float volume = setting.GetFloat("Volume", 0.5f);

// Check existence
if (setting.HasSetting("PlayerName"))
{
    string name = setting.GetString("PlayerName");
}
```

### Remove Settings

```csharp
setting.RemoveSetting("PlayerName");
setting.RemoveAllSettings();
```

### Switch Storage Backend

In the Inspector for `SettingComponent`, change **Setting Helper Type Name** to:

- `GameFrameX.Setting.Runtime.PlayerPrefsSettingHelper` (default)
- `GameFrameX.Setting.Runtime.DefaultSettingHelper` (file-based)

Or assign a custom `ISettingHelper` via the **Custom Setting Helper** field.

## Supported Platforms

| Backend | Standard Unity | Douyin | WeChat | Kuaishou | Alipay |
|---------|:-:|:-:|:-:|:-:|:-:|
| PlayerPrefsSettingHelper | PlayerPrefs | TTStorage | WX SDK | KS SDK | Alipay SDK |
| DefaultSettingHelper | File I/O | File I/O | File I/O | File I/O | File I/O |

## API Reference

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Count` | `int` | Number of stored settings (-1 for PlayerPrefs backend) |

### Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `Load()` | `bool` | Load settings from storage |
| `Save()` | `bool` | Save settings to storage |
| `HasSetting(name)` | `bool` | Check if a setting exists |
| `RemoveSetting(name)` | `bool` | Remove a single setting |
| `RemoveAllSettings()` | `void` | Clear all settings |
| `GetAllSettingNames()` | `string[]` | Get all setting key names |
| `GetBool` / `SetBool` | `bool` | Boolean accessors |
| `GetInt` / `SetInt` | `int` | Integer accessors |
| `GetFloat` / `SetFloat` | `float` | Float accessors (invariant culture) |
| `GetString` / `SetString` | `string` | String accessors |
| `GetObject<T>` / `SetObject<T>` | `T` | JSON-serialized object accessors |

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for release history.

## License

Dual-licensed under [MIT](LICENSE.md) and [Apache-2.0](LICENSE.md).

<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />
</div>

# Game Frame X Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · [QQ Group](https://qm.qq.com/q/5U9Fvebw) · [Language](#language)

---

## Language

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## Project Overview

GameFrameX Setting Configuration Component.

**Setting Configuration Component (Setting Component)** - Manages game configuration information, allowing you to save and retrieve various types of configuration data.

## Quick Start

### Installation (choose one)

1. Add the following to the `dependencies` section of `manifest.json`:
   ```json
   {"com.gameframex.unity.setting": "https://github.com/AlianBlank/com.gameframex.unity.setting.git"}
   ```

2. Add via Unity's `Package Manager` using `Git URL`: https://github.com/AlianBlank/com.gameframex.unity.setting.git

3. Download the repository directly and place it in the Unity project's `Packages` directory. It will be auto-loaded.

## Usage Examples

### Save and Load Settings

```csharp
// Get SettingComponent instance
SettingComponent settingComponent = ...;

// Modify settings
settingComponent.SetBool("IsFullScreen", true);
settingComponent.SetInt("ResolutionWidth", 1920);
settingComponent.SetFloat("Volume", 0.8f);
settingComponent.SetString("PlayerName", "PlayerOne");

// Save settings
settingComponent.Save();
```

### Query and Get Settings

```csharp
// Check if a setting exists
bool hasVolumeSetting = settingComponent.HasSetting("Volume");

// Get setting value with default
float volume = settingComponent.GetFloat("Volume", 0.5f); // Returns 0.5f if not found
```

### Remove Settings

```csharp
// Remove a specific setting
settingComponent.RemoveSetting("PlayerName");

// Remove all settings
settingComponent.RemoveAllSettings();
```

## API Reference

### Properties

- **Count** - Gets the number of game configuration items.

### Methods

| Method | Description |
|--------|-------------|
| `Save()` | Saves all current game configuration items. |
| `GetAllSettingNames()` | Gets all game configuration item names. |
| `HasSetting(string)` | Checks if a configuration item exists. |
| `RemoveSetting(string)` | Removes a specific configuration item. |
| `RemoveAllSettings()` | Removes all configuration items. |
| `GetBool/SetBool` | Get/Set boolean configuration. |
| `GetInt/SetInt` | Get/Set integer configuration. |
| `GetFloat/SetFloat` | Get/Set float configuration. |
| `GetString/SetString` | Get/Set string configuration. |
| `GetObject/SetObject` | Get/Set object configuration. |

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for details.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

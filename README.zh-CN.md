<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />
</div>

# Game Frame X Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> 独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · [QQ群](https://qm.qq.com/q/5U9Fvebw) · [语言](#语言)

---

## 语言

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## 项目简介

GameFrameX 的 Setting 配置信息组件。

**Setting 配置信息组件 (Setting Component)** - 负责管理游戏的配置信息，允许您保存和获取各种类型的配置数据。

## 快速开始

### 使用方式（任选其一）

1. 直接在 `manifest.json` 的文件中的 `dependencies` 节点下添加以下内容
   ```json
   {"com.gameframex.unity.setting": "https://github.com/AlianBlank/com.gameframex.unity.setting.git"}
   ```

2. 在 Unity 的 `Packages Manager` 中使用 `Git URL` 的方式添加库，地址为：https://github.com/AlianBlank/com.gameframex.unity.setting.git

3. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下。会自动加载识别。

## 使用示例

### 保存和加载设置

```csharp
// 获取 SettingComponent 实例
SettingComponent settingComponent = ...;

// 修改设置
settingComponent.SetBool("IsFullScreen", true);
settingComponent.SetInt("ResolutionWidth", 1920);
settingComponent.SetFloat("Volume", 0.8f);
settingComponent.SetString("PlayerName", "PlayerOne");

// 保存修改后的设置
settingComponent.Save();
```

### 查询和获取设置值

```csharp
// 检查是否存在某个设置
bool hasVolumeSetting = settingComponent.HasSetting("Volume");

// 获取设置项的值（如果不存在则返回默认值 0.5f）
float volume = settingComponent.GetFloat("Volume", 0.5f);
```

### 删除设置

```csharp
// 移除某个设置
settingComponent.RemoveSetting("PlayerName");

// 移除所有设置
settingComponent.RemoveAllSettings();
```

## API 参考

### 属性

- **Count** - 获取游戏配置项的数量。

### 方法

| 方法 | 说明 |
|------|------|
| `Save()` | 保存当前所有游戏配置项。 |
| `GetAllSettingNames()` | 获取所有游戏配置项的名称。 |
| `HasSetting(string)` | 检查是否存在指定名称的配置项。 |
| `RemoveSetting(string)` | 移除指定的配置项。 |
| `RemoveAllSettings()` | 清空所有配置项。 |
| `GetBool/SetBool` | 获取/设置布尔类型配置。 |
| `GetInt/SetInt` | 获取/设置整数类型配置。 |
| `GetFloat/SetFloat` | 获取/设置浮点数类型配置。 |
| `GetString/SetString` | 获取/设置字符串类型配置。 |
| `GetObject/SetObject` | 获取/设置对象类型配置。 |

## 更新日志

详见 [CHANGELOG.md](CHANGELOG.md)。

## 开源协议

本项目基于 MIT 协议开源，详见 [LICENSE.md](LICENSE.md) 文件。

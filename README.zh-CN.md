<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>
## 语言

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## 功能特性

- 类型化键值存储：`bool`、`int`、`float`、`string`、可序列化对象（JSON）
- 两种内置存储后端：
  - **PlayerPrefsSettingHelper**（默认）— 使用 Unity PlayerPrefs，适配抖音、微信、快手、支付宝小游戏平台
  - **DefaultSettingHelper** — 基于 `Application.persistentDataPath` 的文件二进制存储
- 可插拔 Helper 架构 — 实现 `ISettingHelper` 接口即可自定义后端
- 启动时自动加载，关闭时自动保存
- 安全解析（`TryParse`）— 数据损坏时返回默认值并记录警告，不会崩溃

## 架构概览

```
SettingComponent (MonoBehaviour)
  └─ SettingManager (ISettingManager)
       └─ ISettingHelper
            ├─ PlayerPrefsSettingHelper (默认)
            └─ DefaultSettingHelper
```

## 快速开始

### 安装

编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

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

然后在 `dependencies` 中添加对应包：

```json
{
  "dependencies": {
    "com.gameframex.unity": "1.1.1",
    "com.gameframex.unity.setting": "1.5.1"
  }
}
```

`scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

### 基本使用

```csharp
// SettingComponent 添加到 GameObject 后自动可用。
// 通过 GameFramework 入口或 GetComponent 获取。

SettingComponent setting = ...;

// 写入设置
setting.SetBool("FullScreen", true);
setting.SetInt("ResolutionWidth", 1920);
setting.SetFloat("Volume", 0.8f);
setting.SetString("PlayerName", "PlayerOne");

// 持久化到存储
setting.Save();
```

### 读取设置

```csharp
// 带默认值回退（键不存在或值损坏时返回默认值）
float volume = setting.GetFloat("Volume", 0.5f);

// 检查是否存在
if (setting.HasSetting("PlayerName"))
{
    string name = setting.GetString("PlayerName");
}
```

### 删除设置

```csharp
setting.RemoveSetting("PlayerName");
setting.RemoveAllSettings();
```

### 切换存储后端

在 `SettingComponent` 的 Inspector 面板中，修改 **Setting Helper Type Name**：

- `GameFrameX.Setting.Runtime.PlayerPrefsSettingHelper`（默认）
- `GameFrameX.Setting.Runtime.DefaultSettingHelper`（文件存储）

也可以通过 **Custom Setting Helper** 字段指定自定义的 `ISettingHelper` 实现。

## 平台支持

| 后端 | 标准 Unity | 抖音 | 微信 | 快手 | 支付宝 |
|------|:-:|:-:|:-:|:-:|:-:|
| PlayerPrefsSettingHelper | PlayerPrefs | TTStorage | WX SDK | KS SDK | Alipay SDK |
| DefaultSettingHelper | 文件 I/O | 文件 I/O | 文件 I/O | 文件 I/O | 文件 I/O |

## API 参考

### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Count` | `int` | 已存储的设置数量（PlayerPrefs 后端返回 -1） |

### 方法

| 方法 | 返回值 | 说明 |
|------|--------|------|
| `Load()` | `bool` | 从存储加载设置 |
| `Save()` | `bool` | 保存设置到存储 |
| `HasSetting(name)` | `bool` | 检查设置是否存在 |
| `RemoveSetting(name)` | `bool` | 移除单个设置 |
| `RemoveAllSettings()` | `void` | 清空所有设置 |
| `GetAllSettingNames()` | `string[]` | 获取所有设置的键名 |
| `GetBool` / `SetBool` | `bool` | 布尔类型存取 |
| `GetInt` / `SetInt` | `int` | 整数类型存取 |
| `GetFloat` / `SetFloat` | `float` | 浮点数类型存取（不变区域性） |
| `GetString` / `SetString` | `string` | 字符串类型存取 |
| `GetObject<T>` / `SetObject<T>` | `T` | JSON 序列化对象存取 |

## 更新日志

详见 [CHANGELOG.md](CHANGELOG.md)。

## 开源协议

双重许可：[MIT](LICENSE.md) 和 [Apache-2.0](LICENSE.md)。

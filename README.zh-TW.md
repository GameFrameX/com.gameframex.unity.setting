<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 語言

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## 功能特性

- 型別化鍵值儲存：`bool`、`int`、`float`、`string`、可序列化物件（JSON）
- 兩種內建儲存後端：
  - **PlayerPrefsSettingHelper**（預設）— 使用 Unity PlayerPrefs，適配抖音、微信、快手、支付寶小遊戲平台
  - **DefaultSettingHelper** — 基於 `Application.persistentDataPath` 的檔案二進位儲存
- 可插拔 Helper 架構 — 實作 `ISettingHelper` 介面即可自訂後端
- 啟動時自動載入，關閉時自動儲存
- 安全解析（`TryParse`）— 資料損壞時回傳預設值並記錄警告，不會崩潰

## 架構概覽

```
SettingComponent (MonoBehaviour)
  └─ SettingManager (ISettingManager)
       └─ ISettingHelper
            ├─ PlayerPrefsSettingHelper (預設)
            └─ DefaultSettingHelper
```

## 快速開始

### 安裝

編輯 Unity 專案的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

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

然後在 `dependencies` 中添加對應套件：

```json
{
  "dependencies": {
    "com.gameframex.unity": "1.1.1",
    "com.gameframex.unity.setting": "1.5.1"
  }
}
```

`scopes` 控制哪些套件透過此註冊表解析。只有以 `com.gameframex` 開頭的套件才會從這個註冊表取得。

### 基本使用

```csharp
// SettingComponent 添加到 GameObject 後自動可用。
// 透過 GameFramework 入口或 GetComponent 取得。

SettingComponent setting = ...;

// 寫入設定
setting.SetBool("FullScreen", true);
setting.SetInt("ResolutionWidth", 1920);
setting.SetFloat("Volume", 0.8f);
setting.SetString("PlayerName", "PlayerOne");

// 持久化到儲存
setting.Save();
```

### 讀取設定

```csharp
// 帶預設值回退（鍵不存在或值損壞時回傳預設值）
float volume = setting.GetFloat("Volume", 0.5f);

// 檢查是否存在
if (setting.HasSetting("PlayerName"))
{
    string name = setting.GetString("PlayerName");
}
```

### 刪除設定

```csharp
setting.RemoveSetting("PlayerName");
setting.RemoveAllSettings();
```

### 切換儲存後端

在 `SettingComponent` 的 Inspector 面板中，修改 **Setting Helper Type Name**：

- `GameFrameX.Setting.Runtime.PlayerPrefsSettingHelper`（預設）
- `GameFrameX.Setting.Runtime.DefaultSettingHelper`（檔案儲存）

也可以透過 **Custom Setting Helper** 欄位指定自訂的 `ISettingHelper` 實作。

## 平台支援

| 後端 | 標準 Unity | 抖音 | 微信 | 快手 | 支付寶 |
|------|:-:|:-:|:-:|:-:|:-:|
| PlayerPrefsSettingHelper | PlayerPrefs | TTStorage | WX SDK | KS SDK | Alipay SDK |
| DefaultSettingHelper | 檔案 I/O | 檔案 I/O | 檔案 I/O | 檔案 I/O | 檔案 I/O |

## API 參考

### 屬性

| 屬性 | 型別 | 說明 |
|------|------|------|
| `Count` | `int` | 已儲存的設定數量（PlayerPrefs 後端回傳 -1） |

### 方法

| 方法 | 回傳值 | 說明 |
|------|--------|------|
| `Load()` | `bool` | 從儲存載入設定 |
| `Save()` | `bool` | 儲存設定到儲存 |
| `HasSetting(name)` | `bool` | 檢查設定是否存在 |
| `RemoveSetting(name)` | `bool` | 移除單個設定 |
| `RemoveAllSettings()` | `void` | 清空所有設定 |
| `GetAllSettingNames()` | `string[]` | 取得所有設定的鍵名 |
| `GetBool` / `SetBool` | `bool` | 布林型別存取 |
| `GetInt` / `SetInt` | `int` | 整數型別存取 |
| `GetFloat` / `SetFloat` | `float` | 浮點數型別存取（不變文化特性） |
| `GetString` / `SetString` | `string` | 字串型別存取 |
| `GetObject<T>` / `SetObject<T>` | `T` | JSON 序列化物件存取 |

## 更新日誌

詳見 [CHANGELOG.md](CHANGELOG.md)。

## 開源協議

雙重許可：[MIT](LICENSE.md) 和 [Apache-2.0](LICENSE.md)。

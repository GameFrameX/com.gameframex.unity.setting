<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />
</div>

# Game Frame X Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> 獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · [QQ群](https://qm.qq.com/q/5U9Fvebw) · [語言](#語言)

---

## 語言

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## 項目簡介

GameFrameX 的 Setting 配置資訊組件。

**Setting 配置資訊組件 (Setting Component)** - 負責管理遊戲的配置資訊，允許您儲存和取得各種類型的配置資料。

## 快速開始

### 使用方式（任選其一）

1. 直接在 `manifest.json` 的檔案中的 `dependencies` 節點下新增以下內容
   ```json
   {"com.gameframex.unity.setting": "https://github.com/AlianBlank/com.gameframex.unity.setting.git"}
   ```

2. 在 Unity 的 `Packages Manager` 中使用 `Git URL` 的方式新增套件，地址為：https://github.com/AlianBlank/com.gameframex.unity.setting.git

3. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下。會自動載入識別。

## 使用範例

### 儲存和載入設定

```csharp
// 取得 SettingComponent 實例
SettingComponent settingComponent = ...;

// 修改設定
settingComponent.SetBool("IsFullScreen", true);
settingComponent.SetInt("ResolutionWidth", 1920);
settingComponent.SetFloat("Volume", 0.8f);
settingComponent.SetString("PlayerName", "PlayerOne");

// 儲存修改後的設定
settingComponent.Save();
```

### 查詢和取得設定值

```csharp
// 檢查是否存在某個設定
bool hasVolumeSetting = settingComponent.HasSetting("Volume");

// 取得設定項的值（如果不存在則回傳預設值 0.5f）
float volume = settingComponent.GetFloat("Volume", 0.5f);
```

### 刪除設定

```csharp
// 移除某個設定
settingComponent.RemoveSetting("PlayerName");

// 移除所有設定
settingComponent.RemoveAllSettings();
```

## API 參考

### 屬性

- **Count** - 取得遊戲配置項的數量。

### 方法

| 方法 | 說明 |
|------|------|
| `Save()` | 儲存當前所有遊戲配置項。 |
| `GetAllSettingNames()` | 取得所有遊戲配置項的名稱。 |
| `HasSetting(string)` | 檢查是否存在指定名稱的配置項。 |
| `RemoveSetting(string)` | 移除指定的配置項。 |
| `RemoveAllSettings()` | 清空所有配置項。 |
| `GetBool/SetBool` | 取得/設定布林值配置。 |
| `GetInt/SetInt` | 取得/設定整數配置。 |
| `GetFloat/SetFloat` | 取得/設定浮點數配置。 |
| `GetString/SetString` | 取得/設定字串配置。 |
| `GetObject/SetObject` | 取得/設定物件配置。 |

## 更新日誌

詳見 [CHANGELOG.md](CHANGELOG.md)。

## 開源協議

本專案基於 MIT 協議開源，詳見 [LICENSE.md](LICENSE.md) 檔案。

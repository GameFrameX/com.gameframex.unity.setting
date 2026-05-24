<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />
</div>

# Game Frame X Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · [QQグループ](https://qm.qq.com/q/5U9Fvebw) · [言語](#言語)

---

## 言語

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

---

## プロジェクト概要

GameFrameX の Setting 設定情報コンポーネント。

**Setting 設定情報コンポーネント (Setting Component)** - ゲームの設定情報を管理し、さまざまなタイプの設定データの保存と取得を可能にします。

## クイックスタート

### インストール（いずれかを選択）

1. `manifest.json` の `dependencies` セクションに以下を追加します：
   ```json
   {"com.gameframex.unity.setting": "https://github.com/AlianBlank/com.gameframex.unity.setting.git"}
   ```

2. Unity の `Package Manager` で `Git URL` を使用して追加：https://github.com/AlianBlank/com.gameframex.unity.setting.git

3. リポジトリを直接ダウンロードして、Unity プロジェクトの `Packages` ディレクトリに配置します。自動的に読み込まれます。

## 使用例

### 設定の保存と読み込み

```csharp
// SettingComponent インスタンスを取得
SettingComponent settingComponent = ...;

// 設定を変更
settingComponent.SetBool("IsFullScreen", true);
settingComponent.SetInt("ResolutionWidth", 1920);
settingComponent.SetFloat("Volume", 0.8f);
settingComponent.SetString("PlayerName", "PlayerOne");

// 設定を保存
settingComponent.Save();
```

### 設定の確認と取得

```csharp
// 設定が存在するか確認
bool hasVolumeSetting = settingComponent.HasSetting("Volume");

// デフォルト値付きで設定値を取得
float volume = settingComponent.GetFloat("Volume", 0.5f); // 見つからない場合は 0.5f を返す
```

### 設定の削除

```csharp
// 特定の設定を削除
settingComponent.RemoveSetting("PlayerName");

// すべての設定を削除
settingComponent.RemoveAllSettings();
```

## API リファレンス

### プロパティ

- **Count** - ゲーム設定項目の数を取得します。

### メソッド

| メソッド | 説明 |
|--------|------|
| `Save()` | 現在のすべてのゲーム設定項目を保存します。 |
| `GetAllSettingNames()` | すべてのゲーム設定項目名を取得します。 |
| `HasSetting(string)` | 指定した名前の設定項目が存在するか確認します。 |
| `RemoveSetting(string)` | 指定した設定項目を削除します。 |
| `RemoveAllSettings()` | すべての設定項目を削除します。 |
| `GetBool/SetBool` | bool型設定の取得/設定。 |
| `GetInt/SetInt` | int型設定の取得/設定。 |
| `GetFloat/SetFloat` | float型設定の取得/設定。 |
| `GetString/SetString` | string型設定の取得/設定。 |
| `GetObject/SetObject` | object型設定の取得/設定。 |

## 変更履歴

詳細は [CHANGELOG.md](CHANGELOG.md) をご覧ください。

## ライセンス

このプロジェクトは MIT ライセンスの下で公開されています。詳細は [LICENSE.md](LICENSE.md) ファイルをご覧ください。

<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>

## 言語

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

---

## 機能

- 型付きキーバリューストレージ：`bool`、`int`、`float`、`string`、シリアライズ可能オブジェクト（JSON）
- 2つの内蔵ストレージバックエンド：
  - **PlayerPrefsSettingHelper**（デフォルト）— Unity PlayerPrefs を使用。Douyin、WeChat、Kuaishou、Alipay ミニゲーム向けプラットフォームアダプター搭載
  - **DefaultSettingHelper** — `Application.persistentDataPath` によるファイルベースのバイナリストレージ
- プラグイン可能な Helper アーキテクチャ — `ISettingHelper` を実装してカスタムバックエンドを作成可能
- 起動時に自動ロード、終了時に自動セーブ
- 安全なパース（`TryParse`）— 破損した値はデフォルトを返し警告ログを出力、クラッシュなし

## アーキテクチャ

```
SettingComponent (MonoBehaviour)
  └─ SettingManager (ISettingManager)
       └─ ISettingHelper
            ├─ PlayerPrefsSettingHelper (デフォルト)
            └─ DefaultSettingHelper
```

## クイックスタート

### インストール

Unity プロジェクトの `Packages/manifest.json` を編集し、`scopedRegistries` セクションを追加してください：

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

次に `dependencies` にパッケージを追加します：

```json
{
  "dependencies": {
    "com.gameframex.unity": "1.1.1",
    "com.gameframex.unity.setting": "1.5.1"
  }
}
```

`scopes` は、どのパッケージをこのレジストリから解決するかを制御します。`com.gameframex` で始まるパッケージのみがこのレジストリから取得されます。

### 基本的な使い方

```csharp
// SettingComponent は GameObject に追加後、自動的に利用可能です。
// GameFramework エントリまたは GetComponent で取得します。

SettingComponent setting = ...;

// 設定の書き込み
setting.SetBool("FullScreen", true);
setting.SetInt("ResolutionWidth", 1920);
setting.SetFloat("Volume", 0.8f);
setting.SetString("PlayerName", "PlayerOne");

// ストレージに保存
setting.Save();
```

### 設定の読み取り

```csharp
// デフォルトフォールバック付き（キーが存在しない、または値が破損している場合はデフォルトを返す）
float volume = setting.GetFloat("Volume", 0.5f);

// 存在確認
if (setting.HasSetting("PlayerName"))
{
    string name = setting.GetString("PlayerName");
}
```

### 設定の削除

```csharp
setting.RemoveSetting("PlayerName");
setting.RemoveAllSettings();
```

### ストレージバックエンドの切り替え

`SettingComponent` の Inspector で **Setting Helper Type Name** を変更：

- `GameFrameX.Setting.Runtime.PlayerPrefsSettingHelper`（デフォルト）
- `GameFrameX.Setting.Runtime.DefaultSettingHelper`（ファイルベース）

または **Custom Setting Helper** フィールドでカスタム `ISettingHelper` 実装を指定します。

## プラットフォーム対応

| バックエンド | 標準 Unity | Douyin | WeChat | Kuaishou | Alipay |
|-------------|:-:|:-:|:-:|:-:|:-:|
| PlayerPrefsSettingHelper | PlayerPrefs | TTStorage | WX SDK | KS SDK | Alipay SDK |
| DefaultSettingHelper | ファイル I/O | ファイル I/O | ファイル I/O | ファイル I/O | ファイル I/O |

## API リファレンス

### プロパティ

| プロパティ | 型 | 説明 |
|-----------|------|------|
| `Count` | `int` | 保存されている設定の数（PlayerPrefs バックエンドでは -1） |

### メソッド

| メソッド | 戻り値 | 説明 |
|---------|---------|------|
| `Load()` | `bool` | ストレージから設定を読み込む |
| `Save()` | `bool` | 設定をストレージに保存する |
| `HasSetting(name)` | `bool` | 設定が存在するか確認する |
| `RemoveSetting(name)` | `bool` | 単一の設定を削除する |
| `RemoveAllSettings()` | `void` | すべての設定をクリアする |
| `GetAllSettingNames()` | `string[]` | すべての設定キー名を取得する |
| `GetBool` / `SetBool` | `bool` | ブール型アクセサ |
| `GetInt` / `SetInt` | `int` | 整数型アクセサ |
| `GetFloat` / `SetFloat` | `float` | 浮動小数点型アクセサ（インバリアントカルチャ） |
| `GetString` / `SetString` | `string` | 文字列型アクセサ |
| `GetObject<T>` / `SetObject<T>` | `T` | JSON シリアライズオブジェクトアクセサ |

## 変更履歴

詳細は [CHANGELOG.md](CHANGELOG.md) をご覧ください。


## 依存関係

| パッケージ | 説明 |
|----------|------|
| `com.gameframex.unity` | 1.1.1 |

## ドキュメントとリソース

- [ドキュメント](https://gameframex.doc.alianblank.com)

## コミュニティとサポート

- QQグループ: 467608841 / 233840761
## ライセンス

デュアルライセンス：[MIT](LICENSE.md) および [Apache-2.0](LICENSE.md)。

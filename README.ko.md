<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />
</div>

# Game Frame X Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> 인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · [QQ 그룹](https://qm.qq.com/q/5U9Fvebw) · [언어](#언어)

---

## 언어

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

---

## 프로젝트 개요

GameFrameX의 Setting 설정 정보 컴포넌트.

**Setting 설정 정보 컴포넌트 (Setting Component)** - 게임의 설정 정보를 관리하며, 다양한 유형의 설정 데이터를 저장하고 가져올 수 있습니다.

## 빠른 시작

### 설치 (선택)

1. `manifest.json` 파일의 `dependencies` 섹션에 다음 내용을 추가합니다:
   ```json
   {"com.gameframex.unity.setting": "https://github.com/AlianBlank/com.gameframex.unity.setting.git"}
   ```

2. Unity의 `Package Manager`에서 `Git URL`을 사용하여 추가: https://github.com/AlianBlank/com.gameframex.unity.setting.git

3. 저장소를 직접 다운로드하여 Unity 프로젝트의 `Packages` 디렉토리에 배치합니다. 자동으로 로드됩니다.

## 사용 예시

### 설정 저장 및 로드

```csharp
// SettingComponent 인스턴스 가져오기
SettingComponent settingComponent = ...;

// 설정 수정
settingComponent.SetBool("IsFullScreen", true);
settingComponent.SetInt("ResolutionWidth", 1920);
settingComponent.SetFloat("Volume", 0.8f);
settingComponent.SetString("PlayerName", "PlayerOne");

// 설정 저장
settingComponent.Save();
```

### 설정 조회 및 가져오기

```csharp
// 설정이 존재하는지 확인
bool hasVolumeSetting = settingComponent.HasSetting("Volume");

// 기본값과 함께 설정 값 가져오기
float volume = settingComponent.GetFloat("Volume", 0.5f); // 없으면 0.5f 반환
```

### 설정 삭제

```csharp
// 특정 설정 제거
settingComponent.RemoveSetting("PlayerName");

// 모든 설정 제거
settingComponent.RemoveAllSettings();
```

## API 참조

### 속성

- **Count** - 게임 설정 항목 수를 가져옵니다.

### 메서드

| 메서드 | 설명 |
|--------|------|
| `Save()` | 현재 모든 게임 설정 항목을 저장합니다. |
| `GetAllSettingNames()` | 모든 게임 설정 항목 이름을 가져옵니다. |
| `HasSetting(string)` | 지정된 이름의 설정 항목이 있는지 확인합니다. |
| `RemoveSetting(string)` | 지정된 설정 항목을 제거합니다. |
| `RemoveAllSettings()` | 모든 설정 항목을 제거합니다. |
| `GetBool/SetBool` | bool 타입 설정 가져오기/설정. |
| `GetInt/SetInt` | int 타입 설정 가져오기/설정. |
| `GetFloat/SetFloat` | float 타입 설정 가져오기/설정. |
| `GetString/SetString` | string 타입 설정 가져오기/설정. |
| `GetObject/SetObject` | object 타입 설정 가져오기/설정. |

## 변경 로그

자세한 내용은 [CHANGELOG.md](CHANGELOG.md)를 참조하세요.

## 라이선스

이 프로젝트는 MIT 라이선스에 따라 배포됩니다. 자세한 내용은 [LICENSE.md](LICENSE.md) 파일을 참조하세요.

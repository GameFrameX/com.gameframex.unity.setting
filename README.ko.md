<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting)](https://github.com/GameFrameX/com.gameframex.unity.setting/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>

## 언어

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

---

## 기능

- 타입화된 키-값 저장소: `bool`, `int`, `float`, `string`, 직렬화 가능 객체 (JSON)
- 두 가지 내장 스토리지 백엔드:
  - **PlayerPrefsSettingHelper** (기본) — Unity PlayerPrefs 사용, Douyin, WeChat, Kuaishou, Alipay 미니게임 플랫폼 어댑터 포함
  - **DefaultSettingHelper** — `Application.persistentDataPath` 기반 파일 바이너리 스토리지
- 플러그인 가능한 Helper 아키텍처 — `ISettingHelper`를 구현하여 커스텀 백엔드 생성 가능
- 시작 시 자동 로드, 종료 시 자동 저장
- 안전한 파싱 (`TryParse`) — 손상된 값은 기본값을 반환하고 경고 로그 출력, 크래시 없음

## 아키텍처

```
SettingComponent (MonoBehaviour)
  └─ SettingManager (ISettingManager)
       └─ ISettingHelper
            ├─ PlayerPrefsSettingHelper (기본)
            └─ DefaultSettingHelper
```

## 빠른 시작

### 설치

다음 방법 중 하나를 선택하세요:

1. Unity 프로젝트의 `Packages/manifest.json`을 편집하여 `scopedRegistries` 섹션을 추가하세요:
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
     ],
     "dependencies": {
       "com.gameframex.unity.setting": "1.5.2"
     }
   }
   ```

   `scopes`는 이 레지스트리를 통해 어떤 패키지를 해석할지 제어합니다. `com.gameframex`로 시작하는 패키지만 이 레지스트리에서 가져옵니다.

2. `manifest.json`의 `dependencies`에 직접 추가:
   ```json
   {
      "com.gameframex.unity.setting": "https://github.com/gameframex/com.gameframex.unity.setting.git"
   }
   ```
3. Unity의 **Package Manager**에서 **Git URL**을 사용하여 추가: `https://github.com/gameframex/com.gameframex.unity.setting.git`
4. 리포지토리를 Unity 프로젝트의 `Packages` 디렉토리에 클론하세요. 자동으로 로드됩니다.
### 기본 사용법

```csharp
// SettingComponent는 GameObject에 추가 후 자동으로 사용 가능합니다.
// GameFramework 진입점 또는 GetComponent로 가져옵니다.

SettingComponent setting = ...;

// 설정 쓰기
setting.SetBool("FullScreen", true);
setting.SetInt("ResolutionWidth", 1920);
setting.SetFloat("Volume", 0.8f);
setting.SetString("PlayerName", "PlayerOne");

// 스토리지에 저장
setting.Save();
```

### 설정 읽기

```csharp
// 기본값 폴백 (키가 없거나 값이 손상된 경우 기본값 반환)
float volume = setting.GetFloat("Volume", 0.5f);

// 존재 여부 확인
if (setting.HasSetting("PlayerName"))
{
    string name = setting.GetString("PlayerName");
}
```

### 설정 삭제

```csharp
setting.RemoveSetting("PlayerName");
setting.RemoveAllSettings();
```

### 스토리지 백엔드 전환

`SettingComponent`의 Inspector에서 **Setting Helper Type Name**을 변경:

- `GameFrameX.Setting.Runtime.PlayerPrefsSettingHelper` (기본)
- `GameFrameX.Setting.Runtime.DefaultSettingHelper` (파일 기반)

또는 **Custom Setting Helper** 필드에서 커스텀 `ISettingHelper` 구현을 지정합니다.

## 플랫폼 지원

| 백엔드 | 표준 Unity | Douyin | WeChat | Kuaishou | Alipay |
|--------|:-:|:-:|:-:|:-:|:-:|
| PlayerPrefsSettingHelper | PlayerPrefs | TTStorage | WX SDK | KS SDK | Alipay SDK |
| DefaultSettingHelper | 파일 I/O | 파일 I/O | 파일 I/O | 파일 I/O | 파일 I/O |

## API 참조

### 속성

| 속성 | 타입 | 설명 |
|------|------|------|
| `Count` | `int` | 저장된 설정 수 (PlayerPrefs 백엔드에서는 -1) |

### 메서드

| 메서드 | 반환값 | 설명 |
|--------|---------|------|
| `Load()` | `bool` | 스토리지에서 설정 로드 |
| `Save()` | `bool` | 설정을 스토리지에 저장 |
| `HasSetting(name)` | `bool` | 설정 존재 여부 확인 |
| `RemoveSetting(name)` | `bool` | 단일 설정 제거 |
| `RemoveAllSettings()` | `void` | 모든 설정 초기화 |
| `GetAllSettingNames()` | `string[]` | 모든 설정 키 이름 가져오기 |
| `GetBool` / `SetBool` | `bool` | 부울 타입 접근자 |
| `GetInt` / `SetInt` | `int` | 정수 타입 접근자 |
| `GetFloat` / `SetFloat` | `float` | 부동소수점 타입 접근자 (고문화권) |
| `GetString` / `SetString` | `string` | 문자열 타입 접근자 |
| `GetObject<T>` / `SetObject<T>` | `T` | JSON 직렬화 객체 접근자 |

## 변경 로그

자세한 내용은 [CHANGELOG.md](CHANGELOG.md)를 참조하세요.


## 의존성

| 패키지 | 설명 |
|--------|------|
| `com.gameframex.unity` | 1.1.1 |

## 문서 및 자료

- [문서](https://gameframex.doc.alianblank.com)

## 커뮤니티 및 지원

- QQ 그룹: 467608841 / 233840761
## 라이선스

듀얼 라이선스: [MIT](LICENSE.md) 및 [Apache-2.0](LICENSE.md).

## [1.6.1](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.6.0...1.6.1) (2026-07-30)


### Bug Fixes

* **dependencies:** 更新 com.gameframex.unity 依赖版本至 2.5.1 ([da85a5a](https://github.com/gameframex/com.gameframex.unity.setting/commit/da85a5a2bf0da35bcb2e62b7d2ff0d150a77f052))

# [1.6.0](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.5.3...1.6.0) (2026-07-03)


### Features

* **setting:** 抽象存储后端并重构 PlayerPrefsSettingHelper ([aadfb68](https://github.com/gameframex/com.gameframex.unity.setting/commit/aadfb68a774e22c3e42724acc4da1c8fa3e8d278)), closes [#if](https://github.com/gameframex/com.gameframex.unity.setting/issues/if)
* **setting:** 注册运行时自动加载 ([dd2bcb3](https://github.com/gameframex/com.gameframex.unity.setting/commit/dd2bcb310eed0244d9ace7c44525ef336b647f6f))
* **setting:** 添加微信小游戏设置存储 adapter ([bd4b119](https://github.com/gameframex/com.gameframex.unity.setting/commit/bd4b119791ff4913d0510afe92a497090118d2fb))
* **setting:** 添加快手小游戏设置存储 adapter ([fca20d9](https://github.com/gameframex/com.gameframex.unity.setting/commit/fca20d974849f01d9c723a5cef299ffb443464f7))
* **setting:** 添加抖音小游戏设置存储 adapter ([9de8317](https://github.com/gameframex/com.gameframex.unity.setting/commit/9de83176a14658c1ce9a6c47157fda880510ebd8))
* **setting:** 添加支付宝小游戏设置存储 adapter ([8796ca0](https://github.com/gameframex/com.gameframex.unity.setting/commit/8796ca0fe6be886a60a184619b757ca688f3de8d))

## Unreleased

### Code Refactoring

* **setting:** 将 PlayerPrefsSettingHelper 小游戏存储分支抽取为平台 adapter backend，普通 Runtime assembly 不再硬依赖小游戏 SDK

## [1.5.3](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.5.2...1.5.3) (2026-06-07)


### Bug Fixes

* 补全包规范文件（LICENSE/CHANGELOG/URL 字段/unity 字段） ([ccf1a71](https://github.com/gameframex/com.gameframex.unity.setting/commit/ccf1a71f6350cb0fa84f451d70dc810175f7ab15))

## [1.5.2](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.5.1...1.5.2) (2026-05-30)


### Bug Fixes

* **setting:** 修复 DefaultSetting 数值解析与流管理 ([d440052](https://github.com/gameframex/com.gameframex.unity.setting/commit/d4400524e4ba148d185fe61861834390a0ddd7a4))
* **setting:** 补充公开属性的 [Preserve] 防裁剪标签 ([4b91010](https://github.com/gameframex/com.gameframex.unity.setting/commit/4b91010fcb396d8ac621ff7e58ca6935e8094e94))

## [1.5.1](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.5.0...1.5.1) (2026-05-28)


### Bug Fixes

* **ci:** 统一 .github 工作流配置 ([31f8c8b](https://github.com/gameframex/com.gameframex.unity.setting/commit/31f8c8bfda2a4fca2df26861c148bdb05f757b49))

# [1.5.0](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.4.0...1.5.0) (2026-05-14)


### Features

* **setting:** 为 PlayerPrefsSettingHelper 添加支付宝小程序存储支持 ([85c6851](https://github.com/gameframex/com.gameframex.unity.setting/commit/85c68511861e951f5d4d8bbe1fa5af5e8f1be29d))

# [1.4.0](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.3.0...1.4.0) (2026-04-12)


### Features

* **setting:** 为Unity编辑器环境添加PlayerPrefs支持 ([ee4c460](https://github.com/gameframex/com.gameframex.unity.setting/commit/ee4c4602d6975ed289cfa43c8400f42df192b984))

# [1.3.0](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.2.0...1.3.0) (2026-04-09)


### Bug Fixes

* **Setting:** 将默认设置助手类型更改为 PlayerPrefsSettingHelper ([bb2052d](https://github.com/gameframex/com.gameframex.unity.setting/commit/bb2052d3dc5b0a54997cdfe763fa4af511db6975))


### Features

* **setting:** 增加快手小游戏平台的存储支持 ([795dc44](https://github.com/gameframex/com.gameframex.unity.setting/commit/795dc44d10c36e0e4af8027e000ba3f53867eb3b))

# [1.2.0](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.1.0...1.2.0) (2026-03-30)


### Bug Fixes

* 为设置模块添加Preserve属性防止代码裁剪 ([0a0d521](https://github.com/gameframex/com.gameframex.unity.setting/commit/0a0d5216e4436833fc983557877c0ddfd06a8eeb))


### Features

* **setting:** 支持抖音和微信小游戏平台存储适配 ([e23ae2d](https://github.com/gameframex/com.gameframex.unity.setting/commit/e23ae2de25476cd4edf3ca32386f8191b1d259d1))

# [1.1.0](https://github.com/gameframex/com.gameframex.unity.setting/compare/1.0.4...1.1.0) (2025-12-23)


### Features

* **ci:** change ci ([5b9d5ab](https://github.com/gameframex/com.gameframex.unity.setting/commit/5b9d5ab449241f2130912aef186f553b63eebbc3))

# Changelog

## [1.0.4](https://github.com/GameFrameX/com.gameframex.unity.setting/tree/1.0.4) (2025-06-01)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.setting/compare/1.0.3...1.0.4)

## [1.0.3](https://github.com/GameFrameX/com.gameframex.unity.setting/tree/1.0.3) (2025-05-31)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.setting/compare/1.0.2...1.0.3)

## [1.0.2](https://github.com/GameFrameX/com.gameframex.unity.setting/tree/1.0.2) (2025-05-30)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.setting/compare/1.0.1...1.0.2)

## [1.0.1](https://github.com/GameFrameX/com.gameframex.unity.setting/tree/1.0.1) (2024-12-14)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.setting/compare/1.0.0...1.0.1)

## [1.0.0](https://github.com/GameFrameX/com.gameframex.unity.setting/tree/1.0.0) (2024-04-09)

[Full Changelog](https://github.com/GameFrameX/com.gameframex.unity.setting/compare/9b852dd01a7608fe31cc4bbfab8b63e44be7a49b...1.0.0)



\* *This Changelog was automatically generated by [github_changelog_generator](https://github.com/github-changelog-generator/github-changelog-generator)*

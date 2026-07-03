using System.Collections.Generic;
using GameFrameX.Setting.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Setting.Tests
{
    internal sealed class UnitTests
    {
        private GameObject m_GameObject;
        private PlayerPrefsSettingHelper m_Helper;
        private FakeSettingStorageBackend m_Backend;

        [SetUp]
        public void Setup()
        {
            m_GameObject = new GameObject("SettingHelperTests");
            m_Helper = m_GameObject.AddComponent<PlayerPrefsSettingHelper>();
            m_Backend = new FakeSettingStorageBackend();
            m_Helper.SetStorageBackendForTests(m_Backend);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(m_GameObject);
            SettingStorageBackendFactory.ResetMiniGameBackendForTests();
        }

        [Test]
        public void PlayerPrefsSettingHelper_BoolUsesIntStorage()
        {
            m_Helper.SetBool("enabled", true);

            Assert.That(m_Backend.IntValues["enabled"], Is.EqualTo(1));
            Assert.That(m_Helper.GetBool("enabled"), Is.True);

            m_Helper.SetBool("enabled", false);

            Assert.That(m_Backend.IntValues["enabled"], Is.EqualTo(0));
            Assert.That(m_Helper.GetBool("enabled", true), Is.False);
        }

        [Test]
        public void PlayerPrefsSettingHelper_ReturnsDefaultValues()
        {
            Assert.That(m_Helper.GetBool("missing_bool", true), Is.True);
            Assert.That(m_Helper.GetInt("missing_int", 17), Is.EqualTo(17));
            Assert.That(m_Helper.GetFloat("missing_float", 2.5f), Is.EqualTo(2.5f));
            Assert.That(m_Helper.GetString("missing_string", "fallback"), Is.EqualTo("fallback"));
        }

        [Test]
        public void PlayerPrefsSettingHelper_RemoveSettingReportsExistence()
        {
            Assert.That(m_Helper.RemoveSetting("score"), Is.False);

            m_Helper.SetInt("score", 9);

            Assert.That(m_Helper.RemoveSetting("score"), Is.True);
            Assert.That(m_Helper.HasSetting("score"), Is.False);
        }

        [Test]
        public void PlayerPrefsSettingHelper_RemoveAllSettingsClearsBackend()
        {
            m_Helper.SetInt("score", 9);
            m_Helper.SetString("name", "player");

            m_Helper.RemoveAllSettings();

            Assert.That(m_Backend.HasKey("score"), Is.False);
            Assert.That(m_Backend.HasKey("name"), Is.False);
            Assert.That(m_Backend.DeleteAllCallCount, Is.EqualTo(1));
        }

        private sealed class FakeSettingStorageBackend : ISettingStorageBackend
        {
            internal readonly Dictionary<string, int> IntValues = new Dictionary<string, int>();
            private readonly Dictionary<string, float> m_FloatValues = new Dictionary<string, float>();
            private readonly Dictionary<string, string> m_StringValues = new Dictionary<string, string>();

            internal int DeleteAllCallCount { get; private set; }

            public bool Save()
            {
                return true;
            }

            public bool HasKey(string key)
            {
                return IntValues.ContainsKey(key) || m_FloatValues.ContainsKey(key) || m_StringValues.ContainsKey(key);
            }

            public bool DeleteKey(string key)
            {
                if (!HasKey(key))
                {
                    return false;
                }

                IntValues.Remove(key);
                m_FloatValues.Remove(key);
                m_StringValues.Remove(key);
                return true;
            }

            public void DeleteAll()
            {
                DeleteAllCallCount++;
                IntValues.Clear();
                m_FloatValues.Clear();
                m_StringValues.Clear();
            }

            public int GetInt(string key, int defaultValue)
            {
                return IntValues.TryGetValue(key, out int value) ? value : defaultValue;
            }

            public void SetInt(string key, int value)
            {
                IntValues[key] = value;
            }

            public float GetFloat(string key, float defaultValue)
            {
                return m_FloatValues.TryGetValue(key, out float value) ? value : defaultValue;
            }

            public void SetFloat(string key, float value)
            {
                m_FloatValues[key] = value;
            }

            public string GetString(string key, string defaultValue)
            {
                return m_StringValues.TryGetValue(key, out string value) ? value : defaultValue;
            }

            public void SetString(string key, string value)
            {
                m_StringValues[key] = value;
            }
        }
    }
}

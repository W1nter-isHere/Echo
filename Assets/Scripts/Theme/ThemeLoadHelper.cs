using System.IO;
using Theme.Behaviours;
using UnityEngine;

namespace Theme
{
    public static class ThemeLoadHelper
    {
        public const string ThemePfKey = "Theme";
        private static ThemeSettings m_currentTheme;
        
        public static void LoadTheme(string location = null, bool updateObjectsInLevel = true)
        {
            if (string.IsNullOrEmpty(location))
            {
                var theme = Resources.Load<TextAsset>("Themes/DefaultTheme");
                LoadTheme(ThemeSettings.FromJson(theme.text), updateObjectsInLevel);
                PlayerPrefs.DeleteKey(ThemePfKey);
                PlayerPrefs.Save();
            }
            else
            {
                LoadTheme(ThemeSettings.FromJson(File.ReadAllText(location)), updateObjectsInLevel);
                PlayerPrefs.SetString(ThemePfKey, location);
                PlayerPrefs.Save();
            }
        }

        private static void LoadTheme(ThemeSettings themeSettings, bool updateObjectsInLevel)
        {
            m_currentTheme = themeSettings;
            
            if (!updateObjectsInLevel) return;
            foreach (var themeLoader in Object.FindObjectsOfType<ThemeLoader>())
            {
                themeLoader.Load(m_currentTheme);
            }
        }

        public static ThemeSettings GetLoadedTheme()
        {
            if (m_currentTheme == null)
            {
                LoadTheme(updateObjectsInLevel: false);
            }

            return m_currentTheme;
        }
    }
}
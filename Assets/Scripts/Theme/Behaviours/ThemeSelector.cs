using UnityEngine;

namespace Theme.Behaviours
{
    public class ThemeSelector : MonoBehaviour
    {
        private void Awake()
        {
            ThemeLoadHelper.LoadTheme(PlayerPrefs.GetString(ThemeLoadHelper.ThemePfKey), false);
        }
    }
}
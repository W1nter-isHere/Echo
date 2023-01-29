using TMPro;
using UnityEngine;

namespace Theme.Behaviours
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextThemeLoader : ThemeLoader
    {
        public override void Load(ThemeSettings themeSettings)
        {
            GetComponent<TMP_Text>().color = ShouldUseSecondary ? themeSettings.secondaryColor : themeSettings.primaryColor;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Theme.Behaviours
{
    [RequireComponent(typeof(Image))]
    public class PanelThemeLoader : ThemeLoader
    {
        public override void Load(ThemeSettings themeSettings)
        {
            GetComponent<Image>().color = ShouldUseSecondary ? themeSettings.secondaryColor : themeSettings.primaryColor;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Theme.Behaviours
{
    [RequireComponent(typeof(Selectable))]
    public class SelectableThemeLoader : ThemeLoader
    {
        public override void Load(ThemeSettings themeSettings)
        {
            GetComponent<Selectable>().colors = ShouldUseSecondary ? themeSettings.secondaryWidget : themeSettings.primaryWidget;
        }
    }
}
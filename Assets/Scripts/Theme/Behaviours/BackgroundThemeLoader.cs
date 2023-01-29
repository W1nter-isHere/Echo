using UnityEngine;

namespace Theme.Behaviours
{
    [RequireComponent(typeof(Camera))]
    public class BackgroundThemeLoader : ThemeLoader
    {
        public override void Load(ThemeSettings themeSettings)
        {
            GetComponent<Camera>().backgroundColor = themeSettings.backgroundColor;
        }
    }
}
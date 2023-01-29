using UnityEngine;
using UnityEngine.UI;

namespace Theme.Behaviours
{
    [RequireComponent(typeof(Image))]
    public class Background2ThemeLoader : ThemeLoader
    {
        public override void Load(ThemeSettings themeSettings)
        {
            GetComponent<Image>().color = themeSettings.backgroundColor2;
        }
    }
}
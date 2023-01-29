using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Theme
{
    [Serializable]
    public class ThemeSettings : JsonSerializable<ThemeSettings>
    {
        [BoxGroup("Main")] public Color backgroundColor;
        [BoxGroup("Main")] public Color backgroundColor2;
        [BoxGroup("Main")] public Color primaryColor;
        [BoxGroup("Main")] public Color secondaryColor;
        
        [BoxGroup("Widget"), Header("Primary")] 
        public ColorBlock primaryWidget;
        [BoxGroup("Widget"), Header("Secondary")] 
        public ColorBlock secondaryWidget;
    }
}
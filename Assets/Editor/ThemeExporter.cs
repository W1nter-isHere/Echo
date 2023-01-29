using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Theme;
using UnityEditor;

namespace Editor
{
    public class ThemeExporter : OdinEditorWindow
    {
        [MenuItem("Echo/Themes/Exporter")]
        private static void OpenWindow()
        {
            var themeExporter = GetWindow<ThemeExporter>();
            themeExporter.themeSettings = ThemeLoadHelper.GetLoadedTheme();
            themeExporter.Show();
        }
        
        public ThemeSettings themeSettings;

        [Button]
        private void ExportSettings()
        {
            var path = EditorUtility.OpenFilePanel("Save Location", "", "json");
            JsonSaveHelper.Save(themeSettings, path);
        }
    }
}

using Sirenix.OdinInspector.Editor;
using Theme;
using UnityEditor;

namespace Editor
{
    public class ThemeImporter : OdinEditorWindow
    {
        [MenuItem("Echo/Themes/Importer")]
        private static void OpenWindow()
        {
            var path = EditorUtility.OpenFilePanel("Select Theme", "", "json");
            if (string.IsNullOrEmpty(path)) return;
            ThemeLoadHelper.LoadTheme(path);
        }
    }
}
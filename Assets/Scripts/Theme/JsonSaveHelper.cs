using System.IO;
using Utils;

namespace Theme
{
    public static class JsonSaveHelper
    {
        public static void Save<T>(JsonSerializable<T> serializable, string location)
        {
            var path = Path.GetDirectoryName(location);
            
            if (!string.IsNullOrEmpty(path))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            
            File.WriteAllText(location, serializable.ToJson());
        }
    }
}
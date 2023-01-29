using UnityEngine;

namespace Utils
{
    public abstract class JsonSerializable<T>
    {
        public string ToJson(bool prettyPrint = true)
        {
            return JsonUtility.ToJson(this, prettyPrint);
        }

        public static T FromJson(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}
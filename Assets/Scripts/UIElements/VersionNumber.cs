using TMPro;
using UnityEngine;

namespace UIElements
{
    public class VersionNumber : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            text.text = "v" + Application.version;
        }
    }
}

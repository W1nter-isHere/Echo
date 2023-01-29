using System.Collections;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIElements
{
    public class SelectFolderLocationButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI text;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            FileBrowser.AddQuickLink("Users", "C:\\Users");
            StartCoroutine(ShowLoadDialogCoroutine());

            IEnumerator ShowLoadDialogCoroutine()
            {
                yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Folders, false, null, null, "Select location to save project");
                if(FileBrowser.Success)
                {
                    var path = FileBrowser.Result[0];
                    text.text = path;
                }
            }
        }
    }
}
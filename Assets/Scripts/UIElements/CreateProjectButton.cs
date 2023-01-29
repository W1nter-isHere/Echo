using System.IO;
using TMPro;
using UnityEngine;
using Utils;

namespace UIElements
{
    public class CreateProjectButton : MonoBehaviour
    {
        [SerializeField] private CanvasGroup createWindow;
        [SerializeField] private TMP_InputField projectName;
        [SerializeField] private TextMeshProUGUI projectLocation;
        [SerializeField] private ProjectsListManager projectsListManager;
        
        public void CreateProject()
        {
            createWindow.FadeIn(0.25f);
        }

        public void ConfirmCreate()
        {
            if (string.IsNullOrEmpty(projectName.text)) return;
            if (string.IsNullOrEmpty(projectLocation.text)) return;
            if (!Directory.Exists(projectLocation.text)) return;
            
            projectsListManager.CreateProject(projectName.text, projectLocation.text + "/" + projectName.text + ".json");
            createWindow.FadeOut(0.25f);
        }
    }
}
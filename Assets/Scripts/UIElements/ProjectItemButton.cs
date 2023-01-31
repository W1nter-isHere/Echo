using System;
using KevinCastejon.MoreAttributes;
using ProjectPage;
using ProjectPage.Projects;
using UnityEngine;
using Utils;

namespace UIElements
{
    public class ProjectItemButton : MonoBehaviour
    {
        [NonSerialized] public Project Project;
        [SerializeField, Scene] private string projectPageScene;
        
        public void EnterProject()
        {
            ProjectPageManager.SelectedProject = Project;
            SceneUtilities.Instance.LoadScene(projectPageScene);
        }

        public void Delete()
        {
            ProjectsListManager.Current.RemoveProject(Project);
            Destroy(gameObject);
        }
    }
}
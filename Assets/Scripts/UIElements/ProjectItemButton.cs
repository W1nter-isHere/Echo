using System;
using KevinCastejon.MoreAttributes;
using ProjectPage;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace UIElements
{
    public class ProjectItemButton : MonoBehaviour, IPointerClickHandler
    {
        [NonSerialized] public Project Project;
        [SerializeField, Scene] private string projectPageScene;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            ProjectPageManager.SelectedProject = Project;
            SceneUtilities.Instance.LoadScene(projectPageScene);
        }
    }
}
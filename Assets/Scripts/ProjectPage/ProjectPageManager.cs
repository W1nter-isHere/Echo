using System;
using KevinCastejon.MoreAttributes;
using TMPro;
using UnityEngine;
using Utils;

namespace ProjectPage
{
    public class ProjectPageManager : MonoBehaviour
    {
        public static Project SelectedProject;
        
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private SoundCueBehaviour cuePrefab;
        [SerializeField] private Transform cueList;
        [SerializeField] private AudioSource cuePreviewSource;
        [SerializeField, Scene] private string mainMenu;
        
        private void Awake()
        {
            if (SelectedProject == null)
            {
                Debug.LogError("Did not select project!");
                return;
            }

            title.text = SelectedProject.title;
        }

        private void Start()
        {
            foreach (var cue in SelectedProject.cues)
            {
                var c = Instantiate(cuePrefab, cueList);
                c.CuePlayerSource = cuePreviewSource;
                c.Cue = cue;
            }
        }

        public void Exit()
        {
            SelectedProject.Save();
            SceneUtilities.Instance.LoadScene(mainMenu);
        }

        public void CreateCue()
        {
            var cue = Instantiate(cuePrefab, cueList);
            cue.CuePlayerSource = cuePreviewSource;
        }
    }
}
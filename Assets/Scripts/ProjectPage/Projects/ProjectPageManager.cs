using KevinCastejon.MoreAttributes;
using ProjectPage.Events;
using ProjectPage.SoundCues;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace ProjectPage.Projects
{
    public class ProjectPageManager : MonoBehaviour
    {
        public static Project SelectedProject;
        
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField, Scene] private string mainMenu;
        
        [SerializeField] private SoundCueBehaviour cuePrefab;
        [SerializeField] private Transform cueList;
        [SerializeField] private AudioSource cuePreviewSource;

        [SerializeField] private CueEventBehaviour cueEventPrefab;
        [SerializeField] private RectTransform eventsList;

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
                var c = INTERNAL_CreateCue();
                c.Cue = cue;
            }

            foreach (var cueEvent in SelectedProject.timeline)
            {
                var e = INTERNAL_CreateEvent();
                e.CueEvent = cueEvent;
            }
        }

        public void Exit()
        {
            SelectedProject.Save();
            SceneUtilities.Instance.LoadScene(mainMenu);
        }

        public void CreateCue()
        {
            INTERNAL_CreateCue();
        }

        public void CreateEvent()
        {
            INTERNAL_CreateEvent();
        }

        private SoundCueBehaviour INTERNAL_CreateCue()
        {
            var cue = Instantiate(cuePrefab, cueList);
            cue.CuePlayerSource = cuePreviewSource;
            return cue;
        }

        private CueEventBehaviour INTERNAL_CreateEvent()
        {
            var e = Instantiate(cueEventPrefab, eventsList);
            e.EventsList = eventsList;
            LayoutRebuilder.ForceRebuildLayoutImmediate(eventsList);
            return e;
        }
    }
}
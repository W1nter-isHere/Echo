using ProjectPage.Events;
using ProjectPage.Projects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectPage.PlayMode
{
    public class EventPopUp : MonoBehaviour
    {
        [SerializeField] private RectTransform backgroundParent;
        [SerializeField] private GameObject notePrefab;
        [SerializeField] private SoundCueElement soundCuePrefab;
        [SerializeField] private Button finishPrefab;

        private RectTransform _rectTransform;

        public void Init(CueEvent cueEvent)
        {
            foreach (var note in cueEvent.noteValues)
            {
                var go = Instantiate(notePrefab, backgroundParent)
                    .GetComponentInChildren<TextMeshProUGUI>();
                go.text = note;
                
                LayoutRebuilder.ForceRebuildLayoutImmediate(go.rectTransform);
            }

            foreach (var cues in cueEvent.GetSoundCues(ProjectPageManager.SelectedProject))
            {
                var go = Instantiate(soundCuePrefab, backgroundParent);
                go.Init(cues);
            }

            var button = Instantiate(finishPrefab, backgroundParent);
            button.onClick.AddListener(DestroySelf);
            LayoutRebuilder.ForceRebuildLayoutImmediate(backgroundParent);
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectPage.Projects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectPage.Events
{
    public class CueEventBehaviour : MonoBehaviour
    {
        [Header("Expand/Collapse")]
        [SerializeField] private Button expandButton;
        [SerializeField] private Image expandIcon;
        [SerializeField] private Sprite expandSprite;
        [SerializeField] private Sprite collapseSprite;

        [Header("TimeSpan")]
        [SerializeField] private TMP_InputField hour;
        [SerializeField] private TMP_InputField minute;
        [SerializeField] private TMP_InputField second;

        [Header("Extras")]
        [SerializeField] private Button newNote;
        [SerializeField] private Button newSoundCue;
        [SerializeField] private RectTransform extrasList;
        
        [Header("Prefabs")] 
        [SerializeField] private TMP_InputField notePrefab;
        [SerializeField] private TMP_Dropdown soundCuePrefab;

        private CueEvent _cueEvent;
        private bool _expanded;
        private List<TMP_Dropdown> _cueDropdowns;
        private RectTransform _self;
        
        [NonSerialized]
        public RectTransform EventsList;

        public CueEvent CueEvent
        {
            get => _cueEvent;
            set
            {
                _cueEvent = value;
                hour.text = _cueEvent.TimeSpan.Hours.ToString();
                minute.text = _cueEvent.TimeSpan.Minutes.ToString();
                second.text = _cueEvent.TimeSpan.Seconds.ToString();

                foreach (var (id, note) in _cueEvent.noteKeys.Zip(_cueEvent.noteValues, (k, v) => new {k, v}).ToDictionary(x => x.k, x => x.v))
                {
                    NewNote(id, note, false);
                }

                foreach (var (id, cue) in _cueEvent.soundKeys.Zip(_cueEvent.soundValues, (k, v) => new {k, v}).ToDictionary(x => x.k, x => x.v))
                {
                    NewSoundCue(id, cue, false);
                }
            }
        }

        private void Start()
        {
            expandButton.onClick.AddListener(Expand);
            hour.onValueChanged.AddListener(HourChanged);
            minute.onValueChanged.AddListener(MinuteChanged);
            second.onValueChanged.AddListener(SecondChanged);
            newNote.onClick.AddListener(NewNote);
            newSoundCue.onClick.AddListener(NewSoundCue);
            ProjectPageManager.SelectedProject.OnCuesChanged += UpdateDropdowns;

            if (_cueEvent == null)
            {
                _cueEvent = new CueEvent();
                ProjectPageManager.SelectedProject.timeline.Add(_cueEvent);
            }
            
            extrasList.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            expandButton.onClick.AddListener(Expand);
            hour.onValueChanged.AddListener(HourChanged);
            minute.onValueChanged.AddListener(MinuteChanged);
            second.onValueChanged.AddListener(SecondChanged);
            newNote.onClick.AddListener(NewNote);
            newSoundCue.onClick.AddListener(NewSoundCue);
            ProjectPageManager.SelectedProject.OnCuesChanged -= UpdateDropdowns;
        }

        private void Expand()
        {
            _expanded = !_expanded;
            SetExpand(_expanded);
        }

        private void SetExpand(bool expand)
        {
            extrasList.gameObject.SetActive(expand);
            expandIcon.sprite = expand ? collapseSprite : expandSprite;
            LayoutRebuilder.ForceRebuildLayoutImmediate(EventsList);
        }
        
        private void HourChanged(string value)
        {
            _cueEvent.timespan = CompileTimeSpan(value, minute.text, second.text);
        }

        private void MinuteChanged(string value)
        {
            _cueEvent.timespan = CompileTimeSpan(hour.text, value, second.text);
        }

        private void SecondChanged(string value)
        {
            _cueEvent.timespan = CompileTimeSpan(hour.text, minute.text, value);
        }

        private string CompileTimeSpan(string h, string m, string s)
        {
            return $"{h}:{m}:{s}";
        }

        private void NewNote()
        {
            NewNote(Guid.NewGuid().ToString(), "", true);
        }

        private void NewSoundCue()
        {
            NewSoundCue(Guid.NewGuid().ToString(), "", true);
        }

        private void NewNote(string guid, string text, bool initializeInCueEvent)
        {
            var note = Instantiate(notePrefab, extrasList);
            note.text = text;
            note.onValueChanged.AddListener(str => NoteChanged(guid, str));

            if (initializeInCueEvent)
            {
                _cueEvent.AddNote(guid, text);
                if (!_expanded)
                {
                    _expanded = true;
                    SetExpand(true);
                }
            }
            
            RebuildLayout();
        }
        
        private void NewSoundCue(string guid, string option, bool initializeInCueEvent)
        {
            var cue = Instantiate(soundCuePrefab, extrasList);
            var options = ProjectPageManager.SelectedProject.cues.Select(c => c.name).Distinct().ToList();
            cue.AddOptions(options);
            var indexOf = options.IndexOf(option);
            cue.value = indexOf == -1 ? 0 : indexOf;

            if (initializeInCueEvent)
            {
                _cueEvent.AddSoundCue(guid, cue.options[cue.value].text);
                if (!_expanded)
                {
                    _expanded = true;
                    SetExpand(true);
                }
            }
            
            _cueDropdowns ??= new List<TMP_Dropdown>();
            _cueDropdowns.Add(cue);
            cue.onValueChanged.AddListener(opt => SoundCueChanged(guid, cue.options[opt].text));
            
            RebuildLayout();
        }

        private void UpdateDropdowns()
        {
            var options = ProjectPageManager.SelectedProject.cues.Select(c => c.name).Distinct().ToList();
            foreach (var dropdown in _cueDropdowns)
            {
                var t = dropdown.captionText.text;
                dropdown.ClearOptions();
                dropdown.AddOptions(options);

                var i = options.IndexOf(t);
                if (i >= 0 ) dropdown.value = i;
            }
        }
        
        private void RebuildLayout()
        {
            _self ??= (RectTransform)transform;
            LayoutRebuilder.ForceRebuildLayoutImmediate(extrasList);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_self);
            LayoutRebuilder.ForceRebuildLayoutImmediate(EventsList);
        }

        private void NoteChanged(string noteId, string value)
        {
            _cueEvent.SetNote(noteId, value);
        }

        private void SoundCueChanged(string cueGuid, string value)
        {
            _cueEvent.SetSoundCue(cueGuid, value);
        }
    }
}
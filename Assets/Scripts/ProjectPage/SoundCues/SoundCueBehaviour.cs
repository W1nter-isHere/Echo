using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using ProjectPage.Projects;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace ProjectPage.SoundCues
{
    public class SoundCueBehaviour : MonoBehaviour
    {
        [SerializeField] private Button playCue;
        [SerializeField] private TextMeshProUGUI filePath;
        [SerializeField] private Button selectFile;
        [SerializeField] private TMP_Dropdown easeIn;
        [SerializeField] private TMP_Dropdown easeOut;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TextMeshProUGUI volumeText;
        [SerializeField] private Slider pitchSlider;
        [SerializeField] private TextMeshProUGUI pitchText;
        [SerializeField] private Button deleteButton;

        [NonSerialized] public AudioSourceWrapper CuePlayerSource;
        
        private SoundCue _cue;
        public SoundCue Cue
        {
            get => _cue;
            set
            {
                _cue = value;
                filePath.text = _cue.soundFile;
                var eases = Enum.GetValues(typeof(Ease)).Cast<Ease>().ToList();
                easeIn.SetValueWithoutNotify(eases.IndexOf(_cue.inEase));
                easeOut.SetValueWithoutNotify(eases.IndexOf(_cue.outEase));
                volumeSlider.SetValueWithoutNotify(_cue.volume);
                volumeText.text = _cue.volume.ToString("F1");
                pitchSlider.SetValueWithoutNotify(_cue.pitch);
                pitchText.text = _cue.pitch.ToString("F1");
            }
        }
        
        private void Start()
        {
            playCue.onClick.AddListener(PlayCue);
            selectFile.onClick.AddListener(SelectFile);

            var options = Enum.GetValues(typeof(Ease))
                .Cast<Ease>()
                .Where(ease => ease != Ease.INTERNAL_Custom && ease != Ease.INTERNAL_Zero)
                .Select(ease => ease.ToString())
                .ToList();

            easeIn.AddOptions(options);
            easeOut.AddOptions(options);
            
            easeIn.onValueChanged.AddListener(EaseInChanged);
            easeOut.onValueChanged.AddListener(EaseOutChanged);
            
            volumeSlider.onValueChanged.AddListener(VolumeChanged);
            pitchSlider.onValueChanged.AddListener(PitchChanged);

            deleteButton.onClick.AddListener(Delete);

            if (_cue == null)
            {
                _cue = new SoundCue("New Sound Cue");
                ProjectPageManager.SelectedProject.cues.Add(_cue);
                ProjectPageManager.SelectedProject.CuesChanged();
            }

            EaseInChanged(0);
            EaseOutChanged(0);
            VolumeChanged(volumeSlider.value);
            PitchChanged(pitchSlider.value);
        }

        private void OnDestroy()
        {
            playCue.onClick.RemoveListener(PlayCue);
            selectFile.onClick.RemoveListener(SelectFile);
            easeIn.onValueChanged.RemoveListener(EaseInChanged);
            easeOut.onValueChanged.RemoveListener(EaseOutChanged);
            volumeSlider.onValueChanged.RemoveListener(VolumeChanged);
            pitchSlider.onValueChanged.RemoveListener(PitchChanged);
            deleteButton.onClick.RemoveListener(Delete);
        }

        private void PlayCue()
        {
            if (CuePlayerSource.source.isPlaying)
            {
                CuePlayerSource.Stop();
            }
            else
            {
                _cue.Play(CuePlayerSource);
            }
        }

        private void SelectFile()
        {
            FileBrowser.SetFilters(true, new FileBrowser.Filter("Sound File", ".mp3", ".ogg", ".wav"));
            FileBrowser.SetDefaultFilter(".mp3");
            FileBrowser.AddQuickLink("Users", "C:\\Users");
		
            StartCoroutine(ShowLoadDialogCoroutine());

            IEnumerator ShowLoadDialogCoroutine()
            {
                yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select sound file to import");
                if(FileBrowser.Success)
                {
                    var path = FileBrowser.Result[0];
                    filePath.text = path;
                    _cue.soundFile = path;
                    _cue.name = path;
                    ProjectPageManager.SelectedProject.CuesChanged();
                    yield return _cue.CacheAudio();
                }
            }
        }

        private void EaseInChanged(int index)
        {
            UpdateEase(index, true);
        }

        private void EaseOutChanged(int index)
        {
            UpdateEase(index, false);
        }

        private void UpdateEase(int index, bool isIn)
        {
            if (!Enum.TryParse<Ease>(easeIn.options[index].text, out var res)) return;

            if (isIn)
            {
                _cue.inEase = res;
            }
            else
            {
                _cue.outEase = res;
            }
        }

        private void VolumeChanged(float volume)
        {
            _cue.volume = volume;
            volumeText.text = volume.ToString("F1");
        }

        private void PitchChanged(float pitch)
        {
            _cue.pitch = pitch;
            pitchText.text = pitch.ToString("F1");
        }

        private void Delete()
        {
            ProjectPageManager.SelectedProject.cues.Remove(_cue);
            ProjectPageManager.SelectedProject.CuesChanged();
            Destroy(gameObject);
        }
    }
}
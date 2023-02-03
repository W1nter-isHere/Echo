using ProjectPage.SoundCues;
using TMPro;
using UnityEngine;
using Utils;

namespace ProjectPage.PlayMode
{
    public class SoundCueElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI cueName;
        [SerializeField] private AudioSource audioSource;

        private SoundCue _soundCue;
        private AudioSourceWrapper _wrapper;
        
        public void Init(SoundCue soundCue)
        {
            cueName.text = soundCue.name;
            _soundCue = soundCue;
            _wrapper = new AudioSourceWrapper(audioSource);
        }

        public void Play()
        {
            if (_wrapper.source.isPlaying)
            {
                _wrapper.Stop();
            }
            else
            {
                _soundCue.Play(_wrapper);
            }
        }
    }
}
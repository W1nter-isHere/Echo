using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class AudioSourceWrapper
    {
        public AudioSource source;

        public event Action<AudioSourceWrapper> OnStopRequested;
        
        public AudioSourceWrapper(AudioSource source)
        {
            this.source = source;
        }

        public void Stop()
        {
            OnStopRequested?.Invoke(this);
        }

        public static implicit operator AudioSourceWrapper(AudioSource audioSource)
        {
            return new AudioSourceWrapper(audioSource);
        }
    }
}
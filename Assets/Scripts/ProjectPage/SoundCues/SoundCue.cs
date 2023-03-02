using System;
using System.IO;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace ProjectPage.SoundCues
{
    [Serializable]
    public class SoundCue
    {
        public string name;
        public string id;
        public string soundFile;
        public Ease inEase;
        public Ease outEase;
        public float volume;
        public float pitch;
        
        private AudioClip _cachedClip;

        public SoundCue(string name)
        {
            this.name = name;
            id = Guid.NewGuid().ToString();
        }

        public async void Play(AudioSourceWrapper source)
        {
            var clip = await GetClip();
            
            source.source.clip = clip;
            source.source.pitch = pitch;

            if (inEase == Ease.Unset)
            {
                source.source.volume = volume;
            }
            else
            {
                source.source.volume = 0;
                source.source.DOFade(volume, 0.5f)
                    .SetEase(inEase);
            }
            
            source.source.Play();
            source.OnStopRequested += StopAudio;
        }
        
        private async Task<AudioClip> GetClip()
        {
            if (_cachedClip != null)
            {
                return _cachedClip;
            }

            await CacheAudio();
            return _cachedClip;
        }

        public async Task CacheAudio()
        {
            using var www = UnityWebRequestMultimedia.GetAudioClip(soundFile, CheckFileType(soundFile));
            var result = www.SendWebRequest();

            while (!result.isDone) { await Task.Delay(100); }

            if (www.result != UnityWebRequest.Result.ConnectionError)
            {
                _cachedClip = DownloadHandlerAudioClip.GetContent(www);
            }
            
            if (string.IsNullOrEmpty(www.error)) return;
            Debug.LogWarning($"Failed to retrieve audioclip at {soundFile}. Reason: {www.error}");
        }

        private void StopAudio(AudioSourceWrapper audioSource)
        {
            audioSource.OnStopRequested -= StopAudio;
            
            if (outEase == Ease.Unset)
            {
                audioSource.source.Stop();
            }
            else
            {
                audioSource.source.DOFade(0, 0.5f)
                    .SetEase(outEase)
                    .OnComplete(audioSource.source.Stop);
            }
        }
        
        private AudioType CheckFileType(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath);

            return fileExtension switch
            {
                ".mp3" => AudioType.MPEG,
                ".wav" => AudioType.WAV,
                _ => fileExtension == ".ogg" ? AudioType.OGGVORBIS : AudioType.UNKNOWN
            };
        }
    }
}
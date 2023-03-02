using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ProjectPage.Projects;
using ProjectPage.SoundCues;
using Utils;

namespace ProjectPage.Events
{
    [Serializable]
    public class CueEvent : JsonSerializable<CueEvent>
    {
        public string timespan;
        
        public List<string> noteKeys;
        public List<string> noteValues;

        public List<string> soundKeys;
        public List<string> soundValues;
        
        public TimeSpan TimeSpan
        {
            get => TimeSpan.Parse(timespan);
            set => timespan = value.ToString();
        }

        public CueEvent()
        {
            TimeSpan = TimeSpan.Zero;

            noteKeys = new List<string>();
            noteValues = new List<string>();
            
            soundKeys = new List<string>();
            soundValues = new List<string>();
        }

        public List<SoundCue> GetSoundCues(Project project)
        {
            return soundValues
                .Select(id => project.cues.Find(cue => cue.id == id))
                .ToList();
        }

        public void AddNote(string id, string value)
        {
            INTERNAL_Add(id, value, noteKeys, noteValues);
        }
        
        public void SetNote(string id, string value)
        {
            INTERNAL_Set(id, value, noteKeys, noteValues);
        }

        public string GetNote(string id)
        {
            return INTERNAL_Get(id, noteKeys, noteValues);
        }

        public void RemoveNote(string id)
        {
            INTERNAL_Remove(id, noteKeys, noteValues);
        }
        
        public void AddSoundCue(string id, string value)
        {
            INTERNAL_Add(id, value, soundKeys, soundValues);
        }
        
        public void SetSoundCue(string id, string value)
        {
            INTERNAL_Set(id, value, soundKeys, soundValues);
        }

        public string GetSoundCue(string id)
        {
            return INTERNAL_Get(id, soundKeys, soundValues);
        }

        public void RemoveSoundCue(string id)
        {
            INTERNAL_Remove(id, soundKeys, soundValues);
        }
        
        private void INTERNAL_Add(string id, string value, List<string> keys, List<string> values)
        {
            keys.Add(id);
            values.Add(value);
        }

        private void INTERNAL_Set(string id, string value, List<string> keys, List<string> values)
        {
            values[keys.IndexOf(id)] = value;
        }
        
        private string INTERNAL_Get(string id, List<string> keys, List<string> values)
        {
            return values[keys.IndexOf(id)];
        }

        private void INTERNAL_Remove(string id, List<string> keys, List<string> values)
        {
            var i = keys.IndexOf(id);
            keys.RemoveAt(i);
            values.RemoveAt(i);
        }

        public new string ToJson(bool prettyPrint = true)
        {
            return JsonConvert.SerializeObject(this);
        }

        public new static CueEvent FromJson(string json)
        {
            return JsonConvert.DeserializeObject<CueEvent>(json);
        }
    }
}
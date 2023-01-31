using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProjectPage.Events;
using ProjectPage.SoundCues;
using Theme;
using Utils;

namespace ProjectPage.Projects
{
    [Serializable]
    public class Project : JsonSerializable<Project>
    {
        public string title;
        public string location;
        public List<SoundCue> cues;
        public List<CueEvent> timeline;

        public event Action OnCuesChanged;
        
        public Project(string title, string location)
        {
            this.title = title;
            this.location = location;
            cues = new List<SoundCue>();
            timeline = new List<CueEvent>();
        }

        public void CuesChanged()
        {
            OnCuesChanged?.Invoke();
        }

        public void Save()
        {
            JsonSaveHelper.Save(this, location);
        }

        public new static Project FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Project>(json);
        }
    }
}
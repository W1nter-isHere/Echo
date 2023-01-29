using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Theme;
using Utils;

namespace ProjectPage
{
    [Serializable]
    public class Project : JsonSerializable<Project>
    {
        public string title;
        public string location;
        public List<SoundCue> cues;
        public List<CueHint> timeline;

        public Project(string title, string location)
        {
            this.title = title;
            this.location = location;
            cues = new List<SoundCue>();
            timeline = new List<CueHint>();
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
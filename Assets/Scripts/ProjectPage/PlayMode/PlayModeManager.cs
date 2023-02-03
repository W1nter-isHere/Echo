using System;
using System.Collections.Generic;
using System.Linq;
using ProjectPage.Projects;
using TMPro;
using UnityEngine;

namespace ProjectPage.PlayMode
{
    public class PlayModeManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private TextMeshProUGUI elapsedTime;
        [SerializeField] private Transform eventsParent;
        [SerializeField] private EventPopUp eventPrefab;

        private List<TimeSpan> _createdEvents;
        private TimeSpan _timeSpan;
        private float _elapsedTime;
        private bool _started;

        private void Start()
        {
            _createdEvents = new List<TimeSpan>();
        }

        private void Update()
        {
            if (!_started) return;
            _elapsedTime += Time.deltaTime;
            _timeSpan = TimeSpan.FromSeconds(_elapsedTime);
            elapsedTime.text = _timeSpan.ToString(@"hh\:mm\:ss");
            
            if (_createdEvents.Any(t => CompareTimeSpans(t, _timeSpan))) return;
            var cueEvent = ProjectPageManager.SelectedProject.timeline.FirstOrDefault(c => CompareTimeSpans(c.TimeSpan, _timeSpan));
            if (cueEvent != null)
            {
                _createdEvents.Add(_timeSpan);
                var ePopup = Instantiate(eventPrefab, eventsParent);
                ePopup.Init(cueEvent);
            }
        }

        public void StartTimeline()
        {
            if (!_started)
            {
                buttonText.text = "Pause Timeline";
                _started = true;
            }
            else
            {
                buttonText.text = "Start Timeline";
                _started = false;
            }
        }

        public void ResetTimeline()
        {
            _elapsedTime = 0;
            _timeSpan = TimeSpan.FromSeconds(_elapsedTime);
            elapsedTime.text = _timeSpan.ToString(@"hh\:mm\:ss");
            _createdEvents.Clear();
        }
        
        private static bool CompareTimeSpans(TimeSpan timeSpan1, TimeSpan timeSpan2)
        {
            return timeSpan1.Hours == timeSpan2.Hours &&
                   timeSpan1.Minutes == timeSpan2.Minutes &&
                   timeSpan1.Seconds == timeSpan2.Seconds;
        }
    }
}
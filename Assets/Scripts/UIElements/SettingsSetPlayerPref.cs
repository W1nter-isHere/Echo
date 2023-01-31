using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UIElements
{
    public class SettingsSetPlayerPref : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private PlayerPrefUtilities.PlayerPrefs pref;

        private void Start()
        {
            toggle.isOn = PlayerPrefs.GetInt(pref.ToString(), 0) == 1;
            toggle.onValueChanged.AddListener(Toggle);
            Toggle(toggle.isOn);
        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveListener(Toggle);
        }

        private void Toggle(bool value)
        {
            PlayerPrefs.SetInt(pref.ToString(), value ? 1 : 0);
        }
    }
}
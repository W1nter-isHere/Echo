using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace UIElements
{
    public class SidePanelButton : MonoBehaviour, IPointerClickHandler
    {
        public static CanvasGroup CurrentGroup { get; private set; }

        [SerializeField] private bool initializeAsThis;
        [SerializeField] private CanvasGroup newPanel;

        private void Start()
        {
            if (initializeAsThis) CurrentGroup = newPanel;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (newPanel == CurrentGroup) return;
            
            if (CurrentGroup != null)
            {
                CurrentGroup.FadeOut(0.2f);
            }

            newPanel.FadeIn(0.2f);
            CurrentGroup = newPanel;
        }
    }
}
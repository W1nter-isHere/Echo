using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIElements
{
    public class SidePanelButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform _rectTransform;
        private Vector2 _startSize;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startSize = _rectTransform.sizeDelta;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _rectTransform.DOSizeDelta(new Vector2(_startSize.x + 20, _startSize.y + 10), 0.5f)
                .SetEase(Ease.OutCubic);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _rectTransform.DOSizeDelta(_startSize, 0.5f)
                .SetEase(Ease.OutCubic);
        }
    }
}
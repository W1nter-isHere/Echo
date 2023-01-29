using System;
using TMPro;
using UnityEngine;

namespace UIElements
{
    [ExecuteAlways]
    public class ShrinkToText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string normalText;
        [SerializeField] private string shrinkText;

        private float _textWidth;
        private RectTransform _rectTransform;
        
        private void OnEnable()
        {
            text.text = normalText;
            text.ForceMeshUpdate();
            _textWidth = text.GetRenderedValues(false).x;
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Eval()
        {
            if (text == null) return;
            text.text = _rectTransform.sizeDelta.x < _textWidth ? shrinkText : normalText;
        }

        private void Update()
        {
            Eval();
        }
    }
}
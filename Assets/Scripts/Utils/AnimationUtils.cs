using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace Utils
{
    public static class AnimationUtils
    {
        public static TweenerCore<float, float, FloatOptions> FadeIn(this CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.alpha = 0;
            canvasGroup.gameObject.SetActive(true);
            return canvasGroup.DOFade(1, duration);
        }
        
        public static TweenerCore<float, float, FloatOptions> FadeOut(this CanvasGroup canvasGroup, float duration)
        {
            return canvasGroup.DOFade(0, duration)
                .OnComplete(() => canvasGroup.gameObject.SetActive(false));
        }
    }
}
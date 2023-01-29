using System.Collections;
using DG.Tweening;
using UIElements;
using UnityEngine;
using Utils;

public class OpeningSequence : MonoBehaviour
{
    [SerializeField] private CanvasGroup title;
    [SerializeField] private CanvasGroup description;
    [SerializeField] private RectTransform sidePanelWrapper;
    [SerializeField] private RectTransform mainMenu;
    
    private IEnumerator Start()
    {
        title.alpha = 0;
        description.alpha = 0;
        
        var sizeDelta = sidePanelWrapper.sizeDelta;
        sidePanelWrapper.sizeDelta = new Vector2(0, sizeDelta.y);
        mainMenu.SetLeft(-180);
        
        title.DOFade(1, 1);
        yield return new WaitForSeconds(0.25f);
        description.DOFade(1, 0.75f);
        yield return new WaitForSeconds(0.75f);

        sidePanelWrapper.DOSizeDelta(sizeDelta, 1f)
            .SetEase(Ease.InOutCubic);
        
        DOTween.To(() => mainMenu.offsetMin.x, f => mainMenu.SetLeft(f), 0, 1f)
            .SetEase(Ease.InOutCubic);
    }

    public void EndingSequence()
    {
        StartCoroutine(Sequence());
        
        IEnumerator Sequence()
        {
            var size = sidePanelWrapper.sizeDelta;
            sidePanelWrapper.DOSizeDelta(new Vector2(0, size.y), 1f);

            var currentGroup = SidePanelButton.CurrentGroup;
            var panel = (RectTransform) currentGroup.transform;
            DOTween.To(() => panel.offsetMin.x, f => panel.SetLeft(f), -180, 1f)
                .SetEase(Ease.InOutCubic);
        
            yield return new WaitForSeconds(1f);
            currentGroup.FadeOut(0.5f);
            yield return new WaitForSeconds(0.75f);
            Application.Quit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UiAnimationMenu : MonoBehaviour
{
    [SerializeField] GameObject noInputPanel;
    [SerializeField] RectTransform popUpPanal;
    [SerializeField] Image close;


    public void OpenMunaulPopup() 
    {
        PreventInput(1.5f);
        close.color = new Color(close.color.r, close.color.g, close.color.b, 0);
        popUpPanal.localScale = new Vector2(0, 0);
        Sequence MunaulSequence = DOTween.Sequence();
        MunaulSequence.Append(popUpPanal.transform.DOScale(new Vector2(1f, 1f), 1f).SetEase(Ease.InOutCubic))
            .Append(close.DOFade(1, 0.5f));
    }
    

    void PreventInput(float duration)
    {
        noInputPanel.transform.SetAsLastSibling();
        StartCoroutine(ChangeSceneDelay(noInputPanel, noInputPanel, duration));
    }
    IEnumerator ChangeSceneDelay(GameObject newCanvas, GameObject oldCanvas, float duration)
    {
        newCanvas.SetActive(true);
        yield return new WaitForSeconds(duration);
        oldCanvas.SetActive(false);
    }
}

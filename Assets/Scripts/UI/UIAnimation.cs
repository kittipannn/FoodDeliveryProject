using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIAnimation : MonoBehaviour
{
    public static UIAnimation uiAnimaInstance;
    [SerializeField] GameObject noInputPanel;
    [Header("Finish")]
    [SerializeField] RectTransform FinishPanel;
    [SerializeField] TMP_Text headFinish;
    [SerializeField] TMP_Text[] textFinish;
    [SerializeField] Image[] imageFinish;
    [SerializeField] Button buttonFinish;

    [Header("Over")]
    [SerializeField] Image[] buttonGameOver;

    [Header("Option")]
    [SerializeField] RectTransform optionPanel;


    private void Awake()
    {
        if (uiAnimaInstance == null)
        {
            uiAnimaInstance = this;
        }
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
    public void OnfinishPanel() 
    {
        PreventInput(7);
        buttonFinish.transform.localScale = new Vector2(0, 0);
        FinishPanel.localScale = new Vector2(0, 0);
        Sequence finishSequence = DOTween.Sequence();
        finishSequence.Append(FinishPanel.transform.DOScale(new Vector2(1f, 1f), 1.5f).SetEase(Ease.InOutCubic))
            .Append(headFinish.DOFade(1, 1))
            .AppendCallback(() => textTween(1, 1, textFinish))
            .AppendInterval(1)
            .AppendCallback(() => imageTween(1,1, imageFinish))
            .AppendInterval(1)
            .Append(buttonFinish.transform.DOScale(new Vector2(1f, 1f), 1.5f).SetEase(Ease.InOutCubic));
    }

    public void OnOverPanel(GameObject overHeader) 
    {
        PreventInput(2.5f);
        Sequence OverSequence = DOTween.Sequence();
        overHeader.transform.localScale = new Vector2(0, 0);
        OverSequence.Append(overHeader.GetComponent<RectTransform>().DOScale(new Vector2(1f, 1f), 1.5f).SetEase(Ease.InOutCubic))
            .AppendCallback(() => imageTween(1, 1, buttonGameOver));
    }
    public void OnOptionPanel() 
    {
        optionPanel.localScale = new Vector2(0, 0);
        Sequence OptionSequence = DOTween.Sequence();
        OptionSequence.Append(optionPanel.transform.DOScale(new Vector2(1f, 1f), .5f))
            .OnComplete(() => optionPrevent());

    }
    void optionPrevent() 
    {
        Time.timeScale = 0;
        UIManager.FindObjectOfType<UIManager>().onOption = true;
    }
    void textTween(float endValue, float duration , TMP_Text[] textArray)
    {
        foreach (var text in textArray)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text.DOFade(endValue, duration);
        }
    }

    void imageTween(float endValue, float duration , Image[] imageArray)
    {
        foreach (var image in imageArray)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            image.DOFade(endValue, duration);
        }
    }
}

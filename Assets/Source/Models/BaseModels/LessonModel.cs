using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class LessonModel : MonoBehaviour
{
    public float PlayDelay;
    public UnityEvent OnLessonPlay;
    public UnityEvent OnLessonStop;
    public RectTransform FingerTransform;
    public string PlainText;
    public TextMeshProUGUI PlainTxtObj;


    public void SetDefaults(float delay, string text, Vector2 anchoredTextPos)
    {
        PlayDelay = delay;
        PlainText = text;
        PlainTxtObj.text = text;
        PlainTxtObj.rectTransform.anchoredPosition = anchoredTextPos;
    }
    
    public virtual void PlayLesson()
    {
        transform.localScale = Vector3.zero;
        PlainTxtObj.text = PlainText;
        gameObject.SetActive(true);
        transform.DOScale(1, .3f).OnComplete(() =>
        {
            OnLessonPlay?.Invoke();
        });
    }

    public virtual void StopLesson()
    {
        transform.DOScale(0, .3f).OnComplete(() =>
        {
            gameObject.SetActive(false);
            OnLessonStop?.Invoke();
        });
    }
}

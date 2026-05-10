using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;

    [SerializeField] private float scale = 2f;
    [SerializeField] private float duration = 0.5f;

    Tween currentTween;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentTween != null)
        {
            currentTween.Kill();
        }
        currentTween = transform
            .DOScale(scale, duration)
            .OnKill(() => currentTween = null)
            .SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentTween != null)
        {
            currentTween.Kill();
        }
        currentTween = transform
            .DOScale(1f, duration)
            .OnKill(() => currentTween = null)
            .SetUpdate(true);
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        currentTween.Kill();
        currentTween = null;
    }

}

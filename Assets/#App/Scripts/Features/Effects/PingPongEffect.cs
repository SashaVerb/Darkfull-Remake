using DG.Tweening;
using UnityEngine;

public class PingPongEffect : MonoBehaviour
{
    [SerializeField] private Transform _movedObject;
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease = Ease.Linear;

    private Tween _tween;

    private void Start()
    {
        _movedObject.transform.position = _point1.position;

        _tween = DOTween.Sequence()
            .Append(_movedObject.DOMove(_point2.position, _duration).SetEase(_ease))
            .Append(_movedObject.DOMove(_point1.position, _duration).SetEase(_ease))
            .SetLoops(-1, LoopType.Restart);
    }

    private void OnDestroy()
    {
        _tween.Kill();
    }
}

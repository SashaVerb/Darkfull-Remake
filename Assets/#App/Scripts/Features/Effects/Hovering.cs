using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovering : MonoBehaviour
{
    [SerializeField] private float height = 2f;
    [SerializeField] private float duration = 3f;
    private void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(Random.value * 2);
        transform.DOMoveY(transform.position.y + height, duration)
            .SetEase(Ease.InOutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject);
    }
}

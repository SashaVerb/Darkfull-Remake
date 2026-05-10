using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EmissionEffectToggle : MonoBehaviour
{
    public Color emissionColor = Color.white;
    public float targetIntensity = 1f;
    public float transitionDuration = 1f;

    private Renderer _renderer;
    private Material _material;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
    }

    public void PlayEffect()
    {
        _material.SetColor("_EmissionColor", Color.black);

        _material.DOEmission(emissionColor, transitionDuration)
            .SetLink(gameObject);
    }

    public void StopEffect()
    {
        _material.DOEmission(Color.black, transitionDuration)
            .SetLink(gameObject);
    }
}

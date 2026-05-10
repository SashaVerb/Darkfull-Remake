using DG.Tweening;
using UnityEngine;

public class EmissionEffectMultiple : MonoBehaviour
{
    public Color emissionColor = Color.white;
    public float targetIntensity = 1f;
    public float transitionDuration = 1f;
    public float stayDuration = 1f;

    private Material[] _materials;

    void Awake()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        _materials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            _materials[i] = renderers[i].material;
        }
    }

    public void PlayEffect()
    {
        foreach (var material in _materials)
        {
            material.SetColor("_EmissionColor", Color.black);

            DOTween.Sequence()
                .Append(material.DOEmission(emissionColor, transitionDuration))
                .AppendInterval(stayDuration)
                .Append(material.DOEmission(Color.black, transitionDuration))
                .SetLink(gameObject);
        }
    }
}

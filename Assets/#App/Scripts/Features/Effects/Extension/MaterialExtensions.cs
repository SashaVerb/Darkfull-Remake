using DG.Tweening;
using UnityEngine;

public static class MaterialExtensions
{
    static readonly int EMISSION_PROPERTY_ID = Shader.PropertyToID("_EmissionColor");
    const string EMISSION_TOGGLE_PROPERTY_NAME = "_EMISSION";

    public static Tweener DOEmission(this Material material, Color targetColor, float duration)
    {
        if (!material.IsKeywordEnabled(EMISSION_TOGGLE_PROPERTY_NAME))
        {
            material.EnableKeyword(EMISSION_TOGGLE_PROPERTY_NAME);
        }

        return DOTween.To(
            () => material.GetColor(EMISSION_PROPERTY_ID),
            (color) => material.SetColor(EMISSION_PROPERTY_ID, color),
            targetColor,
            duration
        );
    }
}

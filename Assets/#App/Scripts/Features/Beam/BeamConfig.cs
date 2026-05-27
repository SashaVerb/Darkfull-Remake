using System;
using System.Collections.Generic;
using Features.Beam;
using Features.Beam.ColorSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "BeamConfig", menuName = "Configs/Beam Config")]
public class BeamConfig : ScriptableObject
{
    [Header("Beam Visual")]
    [field: SerializeField] public BeamSegmentView SegmentPrefab { get; private set; }
    
    [Header("Beam Physics")]
    [field: SerializeField] public float MaxDistance { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public LayerMask RaycastMask { get; private set; }
    [field: SerializeField] public LayerMask ReflectableMask { get; private set; }
    [field: SerializeField] public LayerMask RefractableMask { get; private set; }
    
    [Header("Indicator Detection")]
    [field: SerializeField] public float IndicatorDetectionRadius { get; private set; }
    [field: SerializeField] public LayerMask IndicatorLayerMask { get; private set; }

    [Header("Beam Color")]
    [SerializeField] private string ColorTag;
    
    private TagHandle? cachedColorTagHandle;
    public TagHandle ColorTagHandle => cachedColorTagHandle ??= TagHandle.GetExistingTag(ColorTag);
    
    [field: SerializeField] private List<ColorMaterialBinding> ColorBindings { get; set; }
    
    public Material GetMaterialForColor(BeamColor color)
    {
        return ColorBindings.Find(x => x.Color == color).Material;
    }

    private void OnValidate()
    {
        if (ColorBindings == null)
        {
            ColorBindings = new List<ColorMaterialBinding>();
            foreach (BeamColor color in Enum.GetValues(typeof(BeamColor)))
            {
                ColorBindings.Add(new ColorMaterialBinding(null, color));
            }
        }
    }

    [Serializable]
    private struct ColorMaterialBinding
    {
        public Material Material;
        public BeamColor Color;
        
        public ColorMaterialBinding(Material material, BeamColor color)
        {
            Material = material;
            Color = color;
        }
    }
}

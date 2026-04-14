using UnityEngine;

[CreateAssetMenu(fileName = "BeamConfig", menuName = "Configs/Beam Config")]
public class BeamConfig : ScriptableObject
{
    [Header("Beam Visual")]
    [SerializeField] private MeshFilter _segmentPrefab;
    
    [Header("Beam Physics")]
    [SerializeField] private float _maxDistance = 100f;
    [SerializeField] private LayerMask _raycastMask;
    [SerializeField] private LayerMask _reflectableMask;
    
    [Header("Indicator Detection")]
    [SerializeField] private float _indicatorDetectionRadius = 0.5f;
    [SerializeField] private LayerMask _indicatorLayerMask;

    public MeshFilter SegmentPrefab => _segmentPrefab;
    public float MaxDistance => _maxDistance;
    public LayerMask RaycastMask => _raycastMask;
    public LayerMask ReflectableMask => _reflectableMask;
    public float IndicatorDetectionRadius => _indicatorDetectionRadius;
    public LayerMask IndicatorLayerMask => _indicatorLayerMask;
}

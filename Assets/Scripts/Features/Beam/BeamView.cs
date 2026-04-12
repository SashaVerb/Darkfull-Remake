using System.Collections.Generic;
using UnityEngine;

public class BeamView : MonoBehaviour
{
    [SerializeField] private MeshFilter _segmentPrefab;

    private MonoPool<MeshFilter> _pool;
    private List<MeshFilter> _activeSegments = new();

    private void Awake()
    {
        _pool = new MonoPool<MeshFilter>(_segmentPrefab, transform);
    }

    public void Display(IReadOnlyList<Vector3> points)
    {
        int segmentCount = points.Count - 1;

        for (int i = _activeSegments.Count - 1; i >= segmentCount; i--)
        {
            _pool.Release(_activeSegments[i]);
            _activeSegments.RemoveAt(i);
        }

        for (int i = 0; i < segmentCount; i++)
        {
            MeshFilter segment;
            if (i < _activeSegments.Count)
            {
                segment = _activeSegments[i];
            }
            else
            {
                segment = _pool.Get();
                _activeSegments.Add(segment);
            }

            segment.SetTwoPoints(points[i], points[i + 1]);
        }
    }

    public void Clear()
    {
        for (int i = _activeSegments.Count - 1; i >= 0; i--)
        {
            _pool.Release(_activeSegments[i]);
            _activeSegments.RemoveAt(i);
        }
    }
}

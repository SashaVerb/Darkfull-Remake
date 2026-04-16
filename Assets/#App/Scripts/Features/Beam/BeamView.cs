using System.Collections.Generic;
using Features.Beam;
using Features.Beam.ColorSystem;
using UnityEngine;

public class BeamView
{
    private readonly BeamConfig _config;
    private readonly Transform _parent;
    private readonly MonoPool<BeamSegmentView> _pool;
    private List<BeamSegmentView> _activeSegments = new();

    public BeamView(BeamConfig config, Transform parent)
    {
        _config = config;
        _parent = parent;
        _pool = new(_config.SegmentPrefab, _parent);
    }

    public void Display(in List<BeamPathPoint> points, in List<BeamColor> colors)
    {
        int segmentCount = points.Count - 1;

        for (int i = _activeSegments.Count - 1; i >= segmentCount; i--)
        {
            _pool.Release(_activeSegments[i]);
            _activeSegments.RemoveAt(i);
        }

        for (int i = 0; i < segmentCount; i++)
        {
            BeamSegmentView segment;
            if (i < _activeSegments.Count)
            {
                segment = _activeSegments[i];
            }
            else
            {
                segment = _pool.Get();
                _activeSegments.Add(segment);
            }

            segment.Mesh.SetTwoPoints(points[i].Position, points[i + 1].Position);
            segment.Material = _config.GetMaterialForColor(colors[i]);
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

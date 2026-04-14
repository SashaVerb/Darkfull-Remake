using System.Collections.Generic;
using UnityEngine;

public class IndicatorDetector
{
    private readonly BeamConfig _config;
    private HashSet<IIndicator> _activeIndicators = new();
    private HashSet<IIndicator> _currentIndicators = new();
    private readonly Collider[] _overlapBuffer = new Collider[32];

    public IndicatorDetector(BeamConfig config)
    {
        _config = config;
    }

    public void Check(List<Vector3> points)
    {
        _currentIndicators.Clear();

        foreach (Vector3 point in points)
        {
            int count = Physics.OverlapSphereNonAlloc(point, _config.IndicatorDetectionRadius, _overlapBuffer, _config.IndicatorLayerMask);

            for (int i = 0; i < count; i++)
            {
                if (_overlapBuffer[i].TryGetComponent(out IIndicator indicator))
                {
                    if (_currentIndicators.Add(indicator))
                        indicator.Activate();
                }
            }
        }

        foreach (IIndicator indicator in _activeIndicators)
        {
            if (!_currentIndicators.Contains(indicator))
                indicator.Deactivate();
        }

        (_activeIndicators, _currentIndicators) = (_currentIndicators, _activeIndicators);
    }

    public void Clear()
    {
        foreach (IIndicator indicator in _activeIndicators)
            indicator.Deactivate();

        _activeIndicators.Clear();
    }
}

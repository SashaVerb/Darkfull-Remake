using System.Collections.Generic;
using Features.Beam;
using Modules.Interactable;
using UnityEngine;

public class BeamIndicatorSystem
{
    private readonly BeamConfig _config;

    private HashSet<IInteractable> _activeIndicators = new();

    private HashSet<IInteractable> _currentIndicators = new();

    private readonly Collider[] _overlapBuffer = new Collider[32];

    public void Check(in List<BeamPathPoint> points)
    {
        _currentIndicators.Clear();

        foreach (BeamPathPoint point in points)
        {
            int count = Physics.OverlapSphereNonAlloc(point.Position, _config.IndicatorDetectionRadius, _overlapBuffer, _config.IndicatorLayerMask);

            for (int i = 0; i < count; i++)
            {
                if (_overlapBuffer[i].TryGetComponent(out IInteractable indicator))
                {
                    if (_currentIndicators.Add(indicator))
                        indicator.Activate();
                }
            }
        }

        foreach (IInteractable indicator in _activeIndicators)
        {
            if (!_currentIndicators.Contains(indicator))
                indicator.Deactivate();
        }

        (_activeIndicators, _currentIndicators) = (_currentIndicators, _activeIndicators);
    }

    public BeamIndicatorSystem(BeamConfig config)
    {
        _config = config;
    }

    public void Clear()
    {
        foreach (IInteractable indicator in _activeIndicators)
            indicator.Deactivate();

        _activeIndicators.Clear();
    }
}

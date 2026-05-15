using System.Collections.Generic;
using _App.Scripts.Modules.Interactable;
using Features.Beam;
using Features.Beam.ColorSystem;
using Modules.Interactable;
using UnityEngine;
using VContainer;

public class BeamIndicatorSystem
{
    private readonly BeamConfig _config;
    private readonly IObjectResolver _resolver;

    private HashSet<Interactable> _activeIndicators = new();

    private HashSet<Interactable> _currentIndicators = new();

    private readonly Collider[] _overlapBuffer = new Collider[32];

    public void Check(in List<BeamPathPoint> points, IReadOnlyList<BeamColor> colors = null)
    {
        _currentIndicators.Clear();

        for (int pointIndex = 0; pointIndex < points.Count; pointIndex++)
        {
            BeamPathPoint point = points[pointIndex];
            BeamColor currentColor = (colors != null && pointIndex < colors.Count) ? colors[pointIndex] : default;

            int count = Physics.OverlapSphereNonAlloc(point.Position, _config.IndicatorDetectionRadius, _overlapBuffer, _config.IndicatorLayerMask);

            for (int i = 0; i < count; i++)
            {
                if (_overlapBuffer[i].TryGetComponent(out Interactable indicator))
                {
                    if (colors != null && _overlapBuffer[i].TryGetComponent(out BeamColorFilter colorFilter))
                    {
                        if (!colorFilter.AcceptsColor(currentColor))
                            continue;
                    }

                    if (_currentIndicators.Add(indicator))
                    {
                        if (_overlapBuffer[i].TryGetComponent(out InteractionContext context))
                            context.Provide(_resolver);

                        indicator.Activate();
                    }
                }
            }
        }

        foreach (Interactable indicator in _activeIndicators)
        {
            if (!_currentIndicators.Contains(indicator))
            {
                indicator.Deactivate();
            }
        }

        (_activeIndicators, _currentIndicators) = (_currentIndicators, _activeIndicators);
    }

    public BeamIndicatorSystem(BeamConfig config, IObjectResolver resolver)
    {
        _config = config;
        _resolver = resolver;
    }

    public void Clear()
    {
        foreach (Interactable indicator in _activeIndicators)
        {
            indicator.Deactivate();
        }

        _activeIndicators.Clear();
    }
}

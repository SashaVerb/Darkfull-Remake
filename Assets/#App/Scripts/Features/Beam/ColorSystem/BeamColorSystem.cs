using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Beam.ColorSystem
{
    public class BeamColorSystem
    {
        private readonly BeamConfig _config;
        
        public BeamColorSystem(BeamConfig config)
        {
            _config = config;
        }
        
        public void GetColors(List<BeamPathPoint> points, BeamColor startColor, ref List<BeamColor> colors)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));

            colors ??= new List<BeamColor>(points.Count);
            colors.Clear();

            BeamColor currentColor = startColor;

            for (int i = 0; i < points.Count; i++)
            {
                var point = points[i];

                if (point.Hit.HasValue)
                {
                    Collider hitCollider = point.Hit.Value.collider;
                    if (hitCollider != null && hitCollider.CompareTag(_config.ColorTagHandle))
                    {
                        if (hitCollider.TryGetComponent(out BeamColorChanger changer))
                            currentColor = changer.Color;
                    }
                }

                colors.Add(currentColor);
            }
        }
    }
}
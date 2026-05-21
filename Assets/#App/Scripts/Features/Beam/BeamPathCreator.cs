using System.Collections.Generic;
using Features.Beam;
using LayerMaskExtensions;
using UnityEngine;

public class BeamPathCreator
{
    private readonly BeamConfig _config;
    
    private float distanceLeft;
    
    public BeamPathCreator(BeamConfig config)
    {
        _config = config;
    }

    public void Emit(Vector3 startPoint, Vector3 direction, ref List<BeamPathPoint> points)
    {
        points ??= new List<BeamPathPoint>();
        points.Clear();
        
        points.Add(new BeamPathPoint(startPoint));

        Vector3 currentOrigin = startPoint;
        Vector3 currentDirection = direction.normalized;
        distanceLeft = _config.MaxDistance;

        while (distanceLeft > 0f)
        {
            Ray ray = new Ray(currentOrigin + currentDirection * 0.01f, currentDirection);

            if (Physics.Raycast(ray, out RaycastHit hit, distanceLeft, _config.RaycastMask.value))
            {
                points.Add(new BeamPathPoint(hit.point, hit));
                distanceLeft -= hit.distance;

                if (!_config.ReflectableMask.Contains(hit.collider.gameObject))
                    break;

                currentOrigin = hit.point;
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
            }
            else
            {
                points.Add(new BeamPathPoint(currentOrigin + currentDirection * distanceLeft));
                break;
            }
        }
    }
}

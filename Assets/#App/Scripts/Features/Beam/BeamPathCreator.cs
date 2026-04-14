using System.Collections.Generic;
using LayerMaskExtensions;
using UnityEngine;
using UnityEngine.Pool;

public class BeamPathCreator
{
    private readonly BeamConfig _config;

    public BeamPathCreator(BeamConfig config)
    {
        _config = config;
    }

    public List<Vector3> Emit(Vector3 startPoint, Vector3 direction)
    {
        List<Vector3> points = ListPool<Vector3>.Get();
        points.Add(startPoint);

        Vector3 currentOrigin = startPoint;
        Vector3 currentDirection = direction.normalized;
        float distanceLeft = _config.MaxDistance;

        while (distanceLeft > 0f)
        {
            Ray ray = new Ray(currentOrigin + currentDirection * 0.01f, currentDirection);

            if (Physics.Raycast(ray, out RaycastHit hit, distanceLeft, _config.RaycastMask.value))
            {
                points.Add(hit.point);
                distanceLeft -= hit.distance;

                if (!_config.ReflectableMask.Contains(hit.collider.gameObject))
                    break;

                currentOrigin = hit.point;
                currentDirection = Vector3.Reflect(currentDirection, hit.normal);
            }
            else
            {
                points.Add(currentOrigin + currentDirection * distanceLeft);
                break;
            }
        }

        return points;
    }
}

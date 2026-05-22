using System.Collections.Generic;
using Features.Beam;
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

    public void Emit(Vector3 startPoint, Vector3 direction, ref List<BeamPathPoint> points)
    {
        points ??= new List<BeamPathPoint>();
        points.Clear();
        
        points.Add(new BeamPathPoint(startPoint));

        Vector3 currentOrigin = startPoint;
        Vector3 currentDirection = direction.normalized;
        float distanceLeft = _config.MaxDistance;
        float currentIOR = 1f;

        while (distanceLeft > 0f)
        {
            bool prevBackfaces = Physics.queriesHitBackfaces;
            Physics.queriesHitBackfaces = currentIOR > 1f;

            Ray ray = new Ray(currentOrigin + currentDirection * 0.01f, currentDirection);
            bool didHit = Physics.Raycast(ray, out RaycastHit hit, distanceLeft, _config.RaycastMask.value);

            Physics.queriesHitBackfaces = prevBackfaces;

            if (didHit)
            {
                points.Add(new BeamPathPoint(hit.point, hit));
                distanceLeft -= hit.distance;

                if (_config.RefractableMask.Contains(hit.collider.gameObject) &&
                    hit.collider.TryGetComponent<RefractiveObject>(out var refractive))
                {
                    bool entering = Vector3.Dot(currentDirection, hit.normal) < 0;
                    float eta = entering ? (currentIOR / refractive.IOR) : (refractive.IOR / 1f);
                    Vector3 normal = entering ? hit.normal : -hit.normal;

                    currentOrigin = hit.point;
                    currentDirection = Refract(currentDirection, normal, eta);
                    currentIOR = entering ? refractive.IOR : 1f;
                }
                else if (_config.ReflectableMask.Contains(hit.collider.gameObject))
                {
                    currentOrigin = hit.point;
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                }
                else
                {
                    break;
                }
            }
            else
            {
                points.Add(new BeamPathPoint(currentOrigin + currentDirection * distanceLeft));
                break;
            }
        }
    }

    private static Vector3 Refract(Vector3 incident, Vector3 normal, float eta)
    {
        float cosI = Vector3.Dot(-incident, normal);
        float sin2T = eta * eta * (1f - cosI * cosI);
        if (sin2T >= 1f) return Vector3.Reflect(incident, normal);
        return eta * incident + (eta * cosI - Mathf.Sqrt(1f - sin2T)) * normal;
    }
}

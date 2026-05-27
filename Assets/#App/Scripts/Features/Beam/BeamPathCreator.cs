using LayerMaskExtensions;
using System.Collections.Generic;
using Features.Beam;
using UnityEngine;

public class BeamPathCreator
{
    public float TargetDistanceLeft { get; set; }
    public float ActiveDistanceLeft { get; set; }
    
    private readonly BeamConfig _config;
    
    private HashSet<DistanceExtenderObject> _activeExtenders = new();
    private HashSet<DistanceExtenderObject> _currentExtenders = new();
    
    public BeamPathCreator(BeamConfig config)
    {
        _config = config;
    }

    public void Emit(Vector3 startPoint, Vector3 direction, bool isShooting, ref List<BeamPathPoint> points)
    {
        points ??= new List<BeamPathPoint>();
        points.Clear();
        _currentExtenders.Clear();
        
        points.Add(new BeamPathPoint(startPoint));

        Vector3 currentOrigin = startPoint;
        Vector3 currentDirection = direction.normalized;

        if (isShooting)
        {
            float extenderBonus = 0f;
            foreach (var ext in _activeExtenders)
                extenderBonus += ext.BonusDistance;
            TargetDistanceLeft = _config.MaxDistance + extenderBonus;
        }
        else
        {
            TargetDistanceLeft = 0f;
        }

        if (!Mathf.Approximately(TargetDistanceLeft, ActiveDistanceLeft))
        {
            if (TargetDistanceLeft > ActiveDistanceLeft)
            {
                ActiveDistanceLeft += _config.Speed * Time.deltaTime;
                ActiveDistanceLeft = Mathf.Min(TargetDistanceLeft, ActiveDistanceLeft);
            }
            else
            {
                ActiveDistanceLeft -= _config.Speed * Time.deltaTime;
                ActiveDistanceLeft = Mathf.Max(TargetDistanceLeft, ActiveDistanceLeft);
            }
        }
        
        float currentDistanceLeft = ActiveDistanceLeft;
        
        float currentIOR = 1f;

        while (currentDistanceLeft > 0f)
        {
            bool prevBackfaces = Physics.queriesHitBackfaces;
            Physics.queriesHitBackfaces = currentIOR > 1f;

            Ray ray = new Ray(currentOrigin + currentDirection * 0.01f, currentDirection);
            bool didHit = Physics.Raycast(ray, out RaycastHit hit, currentDistanceLeft, _config.RaycastMask.value);

            Physics.queriesHitBackfaces = prevBackfaces;

            if (didHit)
            {
                points.Add(new BeamPathPoint(hit.point, hit));
                currentDistanceLeft -= hit.distance;

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
                else if (_config.DistanceExtenderMask.Contains(hit.collider.gameObject) &&
                         hit.collider.TryGetComponent<DistanceExtenderObject>(out var extender))
                {
                    _currentExtenders.Add(extender);
                    currentOrigin = hit.point;
                }
                else if (_config.ReflectableMask.Contains(hit.collider.gameObject))
                {
                    currentOrigin = hit.point;
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                }
                else
                {
                    TargetDistanceLeft -= currentDistanceLeft;
                    break;
                }
            }
            else
            {
                points.Add(new BeamPathPoint(currentOrigin + currentDirection * currentDistanceLeft));
                break;
            }
        }

        (_activeExtenders, _currentExtenders) = (_currentExtenders, _activeExtenders);
    }
    
    private static Vector3 Refract(Vector3 incident, Vector3 normal, float eta)
    {
        float cosI = Vector3.Dot(-incident, normal);
        float sin2T = eta * eta * (1f - cosI * cosI);
        if (sin2T >= 1f) return Vector3.Reflect(incident, normal);
        return eta * incident + (eta * cosI - Mathf.Sqrt(1f - sin2T)) * normal;
    }
}
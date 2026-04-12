using System.Collections.Generic;
using LayerMaskExtensions;
using Unity.VisualScripting;
using UnityEngine;

public class BeamPathCreator : MonoBehaviour
{
    [SerializeField] float maxDistance = 100f;
    [SerializeField] LayerMask raycastMask;
    [SerializeField] LayerMask reflectableMask;

    public List<Vector3> Emit(Vector3 startPoint, Vector3 direction)
    {
        List<Vector3> points = ListPool<Vector3>.New();
        points.Add(startPoint);

        Vector3 currentOrigin = startPoint;
        Vector3 currentDirection = direction.normalized;
        float distanceLeft = maxDistance;

        while (distanceLeft > 0f)
        {
            Ray ray = new Ray(currentOrigin + currentDirection * 0.01f, currentDirection);

            if (Physics.Raycast(ray, out RaycastHit hit, distanceLeft, raycastMask.value))
            {
                points.Add(hit.point);
                distanceLeft -= hit.distance;

                if (!reflectableMask.Contains(hit.collider.gameObject))
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

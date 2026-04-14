using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.MouseFollower
{
    public enum ProjectionPlane
    {
        XZ,
        XY,
        YZ
    }

    public class MouseWorldFollower : MonoBehaviour
    {
        [SerializeField] private ProjectionPlane _projectionPlane;
        [SerializeField] private Transform _planePivot;
        [SerializeField] private Camera _camera;

        private void Update()
        {
            Vector3? worldPosition = GetMouseWorldPosition();
            if (worldPosition.HasValue)
                transform.position = worldPosition.Value;
        }

        public Vector3? GetMouseWorldPosition()
        {
            if (_camera == null)
                return null;

            Mouse mouse = Mouse.current;
            if (mouse == null)
                return null;

            Ray ray = _camera.ScreenPointToRay(mouse.position.ReadValue());

            Plane plane = _projectionPlane switch
            {
                ProjectionPlane.XZ => new Plane(Vector3.up, _planePivot.position),
                ProjectionPlane.XY => new Plane(Vector3.forward, _planePivot.position),
                ProjectionPlane.YZ => new Plane(Vector3.right, _planePivot.position),
            };

            if (plane.Raycast(ray, out float distance))
                return ray.GetPoint(distance);

            return null;
        }
        private void OnDrawGizmos()
        {
            if (_planePivot == null)
                return;
            
            Gizmos.color = Color.yellow;

            Quaternion rotation = _projectionPlane switch
            {
                ProjectionPlane.XZ => Quaternion.identity,
                ProjectionPlane.XY => Quaternion.Euler(90f, 0f, 0f),
                ProjectionPlane.YZ => Quaternion.Euler(0f, 90f, 0f),
            };

            Matrix4x4 oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(_planePivot.position, rotation, Vector3.one);
            Gizmos.DrawCube(Vector3.zero, new Vector3(2f, 0f, 2f));
            Gizmos.color = new Color(0f, 1f, 0.5f, 1f);
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(2f, 0f, 2f));
            Gizmos.matrix = oldMatrix;
        }
    }
}

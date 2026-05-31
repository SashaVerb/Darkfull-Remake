using UnityEngine;

namespace _App.Scripts.Features.GameElements
{
    public class Rotating : MonoBehaviour
    {
        public float rotationDegreesSpeed = 90f;

        public Vector3 rotationAxis = Vector3.up;
        
        void Update()
        {
            transform.Rotate(rotationAxis * rotationDegreesSpeed * Time.deltaTime);
        }
    }
}
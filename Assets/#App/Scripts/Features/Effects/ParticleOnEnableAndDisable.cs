using UnityEngine;

public class ParticleOnEnableAndDisable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _particleSystem.Play();
    }
    
    private void OnDisable()
    {
        _particleSystem.Play();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class DragableObjectSound : MonoBehaviour
{
    [SerializeField] private AudioSource dragSoundSource;
    [SerializeField] private AudioSource crashSoundSource;

    [SerializeField] private float speedForCrashSound = 1f;
    [SerializeField] private float speedForDragSound = 1f;
    [SerializeField] private float hitSoundMinInterval = 1f;

    private float lastHitTime = 0f;
    
    private readonly HashSet<int> _currentColliders = new(); 
    
    private void OnCollisionEnter(Collision collision)
    {
        _currentColliders.Add(collision.gameObject.GetInstanceID());
        
        if (collision.relativeVelocity.magnitude >= speedForCrashSound &&
        Time.time >= lastHitTime + hitSoundMinInterval)
        {
            lastHitTime = Time.time;
            crashSoundSource.PlayOneShot(crashSoundSource.clip);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= speedForDragSound)
        {
            if(!dragSoundSource.isPlaying)
                dragSoundSource.Play();
        }
        else
        {
            if(dragSoundSource.isPlaying)
                dragSoundSource.Stop();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _currentColliders.Remove(collision.gameObject.GetInstanceID());
        
        if(_currentColliders.Count == 0)
            dragSoundSource.Stop();
    }
}

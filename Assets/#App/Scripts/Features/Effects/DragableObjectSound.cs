using UnityEngine;

public class DragableObjectSound : MonoBehaviour
{
    [SerializeField] private AudioSource dragSoundSource;
    [SerializeField] private AudioSource crashSoundSource;

    [SerializeField] private float speedForCrashSound = 1f;
    [SerializeField] private float speedForDragSound = 1f;
    [SerializeField] private float hitSoundMinInterval = 1f;

    private float lastHitTime = 0f;

    private void Awake()
    {
        dragSoundSource.Play();
        dragSoundSource.Pause();
    }

    private void OnCollisionEnter(Collision collision)
    {
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
            dragSoundSource.UnPause();
        }
        else
        {
            dragSoundSource.Pause();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        dragSoundSource.Pause();
    }
}

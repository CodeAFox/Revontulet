using UnityEngine;

public class MovementAudio
{
    private Vector2 position;
    private Transform objectPosition;
    private AudioSource audioSource;

    public MovementAudio(Transform gameObject, AudioSource audioSource)
    {
        this.objectPosition = gameObject;
        this.audioSource = audioSource;

        position = objectPosition.position;
    }

    public void MovedAway(float minDistance)
    {
        if(Vector2.Distance(objectPosition.position, position) > minDistance)
        {
            audioSource.Play();
            position = objectPosition.position;
        }
    }
}
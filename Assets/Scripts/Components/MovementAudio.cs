using UnityEngine;

public class MovementAudio
{
    private Vector2 position;
    private readonly Transform objectPosition;
    private readonly AudioSource audioSource;

    public MovementAudio(Transform gameObject, AudioSource audioSource)
    {
        objectPosition = gameObject;
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
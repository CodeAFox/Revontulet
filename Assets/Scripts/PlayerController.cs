using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private float movementX;
    private float movementY;
    private MovementAnimator animationLogic;
    private AudioSource audioSource;
    private Vector2 position;

    public Animator anim;
    public float speed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        position = transform.position;
        
        transform.GetComponent<Rigidbody2D>().freezeRotation = true;
        animationLogic = new MovementAnimator(anim, player.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.MovePosition(
            player.position + 
            new Vector2(movementX, movementY) * 
            speed * Time.fixedDeltaTime);

        if(Vector2.Distance(transform.position, position) > 2)
        {
            audioSource.Play();
            position = transform.position;
        }
    }

    void OnMove(InputValue movementValue)
    {
        audioSource.Play();
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        animationLogic.AnimateMovement(movementX, movementY);
    }
}

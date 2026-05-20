using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private float movementX;
    private float movementY;
    private MovementAnimator animationLogic;

    public Animator anim;
    public float speed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        
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
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        animationLogic.AnimateMovement(movementX, movementY);
    }
}

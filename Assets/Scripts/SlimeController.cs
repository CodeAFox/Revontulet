using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    public Transform player;
    public float speed = 1;
    public Animator anim;
    public Rigidbody2D slimeRB {get; private set;}
    public MovementAnimator animationLogic {get; private set;}
    public event Action SlimeCaptured;
    private ISlimeState state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slimeRB = GetComponent<Rigidbody2D>();
         
        slimeRB.position = SpawnAwayFromPlayer(player, 3);
        transform.GetComponent<Rigidbody2D>().freezeRotation = true;

        animationLogic = new MovementAnimator(anim, slimeRB.gameObject);
        state = new WanderingSlimeState(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        state.InteractWithPlayer();
        state.Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            state.Collision();
        }
        if(collision.gameObject.CompareTag("Chest"))
        {
            SlimeCaptured?.Invoke();
            state.Captured();
        }
    }

    public Vector2 GetDistanceFromPlayer()
    {
        return new Vector2(player.position.x - slimeRB.position.x, player.position.y - slimeRB.position.y);
    }

    public void ChangeState(ISlimeState state)
    {
        this.state = state;
    }

    private Vector2 SpawnAwayFromPlayer(Transform player, int magnitude)
    {
        Vector2 randVector = UnityEngine.Random.insideUnitCircle.normalized * magnitude;
        return new Vector2(player.position.x + randVector.x, player.position.y + randVector.y);
    }
}

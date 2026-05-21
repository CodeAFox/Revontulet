using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    public Transform player;
    public float speed = 1;
    public SlimeTypeEnum type = SlimeTypeEnum.Simple;
    public Animator anim;
    public Rigidbody2D slimeRB {get; private set;}
    public MovementAnimator animationLogic {get; private set;}
    private ISlimeState state;
    private AudioSource audioSource;
    private Vector2 position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slimeRB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
         
        slimeRB.position = SpawnAwayFromPlayer(player, 3);
        position = transform.position;
        transform.GetComponent<Rigidbody2D>().freezeRotation = true;

        animationLogic = new MovementAnimator(anim, slimeRB.gameObject);
        state = new WanderingSlimeState(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        state.InteractWith();
        state.Move();

        int minDistance = type == SlimeTypeEnum.Simple ? 1 : 3;
        if(Vector2.Distance(transform.position, position) > minDistance)
        {
            audioSource.Play();
            position = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            state.Collision();
        }
        if(collision.gameObject.CompareTag("Chest"))
        {
            state.Captured();
        }
    }

    public Vector2 GetDistanceFromPlayer()
    {
        return new Vector2(player.position.x - slimeRB.position.x, player.position.y - slimeRB.position.y);
    }

    public Vector2 GetClosestChestDistance()
    {
        List<GameObject> chests = GameObject.FindGameObjectsWithTag("Chest").ToList();

        float minDistance = GetDistanceFromPlayer().magnitude;
        Vector2 closestChest = GetDistanceFromPlayer();

        for (int i = 0; i < chests.Count; i++)
        {
            Vector2 chest = new Vector2(chests[i].transform.position.x - slimeRB.position.x, chests[i].transform.position.y - slimeRB.position.y);
            
            if(minDistance > chest.magnitude)
            {
                closestChest = chest;
                minDistance = closestChest.magnitude;
            }
        }
        return closestChest;
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

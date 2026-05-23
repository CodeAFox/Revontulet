using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    public GameObject player;
    public float speed = 1;
    public SlimeTypeEnum type = SlimeTypeEnum.Simple;
    public Animator anim;
    public Rigidbody2D slimeRB {get; private set;}
    public MovementAnimator animationLogic {get; private set;}
    private MovementAudio audioLogic;
    private SlimeLogic logic;
    private ISlimeState state;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slimeRB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
        logic = new SlimeLogic(player, gameObject);
        logic.SpawnAwayFromPlayer(3);

        transform.GetComponent<Rigidbody2D>().freezeRotation = true;

        animationLogic = new MovementAnimator(anim, slimeRB.gameObject);
        audioLogic = new MovementAudio(transform, audioSource);
        state = new WanderingSlimeState(this, logic);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        state.InteractWith();
        state.Move();

        float minDistance = type == SlimeTypeEnum.Simple ? 1 : 3;
        audioLogic.MovedAway(minDistance);
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

    public void ChangeState(ISlimeState state)
    {
        this.state = state;
    }
}

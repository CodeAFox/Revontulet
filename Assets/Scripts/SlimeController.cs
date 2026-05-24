using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]

public class SlimeController : MonoBehaviour
{
    public GameObject player;
    public float speed = 1;
    public SlimeTypeEnum type = SlimeTypeEnum.Simple;
    public Animator anim;

    public Rigidbody2D SlimeRB {get; private set;}
    public MovementAnimator AnimationLogic {get; private set;}
    
    private MovementAudio audioLogic;
    private SlimeLogic logic;
    private ISlimeState state;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SlimeRB = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
        logic = new SlimeLogic(player, gameObject);
        logic.SpawnAwayFromPlayer(3);

        transform.GetComponent<Rigidbody2D>().freezeRotation = true;

        AnimationLogic = new MovementAnimator(anim, SlimeRB.gameObject);
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

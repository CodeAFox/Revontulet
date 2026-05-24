using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class ChestController : MonoBehaviour
{
    public Animator anim;
    public BoxCollider2D BoxCollider {get; private set;}

    private IChestState state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoxCollider = GetComponent<BoxCollider2D>();
        state = new EmptyChestState(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            state.Capture();
        }
    }

    public void ChangeState(IChestState state)
    {
        this.state = state;
    }
}

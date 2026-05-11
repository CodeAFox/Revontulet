using UnityEngine;

public class ChestController : MonoBehaviour
{
    public Animator anim;

    private BoxCollider2D boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            boxCollider.enabled = false;
            anim.SetBool("slime_captured", true);
        }
    }
}

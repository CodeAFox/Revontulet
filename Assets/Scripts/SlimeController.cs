using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    public Transform player;
    public float speed = 1;
    private Rigidbody2D slimeRB;
    private Vector2 movement;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slimeRB = GetComponent<Rigidbody2D>();
        slimeRB.position = SpawnAwayFromPlayer(player, 3);

        transform.GetComponent<Rigidbody2D>().freezeRotation = true;

        movement = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        
        slimeRB.MovePosition(slimeRB.position + movement * speed * Time.fixedDeltaTime);

        if(timer <= 0)
        {
            ChangeMovement();
            timer = Random.Range(1, 5);
        }

        RunFromPlayer(player);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Border"))
        {
            movement = -movement;
        }
        if(collision.gameObject.CompareTag("Chest"))
        {
            slimeRB.gameObject.SetActive(false);
        }
    }

    private void ChangeMovement()
    {
        movement = Random.insideUnitCircle.normalized;
    }

    private void RunFromPlayer(Transform player)
    {
        Vector2 distance = new Vector2(player.position.x - slimeRB.position.x, player.position.y - slimeRB.position.y);

        if(distance.magnitude < 2)
        {
            movement = - distance.normalized;
        }
    }

    // Should be moved to a parent class to be used by other enemies too
    private Vector2 SpawnAwayFromPlayer(Transform player, int magnitude)
    {
        Vector2 randVector = Random.insideUnitCircle.normalized * magnitude;
        return new Vector2(player.position.x + randVector.x, player.position.y + randVector.y);
    }
}

using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D slimeRB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slimeRB = GetComponent<Rigidbody2D>();
        slimeRB.MovePosition(SpawnAwayFromPlayer(player, 3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Should be moved to a parent class to be used by other enemies too
    private Vector2 SpawnAwayFromPlayer(Transform player, int magnitude)
    {
        Vector2 randVector = Random.insideUnitCircle.normalized * magnitude;
        return new Vector2(player.position.x + randVector.x, player.position.y + randVector.y);
    }
}

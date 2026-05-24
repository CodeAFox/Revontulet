using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        transform.position = new(player.transform.position.x, player.transform.position.y, -10);
    }
}

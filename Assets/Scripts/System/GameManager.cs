using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager instance;
    private SlimeController slimeController;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
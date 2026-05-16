using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int slimesInCurrentLevel;
    public GameObject continueScreen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        GetNumOfActiveSlimesOnLevel();
        Debug.Log(slimesInCurrentLevel);
    }

    public void LateUpdate()
    {
        
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    private void GetNumOfActiveSlimesOnLevel()
    {
        //slimesInCurrentLevel = GameObject.FindGameObjectsWithTag("Target").Length;
         slimesInCurrentLevel = GameObject.FindGameObjectsWithTag("Target")
            .ToList<GameObject>()
            .Where(target => target.activeSelf)
            .Count();
    }
}
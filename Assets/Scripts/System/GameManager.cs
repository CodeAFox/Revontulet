using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int slimesInCurrentLevel;
    
    public GameObject continueScreen;

    public static bool paused = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
    }

    public void LateUpdate()
    {
        GetNumOfActiveSlimesOnLevel();
        if(slimesInCurrentLevel <= 0)
        {
            continueScreen.SetActive(true);
            Pause();
        }
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
    }

    public static void Resume()
    {
        Time.timeScale = 1f;
        paused = false;
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
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool paused = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        //continueScreenController = GetComponent<MonoScript>();
    }

    public void LateUpdate()
    {
        
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

    public static int GetNumOfActiveSlimesOnLevel()
    {
         return GameObject.FindGameObjectsWithTag("Target")
            .ToList<GameObject>()
            .Where(target => target.activeSelf)
            .Count();
    }

    public static Scene GetNextLevel()
    {
        //Throws an error as it only returns one scene????
        return SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
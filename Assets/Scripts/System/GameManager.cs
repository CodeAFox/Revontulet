using System;
using System.Collections.Generic;
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
        //DontDestroyOnLoad(gameObject);
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

    public static string GetNextLevel()
    {
        List<string> scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToList();

        int currentScene = scenes.FindIndex(scene => scene.Equals("Assets/Scenes/" + SceneManager.GetActiveScene().name + ".unity"));

        return scenes[scenes.Count == currentScene ++ ? currentScene : currentScene ++];
    }
}
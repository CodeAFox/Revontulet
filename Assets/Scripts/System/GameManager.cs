using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool paused = false;

    public void Start()
    {
        Resume();
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
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if(SceneManager.sceneCountInBuildSettings > ++ currentScene)
        {
            return SceneUtility.GetScenePathByBuildIndex(currentScene ++);
        }

        return SceneUtility.GetScenePathByBuildIndex(0);
    }
}
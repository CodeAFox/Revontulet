using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScreenController : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseScreen;

    public GameObject firstSelected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        paused = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void EnterSettings()
    {
        //todo
    }

    public void QuitGame()
    {
        GameManager.QuitGame();
    }
}

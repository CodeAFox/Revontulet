using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScreenController : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject settingsScreen;

    public GameObject firstSelected;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.activatePause == true)
        {
            Pause();
        }
        if(Input.GetButtonDown("Cancel"))
        {
            if(GameManager.paused)
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
        GameManager.activatePause = false;

        GameManager.Pause();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        GameManager.Resume();
    }

    public void EnterSettings()
    {
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(true);
        GameManager.activateSettings = true;
    }

    public void QuitGame()
    {
        GameManager.QuitGame();
    }
}

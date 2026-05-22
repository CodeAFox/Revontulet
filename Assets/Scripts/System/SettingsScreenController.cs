using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class SettingsScreenController : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameObject pauseScreen;

    public GameObject firstSelected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.activateSettings == true)
        {
            ActivateSettingsScreen();
            GameManager.activateSettings = false;
        }
    }

    public void ActivateSettingsScreen()
    {
        GameManager.activateSettings = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void BackToPauseMenu()
    {
        settingsScreen.SetActive(false);
        pauseScreen.SetActive(true);
        GameManager.activatePause = true;
        
    }
}

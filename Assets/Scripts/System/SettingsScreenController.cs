using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsScreenController : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameObject pauseScreen;

    public GameObject firstSelected;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.activateSettings == true)
        {
            ActivateSettingsScreen();
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

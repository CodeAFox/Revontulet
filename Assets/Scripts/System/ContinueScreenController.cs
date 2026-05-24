using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ContinueScreenController : MonoBehaviour
{
    public GameObject firstSelected;
    public GameObject continueScreen;

    // Update is called once per frame
    void LateUpdate()
    {
        int  slimesInCurrentLevel = GameManager.GetNumOfActiveSlimesOnLevel();
        if(slimesInCurrentLevel <= 0 && !GameManager.paused)
        {
            ActivateContinueScreen();
        }
    }

    public void ActivateContinueScreen()
    {
        continueScreen.SetActive(true);

        GameManager.Pause();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void QuitGame()
    {
        GameManager.QuitGame();
    }

    public void ContinueToNextLevel()
    {
        SceneManager.LoadScene(GameManager.GetNextLevel(), LoadSceneMode.Single);
    }
}

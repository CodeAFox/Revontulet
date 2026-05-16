using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ContinueScreenController : MonoBehaviour
{
    public GameObject firstSelected;
    public GameObject continueScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        int  slimesInCurrentLevel = GameManager.GetNumOfActiveSlimesOnLevel();
        if(slimesInCurrentLevel <= 0)
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
        //First button gets selected, but another cannot be chosen afterwards
        SceneManager.SetActiveScene(GameManager.GetNextLevel() == null ? SceneManager.GetActiveScene() : GameManager.GetNextLevel());
    }
}

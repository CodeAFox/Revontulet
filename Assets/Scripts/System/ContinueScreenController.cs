using UnityEngine;

public class ContinueScreenController : MonoBehaviour
{
    public GameObject firstSelected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        GameManager.QuitGame();
    }

    public void ContinueToNextLevel()
    {
        //TODO
    }
}

using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        if(GameManager.Instance.GetLevel()!=0) 
        {
            GameManager.Instance.BackToMenu();
        }
        else
        {
            GameManager.Instance.GameOver();
        }
        ResumeGame();
    }
}

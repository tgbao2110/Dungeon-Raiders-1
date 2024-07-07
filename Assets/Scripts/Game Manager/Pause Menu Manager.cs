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
        var levelLoader = FindObjectOfType<LevelLoader>();
        levelLoader.LoadLevel(0);
        ResumeGame();
    }
}

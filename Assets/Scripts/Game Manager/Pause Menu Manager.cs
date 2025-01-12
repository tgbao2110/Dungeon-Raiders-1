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
        GameStateManager.Instance.SetState(GameState.Paused);
    }

    public void ResumeGame()
    {
        Debug.Log("Resuming game");
        pauseMenu.SetActive(false);
        GameStateManager.Instance.SetState(GameState.Playing);
    }

    public void ReturnToMenu()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.GetLevel() != 0)
            {
                GameManager.Instance.BackToMenu();
            }
            else
            {
                GameManager.Instance.GameOver();
            }
        }
        ResumeGame();
    }
}

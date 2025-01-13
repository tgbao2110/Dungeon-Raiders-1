using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] LevelLoader levelLoader;

    public void NewGame()
    {
        levelLoader.LoadLevel("Lobby");
    }

    public void Continue()
    {
        GameManager.Instance.ContinueGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}

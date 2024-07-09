using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] LevelLoader levelLoader;

    public void NewGame()
    {
        levelLoader.LoadLevel(1);
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

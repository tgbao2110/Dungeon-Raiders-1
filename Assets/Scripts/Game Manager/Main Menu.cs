using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]LevelLoader levelLoader;
    public void Play()
    {
        levelLoader.LoadLevel(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

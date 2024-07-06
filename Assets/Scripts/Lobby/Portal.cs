using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            var levelLoader = FindAnyObjectByType<LevelLoader>();
            levelLoader.LoadLevel(currentScene+1);
        }
    }
}

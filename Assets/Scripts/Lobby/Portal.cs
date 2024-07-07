using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.StartGame();
        }
    }
}

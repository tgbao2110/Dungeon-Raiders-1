using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        GameManager.Instance.GameOver();
    }
    public void Replay()
    {
        Time.timeScale = 1;
        GameManager.Instance.Replay();
    }
}

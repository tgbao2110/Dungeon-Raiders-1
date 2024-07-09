using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void ReturnToMenu()
    {
        GameManager.Instance.GameOver();
    }
    public void Replay()
    {
        GameManager.Instance.Replay();
    }
}

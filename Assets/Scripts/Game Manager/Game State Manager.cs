using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameStateManager is NULL!");

            return _instance;
        }
    }

    public GameState CurrentGameState { get; private set; }

    public delegate void GameStateChangeHandler(GameState newGameState);
    public static event GameStateChangeHandler OnGameStateChanged;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
            return;

        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);

        Debug.Log($"Game state changed to: {newGameState}");
        
        if (PauseManager.Instance != null)
        {
            PauseManager.Instance.SetPaused(newGameState == GameState.Paused);
        }
        else
        {
            Debug.LogWarning("PauseManager instance is not available when changing game state");
        }
    }
}

public enum GameState
{
    Playing,
    Paused,
}


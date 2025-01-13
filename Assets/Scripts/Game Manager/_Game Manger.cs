using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Mirror;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private CharacterData selectedCharacterData;
    public List<LevelData> levels;
    private int currentLevelIndex = 0;
    private int currentRoundIndex = 0;
    private int lastRoundIndex = -1;
    private int lastLevelIndex = -1;
    private DungeonGenerator dungeonGenerator;
    public WeaponData savedWeaponData;
    [SerializeField] ContinueButton continueButton;

    private int coinCount = 10; // Field to store the coin count

    private bool isSceneChanging = false;
    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        coinCount = PlayerPrefs.GetInt("CoinCount", 10); // Default to 10 if no value exists
    }
    else
    {
        Destroy(gameObject);
    }
}

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        if (scene.name == "Dungeon")
        {
            SetupDungeonScene();
            Debug.Log("----Getting player----");
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.StartGame();
        }
    }

    private void SetupDungeonScene()
    {
        Debug.Log("--Setting dungeonGenerator--");

        dungeonGenerator = GameObject.FindGameObjectWithTag("MapGenerator")?.GetComponent<DungeonGenerator>();
        if (dungeonGenerator == null)
        {
            Debug.LogError("DungeonGenerator not found!");
            return;
        }

        RoundData roundData = levels[currentLevelIndex].rounds[currentRoundIndex];
        LevelData levelData = levels[currentLevelIndex];
        dungeonGenerator.StartGame(levelData, roundData);
        Debug.Log("Dungeon Generator started: " + levelData.levelName + " " + roundData.name);
        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        eventSystem.ShowLevel();

        Debug.Log("--Setting player--");

        PlayerItemInteraction playerItemInteraction = FindObjectOfType<PlayerItemInteraction>();
        if (playerItemInteraction != null)
        {
            playerItemInteraction.LoadWeaponState();
        }

        Health health = FindObjectOfType<Health>();
        if (currentLevelIndex == 0 && currentRoundIndex == 0)
        {
            health.StartGame();
        }
    }

    public int GetLevel()
    {
        return currentLevelIndex + 1;
    }

    public int GetRound()
    {
        return currentRoundIndex + 1;
    }

    public int GetLastLevel()
    {
        return lastLevelIndex + 1;
    }

    public void SetSelectedCharacter(CharacterData data)
    {
        selectedCharacterData = data;
    }

    public void StartGame()
    {
        LoadLevel(0);
        savedWeaponData = null;
    }

    public void LoadLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        currentRoundIndex = 0;
        LoadRound(currentRoundIndex);
    }

    public void LoadNextRound()
    {
        if (isSceneChanging)
        {
            Debug.LogWarning("Cannot load next round while a scene change is in progress.");
            return; // Prevent starting a new round load if a scene change is already happening
        }

        Debug.Log("LOADING NEXT ROUND...");
        PlayerItemInteraction playerItemInteraction = FindObjectOfType<PlayerItemInteraction>();
        if (playerItemInteraction != null)
        {
            playerItemInteraction.SaveWeaponState();
        }

        currentRoundIndex++;
        if (currentRoundIndex < levels[currentLevelIndex].rounds.Count)
        {
            LoadRound(currentRoundIndex);
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levels.Count)
        {
            LoadLevel(currentLevelIndex);
        }
        else
        {
            Debug.Log("Game Complete!");
        }
    }

    void LoadRound(int roundIndex)
    {
        Debug.Log("-----------------------------------------");
        StartCoroutine(LoadDungeonSceneCoroutine(roundIndex));
    }

     private IEnumerator LoadDungeonSceneCoroutine(int roundIndex)
    {
        if (isSceneChanging)
        {
            Debug.LogWarning("Scene change already in progress. Skipping additional request.");
            yield break; // Prevent duplicate scene changes
        }

        isSceneChanging = true;
        NetworkManager.singleton.ServerChangeScene("Dungeon");
        Debug.Log("--Loading Dungeon Scene--");

        // Wait for the scene to load
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "Dungeon");

        isSceneChanging = false; // Reset flag when scene change is complete
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Restart()
    {
        LoadRound(currentRoundIndex);
    }

    public void BackToMenu()
{
    // Save the current round data
    SaveCurrentRound();

    // Check if the server or client is active and stop them
    if (NetworkServer.active) // Check if the current instance is hosting as a server
    {
        NetworkManager.singleton.StopHost(); // Stops both server and client on the host
        Debug.Log("Server stopped.");
    }
    else if (NetworkClient.isConnected) // Check if the current instance is a client
    {
        NetworkManager.singleton.StopClient(); // Disconnects the client from the server
        Debug.Log("Client disconnected.");
    }

    // Load the main menu scene
    LoadMenu();
}


    public void GameOver()
    {
        currentLevelIndex = 0;
        currentRoundIndex = 0;
        lastLevelIndex = -1;
        lastRoundIndex = -1;
        savedWeaponData = null;
        LoadMenu();
    }

    public void Replay()
    {
        currentLevelIndex = 0;
        currentRoundIndex = 0;
        lastLevelIndex = -1;
        lastRoundIndex = -1;
        savedWeaponData = null;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    private void SaveCurrentRound()
    {
        lastLevelIndex = currentLevelIndex;
        lastRoundIndex = currentRoundIndex;
    }

    public void ContinueGame()
    {
        LoadLevel(lastLevelIndex);
        currentRoundIndex = lastRoundIndex;
        LoadRound(currentRoundIndex);
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
    }

    public bool RemoveCoins(int amount)
    {
        if (coinCount < amount)
        {
            return false;
        }

        coinCount -= amount;
        return true;
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

    public void ResetCoins()
    {
        coinCount = 0;
    }
}

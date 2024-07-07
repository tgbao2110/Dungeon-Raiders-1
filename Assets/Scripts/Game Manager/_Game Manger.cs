using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<LevelData> levels;
    private int currentLevelIndex = 0;
    private int currentRoundIndex = 0;
    private int selectedCharacterIndex;
    private DungeonGenerator dungeonGenerator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetLevel()
    {
        return currentLevelIndex+1;
    }

    public int GetRound()
    {
        return currentRoundIndex+1;
    }

    public void SetSelectedCharacter(int characterIndex)
    {
        //selectedCharacterIndex = characterIndex;
    }

    public void StartGame()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        currentRoundIndex = 0;
        LoadRound(currentRoundIndex);
    }

    public void LoadNextRound()
    {
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
        StartCoroutine(LoadDungeonSceneCoroutine(roundIndex));
    }

    private IEnumerator LoadDungeonSceneCoroutine(int roundIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Dungeon", LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Scene dungeonScene = SceneManager.GetSceneByName("Dungeon");
        GameObject[] rootObjects = dungeonScene.GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            dungeonGenerator = obj.GetComponent<DungeonGenerator>();
            if (dungeonGenerator != null)
            {
                break;
            }
        }

        if (dungeonGenerator != null)
        {
            RoundData roundData = levels[currentLevelIndex].rounds[roundIndex];
            dungeonGenerator.StartGame(roundData);
            Actions.OnStartNewRound.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("PlayerManager");
                _instance = obj.AddComponent<PlayerManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public string PlayerName { get; private set; }
    public bool IsHost { get; private set; }

    public void SetPlayerData(string playerName, bool isHost)
    {
        PlayerName = playerName;
        IsHost = isHost;
    }
}


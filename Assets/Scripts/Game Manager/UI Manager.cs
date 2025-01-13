using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject singleAndMultipleMenu;
    [SerializeField] private GameObject hostAndJoinMenu;
    [SerializeField] private TMP_InputField playerNameInput;

    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();

        // Đảm bảo đã có NetworkManager
        if (networkManager == null)
        {
            Debug.LogError("No NetworkManager found in the scene!");
        }
    }

    public void OpenSingleAndMultipleMenu()
    {
        mainMenu.SetActive(false);
        singleAndMultipleMenu.SetActive(true);
    }

    public void OpenHostAndJoinMenu()
    {
        singleAndMultipleMenu.SetActive(false);
        hostAndJoinMenu.SetActive(true);
    }

    public void HostGame()
    {
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            Debug.LogWarning("Player name cannot be empty!");
            return;
        }

        PlayerManager.Instance.SetPlayerData(playerNameInput.text, true);

        // Host server
        networkManager.StartHost();

        Debug.Log("Hosting server...");
    }


    public void JoinGame()
    {
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            Debug.LogWarning("Player name cannot be empty!");
            return;
        }

        PlayerManager.Instance.SetPlayerData(playerNameInput.text, false);

        // Connect as client
        networkManager.StartClient();

        Debug.Log("Joining server...");
    }
}

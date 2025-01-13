using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Mirror.Discovery;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject singleAndMultipleMenu;
    [SerializeField] private GameObject hostAndJoinMenu;
    [SerializeField] private TMP_InputField playerNameInput;

    private NetworkManager networkManager;
    private NetworkDiscovery networkDiscovery;

    private void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
        networkDiscovery = FindObjectOfType<NetworkDiscovery>();

        // Ensure the NetworkManager and NetworkDiscovery are present in the scene
        if (networkManager == null)
        {
            Debug.LogError("No NetworkManager found in the scene!");
        }
        if (networkDiscovery == null)
        {
            Debug.LogError("No NetworkDiscovery found in the scene!");
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

        // Start hosting and advertising the server
        networkManager.StartHost();
        networkDiscovery.AdvertiseServer();

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

        // Start client discovery
        networkDiscovery.StartDiscovery();

        Debug.Log("Searching for servers...");
    }
}

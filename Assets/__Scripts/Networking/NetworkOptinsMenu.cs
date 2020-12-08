using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkOptinsMenu : MonoBehaviour
{
    [SerializeField]
    private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private Button joinLobbyBtn, HostBtn = null;
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private GameObject networkMenu, selectionMenu;

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientDisconnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientDisconnected -= HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void StartHost()
    {
        networkManager.StartHost();

        selectionMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void JoinLoby()
    {
        string ipAddress = "localhost";//inputField.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinLobbyBtn.interactable = false;
    }

    private void HandleClientDisconnected()
    {
        gameObject.SetActive(true);
    }

    private void HandleClientConnected()
    {
        selectionMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

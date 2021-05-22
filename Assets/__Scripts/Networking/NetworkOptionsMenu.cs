using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkOptionsMenu : MonoBehaviour
{
    private NetworkManagerLobby networkManager;

    [Header("UI")]
    [SerializeField] private Button joinLobbyBtn, HostBtn;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject networkMenu, selectionMenu;

    private void Start()
    {
        networkManager = GameObject.FindObjectOfType<NetworkManagerLobby>();
    }

    private void OnEnable()
    {
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
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
        string ipAddress = (string.IsNullOrEmpty(inputField.text)) ? "localhost" : inputField.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
    }

    private void HandleClientDisconnected()
    {
        gameObject.SetActive(true);
    }

    private void HandleClientConnected()
    {
        joinLobbyBtn.interactable = false;

        selectionMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}

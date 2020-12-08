using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerLobby : NetworkManager
{
    [Scene, SerializeField]
    private string menuScene = string.Empty;

    //player lobby prefab
    //public GameObject playerMenuPrefab;

    public static event Action OnClientConnected, OnClientDisconnected;

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if (SceneManager.GetActiveScene().name != menuScene)
        {
            //conn.Disconnect();
            //BUG: disconectava logo que conectava por causa desse if
            return;
        }
    }

    // public override void OnServerAddPlayer(NetworkConnection conn)
    // {
    //     Debug.Log("added: " + conn.connectionId);
    //     string menuSceneName = menuScene.Substring(menuScene.LastIndexOf('/') + 1).Split('.')[0];
    //     if (SceneManager.GetActiveScene().name == menuSceneName)
    //     {
    //         GameObject playerMenuInstance = Instantiate(playerMenuPrefab);
    //         NetworkServer.AddPlayerForConnection(conn, playerMenuInstance);
    //     }
    // }
}

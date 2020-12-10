using System;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerLobby : NetworkManager
{
    [Scene, SerializeField]
    private string menuScene = string.Empty;

    public GameObject playerMenuPrefab;

    public List<PlayerMenu> LobbyPlayers {get; } = new List<PlayerMenu>();

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

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<PlayerMenu>();
            LobbyPlayers.Remove(player);
        }
        base.OnServerDisconnect(conn);
    }

    public bool IsEveryoneReadyToStart()
    {
        if( numPlayers < 2 ) return false;

        return LobbyPlayers.TrueForAll(x => x.IsReady);
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        string menuSceneName = menuScene.Substring(menuScene.LastIndexOf('/') + 1).Split('.')[0];
        if (SceneManager.GetActiveScene().name == menuSceneName)
        {
            GameObject playerMenuInstance = Instantiate(playerMenuPrefab);
            NetworkServer.AddPlayerForConnection(conn, playerMenuInstance);
        }
    }
}

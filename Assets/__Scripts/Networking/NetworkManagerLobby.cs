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
    public GameObject playerGamePrefab;

    public List<PlayerMenu> LobbyPlayers {get; } = new List<PlayerMenu>();
    public List<CharacterController> GamePlayers {get; } = new List<CharacterController>();

    public static event Action OnClientConnected, OnClientDisconnected;
    public static event Action<NetworkConnection> OnServerReadied;

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
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
        if( numPlayers < 1 ) return false;

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

    public void StartGame()
    {
        ServerChangeScene("PirateCave(S)");
    }

    public override void ServerChangeScene(string newSceneName)
    {
        for(int i = LobbyPlayers.Count - 1; i >= 0; i--)
        {
            TeamIDEnum team = LobbyPlayers[i].Team;
            CharacterSO selectedHero = LobbyPlayers[i].SelectedCharacter;

            var conn = LobbyPlayers[i].connectionToClient;
            var gamePlayerInstance = Instantiate(playerGamePrefab);
            
            var player = playerGamePrefab.GetComponent<CharacterController>();
            player.Team = team;
            player.SelectedHero = selectedHero;

            NetworkManager.Destroy(conn.identity.gameObject);
            NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject);
        }

        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        //base.OnServerSceneChanged(sceneName);
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        OnServerReadied?.Invoke(conn);
    }
}

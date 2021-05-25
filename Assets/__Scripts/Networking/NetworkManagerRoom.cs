using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManagerRoom : NetworkRoomManager
{
    private bool[] SeatsOcupied = { false, false, false, false };

    public override void OnRoomServerSceneChanged(string sceneName)
    {
        if (sceneName == GameplayScene)
        {
            //Spawner.InitialSpawn();
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log(conn.identity.gameObject.name);
        PlaceOnLeftMostSeat(conn.identity.gameObject);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }

    public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnection conn, GameObject roomPlayer, GameObject gamePlayer)
    {
        Debug.Log("X1");
        //PlayerScore playerScore = gamePlayer.GetComponent<PlayerScore>();
        //playerScore.index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
        return true;
    }

    public override void OnRoomStopClient()
    {
        base.OnRoomStopClient();
    }

    public override void OnRoomStopServer()
    {
        base.OnRoomStopServer();
    }

    public override void OnRoomServerPlayersReady()
    {
        // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
        //showStartButton = true;
#endif
    }

    private void PlaceOnLeftMostSeat(GameObject go)
    {
        for (var i = 0; i < this.SeatsOcupied.Length; i++)
        {
            if (!this.SeatsOcupied[i])
            {
                this.SeatsOcupied[i] = true;
                OcupySeat(go, i + 1);
                break;
            }
        }
    }

    private void OcupySeat(GameObject go, int seat)
    {
        string seatGameObjectName = "";

        switch (seat)
        {
            case 1:
                seatGameObjectName = "Player1";
                break;
            case 2:
                seatGameObjectName = "Player2";
                break;
            case 3:
                seatGameObjectName = "Player3";
                break;
            case 4:
                seatGameObjectName = "Player4";
                break;
        }
        PlayerSeat playerSeat = GameObject.Find(seatGameObjectName).GetComponent<PlayerSeat>();
        playerSeat.JoinPlayer(go);
    }

    private void DesocupySeat(GameObject go)
    {
        //TODO
    }

    public void StartGame()
    {
        if (allPlayersReady)
        {
            ServerChangeScene(GameplayScene);
        }
    }
}

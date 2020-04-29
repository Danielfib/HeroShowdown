using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersSelectionManager : MonoBehaviour
{
    public static bool IsEveryoneReady
    {
        get {
            PlayerMenu[] players = GameObject.FindObjectsOfType<PlayerMenu>();
            return players.Where(x => x.IsReady).Count() == players.Length;
        }
    }

    private MenuInputActions menuInputActions;
    private bool[] SeatsOcupied = { false, false, false, false };

    void Start()
    {
        menuInputActions = new MenuInputActions();
        menuInputActions.Enable();
    }

    public void OnPlayerJoined(PlayerInput playerJoinHandler)
    {
        PlaceOnLeftMostSeat(playerJoinHandler.gameObject);

        playerJoinHandler.gameObject.GetComponent<PlayerMenu>().PlayerIndex = playerJoinHandler.playerIndex;
        PlayersSettings.PlayerDataList.Add(
            new PlayerData(playerJoinHandler.playerIndex, playerJoinHandler.devices[0]));
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        //CAUTION: Following code was causing bug were OnPlayerLeft would trigger before loading scene
        //PlayerSettings toRemove = PlayersSettings.PlayerSettings.Where(x => x.playerIndex == playerInput.playerIndex).FirstOrDefault();
        //PlayersSettings.PlayerSettings.Remove(toRemove);
    }

    private void PlaceOnLeftMostSeat(GameObject go)
    {
        for(var i = 0; i < this.SeatsOcupied.Length; i++)
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

        GameObject seatGameObject = GameObject.Find(seatGameObjectName);
        go.transform.parent = seatGameObject.transform;
        go.transform.localPosition = Vector3.zero;
    }

    private void DesocupySeat(GameObject go)
    {

    }
}

public class Character
{
    public string name;
    public string spritePath;

    public Character(string name, string sprite)
    {
        this.name = name;
        this.spritePath = sprite;
    }
}
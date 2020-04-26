using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersSelectionManager : MonoBehaviour
{
    private MenuInputActions menuInputActions;

    public static Character[] availableCharacters =
    {
        new Character("warrior", "Sprites/Character_Sprite_Sheet"),
        new Character("mage", "Sprites/SpikeBall")
    };

    void Start()
    {
        menuInputActions = new MenuInputActions();
        menuInputActions.Enable();
    }

    public void OnPlayerJoined(PlayerInput playerJoinHandler)
    {
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
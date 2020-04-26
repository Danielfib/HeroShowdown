using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinHandler : MonoBehaviour
{
    public void OnPlayerJoined(PlayerInput playerJoinHandler)
    {
        PlayersSettings.PlayerSettings.Add(
            new PlayerSettings(playerJoinHandler.playerIndex, playerJoinHandler.devices[0]));
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        PlayerSettings toRemove = PlayersSettings.PlayerSettings.Where(x => x.index == playerInput.playerIndex).FirstOrDefault();
        PlayersSettings.PlayerSettings.Remove(toRemove);
    }
}

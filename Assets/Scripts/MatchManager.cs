using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchManager : MonoBehaviour
{
    public PlayerInputManager PlayerInputManager;

    void Start()
    {
        LoadPlayers();
    }

    private void LoadPlayers()
    {
        foreach (var player in PlayersSettings.PlayerSettings)
        {
            PlayerInputManager.JoinPlayer(playerIndex: player.index, pairWithDevice: player.inputDevice);
        }
    }
}

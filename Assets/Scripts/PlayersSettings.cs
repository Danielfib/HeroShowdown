using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayersSettings
{
    public static List<PlayerSettings> PlayerSettings = new List<PlayerSettings>();
}

public class PlayerSettings
{
    public int playerIndex;
    public InputDevice inputDevice;
    public Character character;

    public PlayerSettings(int index, InputDevice inputDevice)
    {
        this.playerIndex = index;
        this.inputDevice = inputDevice;
    }
}

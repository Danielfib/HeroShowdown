using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayersSettings
{
    public static List<PlayerSettings> PlayerSettings = new List<PlayerSettings>();
    public static int oi = 0;
}

public class PlayerSettings
{
    public int index;
    public InputDevice inputDevice;

    public PlayerSettings(int index, InputDevice inputDevice)
    {
        this.index = index;
        this.inputDevice = inputDevice;
    }
}

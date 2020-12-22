using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PlayersSettings
{
    public static List<PlayerData> PlayerDataList = new List<PlayerData>();
}

public class PlayerData
{
    public int playerIndex;
    public InputDevice inputDevice;
    public CharacterSO character;
    public TeamIDEnum team;

    public PlayerData(int index, InputDevice inputDevice)
    {
        this.playerIndex = index;
        this.inputDevice = inputDevice;
    }
}

public enum HeroesEnum
{
    PIRATE,
    MAGE
}

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
        foreach (var player in PlayersSettings.PlayerDataList)
        {
            PlayerInput addedPlayer = PlayerInputManager.JoinPlayer(playerIndex: player.playerIndex, 
                                                                    pairWithDevice: player.inputDevice);
            LoadPlayerHero(addedPlayer);
        }
    }

    private void LoadPlayerHero(PlayerInput player)
    {
        CharacterController playerController = player.gameObject.GetComponent<CharacterController>();
        PlayerData playerData = PlayersSettings.PlayerDataList.Find(x => x.playerIndex == player.playerIndex);

        switch (playerData.character.name)
        {
            case "mage":
                playerController.CharacterBrain = Resources.Load<MageBrain>("CharacterBrains/MageBrain");
                break;
            case "warrior":
                playerController.CharacterBrain = Resources.Load<WarriorBrain>("CharacterBrains/WarriorBrain");
                break;
        }
        player.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(playerData.character.spritePath);

        playerController.Team = playerData.team;
        SetPlayerColor(playerController.Team, player.GetComponentInChildren<SpriteRenderer>());
    }

    private void SetPlayerColor(TeamIDEnum team, SpriteRenderer renderer)
    {
        switch (team)
        {
            case TeamIDEnum.RED:
                renderer.color = ColorUtils.Red;
                break;
            case TeamIDEnum.PURPLE:
                renderer.color = ColorUtils.Purple;
                break;
            case TeamIDEnum.GREEN:
                renderer.color = ColorUtils.Green;
                break;
            case TeamIDEnum.BROWN:
                renderer.color = ColorUtils.Brown;
                break;
            case TeamIDEnum.BLUE:
                renderer.color = ColorUtils.Blue;
                break;
        }
    }

}
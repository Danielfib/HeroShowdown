using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchManager : MonoBehaviour
{
    public PlayerInputManager PlayerInputManager;

    [SerializeField]
    private float FlagScoreRespawnCooldown;
    [SerializeField]
    private float FlagRetrievedRespawnCooldown;

    [SerializeField]
    private GameObject FlagPrefab;

    void Start()
    {
        LoadPlayers();
    }

    #region [Flag_Respawn_Management]
    public void StartFlagRespawn(TeamIDEnum flagColor, bool wasRetrieved)
    {
        float cooldown = wasRetrieved ? this.FlagRetrievedRespawnCooldown : this.FlagScoreRespawnCooldown;

        StartCoroutine(FlagRespawn(cooldown, flagColor));
    }

    private IEnumerator FlagRespawn(float cooldown, TeamIDEnum flagColor)
    {
        //TODO: if needed to call update, where do i call it?
                    // maybe use DOTween to control interface
        
        yield return new WaitForSeconds(cooldown);

        SpawnNewFlag(flagColor);
    }

    private void SpawnNewFlag(TeamIDEnum flagColor)
    {
        Debug.Log("Spawned new flag!");

        GameObject flag = Instantiate(this.FlagPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        flag.GetComponentInChildren<Flag>().teamIDEnum = flagColor;
        flag.GetComponentInChildren<SpriteRenderer>().color = ColorUtils.TeamIdEnumToColor(flagColor);
    }
    #endregion

    #region [Player_Load]
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
    #endregion
}
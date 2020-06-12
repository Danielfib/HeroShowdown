﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    private GameObject UIFlagCountdownPrefab;
    [SerializeField]
    private GameObject FlagRespawnCanvas;

    [SerializeField]
    private PlayerIconsHUD PlayerIconsHUD;


    private int MaxPoints = 3;

    void Start()
    {
        LoadPlayers();
    }

    #region [Points_Management]
    public void UpdatePoints(TeamIDEnum team, int points)
    {
        if (points >= this.MaxPoints)
            EndGame(team);
    }

    private void EndGame(TeamIDEnum winningTeam)
    {
        Debug.Log("Winner: " + winningTeam);
    }
    #endregion

    #region [Flag_Respawn_Management]
    public void StartFlagRespawn(TeamIDEnum flagColor, bool wasRetrieved)
    {
        float cooldown = wasRetrieved ? this.FlagRetrievedRespawnCooldown : this.FlagScoreRespawnCooldown;

        StartCoroutine(FlagRespawn(cooldown, flagColor));
    }

    private IEnumerator FlagRespawn(float cooldown, TeamIDEnum flagColor)
    {
        CreateFlagRespawnCountdown(cooldown, flagColor);
        
        yield return new WaitForSeconds(cooldown);

        SpawnNewFlag(flagColor);
    }

    private void CreateFlagRespawnCountdown(float cooldown, TeamIDEnum team)
    {
        GameObject flagCountdown = Instantiate(this.UIFlagCountdownPrefab, this.FlagRespawnCanvas.transform);
        CircularCountdown countdown = flagCountdown.GetComponent<CircularCountdown>();
        countdown.SetupColor(team);
        countdown.StartCountdown(cooldown);
    }

    public void SpawnNewFlag(TeamIDEnum flagColor)
    {
        //chooses at random a spawnspot of given team to spawn flag into
        FlagSpawnSpot[] spawnSpots = FindObjectsOfType<FlagSpawnSpot>().Where(x => x.TeamID == flagColor).ToArray();
        int rInd = Mathf.RoundToInt(Random.Range(0, spawnSpots.Length - 1));
        Vector3 posToSpawn = spawnSpots[rInd].gameObject.transform.position;

        GameObject flag = Instantiate(this.FlagPrefab, posToSpawn, Quaternion.identity);
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
        PlayerData pd = PlayersSettings.PlayerDataList.Find(x => x.playerIndex == player.playerIndex);

        switch (pd.character.name)
        {
            case "mage":
                playerController.CharacterBrain = Resources.Load<MageBrain>("CharacterBrains/MageBrain");
                break;
            case "warrior":
                playerController.CharacterBrain = Resources.Load<WarriorBrain>("CharacterBrains/WarriorBrain");
                break;
        }
        
        playerController.Team = pd.team;
        SpriteRenderer playerSpriteRenderer = player.gameObject.GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = Resources.Load<Sprite>(pd.character.spritePath);
        playerSpriteRenderer.color = ColorUtils.TeamIdEnumToColor(pd.team);

        PositionPlayer(player.gameObject.transform, pd.team);

        //Loading HUD player icon
        PlayerHUDIconController iconController = PlayerIconsHUD.LoadPlayerIconToTeam(pd);
        playerController.PlayerHUDIconController = iconController;
    }

    private void PositionPlayer(Transform playerTransform, TeamIDEnum team)
    {
        TeamBase teamBase = GameObject.FindObjectsOfType<TeamBase>().Where(x => x.teamIdEnum == team).FirstOrDefault();
        playerTransform.position = teamBase.transform.position;
    }
    #endregion
}
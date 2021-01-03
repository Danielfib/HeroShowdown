﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchManager : Singleton<MatchManager>
{
    public EndingScreenManager EndingScreenManager;

    [SerializeField]
    private float FlagScoreRespawnCooldown;
    [SerializeField]
    private float FlagRetrievedRespawnCooldown;

    [SerializeField]
    private GameObject FlagPrefab;
    [SerializeField]
    private GameObject UIFlagCountdownPrefab;

    private int MaxPoints = 1;

    void Start()
    {
        LoadPlayers();
    }

    #region [Points_Management]
    public void UpdatePoints(TeamIDEnum team, int points)
    {
        HUDManager.Instance.UpdateTeamPoints(points, team);

        if (points >= this.MaxPoints)
            EndGame(team);
    }

    private void EndGame(TeamIDEnum winningTeam)
    {
        Debug.Log("Winner: " + winningTeam);
        EndingScreenManager.SetupEndingScreen(winningTeam);
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
        HUDManager.Instance.StartFlagRespawn(flagColor, cooldown);

        yield return new WaitForSeconds(cooldown);

        SpawnNewFlag(flagColor);
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(var player in players)
        {
            LoadPlayerHero(player);
        }
    }

    private void LoadPlayerHero(GameObject player)
    {
        CharacterController pc = player.gameObject.GetComponent<CharacterController>();

        //Loading HUD player icon
        PlayerHUDIconController iconController = HUDManager.Instance.LoadPlayerIconToTeam(pc.SelectedHero, pc.Team);
        pc.PlayerHUDIconController = iconController;
    }


    #endregion
}
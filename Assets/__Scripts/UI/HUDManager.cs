using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : Singleton<HUDManager>
{
    [SerializeField]
    private GameObject PlayerHUDIconPrefab;

    [SerializeField]
    private TeamHUD RedTeamContainer;
    [SerializeField]
    private TeamHUD BlueTeamContainer;

    public PlayerHUDIconController LoadPlayerIconToTeam(PlayerData pd)
    {
        GameObject playerIcon = Instantiate(this.PlayerHUDIconPrefab, GetTeamHUD(pd.team).membersContainer);

        Image playerImage = playerIcon.GetComponent<Image>();
        playerImage.sprite = pd.character.HUDIcon;

        return playerIcon.GetComponent<PlayerHUDIconController>();
    }

    public void StartFlagRespawn(TeamIDEnum team, float duration)
    {
        GetTeamHUD(team).StartFlagCountdown(duration);
    }

    private Transform GetTransformTeamPanel(TeamIDEnum team)
    {
        return GetTeamHUD(team).transform;
    }

    private TeamHUD GetTeamHUD(TeamIDEnum team)
    {
        switch (team)
        {
            case TeamIDEnum.RED:
                return this.RedTeamContainer;
            case TeamIDEnum.BLUE:
                return this.BlueTeamContainer;
            default:
                return null;
        }
    }

    public void UpdateTeamPoints(int points, TeamIDEnum team)
    {
        GetTeamHUD(team).UpdatePoints(points);
    }

    public void UpdateFlagStatusIcon(TeamIDEnum team, FlagStates flagState)
    {
        GetTeamHUD(team).UpdateFlagStatus(flagState);
    }
}

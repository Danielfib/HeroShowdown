using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIconsHUD : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerHUDIconPrefab;

    [SerializeField]
    private GameObject RedTeamContainer;
    [SerializeField]
    private GameObject BlueTeamContainer;

    public PlayerHUDIconController LoadPlayerIconToTeam(PlayerData pd)
    {
        GameObject playerIcon = Instantiate(this.PlayerHUDIconPrefab, GetTransformTeamPanel(pd.team));

        Image playerImage = playerIcon.GetComponent<Image>();
        playerImage.sprite = pd.character.sprite;

        return playerIcon.GetComponent<PlayerHUDIconController>();
    }

    private Transform GetTransformTeamPanel(TeamIDEnum team)
    {
        switch (team)
        {
            case TeamIDEnum.RED:
                return this.RedTeamContainer.transform;
            case TeamIDEnum.BLUE:
                return this.BlueTeamContainer.transform;
            default:
                return null;
        }
    }
}

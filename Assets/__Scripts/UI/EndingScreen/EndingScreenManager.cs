using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndingScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject seatsHorizontalLayout, seatPrefab;

    [SerializeField]
    private TextMeshProUGUI winningTeamText;

    private void Update()
    {
        //Escape on keyboard or B on xbox controller
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 1"))
        {
            SceneManager.LoadScene("SelectionScreen");
        }
    }

    public void SetupEndingScreen(TeamIDEnum winningTeam)
    {
        this.gameObject.SetActive(true);

        winningTeamText.text = winningTeam + " Team Wins!";

        GameObject[] playerPrefabs = GameObject.FindGameObjectsWithTag("Player");

        foreach(var playerPrefab in playerPrefabs.OrderBy(x => x.GetComponent<CharacterController>().PlayerIndex))
        {
            CharacterController player = playerPrefab.GetComponent<CharacterController>();

            EndingScreenSeat seat = Instantiate(seatPrefab, seatsHorizontalLayout.transform).GetComponent<EndingScreenSeat>();
            seat.UpdateStats(player.deathsStats, player.killsStats, player.flagsScored, player.flagsRetrieved);
            seat.UpdateTitle(player.PlayerIndex);
            seat.UpdateHeroImage(player.UIAnimator);
            seat.UpdateTeamColor(player.Team);
        }
    }
}

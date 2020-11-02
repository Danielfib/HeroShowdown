using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class EndingScreenManager : MonoBehaviour
{
    [SerializeField]
    private GameObject seatsHorizontalLayout, seatPrefab;

    [SerializeField]
    private TextMeshProUGUI winningTeamText;

    [SerializeField]
    private GameObject endingScreenBackground, quitLabel;

    private void Start()
    {
        winningTeamText.gameObject.SetActive(false);
        quitLabel.SetActive(false);
        //this.gameObject.SetActive(false);
    }

    private void Update()
    {
        //Escape on keyboard or B on xbox controller
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 1"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("SelectionScreen");
        }
    }

    public void SetupEndingScreen(TeamIDEnum winningTeam)
    {
        //deactivate character controllers
        foreach (var cc in GameObject.FindObjectsOfType<CharacterController>())
        {
            cc.enabled = false;
            cc.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }

        switch (winningTeam)
        {
            case TeamIDEnum.BLUE:
                endingScreenBackground.GetComponent<Image>().color = ColorUtils.Background_DarkBlue;
                break;
            case TeamIDEnum.RED:
                endingScreenBackground.GetComponent<Image>().color = ColorUtils.Background_DarkRed;
                break;
        }

        this.gameObject.SetActive(true);
        endingScreenBackground.transform.localScale = new Vector3(0, 1, 1);
        endingScreenBackground.transform.DOScaleX(1, 0.5f).OnComplete(() =>
        {
            winningTeamText.text = winningTeam + " Team Wins!";
            winningTeamText.gameObject.SetActive(true);
            quitLabel.SetActive(true);

            InstantiateEndingSeats();
        });
    }

    private void InstantiateEndingSeats()
    {
        var playerPrefabs = GameObject.FindGameObjectsWithTag("Player");
        foreach (var playerPrefab in playerPrefabs.OrderBy(x => x.GetComponent<CharacterController>().PlayerIndex))
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

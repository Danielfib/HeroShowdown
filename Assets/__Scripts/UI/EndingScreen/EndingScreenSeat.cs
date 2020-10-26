using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingScreenSeat : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText, statsText;

    public void UpdateStats(int deaths, int kills, int points)
    {
        statsText.text = "Points Scored: " + points + "\n" +
                         "Deaths: " + deaths + "\n" +
                         "Kills: " + kills;
    }

    public void UpdateTitle(int playerIndex)
    {
        titleText.text = "Player " + (playerIndex + 1);
    }
}

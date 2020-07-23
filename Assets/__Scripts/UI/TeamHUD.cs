using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamHUD : MonoBehaviour
{
    public TeamIDEnum team;

    [SerializeField]
    private Transform membersContainer;
    [SerializeField]
    private CircularCountdown flagCountdown;
    [SerializeField]
    private Slider progressBar;

    public void StartFlagCountdown(float countdown)
    {
        this.flagCountdown.StartCountdown(countdown);
    }

    public void StartPlayerCountdown()
    {

    }

    public void UpdatePoints(int points)
    {
        //TODO: update progress bar
        //and have maximum score on constant somewhere
        progressBar.value = (float)points / 5f;
    }
}

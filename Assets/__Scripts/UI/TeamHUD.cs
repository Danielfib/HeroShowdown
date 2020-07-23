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

    public void IncreaseScore()
    {

    }
}

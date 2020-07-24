using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamHUD : MonoBehaviour
{
    public TeamIDEnum team;

    public Transform membersContainer;

    [SerializeField]
    private CircularCountdown flagCountdown;
    [SerializeField]
    private Slider progressBar;
    [SerializeField]
    private Image FlagStatusImage;

    public void StartFlagCountdown(float countdown)
    {
        this.flagCountdown.StartCountdown(countdown);
    }

    public void StartPlayerCountdown()
    {

    }

    public void UpdatePoints(int points)
    {
        //TODO: have maximum score on constant somewhere
        progressBar.value = (float)points / 5f;
    }

    public void UpdateFlagStatus(FlagStates flagState)
    {
        this.FlagStatusImage.sprite = GetFlagStatusSprite(flagState);

        if (this.FlagStatusImage.sprite == null)
            this.FlagStatusImage.color = new Color(1, 1, 1, 0);
        else
            this.FlagStatusImage.color = new Color(1, 1, 1, 1);
    }

    public Sprite GetFlagStatusSprite(FlagStates flagState)
    {
        Sprite sprite = null;

        switch (flagState)
        {
            case FlagStates.NORMAL:
                break;
            case FlagStates.BEING_CARRIED:
                sprite = Resources.Load<Sprite>("Sprites/FlagStates/FlagBeingCarried");
                break;
            case FlagStates.DROPPED:
                sprite = Resources.Load<Sprite>("Sprites/FlagStates/FlagDropped");
                break;
            case FlagStates.DROPPED_COOLDOWN:
                sprite = Resources.Load<Sprite>("Sprites/FlagStates/FlagInDroppedCooldown");
                break;
        }

        return sprite;
    }
}
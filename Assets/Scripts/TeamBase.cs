using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TeamBase : MonoBehaviour
{
    public TeamIDEnum teamIdEnum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Flag")
        {
            Flag flag = collision.gameObject.GetComponent<Flag>();
            if(flag.teamIDEnum == this.teamIdEnum)
            {
                Debug.Log("Team " + teamIdEnum + " scored!");
                ScoreForTeam(teamIdEnum);
                flag.ReturnedToBase();
            }
        }
    }

    private void ScoreForTeam(TeamIDEnum team)
    {
        switch (team)
        {
            case TeamIDEnum.BLUE:
                break;
            case TeamIDEnum.RED:
                break;
            case TeamIDEnum.PURPLE:
                break;
            case TeamIDEnum.GREEN:
                break;
            case TeamIDEnum.BROWN:
                break;
        }
    }
}

public enum TeamIDEnum
{
    RED, BLUE, BROWN, GREEN, PURPLE
}

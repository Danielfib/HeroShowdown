using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TeamBase : MonoBehaviour
{
    public TeamIDEnum teamIdEnum;

    private int Points;

    private void Start()
    {
        MatchManager.Instance.SpawnNewFlag(this.teamIdEnum);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Flag")
        {
            TeamIDEnum playerTeam = collision.gameObject.transform.parent.parent.parent.GetComponent<CharacterController>().Team;
            if(playerTeam == this.teamIdEnum)
            {
                Flag flag = collision.gameObject.GetComponent<Flag>();
                if(flag.teamIDEnum != this.teamIdEnum)
                {
                    Score();
                    flag.Scored();
                }
            }
        }
    }

    private void Score()
    {
        Debug.Log("Team " + teamIdEnum + " scored!");
        this.Points++;
        MatchManager.Instance.UpdatePoints(this.teamIdEnum, this.Points);
    }
}

public enum TeamIDEnum
{
    RED, BLUE
}
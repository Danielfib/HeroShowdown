using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TeamBase : MonoBehaviour
{
    public TeamIDEnum teamIdEnum;

    private MatchManager MatchManager;
    private int Points;

    private void Start()
    {
        this.MatchManager = GameObject.FindObjectOfType<MatchManager>();
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
        this.MatchManager.UpdatePoints(this.teamIdEnum, this.Points);
    }
}

public enum TeamIDEnum
{
    RED, BLUE, BROWN, GREEN, PURPLE
}
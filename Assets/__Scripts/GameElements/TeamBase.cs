using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Collider2D))]
public class TeamBase : MonoBehaviour
{
    [SerializeField]
    private GameObject scoredParticlesFX;

    public TeamIDEnum teamIdEnum;

    private int Points;

    private void Start()
    {
        Color c = ColorUtils.TeamIdEnumToColor(teamIdEnum);
        GetComponent<SpriteRenderer>().color = c;
        GetComponent<Light2D>().color = c;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Flag")
        {
            Flag flag = collision.gameObject.GetComponent<Flag>();
            TeamIDEnum playerTeam = flag.teamIDEnum;
            if(playerTeam != this.teamIdEnum)
            {
                flag.Scored();
                Score();
            }
        }
    }

    private void Score()
    {
        Debug.Log("Team " + teamIdEnum + " scored!");
        this.Points++;

        GameObject pfx = Instantiate(scoredParticlesFX, transform);
        pfx.GetComponent<Renderer>().material.color = ColorUtils.TeamIdEnumToColor(teamIdEnum);
        Destroy(pfx, 2f);

        MatchManager.Instance.UpdatePoints(this.teamIdEnum, this.Points);
    }
}

public enum TeamIDEnum
{
    RED, BLUE
}
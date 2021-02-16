using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class Flag : NetworkBehaviour
{
    public Animator Animator;

    [SyncVar(hook = nameof(OnTeamIDEnumChanged))]
    public TeamIDEnum teamIDEnum;

    private FlagStates _flagState;
    public FlagStates FlagState
    {
        get { return this._flagState; }
        set
        {
            this._flagState = value;
            UpdateFlagState();
        }
    }

    public float CooldownAfterDrop = 2f;

    private Transform playerHolder;

    public override void OnStartClient()
    {
        base.OnStartClient();
        this.FlagState = FlagStates.NORMAL;
    }

    private void OnTeamIDEnumChanged(TeamIDEnum oldValue, TeamIDEnum newValue)
    {
        GetComponentInChildren<SpriteRenderer>().color = ColorUtils.TeamIdEnumToColor(newValue);
    }

    private void FixedUpdate()
    {
        if(playerHolder && isServer)
            transform.position = playerHolder.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FlagState == FlagStates.BEING_CARRIED || FlagState == FlagStates.DROPPED_COOLDOWN)
            return;

        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<PlayerController>().Team != this.teamIDEnum)
            {
                this.FlagState = FlagStates.BEING_CARRIED;
                playerHolder = collision.gameObject.transform.Find("FlagPos");
                this.Animator.SetBool("IsDropped", false);
            }
            else if (FlagState == FlagStates.DROPPED)
            {
                collision.gameObject.GetComponent<PlayerController>().RetrievedFlag();
                Retrieved();
            }
        }
    }

    public void Retrieved()
    {
        FlagState = FlagStates.NORMAL;
        MatchManager.Instance.StartFlagRespawn(this.teamIDEnum, wasRetrieved: true);
        //TODO: cool effects
        Debug.Log("Flag retrieved!");
        Destroy(this.gameObject);
    }

    public void Scored()
    {
        FlagState = FlagStates.NORMAL;
        MatchManager.Instance.StartFlagRespawn(this.teamIDEnum, wasRetrieved: false);
        playerHolder?.GetComponentInParent<PlayerController>().Scored();
        Destroy(this.gameObject);
    }

    public void Drop()
    {
        StartCoroutine(TriggerCooldownAfterDrop());
        this.playerHolder = null;
    }

    private IEnumerator TriggerCooldownAfterDrop()
    {
        this.FlagState = FlagStates.DROPPED_COOLDOWN;
        yield return new WaitForSeconds(this.CooldownAfterDrop);
        this.FlagState = FlagStates.DROPPED;
        this.Animator.SetBool("IsDropped", true);
    }

    private void UpdateFlagState()
    {
        HUDManager.Instance.UpdateFlagStatusIcon(this.teamIDEnum, FlagState);
    }

    private void OnDestroy()
    {
        NetworkServer.Destroy(gameObject);
    }
}

public enum FlagStates
{
    NORMAL,             //In spawn location 
    DROPPED,            //Dropped in the middle of the map (available to be retrieved)
    BEING_CARRIED,      //Being carried by enemy
    DROPPED_COOLDOWN    //Enemy dropped, but still cant pick back up
}
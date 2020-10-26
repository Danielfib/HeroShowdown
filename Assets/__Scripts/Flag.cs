using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Animator Animator;

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

    [SerializeField]
    private Transform FlagHolder;

    private void Start()
    {
        this.FlagState = FlagStates.NORMAL;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FlagState == FlagStates.BEING_CARRIED || FlagState == FlagStates.DROPPED_COOLDOWN)
            return;

        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<CharacterController>().Team != this.teamIDEnum)
            {
                this.FlagState = FlagStates.BEING_CARRIED;
                Transform playerFlagPos = collision.gameObject.transform.Find("FlagPos");
                this.FlagHolder.parent = playerFlagPos;
                this.FlagHolder.localPosition = new Vector3(0, 0, 0);
                this.Animator.SetBool("IsDropped", false);
            }
            else if (FlagState == FlagStates.DROPPED)
            {
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
        GetComponentInParent<CharacterController>().Scored();
        Destroy(this.gameObject);
    }

    public void Drop()
    {
        StartCoroutine(TriggerCooldownAfterDrop());
        this.FlagHolder.parent = null;
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
}

public enum FlagStates
{
    NORMAL,             //In spawn location 
    DROPPED,            //Dropped in the middle of the map (available to be retrieved)
    BEING_CARRIED,      //Being carried by enemy
    DROPPED_COOLDOWN    //Enemy dropped, but still cant pick back up
}
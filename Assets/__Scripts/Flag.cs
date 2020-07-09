using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Animator Animator;

    public TeamIDEnum teamIDEnum;

    private bool isBeingCarried = false;
    private bool MayBeCarriedAgain = true;

    public float CooldownAfterDrop = 2f;

    private bool _IsDropped;
    private bool IsDropped
    {
        get { return _IsDropped; }
        set
        {
            this.Animator.SetBool("IsDropped", value);
            this._IsDropped = value;
        }
    }

    [SerializeField]
    private Transform FlagHolder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBeingCarried || !this.MayBeCarriedAgain)
            return;

        if(collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<CharacterController>().Team != this.teamIDEnum)
            {
                isBeingCarried = true;
                Transform playerFlagPos = collision.gameObject.transform.Find("FlagPos");
                this.FlagHolder.parent = playerFlagPos;
                this.FlagHolder.localPosition = new Vector3(0, 0, 0);
                IsDropped = false;
            }
            else if (IsDropped)
            {
                Retrieved();
            }
        }
    }

    public void Retrieved()
    {
        GameObject.FindObjectOfType<MatchManager>().StartFlagRespawn(this.teamIDEnum, wasRetrieved: true);
        //TODO: cool effects
        Debug.Log("Flag retrieved!");
        Destroy(this.gameObject);
        //TODO: Spawn next 
    }

    public void Scored()
    {
        GameObject.FindObjectOfType<MatchManager>().StartFlagRespawn(this.teamIDEnum, wasRetrieved: false);
        //TODO: cool effects
        Destroy(this.gameObject);
        //TODO: Spawn next
    }

    public void Drop()
    {
        StartCoroutine("TriggerCooldownAfterDrop");
        this.isBeingCarried = false;
        this.FlagHolder.parent = null;
        IsDropped = true;
    }

    private IEnumerator TriggerCooldownAfterDrop()
    {
        this.MayBeCarriedAgain = false;
        yield return new WaitForSeconds(this.CooldownAfterDrop);
        this.MayBeCarriedAgain = true;
    }
}
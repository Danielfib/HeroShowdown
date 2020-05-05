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

    [SerializeField]
    private Transform FlagHolder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBeingCarried || !this.MayBeCarriedAgain)
            return;

        if(collision.gameObject.tag == "Player"
            && collision.gameObject.GetComponent<CharacterController>().Team != this.teamIDEnum)
        {
            isBeingCarried = true;
            Transform playerFlagPos = collision.gameObject.transform.Find("FlagPos");
            this.FlagHolder.parent = playerFlagPos;
            this.FlagHolder.localPosition = new Vector3(0, 0, 0);
            this.Animator.SetBool("IsDropped", false);
        }
    }

    public void ReturnedToBase()
    {
        //TODO: cool effects
        Destroy(this.gameObject);
        //TODO: Spawn next 
    }

    public void Scored()
    {
        //TODO: cool effects
        Destroy(this.gameObject);
        //TODO: Spawn next
    }

    public void Drop()
    {
        StartCoroutine("TriggerCooldownAfterDrop");
        this.isBeingCarried = false;
        this.FlagHolder.parent = null;
        this.Animator.SetBool("IsDropped", true);
    }

    private IEnumerator TriggerCooldownAfterDrop()
    {
        this.MayBeCarriedAgain = false;
        yield return new WaitForSeconds(this.CooldownAfterDrop);
        this.MayBeCarriedAgain = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public TeamIDEnum teamIDEnum;

    private bool isBeingCarried = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBeingCarried)
            return;

        if(collision.gameObject.tag == "Player")
        {
            isBeingCarried = true;
            Transform playerFlagPos = collision.gameObject.transform.Find("FlagPos");
            this.transform.parent = playerFlagPos;
            this.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void ReturnedToBase()
    {
        //TODO: cool effects
        Destroy(this.gameObject);
        //TODO: Spawn next 
    }

}

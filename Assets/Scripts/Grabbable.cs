using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public GameObject PickupUIGameObject;
    public bool _isBeingGrabbed
    {
        get { return this.transform.parent.parent == null; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Grabber")
        {
            if(!_isBeingGrabbed)
                EnablePickUpUI();

            collision.gameObject.GetComponent<Grabber>().BecameGrababble(this.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grabber")
        {
            DisablePickUpUI();
            collision.gameObject.GetComponent<Grabber>().NoLongerGrabbable();
        }
    }

    private void EnablePickUpUI()
    {
        PickupUIGameObject.SetActive(true);
    }

    private void DisablePickUpUI()
    {
        PickupUIGameObject.SetActive(false);
    }
}

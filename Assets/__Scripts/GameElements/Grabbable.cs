using UnityEngine;
using Mirror;

//This class is solely responsible for the grab/release interaction
public class Grabbable : NetworkBehaviour
{
    private GameObject PickupUIGameObject;
    private Grabber Grabber;

    public void TriedToBeGrabbed(Grabber grabber)
    {
        if(Grabber == null)
        {
            //do not allow to steal grabbed things from other players
            this.Grabber = grabber;
        }
    }

    public void Released()
    {
        Grabber = null;
    }

    private void Update() 
    {
        if(Grabber != null && isServer) 
            transform.position = Grabber.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Grabber")
        {
            //if(Grabber == null) EnablePickUpUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grabber")
        {
            //DisablePickUpUI();
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

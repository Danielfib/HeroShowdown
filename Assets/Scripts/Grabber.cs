using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    //public bool CanGrabObject = true;
    private GameObject CurrentlyGrabbableObject;
    private GameObject CurrentlyGrabingObject;

    private bool _canGrabObject
    {
        get { return this.transform.childCount == 0; }
    }

    public void TryToGrab()
    {
        if (CurrentlyGrabbableObject == null)
            return;

        Grab(CurrentlyGrabbableObject);
    }

    private void Grab(GameObject go)
    {
        if (!_canGrabObject)
            return;

        go.GetComponent<Rigidbody2D>().simulated = false;
        go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        go.transform.parent = this.gameObject.transform;
        go.transform.localPosition = new Vector3(0, 0, 0);
        

        CurrentlyGrabingObject = go;
    }

    private void TossObject()
    {
        if (CurrentlyGrabingObject == null)
            return;

        CurrentlyGrabingObject.transform.parent = null;
        CurrentlyGrabingObject.GetComponent<Rigidbody2D>().simulated = true;
        CurrentlyGrabingObject.GetComponent<ProjectileController>().ReceiveTossAction();
    }

    public void BecameGrababble(GameObject gameObject)
    {
        CurrentlyGrabbableObject = gameObject;
    }

    public void NoLongerGrabbable()
    {
        CurrentlyGrabbableObject = null;
    }

    public void GrabTossAction()
    {
        if (_canGrabObject)
            TryToGrab();
        else
            TossObject();
    }
}

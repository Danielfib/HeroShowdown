using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    private GameObject CurrentlyGrabbableObject;
    private GameObject CurrentlyGrabingObject;

    private bool _canGrabObject
    {
        get { return this.transform.childCount == 0; }
    }

    public GrabTossActionResults TryToGrab()
    {
        if (CurrentlyGrabbableObject == null || !_canGrabObject)
        {
            return GrabTossActionResults.COULD_NOT_GRAB;
        }

        Grab(CurrentlyGrabbableObject);
        return GrabTossActionResults.GRABBED;
    }

    private void Grab(GameObject go)
    {
        go.GetComponent<Rigidbody2D>().simulated = false;
        go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        go.transform.parent = this.gameObject.transform;
        go.transform.localPosition = new Vector3(0, 0, 0);
        
        CurrentlyGrabingObject = go;
        UpdateColliderIsTriggerUponGrab(false);
    }

    private void TossObject(Vector2 dir)
    {
        if (CurrentlyGrabingObject == null)
            return;

        CurrentlyGrabingObject.transform.parent = null;
        CurrentlyGrabingObject.GetComponent<ProjectileController>().ReceiveTossAction(dir);
        UpdateColliderIsTriggerUponGrab(true);
    }

    public void BecameGrababble(GameObject gameObject)
    {
        CurrentlyGrabbableObject = gameObject;
    }

    public void NoLongerGrabbable()
    {
        CurrentlyGrabbableObject = null;
    }

    public GrabTossActionResults GrabTossAction(Vector2 dir)
    {
        if (_canGrabObject)
        {
            return TryToGrab();
        }
        else
        {
            TossObject(dir);
            return GrabTossActionResults.TOSSED;
        }
    }

    /// <summary>
    /// Implemented to fix bug where when grabing a projectile, this projectile would not collidw with walls
    /// Thus, it was possible to toss through walls, if projectile already was inside wall
    /// </summary>
    private void UpdateColliderIsTriggerUponGrab(bool value)
    {
        this.GetComponent<Collider2D>().isTrigger = value;
    }

    public enum GrabTossActionResults
    {
        GRABBED,
        TOSSED,
        COULD_NOT_GRAB
    }
}
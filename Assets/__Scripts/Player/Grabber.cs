using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Grabber : MonoBehaviour
{
    private GameObject CurrentlyGrabingObject;
    private Collider2D collider;

    [SerializeField]
    private LayerMask grabbableLayerMask;

    private bool _canGrabObject
    {
        get { return this.CurrentlyGrabingObject == null; }
    }

    private void Start() 
    {
        collider = GetComponent<Collider2D>();    
    }

    public GrabTossActionResults TryToGrab()
    {
        if (!_canGrabObject)
        {
            return GrabTossActionResults.COULD_NOT_GRAB; //already grabbing something
        }

        List<Collider2D> colidingObjects = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(grabbableLayerMask);
        collider.OverlapCollider(filter, colidingObjects);

        if(colidingObjects.Count > 0)
        {
            Grab(colidingObjects.FirstOrDefault().gameObject);
            return GrabTossActionResults.GRABBED;
        } else {
            return GrabTossActionResults.COULD_NOT_GRAB;  //grabbed nothing
        }
        
    }

    private void Grab(GameObject go)
    {
        Grabbable grabbable = go.GetComponentInChildren<Grabbable>();
        go.GetComponent<Rigidbody2D>().simulated = false;
        go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        grabbable.TriedToBeGrabbed(this);
        
        CurrentlyGrabingObject = go;
        UpdateColliderIsTriggerUponGrab(false);
    }

    private void TossObject(Vector2 dir)
    {
        if (CurrentlyGrabingObject == null)
            return;

        //CurrentlyGrabingObject.GetComponent<Grabbable>().
        CurrentlyGrabingObject.GetComponent<ProjectileController>().ReceiveTossAction(dir, this.GetComponentInParent<CharacterController>());
        CurrentlyGrabingObject = null;
        UpdateColliderIsTriggerUponGrab(true);
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
            if (dir == Vector2.zero)
                return GrabTossActionResults.RELEASED;
            else
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
        COULD_NOT_GRAB,
        RELEASED
    }
}
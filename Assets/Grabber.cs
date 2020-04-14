using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public void Grab(GameObject grabbedObject)
    {
        Debug.Log("Grabbed!");
        grabbedObject.GetComponent<Rigidbody2D>().simulated = false;
        grabbedObject.transform.parent = this.gameObject.transform;
        grabbedObject.transform.localPosition = new Vector3(0, 0, 0);
    }
}

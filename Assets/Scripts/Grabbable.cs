using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Grabber")
        {
            collision.gameObject.GetComponent<Grabber>().Grab(this.gameObject);
        }
    }
}

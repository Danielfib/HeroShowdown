using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDIconController : MonoBehaviour
{
    public void StartRespawnCircle(float duration)
    {
        gameObject.GetComponent<CircularCountdown>().StartCountdown(duration);
    }
}

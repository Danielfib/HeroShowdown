using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private GameObject CannonBall;

    [SerializeField]
    private Transform spawnPoint;

    public void PlayerInteracted()
    {
        ProjectileController projectile = Instantiate(CannonBall, spawnPoint).GetComponent<ProjectileController>();
        projectile.ReceiveTossAction(this.transform.right * this.transform.localScale.x);
    }
}

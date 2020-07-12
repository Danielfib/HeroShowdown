using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private GameObject CannonBall;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float cooldown = 2f;
    [SerializeField]
    private bool isOnCooldown;

    public void PlayerInteracted()
    {
        if (!this.isOnCooldown)
        {
            ProjectileController projectile = Instantiate(CannonBall, spawnPoint).GetComponent<ProjectileController>();
            projectile.ReceiveTossAction(this.transform.right * this.transform.localScale.x);
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        this.isOnCooldown = true;
        yield return new WaitForSeconds(this.cooldown);
        this.isOnCooldown = false;
    }
}

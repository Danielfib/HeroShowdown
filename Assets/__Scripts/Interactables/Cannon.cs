using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Cannon : MonoBehaviour
{
    [SerializeField]
    private GameObject CannonBall;

    [SerializeField]
    private Transform spawnPoint;

    private Animator animator;

    private float cooldown = 2f;
    private bool isOnCooldown;

    private void Start()
    {
        this.cooldown = this.GetComponent<Interactable>().cooldownTime;
        this.animator = this.GetComponent<Animator>();
    }

    public void PlayerInteracted()
    {
        if (!this.isOnCooldown)
        {
            ProjectileController projectile = Instantiate(CannonBall, spawnPoint).GetComponent<ProjectileController>();
            projectile.ReceiveTossAction(this.transform.right * this.transform.localScale.x);
        }
    }

    #region [Messages_Methods]
    private void InteractableCooldownStart()
    {
        this.isOnCooldown = true;
        animator.SetTrigger("StartCooldown");
    }

    private void InteractableCooldownEnd()
    {
        this.isOnCooldown = false;
        animator.SetTrigger("EndCooldown");
    }

    private void SetupCooldown(float cooldownTime)
    {
        this.cooldown = cooldownTime;
    }
    #endregion
}

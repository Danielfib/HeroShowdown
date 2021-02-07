using UnityEngine;
using Mirror;
using System;

[RequireComponent(typeof(Interactable))]
public class Cannon : NetworkBehaviour
{
    [SerializeField]
    private GameObject CannonBall;

    [SerializeField]
    private Transform spawnPoint;

    private Animator animator;

    public override void OnStartClient()
    {
        base.OnStartClient();

        this.animator = this.GetComponent<Animator>();

        GetComponent<Interactable>().InteractedAction += Shoot;
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        GetComponent<Interactable>().InteractedAction -= Shoot;
    }

    [Server]
    private void Shoot()
    {
        var go = Instantiate(CannonBall, spawnPoint.position, Quaternion.identity);
        ProjectileController projectile = go.GetComponent<ProjectileController>();
        NetworkServer.Spawn(go);
        projectile.ReceiveTossAction(this.transform.right * this.transform.localScale.x);
    }

    #region [Messages_Methods]
    private void InteractableCooldownStart()
    {
        animator.SetTrigger("StartCooldown");
    }

    private void InteractableCooldownEnd()
    {
        animator.SetTrigger("EndCooldown");
    }
    #endregion
}

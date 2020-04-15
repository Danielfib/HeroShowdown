using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandardProjectileBrain", menuName = "Projectile Brain/Standard Projectile Brain")]
public class StandardProjectileBrain : ProjectileBrain
{
    public float MinVelocityToStop = 5f;
    public float TossGravityDisableDuration = 0f;
    private InputActions inputActions;

    public override void Initialize(ProjectileController projectileController)
    {
        this.inputActions = new InputActions();
        this.inputActions.Enable();
    }

    public override void Think(ProjectileController projectileController)
    {
        if (projectileController.rb.velocity.y == 0 && //has no vertical movement
            projectileController.rb.velocity.magnitude < MinVelocityToStop)
        {
            projectileController.ReturnToGrabbableState();
            projectileController.rb.velocity = Vector2.zero;
        }
    }

    public override void Toss(ProjectileController projectileController)
    {
        Vector2 dir = this.inputActions.PlayerMovement.Move.ReadValue<Vector2>();

        projectileController.StandardToss(dir);
    }

    public override float GetGravityDisableDurationDuringToss()
    {
        return this.TossGravityDisableDuration;
    }
}

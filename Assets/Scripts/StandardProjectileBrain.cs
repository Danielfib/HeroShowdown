using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandardProjectileBrain", menuName = "Projectile Brain/Standard Projectile Brain")]
public class StandardProjectileBrain : ProjectileBrain
{
    private InputActions inputActions;

    public override void Initialize(ProjectileController projectileController)
    {
        this.inputActions = new InputActions();
        this.inputActions.Enable();
    }

    public override void Think(ProjectileController projectileController)
    {
        
    }

    public override void Toss(ProjectileController projectileController)
    {
        Vector2 dir = this.inputActions.PlayerMovement.Move.ReadValue<Vector2>();

        projectileController.StandardToss(dir);
    }
}

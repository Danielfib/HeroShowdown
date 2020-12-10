using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CannonBallProjectileBrain", menuName = "Projectile Brain/CannonBall Projectile Brain")]
public class CannonBallProjectileBrain : ProjectileBrain
{
    public float ExplodeRadius = 3f;

    public override float GetGravityDisableDurationDuringToss()
    {
        return 0;
    }

    public override void HandleCollision(ProjectileController projectileController)
    {
        projectileController.Explode(ExplodeRadius);
    }

    public override void Initialize(ProjectileController projectileController)
    {
    }

    public override void Think(ProjectileController projectileController)
    {
    }

    public override void Toss(ProjectileController projectileController, Vector2 dir)
    {
        projectileController.StandardToss(dir, LayerMask.NameToLayer("OnlyHitsPlayers"), shouldDecay: false);
    }
}

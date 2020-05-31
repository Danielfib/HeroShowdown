using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBrain : ScriptableObject
{
    public abstract void Initialize(ProjectileController projectileController);

    public abstract void Think(ProjectileController projectileController);

    public abstract void Toss(ProjectileController projectileController, Vector2 dir);

    public abstract float GetGravityDisableDurationDuringToss();

    public abstract void HandleCollision(ProjectileController projectileController);
}

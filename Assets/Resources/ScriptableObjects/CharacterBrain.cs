using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBrain : ScriptableObject
{
    public RuntimeAnimatorController UIAnimator;

    public abstract void Initialize(PlayerController characterController);

    public abstract void Think(PlayerController characterController);

    public abstract void Die(PlayerController characterController);

    public abstract void SpecialAction(PlayerController characterController);
}

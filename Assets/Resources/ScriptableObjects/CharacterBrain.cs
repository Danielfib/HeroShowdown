using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBrain : ScriptableObject
{
    public RuntimeAnimatorController UIAnimator;

    public abstract void Initialize(CharacterController characterController);

    public abstract void Think(CharacterController characterController);

    public abstract void Die(CharacterController characterController);

    public abstract void SpecialAction(CharacterController characterController);
}

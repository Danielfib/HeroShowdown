using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorBrain", menuName = "Character Brain/Warrior Brain")]
public class WarriorBrain : CharacterBrain
{
    public override void Initialize(CharacterController characterController)
    {
        
    }

    public override void Think(CharacterController characterController)
    {
        
    }

    public override void Die(CharacterController characterController)
    {
        characterController.DieDefault();
    }

    public override float GetDeflectMagnetude(CharacterController characterController)
    {
        //TODO: Maybe deflect force may have something to do with deflect timing?
        return 1.5f;
    }
}

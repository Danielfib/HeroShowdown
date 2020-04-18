using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorBrain", menuName = "Character Brain/Warrior Brain")]
public class WarriorBrain : CharacterBrain
{
    private InputActions inputActions;
    public float DodgeSpeed = 300;

    public override void Initialize(CharacterController characterController)
    {
        this.inputActions = new InputActions();
        this.inputActions.Enable();
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

    public override void SpecialAction(CharacterController characterController)
    {
        Vector2 dir = inputActions.PlayerMovement.Move.ReadValue<Vector2>();
        
        characterController.Dodge(dir, DodgeSpeed);
    }
}

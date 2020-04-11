using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorBrain", menuName = "Character Brain/Warrior Brain")]
public class WarriorBrain : CharacterBrain
{
    private InputActions inputActions;

    public override void Initialize(CharacterController characterController)
    {
        this.inputActions = new InputActions();
        this.inputActions.Enable();
    }

    public override void Think(CharacterController characterController)
    {
        Vector2 dir = this.inputActions.PlayerMovement.Move.ReadValue<Vector2>();
        characterController.SetDirection(dir);

        float didJump = this.inputActions.PlayerMovement.Jump.ReadValue<float>();
        if(didJump > 0)
        {
            characterController.Jump();
        } else
        {
            characterController.StoppedJump();
        }
    }
}

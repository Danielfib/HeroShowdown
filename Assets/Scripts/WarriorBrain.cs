using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorBrain", menuName = "Character Brain/Warrior Brain")]
public class WarriorBrain : CharacterBrain
{
    private InputActions inputActions;

    public float DodgeSpeed;
    public float DodgeCooldown;
    public float DodgeDuration;
    private float DodgeTimer;
    private Vector2 DodgeDirection;

    public override void Initialize(CharacterController characterController)
    {
        this.inputActions = new InputActions();
        this.inputActions.Enable();
    }

    public override void Think(CharacterController characterController)
    {
        switch (characterController.SAState)
        {
            case SAState.READY:
                break;
            case SAState.USING:
                characterController.DoDodge(DodgeDirection.normalized, DodgeSpeed);
                DodgeTimer += Time.deltaTime;
                if(DodgeTimer >= DodgeDuration)
                {
                    DodgeTimer = DodgeCooldown;
                    characterController.SetSAState(SAState.COOLDOWN);
                    characterController.SetInvulnerability(false);
                }
                break;
            case SAState.COOLDOWN:
                DodgeTimer -= Time.deltaTime;
                if(DodgeTimer <= 0)
                {
                    DodgeTimer = 0;
                    characterController.SetSAState(SAState.READY);
                }
                break;
        }

        if (characterController.SAState != SAState.USING)
        {
            characterController.MoveRigidBody();
            characterController.ContinueJumping();
        } 
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
        DodgeDirection = inputActions.PlayerMovement.Move.ReadValue<Vector2>();
        characterController.SetSAState(SAState.USING);
        characterController.SetInvulnerability(true);
    }
}
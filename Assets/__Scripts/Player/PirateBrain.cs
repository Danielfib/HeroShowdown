using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PirateBrain", menuName = "Character Brain/Pirate Brain")]
public class PirateBrain : CharacterBrain
{
    private InputActions InputActions;

    public float DodgeSpeed;
    public float DodgeCooldown;
    public float DodgeDuration;
    private float DodgeTimer;
    private Vector2 DodgeDirection;

    public override void Initialize(PlayerController characterController)
    {
        this.InputActions = new InputActions();
        this.InputActions.Enable();
    }

    public override void Think(PlayerController characterController)
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
                    characterController.FinishedSpecialAction();
                    characterController.sbCooldown.startCountdown(DodgeCooldown);
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

    public override void Die(PlayerController characterController)
    {
        characterController.DieDefault();
    }

    public override void SpecialAction(PlayerController characterController)
    {
        DodgeDirection = characterController.GetMoveDirection();
        characterController.SetSAState(SAState.USING);
        characterController.SetInvulnerability(true);
    }
}
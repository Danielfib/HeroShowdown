using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageBrain", menuName = "Character Brain/MageBrain")]
public class MageBrain : CharacterBrain
{
    private InputActions InputActions;

    public float DeflectDuration;
    public float DeflectCooldown;
    public float DeflectMagnetude;
    private float DeflectTimer;

    public override void Die(CharacterController characterController)
    {
        characterController.DieDefault();
    }

    public override void Initialize(CharacterController characterController)
    {
        this.InputActions = new InputActions();
        this.InputActions.Enable();
    }

    public override void SpecialAction(CharacterController characterController)
    {
        characterController.ActivatedDeflective();
    }

    public override void Think(CharacterController characterController)
    {
        switch (characterController.SAState)
        {
            case SAState.READY:
                break;
            case SAState.USING:
                DeflectTimer += Time.deltaTime;
                if(DeflectTimer >= DeflectDuration)
                {
                    DeflectTimer = DeflectCooldown;
                    characterController.DeactivatedDeflective();
                    characterController.SetSAState(SAState.COOLDOWN);
                    characterController.sbCooldown.startCountdown(DeflectCooldown);
                }
                break;
            case SAState.COOLDOWN:
                DeflectTimer -= Time.deltaTime;
                if(DeflectTimer <= 0)
                {
                    DeflectTimer = 0;
                    characterController.SetSAState(SAState.READY);
                }
                break;
        }

        if(characterController.SAState != SAState.USING)
        {
            characterController.MoveRigidBody();
            characterController.ContinueJumping();
        }
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    public Grabber Grabber;

    public CharacterBrain CharacterBrain;

    public TeamIDEnum Team;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpForce = 500f;

    [SerializeField]
    private Animator Animator;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool canContinueJumping;

    public SAState _SAState = SAState.READY;
    public SAState SAState
    {
        get { return _SAState; }
        set {
            _SAState = value;
        }
    }

    private bool IsInvulnerable = false;
    public bool IsReflectiveToProjectiles = false;

    public PlayerHUDIconController PlayerHUDIconController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        this.CharacterBrain.Initialize(this);
    }

    private void FixedUpdate()
    {
        FlipSpriteOnWalkDirection();

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        this.CharacterBrain.Think(this);
    }

    #region [Combat]
    public void GrabToss(CallbackContext context)
    {
        //prevent tossing before turning, and killing yourself
        FlipSpriteOnWalkDirection();

        if (context.performed)
            DoGrabToss(this.moveDirection);
    }

    public void DoGrabToss(Vector2 dir)
    {
        Grabber.GrabTossAction(dir);
    }

    public void DieDefault()
    {
        //this.Animator.SetTrigger("Die");
        GetComponent<PlayerRespawnManager>().StartRespawnCounter(this.PlayerHUDIconController);
    }
    
    public void SpawnOnBase()
    {
        TeamBase teamBase = GameObject.FindObjectsOfType<TeamBase>().Where(x => x.teamIdEnum == this.Team).FirstOrDefault();
        this.gameObject.transform.position = teamBase.transform.position;
    }

    public void GotHit()
    {
        if (!IsInvulnerable && !IsReflectiveToProjectiles)
        {
            DropFlag();
            CharacterBrain.Die(this);
        }
    }
    #endregion

    private void DropFlag()
    {
        Flag flag = this.GetComponentInChildren<Flag>();
        if (flag != null)
            flag.Drop();
    }

    #region [Walk]
    public void SetDirection(Vector2 dir)
    {
        this.moveDirection = dir;
        this.moveDirection.Normalize();
    }

    public void Move(CallbackContext context)
    {
        SetDirection(context.ReadValue<Vector2>());
    }

    private void FlipSpriteOnWalkDirection()
    {
        float currentVelX = this.gameObject.GetComponent<Rigidbody2D>().velocity.x;
        if (currentVelX < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (currentVelX > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void MoveRigidBody()
    {
        this.rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }
    #endregion

    #region [Jump]
    public void ExecuteJump()
    {
        if (isGrounded)
        {
            canContinueJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
        }
    }

    public void StoppedJump()
    {
        canContinueJumping = false;
    }

    public void ContinueJumping()
    {
        if (canContinueJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                canContinueJumping = false;
            }
        }
    }

    public void Jump(CallbackContext context)
    {
        float didJump = context.ReadValue<float>();

        if (didJump > 0)
        {
            ExecuteJump();
        }
        else
        {
            StoppedJump();
        }
    }
    #endregion

    #region [SpecialActions]
    public void SpecialAction(CallbackContext context)
    {
        if(context.performed
           && this.SAState == SAState.READY)
            this.CharacterBrain.SpecialAction(this);
    }
    
    public void DoDodge(Vector2 dir, float dodgeSpeed)
    {
        this.rb.velocity = dir * dodgeSpeed;
    }

    public void SetSAState(SAState state)
    {
        this.SAState = state;
    }

    public void SetInvulnerability(bool value)
    {
        this.IsInvulnerable = value;
    }

    public void SetDeflective(bool value)
    {
        this.IsReflectiveToProjectiles = value;
    }

    internal void ActivatedDeflective()
    {
        if (this.rb == null)
            return;

        //maybe while deflecting, make super bouncing material? To enable other interactions
        //and not only projectile deflection (e.g. player using other player deflecting to jump higher)
        this.rb.gravityScale = 0;
        this.rb.velocity = Vector2.zero;
        this.rb.mass = 500;
        this.IsReflectiveToProjectiles = true;
        this.SAState = SAState.USING;
    }

    internal void DeactivatedDeflective()
    {
        this.rb.gravityScale = 3;
        this.rb.mass = 1;
        this.IsReflectiveToProjectiles = false;
        this.SAState = SAState.COOLDOWN;
    }

    public float GetDeflectMagnetude()
    {
        return ((MageBrain)CharacterBrain).DeflectMagnetude;
    }
    #endregion

    #region [Auxiliary]
    public Vector2 GetMoveDirection()
    {
        return this.moveDirection;
    }
    #endregion
}

public enum SAState
{
    READY,
    USING,
    COOLDOWN
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
    public Grabber Grabber;

    [SerializeField]
    private CharacterBrain CharacterBrain;

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

    private bool IsInvulnerable = false;
    public bool IsReflectiveToProjectiles = false;

    public float DeflectMagnetude
    {
        get { return this.CharacterBrain.GetDeflectMagnetude(this); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        this.CharacterBrain.Initialize(this);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        this.CharacterBrain.Think(this);
        this.MoveRigidBody();
        this.ContinueJumping();

        FlipSpriteOnWalkDirection();
    }

    #region [Combat]
    public void GrabToss(CallbackContext context)
    {
        if(context.performed)
            Grabber.GrabTossAction();
    }

    public void DieDefault()
    {
        //this.Animator.SetTrigger("Die");
        Destroy(this.gameObject);
    }
    
    public void GotHit()
    {
        if (!IsInvulnerable)
            CharacterBrain.Die(this);
    }
    #endregion

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

    private void ContinueJumping()
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
        if(context.performed)
            this.CharacterBrain.SpecialAction(this);
    }

    public void Dodge(Vector2 dir, float dodgeSpeed)
    {
        Debug.Log("Yet not implemented dodge");
        //https://answers.unity.com/questions/892955/dashing-mechanic-using-rigidbodyaddforce.html
    }
    #endregion
}
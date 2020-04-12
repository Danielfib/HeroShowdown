using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{
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
    }

    public void SetDirection(Vector2 dir)
    {
        this.moveDirection = dir;
        this.moveDirection.Normalize();
    }

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

    public void Move(CallbackContext context)
    {
        SetDirection(context.ReadValue<Vector2>());
    }

    public void MoveRigidBody()
    {
        this.rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
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
}

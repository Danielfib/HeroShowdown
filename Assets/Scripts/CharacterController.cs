using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.CharacterBrain.Initialize(this);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        this.CharacterBrain.Think(this);
        this.Move();
    }

    private void Move()
    {
        this.rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    public void SetDirection(Vector2 dir)
    {
        //Debug.Log(dir);
        this.moveDirection = dir;
        this.moveDirection.Normalize();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            canContinueJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
        }

        if (canContinueJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else
            {
                canContinueJumping = false;
            }
        }
    }

    public void StoppedJump()
    {
        canContinueJumping = false;
    }
}

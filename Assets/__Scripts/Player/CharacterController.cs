using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : NetworkBehaviour
{ 
    public int PlayerIndex;
    public Grabber Grabber;

    public CharacterBrain CharacterBrain;

    public TeamIDEnum Team;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float jumpForce = 500f;

    [SerializeField]
    private Animator Animator;
    [HideInInspector]
    public RuntimeAnimatorController UIAnimator;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    [HideInInspector]
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool canContinueJumping;

    private SAState _SAState = SAState.READY;
    [HideInInspector]
    public SAState SAState
    {
        get { return _SAState; }
        set {
            _SAState = value;
        }
    }
    [HideInInspector]
    public SpriteBarCooldown sbCooldown;

    private bool IsInvulnerable = false;
    [HideInInspector]
    public bool IsReflectiveToProjectiles = false;
    [HideInInspector]
    public PlayerHUDIconController PlayerHUDIconController;

    private AnimatorsController animController;

    [SerializeField]
    private GameObject dieFXPrefab;
    [SerializeField]
    private GameObject tossFXPrefab;

    [HideInInspector]
    public int flagsScored, deathsStats, killsStats, flagsRetrieved;

    public CharacterSO SelectedHero;

    private NetworkManagerLobby lobby;
    private NetworkManagerLobby Lobby
    {
        get
        {
            if (lobby != null) { return lobby; }
            return lobby = NetworkManager.singleton as NetworkManagerLobby;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animController = GetComponentInChildren<AnimatorsController>();
        sbCooldown = GetComponentInChildren<SpriteBarCooldown>();

        if(!hasAuthority) GetComponent<PlayerInput>().actions = null;

        animController.teamIDEnum = this.Team;
        KilledAction += Killed;
        this.CharacterBrain.Initialize(this);
    }

    private void FixedUpdate()
    {
        FlipSpriteOnWalkDirection();

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        this.CharacterBrain.Think(this);

        AnimateMovement();
    }

    #region [Networking]
    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        
        Lobby.GamePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Lobby.GamePlayers.Remove(this);
    }
    #endregion

    #region [Stats_Count]
    public Action KilledAction;
    private void Killed()
    {
        this.killsStats++;
    }

    public void Scored()
    {
        flagsScored++;
    }

    private void DieCount()
    {
        this.deathsStats++;
    }

    public void RetrievedFlag()
    {
        flagsRetrieved++;
    }
    #endregion

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
        var didGrab = Grabber.GrabTossAction(dir);

        switch (didGrab)
        {
            case Grabber.GrabTossActionResults.GRABBED:
                Animate(AnimationUtils.AnimationTriggers.IS_GRAB);
                break;
            case Grabber.GrabTossActionResults.TOSSED:
                Animate(AnimationUtils.AnimationTriggers.IS_TOSS);

                //toss particle fx
                var angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
                GameObject pfx = Instantiate(tossFXPrefab);
                pfx.transform.position = transform.position;
                pfx.transform.Rotate(new Vector3(angle - 90, 0, 0));
                break;
            case Grabber.GrabTossActionResults.COULD_NOT_GRAB:
                //Animate(AnimationUtils.AnimationTriggers.IS_TOSS);
                break;
        }
    }

    public void DieDefault()
    {
        //this.Animator.SetTrigger("Die");
        var fx = Instantiate(dieFXPrefab);
        fx.transform.position = transform.position;
        fx.GetComponent<Renderer>().material.color = ColorUtils.TeamIdEnumToColor(Team);

        GetComponent<PlayerRespawnManager>().StartRespawnCounter(this.PlayerHUDIconController);
    }
    
    public void SpawnOnBase()
    {
        TeamBase teamBase = GameObject.FindObjectsOfType<TeamBase>().Where(x => x.teamIdEnum == this.Team).FirstOrDefault();
        this.gameObject.transform.position = teamBase.transform.position;
    }

    public void GotHit(Action tossingPlayerCallback)
    {
        if (!IsInvulnerable && !IsReflectiveToProjectiles)
        {
            tossingPlayerCallback?.Invoke();
            DieCount();
            DropFlag();
            CharacterBrain.Die(this);
        }
    }
    #endregion

    #region [Interact]
    public void TryInteract(CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 0.5f);
            foreach (var col in colliders)
            {
                if (col.tag == "Interactable"
                    && GetComponent<Collider2D>().IsTouching(col))
                {
                    col.GetComponent<Interactable>().InteractedAction();
                    return;
                }
            }
        }
    }

    private void DropFlag()
    {
        Flag flag = this.GetComponentInChildren<Flag>();
        if (flag != null)
            flag.Drop();
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
        {
            this.CharacterBrain.SpecialAction(this);
            Animate(AnimationUtils.AnimationTriggers.IS_SPECIAL_ACTION);
        }
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
    }

    public float GetDeflectMagnetude()
    {
        return ((MageBrain)CharacterBrain).DeflectMagnetude;
    }

    public void FinishedSpecialAction()
    {
        Animate(AnimationUtils.AnimationTriggers.ENDED_SPECIAL_ACTION);
    }
    #endregion

    #region [Auxiliary]
    public Vector2 GetMoveDirection()
    {
        return this.moveDirection;
    }
    #endregion

    #region [Animation]
    public void InitializeAnimators(RuntimeAnimatorController up, RuntimeAnimatorController lower)
    {
        if(up == null && lower == null)
        {
            Debug.LogError("No animators to asign");
        }

        Animator[] animators = this.transform.GetComponentsInChildren<Animator>();

        if(lower) animators[0].runtimeAnimatorController = lower;
        if(up) animators[1].runtimeAnimatorController = up;
    }

    private void Animate(string triggerID)
    {
        //Debug.Log(triggerID);
        this.animController?.TrySetTrigger(triggerID);
    }

    private void AnimateMovement()
    {
        if (isGrounded)
        {
            if(this.moveDirection.x != 0)
            {
                Animate(AnimationUtils.AnimationTriggers.IS_RUNNING);
            }
            else
            {
                Animate(AnimationUtils.AnimationTriggers.IS_IDLE);
            }
        } else
        {
            //Animate(AnimationUtils.AnimationTriggers.IS_JUMPING);
        }
    }
    #endregion
}

public enum SAState
{
    READY,
    USING,
    COOLDOWN
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    public ProjectileBrain ProjectileBrain;
    [HideInInspector]
    public Rigidbody2D rb;

    [SerializeField]
    private float TossMagnetude = 20;

    [SerializeField]
    private bool ShouldDestroyByTime;
    [SerializeField]
    private float DestroyAfterSeconds = 5f;

    private CharacterController ignoreCharacter;
    private Action playerKilledCallback;
    private Collider2D col;

    private void Awake()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    void Start()
    {
        ProjectileBrain.Initialize(this);
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (ShouldDestroyByTime)
            StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(DestroyAfterSeconds);
        Destroy(this.gameObject);
    }

    void Update()
    {
        ProjectileBrain.Think(this);
    }

    public void ReceiveTossAction(Vector2 dir, CharacterController cc = null)
    {
        if(cc != null)
        {
            StartCoroutine(IgnoreCharacterCoroutine(cc));
            this.transform.position = cc.transform.position;
        }
        GetComponent<Rigidbody2D>().simulated = true;
        this.ProjectileBrain.Toss(this, dir);
    }

    public void StandardToss(Vector2 dir, LayerMask layer, bool shouldDecay = true)
    {
        this.gameObject.layer = layer;

        if (shouldDecay)
            StartCoroutine(TurnOffGravityForDuration(ProjectileBrain.GetGravityDisableDurationDuringToss()));

        rb.AddForce(dir * TossMagnetude);
    }

    public void ReleaseProjectile()
    {
        ReturnToGrabbableState();   
    }

    public void ReturnToGrabbableState()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Grabbables");
        playerKilledCallback = null;
        EnableGravity(0.9f);
    }

    private void DisableGravity()
    {
        this.rb.gravityScale = 0;
    }

    public void EnableGravity(float gravityScale)
    {
        this.rb.gravityScale = gravityScale;
    }

    private IEnumerator TurnOffGravityForDuration(float duration)
    {
        float gravityScale = this.rb.gravityScale;
        DisableGravity();

        yield return new WaitForSeconds(duration);

        EnableGravity(gravityScale);
    }

    public void Explode(float radius)
    {
        //TODO
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool gotDeflected = false;

        if (collision.gameObject.tag == "Player"
            && (this.gameObject.layer == LayerMask.NameToLayer("Projectiles")
            || this.gameObject.layer == LayerMask.NameToLayer("OnlyHitsPlayers")))
        {
            CharacterController charController = collision.gameObject.GetComponent<CharacterController>();
            if (charController != this.ignoreCharacter)
            {
                charController.GotHit(playerKilledCallback);
                if (charController.IsReflectiveToProjectiles)
                {
                    //Perhaps bring this behaviour to character side?
                    //^if using deflect, for example, to bounce other players, and not only on projectiles
                    Debug.Log("Deflected!!");
                    float deflectForce = TossMagnetude * charController.GetDeflectMagnetude();
                    gotDeflected = true;
                
                    //Use this code if deflecting to direction following collision direction
                    //Vector3 reflectDir = this.transform.position - collision.gameObject.transform.position;
                    Vector2 dir = this.rb.velocity.normalized * -1;//new Vector2(reflectDir.x, reflectDir.y).normalized;
                    this.rb.velocity = Vector2.zero;
                    this.rb.AddForce(dir * deflectForce);
                }
            }
            else
            {
                //is colliding with player that tossed on the initial period of ignoring him
                Physics2D.IgnoreCollision(ignoreCharacter.GetComponentInChildren<Collider2D>(), col);
                return;
            }
        }

        if(!gotDeflected) {
            ProjectileBrain.HandleCollision(this);
        }
    }

    /// <summary>
    /// This method is used so that when player tossed the projectile, it is not hit by it initially
    /// </summary>
    /// <param name="cc"></param>
    /// <returns></returns>
    private IEnumerator IgnoreCharacterCoroutine(CharacterController cc)
    {
        this.ignoreCharacter = cc;
        playerKilledCallback = cc.KilledAction;
        yield return new WaitForSeconds(1f);
        this.ignoreCharacter = null;
    }
}
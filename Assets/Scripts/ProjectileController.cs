using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    public ProjectileBrain ProjectileBrain;
    public Rigidbody2D rb;

    [SerializeField]
    private float TossMagnetude = 20;

    void Start()
    {
        ProjectileBrain.Initialize(this);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProjectileBrain.Think(this);
    }

    public void ReceiveTossAction()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Projectiles");
        this.ProjectileBrain.Toss(this);
    }

    public void StandardToss(Vector2 dir)
    {
        //dir = MathUtils.ClampVectorTo8DiagonalVector(dir, 0.4f);
        StartCoroutine(TurnOffGravityForDuration(ProjectileBrain.GetGravityDisableDurationDuringToss()));
        StartCoroutine(TurnOffCollisionForDuration(0.1f));
        rb.AddForce(dir * TossMagnetude);
    }

    public void ReleaseProjectile()
    {
        ReturnToGrabbableState();   
    }

    public void ReturnToGrabbableState()
    {
        EnableGravity(0.9f);
        this.gameObject.layer = LayerMask.NameToLayer("Grabbables");
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

    private IEnumerator TurnOffCollisionForDuration(float duration)
    {
        this.GetComponent<CircleCollider2D>().isTrigger = true;

        yield return new WaitForSeconds(duration);

        this.GetComponent<CircleCollider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool gotDeflected = false;

        if (collision.gameObject.tag == "Player"
            && this.gameObject.layer == LayerMask.NameToLayer("Projectiles"))
        {
            CharacterController charController = collision.gameObject.GetComponent<CharacterController>();
            charController.GotHit();

            if (charController.IsReflectiveToProjectiles)
            {
                //Perhaps bring this behaviour to character side?
                //^if using deflect, for example, to bounce other players, and not only on projectiles
                Debug.Log("Deflected!!");
                float deflectForce = TossMagnetude * charController.GetDeflectMagnetude();
                
                gotDeflected = true;
                Vector3 reflectDir = this.transform.position - collision.gameObject.transform.position;
                Vector2 dir = new Vector2(reflectDir.x, reflectDir.y).normalized;
                this.rb.velocity = Vector2.zero;
                this.rb.AddForce(dir * deflectForce);
                
            }
        }

        if(!gotDeflected) {
            ProjectileBrain.HandleCollision(this);
        }
    }
}

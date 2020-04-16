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

    public void ReturnToGrabbableState()
    {
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
        ProjectileBrain.HandleCollision(this);
    }
}

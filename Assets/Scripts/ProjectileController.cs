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
        //TODO: clamp values to 8 diagonals, 
        //so controller does not have a vantage over keyboard
        StartCoroutine(TurnOffGravityForDuration(ProjectileBrain.GetGravityDisableDurationDuringToss()));
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

    private void EnableGravity(float gravityScale)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnableGravity(0.8f);
    }
}

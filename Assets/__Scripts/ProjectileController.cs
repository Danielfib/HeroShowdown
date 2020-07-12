using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    public ProjectileBrain ProjectileBrain;
    public Rigidbody2D rb;

    [SerializeField]
    private float TossMagnetude = 20;

    [HideInInspector]
    public bool ShouldDestroyByTime;
    [HideInInspector]
    public float DestroyAfterSeconds = 5f;

    void Start()
    {
        ProjectileBrain.Initialize(this);
        rb = GetComponent<Rigidbody2D>();

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

    public void ReceiveTossAction(Vector2 dir)
    {
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
            charController.GotHit();
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

        if(!gotDeflected) {
            ProjectileBrain.HandleCollision(this);
        }
    }
}

[CustomEditor(typeof(ProjectileController))]
public class ProjectileControllerEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var myScript = target as ProjectileController;
        myScript.ShouldDestroyByTime = GUILayout.Toggle(myScript.ShouldDestroyByTime, "ShouldDestroyByTime ");
        if(myScript.ShouldDestroyByTime)
            myScript.DestroyAfterSeconds = EditorGUILayout.FloatField("Destroy after: ", myScript.DestroyAfterSeconds);
    }
}
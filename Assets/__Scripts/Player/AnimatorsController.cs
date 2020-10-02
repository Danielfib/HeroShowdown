using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatorsController : MonoBehaviour
{
    private Animator[] animators;
    private CharacterController cc;

    [System.NonSerialized]
    public TeamIDEnum teamIDEnum;

    void Start()
    {
        var animators = GetComponentsInChildren<Animator>().Where(x => x.runtimeAnimatorController).ToArray();
        cc = GetComponentInParent<CharacterController>();
        this.animators = animators;
        SetupSpriteMaterials();
        FitColliderToSprites();
    }

    private void Update()
    {
        foreach(var animator in this.animators)
        {
            animator.SetBool("IsGrounded", cc.isGrounded);
        }
    }

    public void TrySetTrigger(string triggerID)
    {
        if (this.animators != null)
        {
            foreach(var animator in this.animators)
            {
                animator.SetTrigger(triggerID);
            }
        }
    }

    private void SetupSpriteMaterials()
    {
        SpriteRenderer[] spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        Color color = ColorUtils.TeamIdEnumToColor(this.teamIDEnum);

        if (spriteRenderers != null)
        {
            foreach(var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.material.SetColor("_NewColor", color);
            }
        }
    }

    private void FitColliderToSprites()
    {
        SpriteRenderer[] spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        Vector2 sum = Vector2.zero;
        int i = 0;
        Bounds bound = new Bounds(this.transform.position, Vector3.zero);
        foreach(var sr in spriteRenderers)
        {
            if (sr.sprite == null) continue;

            bound.Encapsulate(sr.bounds.max);
            bound.Encapsulate(sr.bounds.min);
            Vector2 v = new Vector2(sr.sprite.bounds.center.x, sr.sprite.bounds.center.y);
            sum += v;
            i++;
        }
        Vector2 mediumCenter = sum / i;

        BoxCollider2D col = transform.root.GetComponent<BoxCollider2D>();
        float eRad = col.edgeRadius;
        col.size = bound.size * ((1 - eRad) - col.edgeRadius * col.edgeRadius);
        col.offset = mediumCenter;
    }
}

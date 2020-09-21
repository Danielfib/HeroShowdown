using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorsController : MonoBehaviour
{
    private Animator[] animators;
    private CharacterController cc;

    [System.NonSerialized]
    public TeamIDEnum teamIDEnum;

    void Start()
    {
        var animators = GetComponentsInChildren<Animator>();
        cc = GetComponentInParent<CharacterController>();
        this.animators = animators;
        SetupSpriteMaterials();
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
}

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

        foreach(var s in GetComponentsInChildren<SwitchColorToTeamColor>()) { s.ChangeMaterialColor(teamIDEnum); }
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
}

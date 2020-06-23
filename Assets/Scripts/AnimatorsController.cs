using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorsController : MonoBehaviour
{
    private Animator[] animators;
    public bool blockAnimationTransitions;

    void Start()
    {
        var animators = GetComponentsInChildren<Animator>();
        this.animators = animators;
    }

    public void TrySetTrigger(string triggerID)
    {
        if (!blockAnimationTransitions && this.animators != null)
        {
            foreach(var animator in this.animators)
            {
                animator.SetTrigger(triggerID);
            }
        }
    }
}

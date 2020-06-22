using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsRedirector : MonoBehaviour
{
    private AnimatorsController controller;

    // Start is called before the first frame update
    void Start()
    {
        this.controller = this.GetComponentInParent<AnimatorsController>();
    }

    public void BlockAnimationTransition()
    {
        this.controller.blockAnimationTransitions = true;
    }

    public void UnblockAnimationTransition()
    {
        this.controller.blockAnimationTransitions = false;
    }
}

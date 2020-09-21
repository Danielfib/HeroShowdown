using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpriteBarCooldown : MonoBehaviour
{
    private SpriteRenderer sr;
    private Vector3 originalScale;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        this.enabled = false;
    }

    public void startCountdown(float duration)
    {
        transform.localScale = originalScale;
        this.enabled = true;
        transform.DOScaleX(0, duration).OnComplete(() => this.enabled = false);
    }
}

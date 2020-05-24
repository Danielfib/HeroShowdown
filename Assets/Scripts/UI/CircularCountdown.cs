using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularCountdown : MonoBehaviour
{
    [SerializeField]
    private Image MainImage;
    [SerializeField]
    private Image DecreasingImage;

    [SerializeField]
    private bool ShouldDestroyOnComplete;
    

    public void SetupColor(TeamIDEnum team)
    {
        this.MainImage.color = ColorUtils.TeamIdEnumToColor(team);
    }

    public void StartCountdown(float time)
    {
        this.DecreasingImage.fillAmount = 1;
        TweenCallback finishedCallback = new TweenCallback(FinishedCountdown);

        Sequence fillSequence = DOTween.Sequence();
        fillSequence.Append(this.DecreasingImage.DOFillAmount(0f, time));
        fillSequence.Play();
        fillSequence.OnComplete(finishedCallback);
    }

    private void FinishedCountdown()
    {
        if(this.ShouldDestroyOnComplete)
            Destroy(this.gameObject);
    }
}

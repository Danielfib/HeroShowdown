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
    

    public void SetupColor(TeamIDEnum team)
    {
        this.MainImage.color = ColorUtils.TeamIdEnumToColor(team);
    }

    public void StartCountdown(float time)
    {
        TweenCallback finisheCallback = new TweenCallback(FinishedCountdown);

        Sequence opa = DOTween.Sequence();
        opa.Append(this.DecreasingImage.DOFillAmount(0f, time));
        opa.Play();
        opa.OnComplete(finisheCallback);
    }

    private void FinishedCountdown()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSeat : MonoBehaviour
{
    [SerializeField]
    private GameObject bg, frontBg, joinLabel;

    public void JoinPlayer(GameObject playerGO)
    {
        joinLabel.SetActive(false);

        playerGO.transform.SetParent(transform, false);
        GameObject graphicalComponents = playerGO.transform.GetChild(0).gameObject;
        graphicalComponents.transform.localScale = Vector3.zero;

        RectTransform frontBGTransform = frontBg.GetComponent<RectTransform>();
        frontBGTransform.DOSizeDelta(new Vector2(frontBGTransform.sizeDelta.x, 340), 0.4f).OnComplete(() =>
        {
            graphicalComponents.transform.localScale = Vector3.one;
        }).SetEase(Ease.Linear);
    }

    public void DejoinPlayer()
    {
        //TODO
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractableButtonIcon : MonoBehaviour
{
    [SerializeField]
    private Sprite controllerButtonSprite;
    [SerializeField]
    private Sprite KeyboardButtonSprite;

    private SpriteRenderer sr;

    private void Start()
    {
        this.sr = this.GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    public void SetupAndAppear(string inputDevice)
    {
        switch (inputDevice)
        {
            case "Controller":
                sr.sprite = controllerButtonSprite;
                break;
            case "Keyboard":
                sr.sprite = KeyboardButtonSprite;
                break;
        }

        sr.enabled = true;
    }

    public void Hide()
    {
        sr.enabled = false;
    }

    #region [Messages_Methods]
    private void InteractableCooldownStart()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f);
    }

    private void InteractableCooldownEnd()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
    }
    #endregion
}



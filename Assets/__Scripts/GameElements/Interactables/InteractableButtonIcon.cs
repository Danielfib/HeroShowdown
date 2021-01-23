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

    public void SetupAndAppear(DeviceType inputDevice)
    {
        if(inputDevice == null)
        {
            sr.sprite = KeyboardButtonSprite;
        } else
        {
            if(inputDevice.Contains("Controller"))
                sr.sprite = controllerButtonSprite;
            else if(inputDevice.Equals("Keyboard"))
                sr.sprite = KeyboardButtonSprite;
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



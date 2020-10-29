using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class MS_PlayerInputHandler : MonoBehaviour
{
    private MS_OptionsManager optionsManager;

    void Start()
    {
        optionsManager = GameObject.FindObjectOfType<MS_OptionsManager>();
        optionsManager.InitializeSelector();
    }

    public void NavUp(CallbackContext context)
    {
        if (optionsManager == null || !context.performed) return;

        optionsManager.MSNavigate(Vector2.up);
    }

    public void NavDown(CallbackContext context)
    {
        if (optionsManager == null || !context.performed) return;

        optionsManager.MSNavigate(Vector2.down);
    }

    public void Submit(CallbackContext context)
    {
        if (optionsManager == null || !context.performed) return;

        optionsManager.Submit();
    }

    public void Cancel(CallbackContext context)
    {
        if (optionsManager == null || !context.performed) return;

        optionsManager.Cancel();
    }
}

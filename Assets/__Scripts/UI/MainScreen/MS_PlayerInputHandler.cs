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

    public void Navigate(CallbackContext context)
    {
        if (optionsManager == null || !context.performed) return;

        Vector2 dir = context.ReadValue<Vector2>();
        optionsManager.Navigate(dir);
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

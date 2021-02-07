using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using Mirror;

[RequireComponent(typeof(Collider2D))]
public class Interactable : NetworkBehaviour
{
    public Action InteractedAction;

    [SerializeField]
    private GameObject ButtonIconGO;

    private InteractableButtonIcon buttonIcon;

    public float cooldownTime = 2f;

    [SyncVar(hook = nameof(HandleCooldownStatusChange))]
    private bool isOnCooldown = false;

    public void HandleCooldownStatusChange(bool oldValue, bool newValue)
    {
        if(newValue)
            BroadcastMessage("InteractableCooldownStart");
        else
            BroadcastMessage("InteractableCooldownEnd");
    }

    private void Start()
    {
        this.buttonIcon = Instantiate(ButtonIconGO, this.transform).GetComponent<InteractableButtonIcon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var cc = collision.gameObject.GetComponent<CharacterController>();
            if (cc.isLocalPlayer)
            {
                PlayerInput playerInput = collision.gameObject.GetComponent<PlayerInput>();
                string deviceName = playerInput.devices.Count == 0 ? null : playerInput.devices[0].name;
                buttonIcon.SetupAndAppear(deviceName);
            }
        }
    }

    [Server]
    public void TryInteract()
    {
        if (!isOnCooldown)
        {
            InteractedAction.Invoke();
            StartCoroutine(CooldownCoroutine());
        }
    }

    [Server]
    private IEnumerator CooldownCoroutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(this.cooldownTime);
        isOnCooldown = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerInput playerInput = collision.gameObject.GetComponent<PlayerInput>();

            buttonIcon.Hide();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonIconGO;

    private InteractableButtonIcon buttonIcon;

    public float cooldownTime = 2f;
    private bool isOnCooldown;

    private void Start()
    {
        this.buttonIcon = Instantiate(ButtonIconGO, this.transform).GetComponent<InteractableButtonIcon>();
        BroadcastMessage("SetupCooldown", cooldownTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerInput playerInput = collision.gameObject.GetComponent<PlayerInput>();

            string deviceName = playerInput.devices.Count == 0 ? null : playerInput.devices[0].name;
            buttonIcon.SetupAndAppear(deviceName);
        }
    }

    public void InteractedAction()
    {
        if (!isOnCooldown)
        {
            DoInteract();
        }
    }

    private void DoInteract()
    {
        BroadcastMessage("PlayerInteracted");
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        this.isOnCooldown = true;
        BroadcastMessage("InteractableCooldownStart");
        yield return new WaitForSeconds(this.cooldownTime);
        this.isOnCooldown = false;
        BroadcastMessage("InteractableCooldownEnd");
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

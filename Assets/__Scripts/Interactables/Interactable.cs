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

    private void Start()
    {
        this.buttonIcon = Instantiate(ButtonIconGO, this.transform).GetComponent<InteractableButtonIcon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerInput playerInput = collision.gameObject.GetComponent<PlayerInput>();

            //if(playerInput.devices[0].)
            buttonIcon.SetupAndAppear(playerInput.devices[0].name);
        }
    }

    public void Interacted()
    {
        SendMessage("PlayerInteracted");
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

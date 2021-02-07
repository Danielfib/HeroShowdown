﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRespawnManager : MonoBehaviour
{
    [SerializeField]
    private float SpawnCooldown = 10f;

    [SerializeField]
    private Collider2D Collider;
    [SerializeField]
    private PlayerController CharacterController;
    [SerializeField]
    private PlayerInput PlayerInput;
    [SerializeField]
    private Rigidbody2D rb;

    private bool isRespawning = false;
    private float SpawnCounter;

    private void Start()
    {
        SpawnCounter = SpawnCooldown;
    }

    private void Update()
    {
        if (isRespawning)
        {
            SpawnCounter -= Time.deltaTime;
            if(SpawnCounter <= 0)
            {
                Spawned();
            }
        }
    }

    public void StartRespawnCounter(PlayerHUDIconController iconController)
    {
        isRespawning = true;
        ToggleComponentsEnabled(false);
        iconController.StartRespawnCircle(this.SpawnCooldown);
    }

    private void Spawned()
    {
        isRespawning = false;
        SpawnCounter = SpawnCooldown;
        ToggleComponentsEnabled(true);
        SendMessage("SpawnOnBase");
    }

    private void ToggleComponentsEnabled(bool value)
    {
        for(var i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(value);
        }

        this.Collider.enabled = value;
        this.CharacterController.enabled = value;
        this.PlayerInput.enabled = value;
        this.rb.simulated = value;
    }
}

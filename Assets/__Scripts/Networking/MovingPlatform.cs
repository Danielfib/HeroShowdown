using System;
using UnityEngine;
using Mirror;
using DG.Tweening;
using System.Collections;
using System.Linq;

public class MovingPlatform : NetworkBehaviour 
{
    public float speed = 5;
    public Transform platform;
    public Rigidbody2D rigidbody2d;
    public Transform positionsHolder;

    private Transform[] positions;
    private Vector3 currentTargetPos;
    private int currentTargetIndex = 0;

    public override void OnStartServer()
    {
        base.OnStartServer();

        // only simulate ball physics on server
        rigidbody2d.simulated = true;

        SetupPositions();
        GoToNextPosition();
    }

    private void SetupPositions()
    {
        positions = new Transform[positionsHolder.childCount];
        for (var i = 0; i < positions.Length; i++)
        {
            positions[i] = positionsHolder.GetChild(i);
        }

        currentTargetPos = positions[0].position;
    }

    [Server]
    private void GoToNextPosition()
    {
        DoGoToNextPosition();
    }

    private void DoGoToNextPosition()
    {
        //calculate transition duration based on fixed speed
        float duration = (currentTargetPos - platform.transform.position).magnitude / speed;

        platform.DOMove(currentTargetPos, duration).OnComplete(() => 
        {
            currentTargetIndex = positions.GetNextIndexOfArray(currentTargetIndex);
            currentTargetPos = positions[currentTargetIndex].position;
            GoToNextPosition();
        }).SetEase(Ease.Linear);
    }
}

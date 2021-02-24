using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class OutOfBoundsHandler : MonoBehaviour
{
    public ParHandler parHandler;
    public DragPower dragPower;

    public Collision player;

    public bool resetPosition;

    public bool checkForMovement;

    private void Start()
    {
        resetPosition = false;
        checkForMovement = false;
    }

    public void Update()
    {
        if (checkForMovement)
        {
            //if ball is not moving then request to reset back to last position
            if (dragPower.ball.IsSleeping())
            {
                resetPosition = true;

                checkForMovement = false;
            }
        }

        if (resetPosition)
        {
            //move back to last position
            player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
            resetPosition = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        checkForMovement = true;

        player = collision;
    }
}

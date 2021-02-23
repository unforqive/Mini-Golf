using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragPower : MonoBehaviour
{
    public Rigidbody ball = null;

    public Camera playerCamera;

    public int strokes = 0;

    public float minHitPower = 1f;
    public float maxHitPower = 100f;
    public float hitPower;

    private float powerIncrement = 5f;
    private float powerMultiplier = 10;

    public float lastPower;

    public Slider powerBar;
    public bool increasing = true;

    public CameraController cameraController;

    public GameObject Line;

    public bool isMoving = false;
    public bool canRotate;

    public GameHandler gameHandler;

    private Vector3 ballDir;

    public bool preparingToShoot;

    void Start()
    {
        preparingToShoot = false;
        powerBar.gameObject.transform.parent.gameObject.SetActive(false);
        canRotate = true;
        this.gameObject.SetActive(false);
        ball.sleepThreshold = 0.05f; //default is 0.005f;
    }

    void Update()
    {
        if (ball != null)
        {
            if (Input.GetMouseButton(0) && !isMoving && !cameraController.cancelShot)
            {
                preparingToShoot = true;
                CalculatePower();
            }

            if (Input.GetMouseButtonUp(0) && !isMoving && !cameraController.cancelShot)
            {
                cameraController.cancelShot = false;
                preparingToShoot = false;
                powerBar.gameObject.transform.parent.gameObject.SetActive(false);
                lastPower = hitPower;
                //calculate direction to hit ball
                ballDir = gameHandler.playerCamera.transform.forward.normalized;
                ballDir.y = 0;

                HitBall(hitPower);

                isMoving = true;
                canRotate = false;
            }

            if (Input.GetMouseButtonUp(0) && cameraController.cancelShot)
            {
                cameraController.cancelShot = false;
                powerBar.gameObject.transform.parent.gameObject.SetActive(true);
            }
        }

        if (cameraController.cancelShot)
        {
            powerBar.gameObject.transform.parent.gameObject.SetActive(false);
        }
        
        //Detect when ball stops
        if (isMoving)
        {
            if (ball.IsSleeping())
            {
                isMoving = false;
                canRotate = true;
                powerBar.gameObject.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                return;
            }
        }

        //Update slider
        hitPower = powerBar.value;
    }

    public void CalculatePower()
    {
       //Increase power if less than max power
       if (increasing)
       {
            if (hitPower < maxHitPower)
            {
                hitPower += powerIncrement * powerMultiplier;
                increasing = true;
            }
            else if (hitPower >= maxHitPower)
            {
                increasing = false;
            }
       }

       //Decrease power if power level is not increasing until power hits minimum threshold
       if (!increasing)
       {
            if (hitPower > minHitPower)
            {
                hitPower -= powerIncrement * powerMultiplier;
            }
            else if (hitPower <= minHitPower)
            {
                increasing = true;
            }
       }
    }

    public void HitBall (float power)
    {
        //add force to ball
        ball.AddForce(ballDir * power, ForceMode.Impulse);
    }
}

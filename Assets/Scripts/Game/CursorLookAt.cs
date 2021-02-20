using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLookAt : MonoBehaviour
{
    public float speed = 5;

    public bool showCursor;
    public float sensitivity;

    private Quaternion camRotation;
    private float lookUpMin = -50;
    private float lookUpMax = 50;

    public float cameraSmoothFactor = 1;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (!showCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        camRotation.x += speed * Input.GetAxis("Mouse Y") * cameraSmoothFactor * (-1);
        camRotation.y += speed * Input.GetAxis("Mouse X") * cameraSmoothFactor;

        camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);

        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);
    }
}

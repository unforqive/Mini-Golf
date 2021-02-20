using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraParent;
    public float speed = 5f;
    public float moveSpeed = 0.25f;

    public Transform player;

    private Quaternion camRotation;
    private float lookUpMin = -50;
    private float lookUpMax = 50;

    public float newMinX;
    public float newMaxX;

    public float newMinY;
    public float newMaxY;

    public float newMinZ;
    public float newMaxZ;

    public float cameraSmoothFactor = 1;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            camRotation.x += speed * Input.GetAxis("Mouse Y") * cameraSmoothFactor * (-1);
            camRotation.y += speed * Input.GetAxis("Mouse X") * cameraSmoothFactor;

            camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);

            cameraParent.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);
        }

        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }

        cameraParent.Translate(p_Velocity * moveSpeed, Space.Self);

        float minX = player.position.x - newMinX;
        float maxX = player.position.x - newMaxX;

        float minY = player.position.y - newMinY;
        float maxY = player.position.y - newMaxY;

        float minZ = player.position.z - newMinZ;
        float maxZ = player.position.z - newMaxZ;

        float posX = Mathf.Clamp(transform.position.x, minX, maxX);
        float posY = Mathf.Clamp(transform.position.y, minY, maxY);
        float posZ = Mathf.Clamp(transform.position.z, minZ, maxZ);

        cameraParent.position = new Vector3(posX, posY, posZ);
    }
}
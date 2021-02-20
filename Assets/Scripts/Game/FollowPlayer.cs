using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerCamera;
    public Transform target;

    public Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = playerCamera.transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = target.transform.position + cameraOffset;
        playerCamera.transform.position = newPosition;
    }
}

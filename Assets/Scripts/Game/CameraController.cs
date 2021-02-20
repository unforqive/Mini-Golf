using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5;

    public Transform target;

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            transform.eulerAngles += speed * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * speed, Space.Self);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * speed, Space.Self);
        }

        //transform.position = new Vector3(Mathf.Clamp(target.position.x, 1f, 1f), Mathf.Clamp(target.position.y, 1f, 1f), Mathf.Clamp(target.position.y, 1f, 1f));
    }
}

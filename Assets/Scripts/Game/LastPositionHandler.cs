using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPositionHandler : MonoBehaviour
{
    public GameObject LastPosition;

    public ParHandler parHandler;
    public DragPower dragPower;

    void Start()
    {
        
    }

    void Update()
    {
        LastPosition.transform.position = dragPower.lastPosition;
    }
}

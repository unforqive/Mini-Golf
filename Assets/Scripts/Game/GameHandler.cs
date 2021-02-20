using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject menuCamera;

    public GameObject playerCamera;
    public GameObject spectatorCamera;
    void Start()
    {
        menuCamera.SetActive(true);
        playerCamera.SetActive(false);
        spectatorCamera.SetActive(false);
    }

    void Update()
    {
        
    }

    public void EnablePlayerCamera()
    {
        menuCamera.SetActive(false);
        playerCamera.SetActive(true);
        spectatorCamera.SetActive(false);
    }

    public void EnableSpectatorCamera()
    {
        menuCamera.SetActive(false);
        playerCamera.SetActive(false);
        spectatorCamera.SetActive(true);
    }
}

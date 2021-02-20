using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject menuCamera;

    public GameObject playerCamera;
    public GameObject spectatorCamera;

    public MenuController menuController;

    public CursorLock cursorLock;
    void Start()
    {
        menuCamera.SetActive(true);
        playerCamera.SetActive(false);
        spectatorCamera.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            //return to main menu
            playerCamera.SetActive(false);
            menuCamera.SetActive(true);

            MenuController.nextMenu = "Start Menu";
            menuController.returnToMenu = true;

            cursorLock.showCursor();
        }
    }

    public void EnablePlayerCamera()
    {
        menuCamera.SetActive(false);
        playerCamera.SetActive(true);
        spectatorCamera.SetActive(false);
        cursorLock.hideCursor();
    }

    public void EnableSpectatorCamera()
    {
        menuCamera.SetActive(false);
        playerCamera.SetActive(false);
        spectatorCamera.SetActive(true);
    }
}

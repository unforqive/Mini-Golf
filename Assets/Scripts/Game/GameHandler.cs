using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject menuCamera;
    public GameObject Player;

    public GameObject playerCamera;
    public GameObject spectatorCamera;

    public MenuController menuController;

    public CursorLock cursorLock;

    void Start()
    {
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
            menuController.inGame = false;
        }
    }

    public void EnablePlayerCamera()
    {
        Player.SetActive(true);
        menuCamera.SetActive(false);
        spectatorCamera.SetActive(false);   
    }

    public void EnableSpectatorCamera()
    {
        menuCamera.SetActive(false);
        playerCamera.SetActive(false);
        spectatorCamera.SetActive(true);
    }
}

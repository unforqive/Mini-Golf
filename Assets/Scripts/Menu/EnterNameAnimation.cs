using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNameAnimation : MonoBehaviour
{
    public GameObject enterButton;
    public GameObject displayName;
    public Animator enterNameAnimation;

    public MenuController menuController;
    public LoadName loadName;

    private int animationTimer;
    private bool startAnimationTimer;

    private int loggingInTimer;
    private bool startLoggingInTimer;

    void Start()
    {
        displayName.SetActive(false);
        animationTimer = 0;

        loggingInTimer = 0;
    }

    void Update()
    {
        if (startAnimationTimer)
        {
            animationTimer += 1;
        }

        if (animationTimer == 200)
        {
            displayName.SetActive(true);
            startAnimationTimer = false;
        }

        if (startLoggingInTimer)
        {
            loggingInTimer += 1;
        }

        if (loggingInTimer == 400)
        {
            displayName.SetActive(false);
            menuController.BeginningScreen();
            loadName.displayName.SetActive(true);
            loadName.DisplayName();
        }
    }

    public void EnterButton ()
    {
        enterNameAnimation.SetBool("Name Disappear", true);
        startAnimationTimer = true;
        startLoggingInTimer = true;
    }
}

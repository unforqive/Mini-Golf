using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNameAnimation : MonoBehaviour
{
    public GameObject enterButton;
    public GameObject displayName;
    public Animator enterNameAnimation;
    public Animator splashScreenAnimation;

    public MenuController menuController;
    public LoadName loadName;
    public EnterName enterName;
    public DefaultSettings defaultSettings;

    private int animationTimer;
    private bool startAnimationTimer;

    private int loggingInTimer;
    private bool startLoggingInTimer;

    private int waitTimer;
    private bool startWaitTimer;

    void Start()
    {
        displayName.SetActive(false);
        animationTimer = 0;

        loggingInTimer = 0;

        waitTimer = 0;

        if (enterName.hasEnteredUserName)
        {
            displayName.SetActive(true);
            enterName.LoadName();
            startWaitTimer = true;
        }

        //if first time playing
        if (!enterName.hasEnteredUserName)
        {
            enterNameAnimation.SetBool("Name Disappear", false);
            enterNameAnimation.SetBool("Name Appear", true);

            defaultSettings.Default();
        }
    }

    void Update()
    {
        if (startWaitTimer)
        {
            waitTimer += 1;
        }

        if (waitTimer == 1)
        {
            startLoggingInTimer = true;
            startWaitTimer = false;
        }

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
            splashScreenAnimation.SetBool("Splash Screen Exit", true);
        }

        if (loggingInTimer == 430)
        {
            menuController.BeginningScreen();
            loadName.displayName.SetActive(true);
            loadName.DisplayName();
        }
    }

    public void EnterButton ()
    {
        enterNameAnimation.SetBool("Name Disappear", true);
        enterNameAnimation.SetBool("Name Appear", false);
        startAnimationTimer = true;
        startLoggingInTimer = true;
    }
}

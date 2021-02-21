using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Leaderboard : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public RectTransform leaderboardBackground;

    public GameObject leaderboard;

    public MenuController menuController;

    public GameObject clones;

    public Animator leaderboardAnimation;

    public int amountOfBots;
    public int amountOfPlayers;

    public int animationTimer;
    public bool startAnimationTimer;

    private bool canPressTab;

    void Start()
    {
        leaderboard.SetActive(false);
        amountOfBots = 1;
        amountOfPlayers = 2;
        canPressTab = false;

        animationTimer = 0;
    }

    void Update()
    {
        if (startAnimationTimer)
        {
            animationTimer += 1;
        }

        if (animationTimer > 55)
        {
            clones.SetActive(true);

            startAnimationTimer = false;
            animationTimer = 0;
        }

        if(canPressTab)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && menuController.inGame)
            {
                startAnimationTimer = true;
                entryTemplate.gameObject.SetActive(true);

                leaderboard.SetActive(true);

                leaderboardAnimation.SetBool("In", true);
                leaderboardAnimation.SetBool("Out", false);
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                startAnimationTimer = false;
                animationTimer = 0;

                clones.SetActive(false);
                entryTemplate.gameObject.SetActive(true);

                leaderboardAnimation.SetBool("In", false);
                leaderboardAnimation.SetBool("Out", true);
            }
        } 
    }

    public void CreateLeaderboard()
    {
        entryTemplate.gameObject.SetActive(false);

        canPressTab = true;

        float templateHeight = 100f;

        for (int i = 0; i < amountOfPlayers; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            entryTransform.SetParent(clones.transform);
            clones.SetActive(false);

            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);
        }

        if (amountOfPlayers == 2)
        {
            leaderboardBackground.sizeDelta = new Vector2(2000, 1000);
            entryContainer.position = new Vector3(960, 550, 0);
        }

        if (amountOfPlayers == 3)
        {
            leaderboardBackground.sizeDelta = new Vector2(2000, 1200);
        }

        if (amountOfPlayers == 4)
        {
            leaderboardBackground.sizeDelta = new Vector2(2000, 1400);
            entryContainer.position = new Vector3(960, 640, 0);
        }

        //Max Players (4 bots, 1 human)
        if (amountOfPlayers == 5)
        {
            leaderboardBackground.sizeDelta = new Vector2(2000, 1600);
            entryContainer.position = new Vector3(960, 700, 0);
        }
    }

    public void ShowLeaderboard()
    {
        leaderboard.SetActive(true);
    }

    public void HideLeaderboard()
    {
        leaderboard.SetActive(false);
    }
}

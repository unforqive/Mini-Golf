using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public RectTransform leaderboardBackground;

    public GameObject leaderboard;

    public int amountOfBots;
    public int amountOfPlayers;

    private bool canPressTab;

    void Start()
    {
        leaderboard.gameObject.SetActive(false);
        amountOfBots = 1;
        amountOfPlayers = 2;
        canPressTab = false;
    }

    void Update()
    {
        if(canPressTab)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ShowLeaderboard();
                Debug.Log("show leader");
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                HideLeaderboard();
                Debug.Log("hide leader");
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
        leaderboard.gameObject.SetActive(false);
    }
}

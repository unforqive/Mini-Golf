using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [Header("Sliders")]
    public Slider amountOfBots;
    public Slider friction;
    public Slider gravity;
    public Slider bounciness;

    public Leaderboard leaderboard;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetAmountOfBots()
    {
        leaderboard.amountOfBots = Convert.ToInt32(amountOfBots.value);
        leaderboard.amountOfPlayers = leaderboard.amountOfBots + 1;
    }
}

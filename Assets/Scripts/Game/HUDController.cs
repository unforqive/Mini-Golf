using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject ShotPowerContainer;
    public GameObject LeaderboardContainer;

    void Start()
    {
        ShotPowerContainer.SetActive(true);
        LeaderboardContainer.SetActive(true);
    }
}

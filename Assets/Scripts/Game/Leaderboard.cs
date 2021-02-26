using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboard;

    public EnterName enterName;

    public GameObject NAME;

    public MenuController menuController;

    public Animator leaderboardAnimation;

    void Start()
    {
        leaderboard.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && menuController.inGame)
        {
            NAME.GetComponent<TMPro.TMP_Text>().text = enterName.userName;
            leaderboard.SetActive(true);

            leaderboardAnimation.SetBool("Out", true);
            leaderboardAnimation.SetBool("In", false);
        }

        if (Input.GetKeyUp(KeyCode.Tab) && menuController.inGame)
        {
            leaderboardAnimation.SetBool("Out", false);
            leaderboardAnimation.SetBool("In", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnterName : MonoBehaviour
{
    public string userName;
    public GameObject inputField;
    public GameObject textDisplay;

    public bool hasEnteredUserName = false;

    void Awake()
    {
        userName = PlayerPrefs.GetString("PlayerName");
        hasEnteredUserName = (PlayerPrefs.GetInt("HasEnteredName") != 0);
    }

    void Start()
    {
        
    }

    public void Storename()
    {
        userName = inputField.GetComponent<TMPro.TMP_Text>().text;
        textDisplay.GetComponent<TMPro.TMP_Text>().text = "Logging in as " + userName;

        hasEnteredUserName = true;

        PlayerPrefs.SetString("PlayerName", userName);
        PlayerPrefs.SetInt("HasEnteredName", hasEnteredUserName ? 1 : 0);
    }

    public void LoadName()
    {
        textDisplay.GetComponent<TMPro.TMP_Text>().text = "Logging in as " + userName;
    }
}

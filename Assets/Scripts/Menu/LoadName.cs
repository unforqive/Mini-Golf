using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadName : MonoBehaviour
{
    public GameObject displayName;
    public EnterName enterName;
    public TMPro.TMP_Text text;

    void Start()
    {
        displayName.SetActive(false);
    }

    public void DisplayName()
    {
        text.text = enterName.userName;
    }
}

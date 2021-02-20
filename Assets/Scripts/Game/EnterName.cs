using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnterName : MonoBehaviour
{
    public string userName;
    public GameObject inputField;
    public GameObject textDisplay;

    public void Storename()
    {
        userName = inputField.GetComponent<TMPro.TMP_Text>().text;
        textDisplay.GetComponent<TMPro.TMP_Text>().text = "Logging in as " + userName;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShotValueToText : MonoBehaviour
{
    public Slider sliderUI;
    public TMPro.TMP_Text textSliderValue;

    void Start()
    {
        textSliderValue = GetComponent<TMPro.TMP_Text>();
        ShowSliderValue();
    }

    public void ShowSliderValue()
    {
        string sliderMessage =  sliderUI.value.ToString();
        textSliderValue.text = sliderMessage;
    }
}

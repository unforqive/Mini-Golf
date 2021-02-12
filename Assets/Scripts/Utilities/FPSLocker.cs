using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLocker : MonoBehaviour
{
    public TMPro.TMP_Text fpsText;

    void Awake()
    {
        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
    }
    public void Update()
    {
        if (Application.targetFrameRate == 60)
        {
            fpsText.text = "60 FPS";
        }

        if (Application.targetFrameRate == 144)
        {
            fpsText.text = "144 FPS";
        }
    }

    public void SetLowFrameRate()
    {
        Application.targetFrameRate = 60;
    }

    public void SetHighFrameRate()
    {
        Application.targetFrameRate = 144;
    }

    public void SetFrameRate()
    {
        PlayerPrefs.SetInt("FPS", Application.targetFrameRate);
    }
}

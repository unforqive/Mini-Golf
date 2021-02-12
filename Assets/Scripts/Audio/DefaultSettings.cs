using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DefaultSettings : MonoBehaviour
{
    public Slider soundtrackSlider, sfxSlider, fovSlider;
    private float soundtrackFloat, sfxFloat, fov;

    public int gameQuality;
    public TMP_Dropdown qualityDropDown;

    public int fpsLimit;

    public PostProcessing postProcess;

    //Contains a list of default values 
    public void Default()
    {
        soundtrackFloat = 2;
        sfxFloat = 10;
        soundtrackSlider.value = soundtrackFloat;
        sfxSlider.value = sfxFloat;

        fov = 50;
        fovSlider.value = fov;

        postProcess.EnablePostProcessing(true);

        gameQuality = 0;

        qualityDropDown.value = gameQuality;

        fpsLimit = 60;
        Application.targetFrameRate = fpsLimit;
    }
}

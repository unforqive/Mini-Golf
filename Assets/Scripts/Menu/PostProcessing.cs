using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
using JetBrains.Annotations;

public class PostProcessing : MonoBehaviour
{
    public PostProcessVolume volume;

    private AmbientOcclusion ambientOcc;
    private Vignette vignette;
    private DepthOfField dof;

    public TMPro.TMP_Text postText;

    public TMPro.TMP_Text ambientText;
    public TMPro.TMP_Text vignetteText;
    public TMPro.TMP_Text dofText;

    public float floatValue;

    public void Awake()
    {
        PostProcessingActive.postProcessingActive = (PlayerPrefs.GetInt("Post Processing Value") != 0);
    }
    private void Start()
    {
        volume.profile.TryGetSettings(out ambientOcc);
        volume.profile.TryGetSettings(out vignette);
        volume.profile.TryGetSettings(out dof);

        ambientOcc.active = false;
        vignette.active = false;
        dof.active = false;

        if (PostProcessingActive.postProcessingActive)
        {
            EnablePostProcessing(true);
        }
        else
        {
            return;
        }
    }

    public void EnablePostProcessing (bool value)
    {
        if (value)
        {
            ambientOcc.active = true;
            vignette.active = true;
            dof.active = true;

            postText.text = "ON";

            ambientText.text = "ON";
            vignetteText.text = "ON";
            dofText.text = "ON";

            PostProcessingActive.postProcessingActive = true;
        }

        if (!value)
        {
            ambientOcc.active = false;
            vignette.active = false;
            dof.active = false;

            postText.text = "OFF";

            ambientText.text = "OFF";
            vignetteText.text = "OFF";
            dofText.text = "OFF";

            PostProcessingActive.postProcessingActive = false;
        }
    }

    public void EnableAmbientOcclusion (bool value)
    {
        if (PostProcessingActive.postProcessingActive)
        {
            if (value)
            {
                ambientOcc.active = true;

                ambientText.text = "ON";
            }

            if (!value)
            {
                ambientOcc.active = false;

                ambientText.text = "OFF";
            }
        }

        else
        {
            return;
        }
    }

    public void EnableVignette(bool value)
    {
        if (PostProcessingActive.postProcessingActive)
        {
            if (value)
            {
                vignette.active = true;

                vignetteText.text = "ON";
            }

            if (!value)
            {
                vignette.active = false;

                vignetteText.text = "OFF";
            }
        }

        else
        {
            return;
        } 
    }

    public void EnableDepthOfField(bool value)
    {
        if (PostProcessingActive.postProcessingActive)
        {
            if (value)
            {
                dof.active = true;

                dofText.text = "ON";
            }

            if (!value)
            {
                dof.active = false;

                dofText.text = "OFF";
            }
        }

        else
        {
            return;
        }
    }

    public void SetPostProcessing()
    {
        PlayerPrefs.SetInt("Post Processing Value", PostProcessingActive.postProcessingActive ? 1 : 0);
    }
}

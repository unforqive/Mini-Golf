using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FOVHandler : MonoBehaviour
{
    #region Public Variables

    public float fov;

    [Header("Sliders")]
    public Slider fovSlider;

    [Header("Game Objects")]
    Camera mainCamera;

    #endregion

    void Awake()
    {
        fovSlider.value = PlayerPrefs.GetFloat("FOV Value");

        mainCamera = GetComponent<Camera>();
        mainCamera.fieldOfView = fov;
    }

    public void Start()
    {
        fov = mainCamera.fieldOfView;
    }

    public void Update()
    {
        mainCamera.fieldOfView = fovSlider.value;
    }

    public void FieldOfView(float zoom)
    {
        fov = zoom;
    }

    public void SetFov()
    {
        PlayerPrefs.SetFloat("FOV Value", fovSlider.value);
    }
}

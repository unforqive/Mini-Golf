using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviour
{
    #region Public Variables

    [Header("Audio Sources")]
    public AudioSource soundtrack;
    public AudioSource sfx;

    [Header("Audio Mixers")]
    public AudioMixer soundtrackMixer;
    public AudioMixer sfxMixer;

    [Header("Sliders")]
    public Slider soundtrackSlider;
    public Slider sfxSlider;

    [Header("Soundtracks")]
    public AudioClip titleSoundtrack;

    [Header("Sound FX")]
    public AudioClip popSFX;
    public AudioClip swooshSFX;
    public AudioClip longSwooshSFX;

    #endregion

    void Update()
    {
        soundtrack.volume = soundtrackSlider.value / 10;
        sfx.volume = sfxSlider.value / 10;
    }

    void Awake()
    {
        soundtrackSlider.value = PlayerPrefs.GetFloat("Soundtrack Volume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume");
    }

    public void SetVolume()
    {
        PlayerPrefs.SetFloat("Soundtrack Volume", soundtrackSlider.value);
        PlayerPrefs.SetFloat("SFX Volume", sfxSlider.value);
    }

    public void PlayPopSFX()
    {
        sfx.PlayOneShot(popSFX);
    }

    public void PlaySwooshSFX()
    {
        sfx.PlayOneShot(swooshSFX);
    }

    public void PlayLongSwooshSFX()
    {
        sfx.PlayOneShot(longSwooshSFX);
    }
}

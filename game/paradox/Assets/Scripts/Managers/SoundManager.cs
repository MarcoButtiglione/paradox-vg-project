using System;
using System.Collections;
using System.Collections.Generic;using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider effectVolume;
    [SerializeField] private Slider themeVolume;
    [SerializeField] private Slider masterVolume;

    public void Start()
    {
        Debug.Log(AudioManager.instance.GetEffectVolume());
        Debug.Log(AudioManager.instance.GetThemeVolume());
        Debug.Log(AudioManager.instance.GetMasterVolume());


        effectVolume.value = AudioManager.instance.GetEffectVolume();
        themeVolume.value = AudioManager.instance.GetThemeVolume();
        //masterVolume.value = AudioManager.instance.GetMasterVolume();
    }

    public void SetEffectVolume(float volume)
    {
        AudioManager.instance.SetEffectVolume(volume);
    }

    public void SetThemeVolume(float volume)
    {
        AudioManager.instance.SetThemeVolume(volume);
    }
    public void SetMasterVolume(float volume)
    {
        AudioManager.instance.SetMasterVolume(volume);
    }
}

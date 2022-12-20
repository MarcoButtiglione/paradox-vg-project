using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
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

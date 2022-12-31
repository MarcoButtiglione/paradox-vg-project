using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{
    public Toggle fullscreenToggle;
    
    // Start is called before the first frame update
    void Start()
    {
        fullscreenToggle.isOn = true;
    }
    public void FullScreene()
    {
        if(fullscreenToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            Screen.fullScreen = true;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Screen.fullScreen = false;
        }
    }

    public void NotFullScreen()
    {
        QualitySettings.vSyncCount = 0;
    }

}

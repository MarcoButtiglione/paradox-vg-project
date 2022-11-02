using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        LevelManager.Instance.PlayFirstLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}

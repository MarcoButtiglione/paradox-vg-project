using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject selectLevelMenu;
    public GameObject mainMenu;
    public GameObject feedbackMenu;
    public GameObject optionsMenu;
    public GameObject soundsMenu;
    public GameObject resoultionMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        selectLevelMenu.SetActive(false);
        feedbackMenu.SetActive(false);
        optionsMenu.SetActive(false);
        resoultionMenu.SetActive(false);
        soundsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        LevelManager.Instance.PlayFirstLevel();
    }

    public void SelectLevel()
    {
        mainMenu.SetActive(false);
        selectLevelMenu.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        selectLevelMenu.SetActive(false);
        feedbackMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void DisActivate(GameObject menu)
    {
        menu.SetActive(false);
    }
    public void Activate(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void Sounds()
    {
        optionsMenu.SetActive(false);
        soundsMenu.SetActive(true);
    }

    public void Resolutions()
    {
        optionsMenu.SetActive(false);
        resoultionMenu.SetActive(true);
    }
    
    public void FeedBack()
    {
        mainMenu.SetActive(false);
        feedbackMenu.SetActive(true);
        
    }
    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject selectLevelMenu;
    public GameObject mainMenu;
    public GameObject feedbackMenu;

    private void Start()
    {
        mainMenu.SetActive(true);
        selectLevelMenu.SetActive(false);
        feedbackMenu.SetActive(false);
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
    }

    public void FeedBack()
    {
        mainMenu.SetActive(false);
        feedbackMenu.SetActive(true);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}

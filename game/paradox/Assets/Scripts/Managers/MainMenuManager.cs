using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject selectLevelMenu;
    public GameObject mainMenu;
    public GameObject feedbackMenu;
    public GameObject optionsMenu;
    public GameObject optionsButton;
    public GameObject levelButton;
    public GameObject startButton;

    private void Start()
    {
        mainMenu.SetActive(true);
        selectLevelMenu.SetActive(false);
        feedbackMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        LevelManager.Instance.PlayFirstLevel();
    }

    public void SelectLevel()
    {
        //Clear
        EventSystem.current.SetSelectedGameObject(null);
        //Reassign
        EventSystem.current.SetSelectedGameObject(levelButton);
        
        mainMenu.SetActive(false);
        selectLevelMenu.SetActive(true);
    }

    public void MainMenu()
    {
        //Clear
        EventSystem.current.SetSelectedGameObject(null);
        //Reassign
        EventSystem.current.SetSelectedGameObject(startButton);
        
        mainMenu.SetActive(true);
        selectLevelMenu.SetActive(false);
        feedbackMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    public void FeedBack()
    {
        mainMenu.SetActive(false);
        feedbackMenu.SetActive(true);
        
    }
    public void Options()
    {
        //Clear
        EventSystem.current.SetSelectedGameObject(null);
        //Reassign
        EventSystem.current.SetSelectedGameObject(optionsButton);
        
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}

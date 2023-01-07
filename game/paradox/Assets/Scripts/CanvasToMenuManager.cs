using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToMenuManager : MonoBehaviour
{
    private MenuManager _menuManager;
    private bool _isWaiting;
    private void Awake()
    {
        _menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        _isWaiting = false;
    }

    public void ResumeGame()
    {
        _menuManager.ResumeGame();
    }

    public void RestartLevel()
    {
        _menuManager.RestartLevel();
    }

    public void GoToMainMenu()
    {
        if (!_isWaiting)
        {
            _isWaiting = true;
            _menuManager.GoToMainMenu();
        }
        
     
    }
}

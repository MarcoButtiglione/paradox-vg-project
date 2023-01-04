using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameObject _pauseMenu;
    private GameObject _statisticsMenu;

    //public static bool isPaused; //it can be used in other scripts to stop key functioning.
    private void Awake()
    {
        _pauseMenu = GameObject.Find("Canvases").gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        _statisticsMenu = GameObject.Find("Canvases").gameObject.transform.GetChild(1).gameObject.transform.GetChild(3).gameObject;
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        
        _pauseMenu.SetActive(false);
        _statisticsMenu.SetActive(false);
        
    }
    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;

    }
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.PauseMenu)
        {
            _pauseMenu.SetActive(true);
            _statisticsMenu.SetActive(false);
        }
        else if (state == GameState.GameOverMenu)
        {
            _pauseMenu.SetActive(false);
            _statisticsMenu.SetActive(true);
        }
        else
        {
            _pauseMenu.SetActive(false);
            _statisticsMenu.SetActive(false);
        }
    }
    
    
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            GameManager.Instance.TriggerMenu();
        }
        else if (Input.GetButtonDown("Restart"))
        {
            GameManager.Instance.UpdateGameState(GameState.StartingYoungTurn);
        }
    }
    
    public void ResumeGame()
    {
        GameManager.Instance.TriggerMenu();
    }
    
    public void RestartLevel()
    {
        GameManager.Instance.UpdateGameState(GameState.StartingYoungTurn);
    }

    public void GoToMainMenu()
    {
        LevelManager l = LevelManager.Instance;
        if (l)
        {
            l.PlayMainMenu();
        }
        else
        {
            Debug.LogWarning("LevelManager not found!");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

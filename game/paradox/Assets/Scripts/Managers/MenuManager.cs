using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    //public static bool isPaused; //it can be used in other scripts to stop key functioning.
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        
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
            pauseMenu.SetActive(true);
            gameOverMenu.SetActive(false);
        }
        else if (state == GameState.GameOverMenu)
        {
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
            gameOverMenu.SetActive(false);
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

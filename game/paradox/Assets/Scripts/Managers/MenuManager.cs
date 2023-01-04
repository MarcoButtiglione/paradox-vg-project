using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    private GameObject _pauseMenu;
    private GameObject _statisticsMenuR;
    private GameObject _statisticsMenuL;
    private GameObject _nextButton;
    private GameObject _stars;
    private TMP_Text _paradoxText;
    private TMP_Text _retryText;
    private TMP_Text _overallTimeText;

    //public static bool isPaused; //it can be used in other scripts to stop key functioning.
    private void Awake()
    {
        _pauseMenu = GameObject.Find("Canvases").transform.GetChild(1).GetChild(0).gameObject;
        _statisticsMenuL=GameObject.Find("Door").transform.GetChild(0).GetChild(0).gameObject;
        _statisticsMenuR = GameObject.Find("Door").transform.GetChild(1).GetChild(0).gameObject;

        _stars = GameObject.Find("Stars").gameObject;
        _paradoxText = GameObject.Find("NumPar").GetComponent<TMP_Text>();
        _retryText = GameObject.Find("Retry").GetComponent<TMP_Text>();
        _overallTimeText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
        _nextButton = _statisticsMenuR.transform.GetChild(1).GetChild(0).gameObject;
        
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        
        _pauseMenu.SetActive(false);
        _statisticsMenuL.SetActive(false);
        _statisticsMenuR.SetActive(false);
        
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
            _statisticsMenuL.SetActive(false);
            _statisticsMenuR.SetActive(false);
        }
        else if (state == GameState.LevelCompleted)
        {
            //Clear
            EventSystem.current.SetSelectedGameObject(null);
            //Reassign
            EventSystem.current.SetSelectedGameObject(_nextButton);
            
            _pauseMenu.SetActive(false);
            _statisticsMenuL.SetActive(true);
            _statisticsMenuR.SetActive(true);  
        }
        else
        {
            _pauseMenu.SetActive(false);
            _statisticsMenuL.SetActive(false);
            _statisticsMenuR.SetActive(false);
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

    public void RestartDoor()
    {
        LevelManager.Instance.RestartLevel();
    }
    public void NextLevelDoor()
    {
        GameManager.Instance.UpdateGameState(GameState.NextLevel);
    }
    
    public void setStatistics(int _counterRetry, int _counterParadoxOverall, TimeSpan _overallTime, int _numStars)
    {
        if (_numStars > 0)
        {
            GameObject activeStar = _stars.transform.GetChild(_numStars).gameObject; 
            activeStar.SetActive(true);
        }

        _paradoxText.text = "Paradox: " + _counterParadoxOverall;
        _retryText.text = "Retry: " + _counterRetry;
        _overallTimeText.text = "Time: " + _overallTime.ToString(@"mm\:ss\:ff");

    }

}

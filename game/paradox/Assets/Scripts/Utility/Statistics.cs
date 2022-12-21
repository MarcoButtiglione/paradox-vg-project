using System;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    private float _completionTime;
    private float _overallTime;
    //NOT NEEDED FOR NOW
    //private float _bestTime;
    private int _counterRetry;
    private int _counterParadoxRun;
    private int _counterParadoxOverall;
    private int _numOfStars;
    private bool _firstTimeYoung = true;

    private float _timeForLevel;
    private float _score;

    //Event managment 
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void Start()
    {
        _counterRetry = 0;
        _counterParadoxRun = 0;
        _counterParadoxOverall = 0;
        _completionTime = 0;
        _overallTime = 0;
        _timeForLevel = GameObject.Find("Canvases").GetComponentInChildren<TimerScript>().getTimer();
    }

    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameState state)
    {

        if (state == GameState.StartingYoungTurn)
        {
            _completionTime = 0;
            if (_firstTimeYoung)
            {
                _firstTimeYoung = false;
            }
            else
            {
                _counterRetry++;
                _counterParadoxRun = 0;
            }
        }
        else if (state == GameState.Paradox)
        {
            _counterParadoxRun++;
            _counterParadoxOverall++;
        }
        else if (state == GameState.LevelCompleted)
        {

            _score = _completionTime - 1 * (5 - _counterParadoxRun);

            TimeSpan timeSpanOverall = TimeSpan.FromSeconds(_overallTime);
            TimeSpan timeSpanCompletion = TimeSpan.FromSeconds(_completionTime);

            Debug.Log("Number of retrial young: " + _counterRetry);
            Debug.Log("Number of paradoxes caused: " + _counterParadoxRun);
            Debug.Log("CompletionTime: " + timeSpanCompletion.ToString(@"mm\:ss\:ff"));
            Debug.Log("OverallTime: " + timeSpanOverall.ToString(@"mm\:ss\:ff"));
            Debug.Log("TimerForLevel: " + _timeForLevel);

            if (_score < _timeForLevel / 3)
            {
                _numOfStars = 3;
            }
            else if (_score < _timeForLevel / 2)
            {
                _numOfStars = 2;
            }
            else if (_score < _timeForLevel)
            {
                _numOfStars = 1;
            }
            else { _numOfStars = 0; }

            Debug.Log("NumberOfStars: " + _numOfStars);
        }
    }

    void Update()
    {
        if (GameManager.Instance.State == GameState.YoungPlayerTurn)
        {
            _completionTime += Time.deltaTime;
            _overallTime += Time.deltaTime;


        }
        if (GameManager.Instance.State == GameState.OldPlayerTurn)
        {
            _overallTime += Time.deltaTime;
        }

    }



}
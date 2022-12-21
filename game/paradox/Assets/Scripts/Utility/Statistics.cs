using System;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    private float _completionTime;
    private float _overallTime;
    //NOT NEEDED FOR NOW
    //private float _bestTime;
    private int _counterRetry;
    private int _counterParadox;
    private bool _firstTimeYoung = true;

    //Event managment 
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void Start()
    {
        _counterRetry = 0;
        _counterParadox = 0;
        _completionTime = 0;
        _overallTime = 0;
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
                _counterRetry ++;
            }
        }
        else if (state == GameState.Paradox)
        {
            _counterParadox ++;
        }
        else if( state == GameState.LevelCompleted ){

            TimeSpan timeSpanOverall = TimeSpan.FromSeconds(_overallTime);
            TimeSpan timeSpanCompletion = TimeSpan.FromSeconds(_completionTime);

            Debug.Log("Number of retrial young: " + _counterRetry);
            Debug.Log("Number of paradoxes caused: " + _counterParadox);
            Debug.Log("CompletionTime: " + timeSpanCompletion.ToString(@"mm\:ss\:ff"));
            Debug.Log("OverallTime: " + timeSpanOverall.ToString(@"mm\:ss\:ff"));
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
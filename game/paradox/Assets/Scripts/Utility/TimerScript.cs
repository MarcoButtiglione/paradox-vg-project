using TMPro;
using System;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private TMP_Text _timerText;
    [SerializeField] private TimerLevelsParameters _timerLevelsParameters;
    private float TimeLeft;
    private float _countdown = 4.2f;
    private bool _countdownStarted = false;

    //Event managment 
    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();

        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
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
            gameObject.SetActive(true);
            TimeLeft = _timerLevelsParameters.timerLevel;
            if (_countdownStarted)
            {
                _countdownStarted = false;
                stopCountdown();
            }
            
            updateTimer(TimeLeft);
        }
        else if (state == GameState.StartingOldTurn || state == GameState.StartingThirdPart)
        {
            if (_countdownStarted)
            {
                _countdownStarted = false;
                stopCountdown();
            }
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (GameManager.Instance.State == GameState.YoungPlayerTurn)
        {
            if (TimeLeft < _countdown && !_countdownStarted)
            {
                _countdownStarted = true;
                playCountdown();
            }
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                _countdownStarted = false;
                //GAME OVER
                GameManager.Instance.UpdateGameState(GameState.StartingYoungTurn);
            }
        }

    }

    private void playCountdown()
    {
        AudioManager a = FindObjectOfType<AudioManager>();
        if (a)
        {
            a.Play("Countdown");
        }
    }

    private void stopCountdown()
    {
        AudioManager a = FindObjectOfType<AudioManager>();
        if (a)
        {
            a.Stop("Countdown");
        }
    }

    void updateTimer(float currentTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }
    public float getTimer(){
        return _timerLevelsParameters.timerLevel;
    }


}

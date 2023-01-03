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
    [SerializeField] private Animator animator;
    private AudioManager a;
    
    //Event managment 
    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();
        //a = FindObjectOfType<AudioManager>();

        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void Start()
    {
        a = GameObject.Find("AudioManager").GetComponentInChildren<AudioManager>();

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
            animator.SetTrigger("Restart Game");
            gameObject.SetActive(true);
            TimeLeft = _timerLevelsParameters.timerLevel;
            if (_countdownStarted)
            {
                _countdownStarted = false;
                stopCountdown();
            }
            PressAKey();
            //updateTimer(TimeLeft);
        }
        
        if (state == GameState.YoungPlayerTurn)
        {
            animator.SetTrigger("Start Young");
        }
        else if (state == GameState.StartingOldTurn && GameManager.Instance.PreviousGameState==GameState.YoungPlayerTurn)
        {
            animator.SetTrigger("Young Completed");
        }
        else if (state == GameState.StartingThirdPart)
        {
            animator.SetTrigger("Young Completed");
        }
        else if (state == GameState.StartingOldTurn && GameManager.Instance.PreviousGameState==GameState.Paradox)
        {
            animator.SetTrigger("Pause Old");
        }
        else if (state == GameState.OldPlayerTurn)
        {
            animator.SetTrigger("Start Old");
        }
        else if (state == GameState.Paradox)
        {
            animator.SetTrigger("Paradox");
        }
        
        if (state == GameState.StartingOldTurn || state == GameState.StartingThirdPart)
        {
            if (_countdownStarted)
            {
                _countdownStarted = false;
                stopCountdown();
            }
        }
        
        if (state == GameState.PauseMenu && _countdownStarted)
        {
            a.Pause("Countdown");
        }

        if (GameManager.Instance.PreviousGameState == GameState.PauseMenu && _countdownStarted)
        {
            a.Resume("Countdown");
        }
    }

    void Update()
    {
        if (GameManager.Instance.State == GameState.YoungPlayerTurn)
        {
            if (TimeLeft < _countdown && !_countdownStarted)
            {
                animator.SetTrigger("Low Time Timer");
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
                animator.SetTrigger("Restart Game");
                GameManager.Instance.UpdateGameState(GameState.StartingYoungTurn);
            }
        }

    }

    private void playCountdown()
    {
        //AudioManager a = FindObjectOfType<AudioManager>();

        a.Play("Countdown");

    }

    private void stopCountdown()
    {
        //AudioManager a = FindObjectOfType<AudioManager>();

        a.Stop("Countdown");

    }

    void updateTimer(float currentTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }

    private void PressAKey()
    {
        _timerText.text = "Ready?";
    }
    
    public float getTimer(){
        return _timerLevelsParameters.timerLevel;
    }


}

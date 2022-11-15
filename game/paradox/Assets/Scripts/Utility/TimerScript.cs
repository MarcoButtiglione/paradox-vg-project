using TMPro;
using System;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private TMP_Text _timerText;
    [SerializeField] private TimerLevelsParameters _timerLevelsParameters;
    private float TimeLeft;

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
            updateTimer(TimeLeft);
        }
        else if (state == GameState.StartingOldTurn || state == GameState.StartingThirdPart)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (GameManager.Instance.State == GameState.YoungPlayerTurn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                //GAME OVER
                GameManager.Instance.UpdateGameState(GameState.StartingYoungTurn);
            }
        }

    }

    void updateTimer(float currentTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }


}

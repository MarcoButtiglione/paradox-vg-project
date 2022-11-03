using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            TimeLeft = _timerLevelsParameters.timerLevel;
            gameObject.SetActive(true);
        }
        else if (state == GameState.StartingOldTurn)
        {
            gameObject.SetActive(false);
        }
    }
    
    void Update()
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

    void updateTimer(float currentTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }
    

}

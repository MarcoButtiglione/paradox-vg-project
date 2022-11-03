using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
    [SerializeField] private GameObject _replayButton;

    private void Awake()
    {
        _replayButton.gameObject.SetActive(false);
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

        if (state == GameState.Paradox)
        {
            _replayButton.SetActive(true);
        }
        else
        {
            _replayButton.SetActive(false);
        }
    } 
}


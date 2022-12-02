using System;
using UnityEngine;
public class TipManager : MonoBehaviour
{
    [Header("Initial state (Young/Old phase)")]
    [SerializeField] private bool _initYoungState;
    [SerializeField] private bool _initOldState;
    private bool _firstTimeOld = true;
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.YoungPlayerTurn:
                this.gameObject.SetActive(_initYoungState);
                break;
            case GameState.OldPlayerTurn:
                if (_firstTimeOld)
                    _firstTimeOld = false;
                this.gameObject.SetActive(_initOldState);
                break;
            default:
                if (_initOldState && !_firstTimeOld)
                {
                    this.gameObject.SetActive(true);
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
                break;
        }
    }

    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

}

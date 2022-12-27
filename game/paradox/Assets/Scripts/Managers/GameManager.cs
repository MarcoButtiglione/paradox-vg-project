using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public GameState PreviousGameState;


    public static event Action<GameState> OnGameStateChanged;

    private bool _isTutorial;


    private void Awake()
    {
        Instance = this;
        var l = LevelManager.Instance;
        _isTutorial = l && l.IsTutorialLevel();
    }

    private void Start()
    {
        StartCoroutine(nameof(WaitToStart));
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    /*
     * Call this function with:
     * GameManager.Instance.UpdateGameState(GameState.YoungPlayerTurn);
     */
    public void UpdateGameState(GameState newState)
    {
        Debug.Log("Current State: " + newState + " ----- IsTutorial: " + _isTutorial);

        PreviousGameState = State;
        State = newState;

        if (_isTutorial)
        {
            switch (newState)
            {
                case GameState.StartingYoungTurn:
                    Time.timeScale = 0f;
                    break;
                case GameState.YoungPlayerTurn:
                    Time.timeScale = 1f;
                    break;
                case GameState.StartingSecondPart:
                    if (PreviousGameState != GameState.YoungPlayerTurn)
                    {
                        UpdateGameState(GameState.SecondPart);
                    }
                    break;
                case GameState.SecondPart:
                    Time.timeScale = 1f;
                    break;
                case GameState.StartingThirdPart:
                    GameObject.Find("DisappearingPlatform 0").GetComponent<ActivableController>().SwitchState();
                    UpdateGameState(GameState.ThirdPart);
                    break;
                case GameState.ThirdPart:
                    break;
                case GameState.StartingOldTurn:
                    //Time.timeScale = 0f;
                    break;
                case GameState.OldPlayerTurn:
                    Time.timeScale = 1f;
                    break;
                case GameState.Paradox:
                    break;
                case GameState.PauseMenu:
                    break;
                case GameState.GameOverMenu:
                    break;
                case GameState.LevelCompleted:
                    UpdateGameState(GameState.NextLevel);
                    break;
                case GameState.NextLevel:
                    LevelManager.Instance.PlayNextLevel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
        else
        {
            switch (newState)
            {
                case GameState.StartingYoungTurn:

                    Time.timeScale = 0f;
                    break;
                case GameState.YoungPlayerTurn:
                    Time.timeScale = 1f;
                    break;
                case GameState.StartingOldTurn:
                    /*if (PreviousGameState != GameState.YoungPlayerTurn)
                    {
                        Time.timeScale = 0f;
                    }
                    break;*/
                case GameState.OldPlayerTurn:
                    Time.timeScale = 1f;
                    break;
                case GameState.Paradox:
                    break;
                case GameState.PauseMenu:
                    break;
                case GameState.GameOverMenu:
                    break;
                case GameState.LevelCompleted:
                    UpdateGameState(GameState.NextLevel);
                    break;
                case GameState.NextLevel:
                    LevelManager.Instance.PlayNextLevel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void TriggerMenu()
    {
        if (State == GameState.PauseMenu)
        {
            UpdateGameState(PreviousGameState);
            Time.timeScale = 1f;
        }
        else if (State == GameState.YoungPlayerTurn || State == GameState.OldPlayerTurn || State == GameState.SecondPart || State == GameState.ThirdPart)
        {
            UpdateGameState(GameState.PauseMenu);
            Time.timeScale = 0f;
        }
    }


    public bool IsTutorial()
    {
        return _isTutorial;
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        UpdateGameState(GameState.StartingYoungTurn);
    }


}


public enum GameState
{

    StartingYoungTurn,
    YoungPlayerTurn,

    StartingSecondPart,
    SecondPart,
    ThirdPart,
    StartingThirdPart,

    StartingOldTurn,
    OldPlayerTurn,
    Paradox,

    PauseMenu,
    GameOverMenu,
    LevelCompleted,
    NextLevel
}



using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public GameState PreviousGameState;
    

    public static event Action<GameState> OnGameStateChanged;

    private bool isTutorial;
   

    private void Awake()
    {
        Instance = this;
        LevelManager l = LevelManager.Instance;
        if (l)
        {
            isTutorial = l.IsTutorialLevel();
        }
        else
        { isTutorial = false; }
    }

    private void Start()
    {
        StartCoroutine("WaitToStart");
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
        Debug.Log("Current State: " + newState + " ----- IsTutorial: " + isTutorial);

        PreviousGameState = State;
        State = newState;

        if (isTutorial)
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
                    break;
                case GameState.StartingThirdPart:
                    GameObject.Find("DisappearingPlatform 0").GetComponent<ActivableController>().SwitchState();
                    UpdateGameState(GameState.ThirdPart);
                    break;
                case GameState.ThirdPart:
                    break;
                case GameState.StartingOldTurn:
                    Time.timeScale = 0f;
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
                    if (PreviousGameState != GameState.YoungPlayerTurn)
                    {
                        Time.timeScale = 0f;
                    }
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
        return isTutorial;
    }

    IEnumerator WaitToStart(){
    yield return new WaitForSecondsRealtime(0.1f);
    UpdateGameState(GameState.StartingYoungTurn);
    }

    public bool IsPlayablePhase()
    {
        if (State is GameState.YoungPlayerTurn or GameState.SecondPart or GameState.ThirdPart or GameState.OldPlayerTurn)
        {
            return true;
        }
        return false;
    }


}


public enum GameState
{

    StartingYoungTurn,
    YoungPlayerTurn,

    StartingSecondPart,
    SecondPart,
    
    StartingThirdPart,
    ThirdPart,
    
    StartingOldTurn,
    OldPlayerTurn,
    
    Paradox,

    PauseMenu,
    GameOverMenu,
    LevelCompleted,
    NextLevel
}



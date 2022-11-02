using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
public class RewindManager : MonoBehaviour
{
    public GameObject GhostPrefab;
    
    public GameObject Old_Player;
    private Vector3 _initPosOld;
    public GameObject Young_Player;
    private Vector3 _initPosYoung;
    private List<Vector3> positions_young_p;
    private List<TypeOfInputs> inputs;
    
    private List<Vector3> positions_old_p;
    private TypeOfInputs structInputs;
    private PlayerMovement toTrack;
    
    private bool jump = false;
    
    private int index;
    private int _reloadSpeed;
    
    [SerializeField]
    private int parameterReload = 40;
    
    
    //Event managment 
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;

        _initPosOld = Old_Player.transform.position;
        _initPosYoung = Young_Player.transform.position;
        GhostPrefab = Instantiate(GhostPrefab, GhostPrefab.transform.position, Quaternion.identity);

    }
    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.YoungPlayerTurn:
                Init();
                break;
            case GameState.SwitchingPlayerTurn:
                break;
            case GameState.OldPlayerTurn:
                StartSecondPart();
                break;
            case GameState.Paradox:
                _reloadSpeed = index / parameterReload;
                Old_Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                break;
            case GameState.PauseMenu:
                break;
            case GameState.GameOverMenu:
                break;
            case GameState.LevelCompleted:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void Init()
    {
        GhostPrefab.SetActive(false);
        
        Old_Player.SetActive(false);
        Old_Player.transform.position = _initPosOld;
        Young_Player.SetActive(true);
        Young_Player.transform.position = _initPosYoung;
        
        positions_young_p = new List<Vector3>();
        inputs = new List<TypeOfInputs>();
        
        positions_old_p = new List<Vector3>();
        
        parameterReload = 40;
        index = 0;
        
        
    }
    
    private void Start()
    {
        toTrack = Young_Player.GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        jump = toTrack.getJump();
    }
    

    private void FixedUpdate()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.YoungPlayerTurn:
                Record();
                break;
            case GameState.SwitchingPlayerTurn:
                break;
            case GameState.OldPlayerTurn:
                positions_old_p.Insert(positions_old_p.Count, Old_Player.transform.position);
                MoveGhost();
                break;
            case GameState.Paradox:
                RestartOldAndGhost();
                break;
            case GameState.PauseMenu:
                break;
            case GameState.GameOverMenu:
                break;
            case GameState.LevelCompleted:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(GameManager.Instance.State), GameManager.Instance.State, null);
        }
    }
    
    void Record()
    {
        positions_young_p.Insert(positions_young_p.Count, Young_Player.transform.position);
        
        structInputs = new TypeOfInputs(toTrack.getHorizontal(), toTrack.getCrouch(), jump);
        jump = false;
        inputs.Insert(inputs.Count, structInputs);
    }
    
    public void StartSecondPart()
    {
        index = 0;
        GhostPrefab.transform.position = positions_young_p[0];

        positions_old_p = new List<Vector3>();
        Old_Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        Old_Player.SetActive(true);
        Young_Player.SetActive(false);
        GhostPrefab.SetActive(true);
    }
    
    public void RestartOldAndGhost()
    {
        if (_reloadSpeed > 0 && index - _reloadSpeed > 0)
        {
            index = index - _reloadSpeed;
        }
        else if (index - 1 > 0)
        {
            index--;
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.OldPlayerTurn);
            index = 0;
        }
        Old_Player.transform.position = positions_old_p[index];
        GhostPrefab.SetActive(false);
    }
    
    public void MoveGhost()
    {
        if (Vector2.Distance(new Vector2(GhostPrefab.transform.position.x, GhostPrefab.transform.position.y), new Vector2(positions_young_p[index].x, positions_young_p[index].y)) > 0.5) {
            GameManager.Instance.UpdateGameState(GameState.Paradox);
            return;
        }
        GhostPrefab.GetComponent<CharacterController2D>().Move(inputs[index].getHorizontal() * Time.fixedDeltaTime, inputs[index].getCrouch(), inputs[index].getJump()); 
        index++;
    }
}


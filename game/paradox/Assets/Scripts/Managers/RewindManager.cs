using System;
using System.Collections.Generic;
using UnityEngine;
public class RewindManager : MonoBehaviour
{
    //Ghost
    public GameObject _initGhostPrefab;
    private GameObject _ghost;
    private Rigidbody2D _rigidbodyGhost;
    private CharacterController2D _controllerGhost;
    private Animator _animatorGhost;

    //DebugWorker
    public GameObject _initWorker;
    private GameObject _worker;
    
    //Old
    public GameObject _initOldPrefab;
    public GameObject Old_Start_Position;
    private GameObject _old;
    private Vector3 _initPosOld;

    //Young
    public GameObject _initYoungPrefab;
    public GameObject Young_Start_Position;
    private Vector3 _initPosYoung;
    private GameObject _young;
    private Rigidbody2D _rigidbodyYoung;
    private PlayerMovement _playerMovementYoung;

    //Recording
    private List<Vector3> _positionsYoungP;
    private List<TypeOfInputs> _inputs;
    private List<bool> _youngWasGrounded;
    private List<Vector3> _positionsOldP;
    
    

    
    private int _index;
    private int _reloadSpeed;

    [SerializeField]
    private int parameterReload = 40;
    

    private static readonly int Speed = Animator.StringToHash("Speed");


    //Event management 
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;

        //Old
        _initPosOld = Old_Start_Position.transform.position;
        //Young
        _initPosYoung = Young_Start_Position.transform.position;

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
            Init();
        }
        else if (state == GameState.YoungPlayerTurn)
        {
            _rigidbodyYoung.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (state == GameState.StartingSecondPart)
        {
            StartSecondPartTutorial();
        }
        else if (state == GameState.StartingThirdPart)
        {
            StartThirdPartTutorial();
        }
        
        else if (state == GameState.StartingOldTurn)
        {
            StartingOldTurn();
        }
        else if (state == GameState.Paradox)
        {
            if (GameManager.Instance.PreviousGameState == GameState.OldPlayerTurn)
            {
                _reloadSpeed = _index / parameterReload;
                _old.GetComponent<OldPlayerMovement>().enabled = false;
            }
        }
    }

    private void Init()
    {
        DestroyAll();

        //Init Young
        InitYoung();
        
        _index = 0;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.State == GameState.OldPlayerTurn)
        {
            _positionsOldP.Insert(_positionsOldP.Count, _old.transform.position);
            _rigidbodyGhost.isKinematic = true;
            MoveGhost();
        }
        else if (GameManager.Instance.State == GameState.ThirdPart)
        {
            MoveGhost();
        }
        else if (GameManager.Instance.State == GameState.Paradox)
        {
            RestartOldAndGhost();
        }
    }
    
    private void LateUpdate()
    {

        if (GameManager.Instance.State is GameState.OldPlayerTurn or GameState.ThirdPart)
        {
            //Debug.Log("Ghost grounded: "+GhostPrefab.GetComponent<CharacterController2D>().GetGrounded()+" "+index);
            //Debug.Log("Young was grounded: "+youngWasGrounded[index]+" "+index);

            //Debug.Log(" Delta position x:" + (GhostPrefab.transform.position.x - positions_young_p[index].x));
            //Debug.Log(" Delta position y :" + (GhostPrefab.transform.position.y - positions_young_p[index].y));
            if (_index <= 1) return;
            if (!_youngWasGrounded[_index - 1] || _controllerGhost.GetGrounded()) return;
            if (_ghost)
            {
                Destroy(_ghost);
            }
            if (_worker)
            {
                Destroy(_worker);
            }
            if(GameManager.Instance.State == GameState.OldPlayerTurn)
                GameManager.Instance.UpdateGameState(GameState.Paradox);
            if(GameManager.Instance.State == GameState.ThirdPart)
                GameManager.Instance.UpdateGameState(GameState.StartingSecondPart);
        }
    }

    
    private void StartingOldTurn()
    {
        //Init record
        InitRecordData();
        
        DestroyAll();

        _index = 0;

        //Init Ghost
        InitGhost();
        //-------------

        //Init Worker
        InitWorker();
        //-------------

        //Init Old
        InitOld();
        //-------------
    }

    private void RestartOldAndGhost()
    {
        if (_reloadSpeed > 0 && _index - _reloadSpeed > 0)
        {
            _index = _index - _reloadSpeed;
        }
        else if (_index - 1 > 0)
        {
            _index--;
        }
        else
        {
            _index = 0;
            _old.transform.position = _positionsOldP[0];
            GameManager.Instance.UpdateGameState(GameState.StartingOldTurn);
            return;
        }
        _old.transform.position = _positionsOldP[_index];
    }

    private void MoveGhost()
    {
        if (_index < _inputs.Count)
        {
            _worker.transform.position = new Vector3(_positionsYoungP[_index].x, _positionsYoungP[_index].y, _worker.transform.position.z);
            _animatorGhost.SetFloat(Speed, Math.Abs(_inputs[_index].getHorizontal()));
            _ghost.transform.position = new Vector3(_positionsYoungP[_index].x, _positionsYoungP[_index].y, _ghost.transform.position.z);
            _index++;
        }
        else
        {
            _index--;
            GameManager.Instance.UpdateGameState(GameState.LevelCompleted);
        }

    }
    private void StartSecondPartTutorial()
    {
        DestroyAll();
        //Init Old
        InitOld();
        //-------------
    }
    private void StartThirdPartTutorial()
    {
        //Init record
        InitRecordData();

        DestroyAll();
        
        _index = 0;
        
        //Init Ghost
        InitGhost();
        //-------------
        //Init Worker
        InitWorker();
        //-------------
    }

    private void DestroyAll()
    {
        if (_young)
        {
            Destroy(_young);
        }
        if (_old)
        {
            Destroy(_old);
        }
        if (_ghost)
        {
            Destroy(_ghost);
        }
        if (_worker)
        {
            Destroy(_worker);
        }
    }
    private void InitRecordData()
    {
        _inputs = _playerMovementYoung.GetListInputs();
        _positionsYoungP = _playerMovementYoung.GetPosYoung();
        _youngWasGrounded = _playerMovementYoung.GetGroundedYoung();
    }
    private void InitWorker()
    {
        _worker = Instantiate(_initWorker, _initPosYoung, Quaternion.identity);
    }
    private void InitGhost()
    {
        _ghost = Instantiate(_initGhostPrefab, _initPosYoung, Quaternion.identity);
        _rigidbodyGhost = _ghost.GetComponent<Rigidbody2D>();
        _controllerGhost = _ghost.GetComponent<CharacterController2D>();
        _animatorGhost = _ghost.GetComponent<Animator>();
    }
    private void InitOld()
    {
        _old = Instantiate(_initOldPrefab, _initPosOld, Quaternion.identity);
        _positionsOldP = new List<Vector3>();
    }
    private void InitYoung()
    {
        _young = Instantiate(_initYoungPrefab, _initPosYoung, Quaternion.identity);
        _rigidbodyYoung = _young.GetComponent<Rigidbody2D>();
        _playerMovementYoung = _young.GetComponent<PlayerMovement>();
        _positionsYoungP = new List<Vector3>();
    }

}


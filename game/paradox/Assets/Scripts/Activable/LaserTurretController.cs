using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretController : MonoBehaviour
{
    [SerializeField] private GameObject _laserHead;
    public LineRenderer lineRenderer;
    private GameObject _laserRay;
    private Vector3 _direction;
    private bool _isActive;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _laserOutput;
    [SerializeField] private LaserType _laserType;
    [SerializeField] private LaserFunctioning _functionYoung;
    [SerializeField] private LaserFunctioning _functionOldActive;
    [SerializeField] private float _intermittentPeriod = 2.0f;
    [Header("Moving platform configuration")] 
    [SerializeField] private GameObject[] _waypoints;
    [SerializeField] private float speed = 2.0f;
    private int _currentWaypointIndex = 0;
    private Vector3 _initPosition;
    private Vector3 _laserPosition;
    private bool _isMoving;
    [Header("Rotating platform configuration")] 
    [SerializeField] private float _angularSpeed = 1f;
    [SerializeField] private float _rotationArc = 90f;
    [SerializeField] private bool _continuousRotation=false;
    private bool _clockwiseRotation=false;
    
    private bool _isRotating;
    private Vector3 _initDirection;
    private Vector3 _laserDirection;

    [Header("Initial state (Young/Old phase)")] 
    [SerializeField] private LaserState _initYoungState;
    [SerializeField] private LaserState _initOldStateOFF;
    [Header("Laser sprites")]
    [SerializeField] private Sprite _spriteStaticOff;
    [SerializeField] private Sprite _spriteStaticOn;
    [SerializeField] private Sprite _spriteRotatingOff;
    [SerializeField] private Sprite _spriteRotatingOn;
    [SerializeField] private Sprite _spriteMovingOff;
    [SerializeField] private Sprite _spriteMovingOn;

    [SerializeField] private Sprite _spriteMovingRotatingOff;
    [SerializeField] private Sprite _spriteMovingRotatingOn;
    //-------------------------------
    private void Awake()
    {
        _laserRay = GameObject.Find("LaserRay");
        _direction = (_laserOutput.position - _laserHead.transform.position).normalized;
        _initDirection = _laserHead.transform.rotation.eulerAngles;
        _spriteRenderer = _laserHead.GetComponent<SpriteRenderer>();
        //Moving LASER
        _isMoving = false; 
        _initPosition = _laserHead.transform.position;
        _laserPosition = _initPosition;
        //------------------------------------
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
            InitYoung();
        }

        if (state == GameState.StartingOldTurn)
        {
            InitOld();
        }
    }

    private void InitYoung()
    {
        _laserPosition = _initPosition;
        _laserHead.transform.rotation = Quaternion.Euler(_initDirection);
        _clockwiseRotation = false;
        _currentWaypointIndex = 0;
        CancelInvoke();
        if (_laserType==LaserType.Moving||_laserType==LaserType.MovingRotating)
        {
            _isMoving = true;
        }
        if (_laserType==LaserType.Rotating||_laserType==LaserType.MovingRotating)
        {
            _isRotating = true;
        }
        if (_initYoungState == LaserState.Active)
        {
            _isActive = true;
            _laserRay.SetActive(true);
            SetSpriteOn();
            if (_functionYoung == LaserFunctioning.Intermittent)
            {
                InvokeRepeating("Intermittent", _intermittentPeriod/2, _intermittentPeriod/2);
            }
        }
        else if (_initYoungState == LaserState.Inactive)
        {
            _isActive = false;
            _laserRay.SetActive(false);
            SetSpriteOff();
        }
    }

    private void InitOld()
    {
        _laserHead.transform.rotation = Quaternion.Euler(_initDirection);
        _clockwiseRotation = false;
        _laserPosition = _initPosition;
        _currentWaypointIndex = 0;
        CancelInvoke();
        if (_laserType==LaserType.Moving)
        {
            _isMoving = true;
        }
        if (_laserType==LaserType.Rotating)
        {
            _isRotating = true;
        }
        if (_initOldStateOFF == LaserState.Active)
        {
            _isActive = true;
            _laserRay.SetActive(true);
            SetSpriteOn();
            if (_functionOldActive == LaserFunctioning.Intermittent)
            {
                InvokeRepeating("Intermittent", _intermittentPeriod/2, _intermittentPeriod / 2);
            }
            
        }
        else if (_initOldStateOFF == LaserState.Inactive)
        {
            _isActive = false;
            _laserRay.SetActive(false);
            SetSpriteOff();
        }
    }
    //-------------------------------


    void Update()
    {
        if (_isActive)
        {
            _direction = (_laserOutput.position - _laserHead.transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(_laserOutput.position, _direction);
            lineRenderer.SetPosition(0, _laserOutput.position);
            if (hit)
            {
                lineRenderer.SetPosition(1, hit.point);
                CheckCollision(hit.collider);
            }
            else
            {
                lineRenderer.SetPosition(1, _direction * 100);
            }
        }
        _laserHead.transform.position = _laserPosition;

    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(_waypoints[_currentWaypointIndex].transform.position, _laserPosition) < .1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }
        }

        if (_isMoving&& GameManager.Instance.State!=GameState.StartingYoungTurn&& GameManager.Instance.State!=GameState.StartingOldTurn)
        {
            _laserPosition = Vector2.MoveTowards(_laserPosition,
                _waypoints[_currentWaypointIndex].transform.position, Time.fixedDeltaTime * speed);
        }

        if (!_continuousRotation)
        {
            if (Math.Abs(_initDirection.z+_rotationArc/2)-(_laserHead.transform.rotation.eulerAngles.z) <.1f||
                        Math.Abs(_initDirection.z-_rotationArc/2)-(_laserHead.transform.rotation.eulerAngles.z) >.1f
                       )
            { 
                _clockwiseRotation = !_clockwiseRotation; 
            }
        }
        
        if (_isRotating&& GameManager.Instance.State!=GameState.StartingYoungTurn&& GameManager.Instance.State!=GameState.StartingOldTurn)
        {
            if(_clockwiseRotation)
                _laserHead.transform.Rotate(new Vector3(0,0,Time.fixedDeltaTime - _angularSpeed));
            else
                _laserHead.transform.Rotate(new Vector3(0,0,Time.fixedDeltaTime + _angularSpeed));
        }
        
    }
    

    // Check if the laser ray is hitting the player/ghost
    private void CheckCollision(Collider2D col)
    {
        if (_laserRay.activeSelf)
        {
            if (col.gameObject.CompareTag("Young"))
            {
                GameManager.Instance.UpdateGameState(GameState.StartingYoungTurn);
            }
            else if (col.gameObject.CompareTag("Old") || col.gameObject.CompareTag("Ghost"))
            {
                GameManager.Instance.UpdateGameState(GameState.Paradox);
            }
        }
    }

    
    
    private void Intermittent()
    {
        if (GameManager.Instance.State is not GameState.StartingYoungTurn or GameState.StartingSecondPart or GameState.StartingThirdPart or GameState.StartingOldTurn)
        {
            _isActive = !_isActive;
            if (_isActive)
            {
                SetSpriteOn();
            }
            else
            {
                SetSpriteOff();
            }
            _laserRay.SetActive(!_laserRay.activeSelf);
        }
        
    }
    public void SwitchState()
    {
        if (_functionOldActive==LaserFunctioning.Fixed)
        {
            if (_isActive)
            {
                _isActive = false;
                _laserRay.SetActive(false);
                SetSpriteOff();
            }
            else
            {
                _isActive = true;
                _laserRay.SetActive(true);
                SetSpriteOn();
            }
            
        }
        else if (_functionOldActive==LaserFunctioning.Intermittent)
        {
            if (_isActive)
            {
                _isActive = false;
                _laserRay.SetActive(false);
                CancelInvoke();
                SetSpriteOff();
            }
            else
            {
                _isActive = true;
                _laserRay.SetActive(true); 
                SetSpriteOn();
                InvokeRepeating("Intermittent", _intermittentPeriod/2, _intermittentPeriod / 2);
            }
        }
    }


    private void SetSpriteOn()
    {
        if (_laserType==LaserType.Static)
        {
            _spriteRenderer.sprite = _spriteStaticOn;
        }
        else if (_laserType==LaserType.Moving)
        {
            _spriteRenderer.sprite = _spriteMovingOn;
        }
        else if (_laserType==LaserType.Rotating)
        {
            _spriteRenderer.sprite = _spriteRotatingOn;

        }
        else if (_laserType==LaserType.MovingRotating)
        {
            _spriteRenderer.sprite = _spriteMovingRotatingOn;

        }
    }

    private void SetSpriteOff()
    {
        if (_laserType==LaserType.Static)
        {
            _spriteRenderer.sprite = _spriteStaticOff;
        }
        else if (_laserType==LaserType.Moving)
        {
            _spriteRenderer.sprite = _spriteMovingOff;
        }
        else if (_laserType==LaserType.Rotating)
        {
            _spriteRenderer.sprite = _spriteRotatingOff;

        }
        else if (_laserType==LaserType.MovingRotating)
        {
            _spriteRenderer.sprite = _spriteMovingRotatingOff;

        }
    }
}

[System.Serializable]
public enum LaserState
{
    Inactive,
    Active
}

[System.Serializable]
public enum LaserFunctioning
{
    Intermittent,
    Fixed
}

[System.Serializable]
public enum LaserType
{
    Static,
    Rotating,
    Moving,
    MovingRotating
}


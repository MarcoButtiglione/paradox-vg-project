using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
     
     private int _currentWaypointIndex = 0;
     private Vector3 _initPosition;
     private Vector3 _platformPosition;
     private bool _isMoving;
     [SerializeField]private GameObject _platform;
     [SerializeField] private GameObject[] _waypoints;
     [SerializeField] private MovingFunctioning _function;
     [SerializeField] private float speed = 2.0f;
     [Header("Initial state (Young/Old phase)")]
     [SerializeField] private MovingState _initYoungState;
     [SerializeField] private MovingState _initOldStateOFF;
     
     
     //-------------------------------
     private void Awake()
     { 
         _isMoving = false; 
         _initPosition = _platform.transform.position;
         _platformPosition = _initPosition;
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
         _platformPosition = _initPosition;
         _currentWaypointIndex = 0;
         if (_initYoungState == MovingState.Active)
         {
             _isMoving = true;
             _platform.transform.position = _initPosition;
             _platform.SetActive(true);
         }
         else if (_initYoungState==MovingState.Inactive)
         {
             if (_function==MovingFunctioning.DisappearedIfInactive)
             {
                 _platform.SetActive(false);
                 _isMoving = true;
                 
             }
             else if (_function==MovingFunctioning.StoppedIfInactive)
             {
                 _isMoving = false; 
             }
         }
     }
     private void InitOld()
     {
         _currentWaypointIndex = 0;
         _platformPosition = _initPosition;
         if (_initOldStateOFF == MovingState.Active)
         { 
             _isMoving = true;
             _platform.transform.position = _initPosition;
             _platform.SetActive(true);
         }
         else if (_initOldStateOFF==MovingState.Inactive)
         { 
             if (_function==MovingFunctioning.DisappearedIfInactive)
             {
                 _platform.SetActive(false);
                 _isMoving = true;
                 
             }
             else if (_function==MovingFunctioning.StoppedIfInactive)
             {
                 _isMoving = false; 
             }
         }
     }
     //-------------------------------
     
     private void FixedUpdate()
     {
          if (Vector2.Distance(_waypoints[_currentWaypointIndex].transform.position, _platformPosition) < .1f)
          {
               _currentWaypointIndex++;
               if (_currentWaypointIndex >= _waypoints.Length)
               {
                    _currentWaypointIndex = 0;
               }
          }

          if (_isMoving&& GameManager.Instance.State!=GameState.StartingYoungTurn&& GameManager.Instance.State!=GameState.StartingOldTurn)
          {
              _platformPosition = Vector2.MoveTowards(_platformPosition,
                    _waypoints[_currentWaypointIndex].transform.position, Time.fixedDeltaTime * speed);
          }
     }

     private void Update()
     {
         _platform.transform.position = _platformPosition;
     }

     public void SwitchState()
     {
         if (_function==MovingFunctioning.DisappearedIfInactive)
         {
             _platform.SetActive(!_platform.activeSelf);
             _isMoving = true;
         }
         else if (_function==MovingFunctioning.StoppedIfInactive)
         {
             _isMoving = !_isMoving;
         }
     }
     /*
     public void SetDeactivated()
     {
          _isActive = false;
          _currentWaypointIndex = 0;
          gameObject.transform.position=_initPosition;
     }
     public void SetActivated()
     {
          _isActive = true;
          _currentWaypointIndex = 0;
          gameObject.transform.position=_initPosition;
     }
     */

     
}

[System.Serializable]
public enum MovingState
{
    Inactive,
    Active
}
[System.Serializable]
public enum MovingFunctioning
{
    StoppedIfInactive,
    DisappearedIfInactive
}

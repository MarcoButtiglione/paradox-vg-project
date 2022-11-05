using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
     [SerializeField] private GameObject[] _waypoints;
     private int _currentWaypointIndex = 0;
     private Vector3 _initPosition;
     private bool _isActive = true;
     [SerializeField] private float speed = 2.0f;
     

     private void Awake()
     {
          _initPosition = gameObject.transform.position;
     }

     private void Update()
     {
          if (Vector2.Distance(_waypoints[_currentWaypointIndex].transform.position, transform.position) < .1f)
          {
               _currentWaypointIndex++;
               if (_currentWaypointIndex >= _waypoints.Length)
               {
                    _currentWaypointIndex = 0;
               }
          }

          if (_isActive)
          {
               transform.position = Vector2.MoveTowards(transform.position,
                    _waypoints[_currentWaypointIndex].transform.position, Time.deltaTime * speed);
          }
     }

     

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

     public void SwitchState()
     {
          _isActive = !_isActive;
     }
}

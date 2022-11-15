using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaserController : MonoBehaviour
{
    [SerializeField] private GameObject[] _waypoints;
    private int _currentWaypointIndex = 0;
    private Vector3 _initPosition;
    private bool _isActive = true;
    [SerializeField] private float speed = 4.0f;
    private GameObject _top;

    private void Awake()
    {
        _top = GameObject.Find("LaserTop");
        if (_top)
        {
            Debug.Log("Top is here");
        }
        _initPosition = gameObject.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(_waypoints[_currentWaypointIndex].transform.position, gameObject.transform.position) < .1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Length)
            {
                _currentWaypointIndex = 0;
            }
        }

        if (_isActive)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position,
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
}

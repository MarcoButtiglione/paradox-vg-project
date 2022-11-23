using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingLaserController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform laserPosition;
    private GameObject _laserRay;
    [SerializeField] float speed = 20.0f;
    [SerializeField] float angle = 0.75f;
    private Vector2 _direction;
    private Vector2 _startingDirection;

    private void Awake()
    {
        _laserRay = transform.GetChild(0).gameObject;
        _direction = -transform.up;
        _startingDirection = -transform.up;    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _direction = Quaternion.Euler(0f, 0f, speed * Time.deltaTime) * _direction;

        if (Math.Abs(_direction.y) <= angle)
        {
            speed = -speed;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction);
        lineRenderer.SetPosition(0, laserPosition.position);
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
            CheckCollision(hit.collider);
        }
        else
        {
            lineRenderer.SetPosition(1, _direction*100);
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
            else if(col.gameObject.CompareTag("Old") || col.gameObject.CompareTag("Ghost"))
            {
                GameManager.Instance.UpdateGameState(GameState.Paradox);
            } 
        }
    }
    
    public void SetActivated()
    {
        _laserRay.SetActive(true);
        _direction = _startingDirection;
        speed = 20.0f;
    }
}

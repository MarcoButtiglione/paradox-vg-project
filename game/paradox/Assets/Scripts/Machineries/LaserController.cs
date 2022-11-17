using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform laserPosition;
    private GameObject _laserRay;
    private float _timer = 1.0f;
    private Vector2 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _laserRay = GameObject.Find("LaserRay");
        _direction = -transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        _direction = Quaternion.Euler(0f, 0f, 20f * Time.deltaTime) * _direction;
        Debug.Log(_direction.y);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up);
        lineRenderer.SetPosition(0, laserPosition.position);
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
            CheckCollision(hit.collider);
        }
        else
        {
            lineRenderer.SetPosition(1, -transform.up*100);
        }
    }
    
    // Check if the laser ray is hitting the player/ghost
    private void CheckCollision(Collider2D col)
    {
        if (_laserRay.activeSelf)
        {
           if (col.gameObject.CompareTag("Young"))
           {
               StopAllCoroutines();
               GameManager.Instance.UpdateGameState(GameState.StartingYoungTurn);
           }
           else if(col.gameObject.CompareTag("Old") || col.gameObject.CompareTag("Ghost"))
           {
               GameManager.Instance.UpdateGameState(GameState.Paradox);
           } 
        }
        
    }

    // In the young player turn, the laser activates and deactivates periodically
    public void StartPeriodic()
    {
        //StopAllCoroutines();
        StartCoroutine(Periodic());
    }

    private IEnumerator Periodic()
    {
        while (GameManager.Instance.State == GameState.YoungPlayerTurn)
        {
            _laserRay.SetActive(false);
            yield return new WaitForSeconds(_timer);
            _laserRay.SetActive(true);
            yield return new WaitForSeconds(_timer);
        }
    }

    // In the old player turn, the laser is fixed and it is the player's job to deactivate it
    public void StartFixed()
    {
        //StopAllCoroutines();
        _laserRay.SetActive(true);
    }
}

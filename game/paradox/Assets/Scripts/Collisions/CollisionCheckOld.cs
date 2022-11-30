using System;
using UnityEngine;

public class CollisionCheckOld : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ghost"))
        {
            GameManager.Instance.UpdateGameState(GameState.Paradox);
        }
    }
}

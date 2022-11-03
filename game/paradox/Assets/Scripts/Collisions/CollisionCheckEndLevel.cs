using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionCheckEndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Young"))
        {
            if (GameManager.Instance.IsTutorial())
            {
                GameManager.Instance.UpdateGameState(GameState.StartingSecondPart);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.StartingOldTurn);
            }
        }
        else if (col.CompareTag("Ghost") && GameManager.Instance.State != GameState.NextLevel)
        {
            GameManager.Instance.UpdateGameState(GameState.LevelCompleted);
        }
    }

}

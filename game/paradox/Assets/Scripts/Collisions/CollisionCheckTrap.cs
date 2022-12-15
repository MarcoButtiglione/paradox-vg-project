using UnityEngine;

public class CollisionCheckTrap : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D col)
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


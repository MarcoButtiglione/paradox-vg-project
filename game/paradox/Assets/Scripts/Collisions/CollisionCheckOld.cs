using UnityEngine;

public class CollisionCheckOld : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D col)
    { 
        if(col.CompareTag("Ghost")){ 
            GameManager.Instance.UpdateGameState(GameState.Paradox);
        }
    }
    
}

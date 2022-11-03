using UnityEngine;
public class CollisionCheckDeathLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Young")){
            GameManager.Instance.UpdateGameState(GameState.StartingYoungTurn);
        }
        else if(col.CompareTag("Old"))
        {
            GameManager.Instance.UpdateGameState(GameState.Paradox);
        }
    }
   
}

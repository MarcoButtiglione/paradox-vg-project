
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Old")){
            GameManager.Instance.UpdateGameState(GameState.Paradox);
        }
    }
}

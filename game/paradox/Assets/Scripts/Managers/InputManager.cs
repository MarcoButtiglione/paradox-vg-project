using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.StartingYoungTurn)
        {
            if (Input.anyKey)
            {
                GameManager.Instance.UpdateGameState(GameState.YoungPlayerTurn);
            }
        }
        
        if (GameManager.Instance.State == GameState.StartingOldTurn && !PostProcessingManager.Instance.isProcessing)
        {
            if (Input.anyKey)
            {
                    GameManager.Instance.UpdateGameState(GameState.OldPlayerTurn);
            }
        }
    }
}

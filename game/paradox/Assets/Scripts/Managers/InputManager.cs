using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    private bool holdingDown = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameState.StartingYoungTurn)
        {
            if (Input.anyKey)
            {
                holdingDown = true;
            }

            if (!Input.anyKey && holdingDown)
            {
                GameManager.Instance.UpdateGameState(GameState.YoungPlayerTurn);
                holdingDown = false;
            }

        }

        if (GameManager.Instance.State == GameState.StartingOldTurn && !PostProcessingManager.Instance.isProcessing)
        {
            Time.timeScale = 0f;
            if (Input.anyKey)
            {
                GameManager.Instance.UpdateGameState(GameState.OldPlayerTurn);
            }
        }
    }
}

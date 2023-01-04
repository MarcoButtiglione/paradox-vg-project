using UnityEngine;

public class DeactivatorOldPhase : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameState state)
    {

        if (state == GameState.StartingYoungTurn || state == GameState.YoungPlayerTurn || state == GameState.ThirdPart || state == GameState.StartingThirdPart)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}

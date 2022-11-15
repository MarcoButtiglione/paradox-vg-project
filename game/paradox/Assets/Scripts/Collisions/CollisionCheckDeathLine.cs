using UnityEngine;
public class CollisionCheckDeathLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Young"))
        {
            //In common for tutorial and game

            GameManager.Instance.UpdateGameState(GameState.StartingYoungTurn);

        }
        else if (col.CompareTag("Old"))
        {
            if (GameManager.Instance.IsTutorial() && GameManager.Instance.State == GameState.SecondPart)
            {
                GameManager.Instance.UpdateGameState(GameState.StartingSecondPart);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.Paradox);
            }
        }
    }

}

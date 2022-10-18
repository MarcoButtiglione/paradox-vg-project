using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void OnEnable()
    {
        //SceneController.OnPlayerDeath += EnableGameOverMenu;
        CollisionCheckOld.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        CollisionCheckOld.OnPlayerDeath -= EnableGameOverMenu;
        //SceneController.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

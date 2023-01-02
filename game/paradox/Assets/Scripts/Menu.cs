using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    
    
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }
    
    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state != GameState.PauseMenu)
        {
            optionsMenu.SetActive(false);
        }
    }
    
    public void GoToOptions()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void ReturnToPauseMenu()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
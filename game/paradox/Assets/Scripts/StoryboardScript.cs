using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryboardScript : MonoBehaviour
{
    private int counter = 8;
    private int currentIdx = 0;
    public GameObject storyboards;
    private bool _isWaiting=false;

    private void Awake()
    {
        storyboards.transform.GetChild(0).gameObject.SetActive(true);
    }

  

    public void Next()
    {
        

        currentIdx++;
        if (currentIdx < counter)
        {
            storyboards.transform.GetChild(currentIdx-1).gameObject.SetActive(false);
            storyboards.transform.GetChild(currentIdx).gameObject.SetActive(true);
        }
        else
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                LevelManager.Instance.EndedStoryboard();
            }
           
            //LOAD SCENE
            //LevelManager.Instance.PlayFirstLevel();
        }
    }

    public void Previous()
    {
        

        currentIdx--;
        if (currentIdx >= 0)
        {
            storyboards.transform.GetChild(currentIdx+1).gameObject.SetActive(false);
            storyboards.transform.GetChild(currentIdx).gameObject.SetActive(true);
        }
        else
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                LevelManager.Instance.PlayMainMenu();
            }
            //LOAD MENU
            
        }
    }
}

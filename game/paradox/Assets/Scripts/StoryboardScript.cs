using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryboardScript : MonoBehaviour
{
    private int counter = 8;
    private int currentIdx = 0;
    public GameObject storyboards;

    private void Awake()
    {
        storyboards.transform.GetChild(0).gameObject.SetActive(true);
    }

  

    public void Next()
    {
        storyboards.transform.GetChild(currentIdx).gameObject.SetActive(false);

        currentIdx++;
        if (currentIdx < counter)
        {
            storyboards.transform.GetChild(currentIdx).gameObject.SetActive(true);
        }
        else
        {
            LevelManager.Instance.EndedStoryboard();
            //LOAD SCENE
            //LevelManager.Instance.PlayFirstLevel();
        }
    }

    public void Previous()
    {
        storyboards.transform.GetChild(currentIdx).gameObject.SetActive(false);

        currentIdx--;
        if (currentIdx >= 0)
        {
            storyboards.transform.GetChild(currentIdx).gameObject.SetActive(true);
        }
        else
        {
            //LOAD MENU
            LevelManager.Instance.PlayMainMenu();
        }
    }
}

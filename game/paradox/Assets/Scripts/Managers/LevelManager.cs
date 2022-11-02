using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private int _currentLevel;

    /*
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _target;
    */
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _currentLevel = 0;
    }

    
    private async void LoadScene(string sceneName)
    {
        /*
        _target = 0;
        _progressBar.fillAmount = 0;
        */
        var scene = SceneManager.LoadSceneAsync(sceneName);
        
        //TODO
        /*
        scene.allowSceneActivation = false;
        
        _loaderCanvas.SetActive(true);
        do
        {
            _target = scene.progress;
        } while (scene.progress < 0.9f);
        
        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
        */
    }

    /*
    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }
    */

    public void PlayFirstLevel()
    {
        PlayLevel(1);
    }
    public void PlayNextLevel()
    {
        PlayLevel(_currentLevel+1);
    }
    public void RestartLevel()
    {
        PlayLevel(_currentLevel);
    }
    private void PlayLevel(int level)
    {
        _currentLevel = level;
        
        switch (level)
        {
            case 1:
                LoadScene("level0");
                break;
            case 2:
                PlayMainMenu();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        
    }

    public void PlayMainMenu()
    {
        LoadScene("Menu");
        _currentLevel = 0;
    }
}


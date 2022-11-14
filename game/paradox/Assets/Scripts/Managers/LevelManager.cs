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

        _currentLevel = SceneManager.GetActiveScene().buildIndex;
    }


    private void LoadScene(int level)
    {
        /*
        _target = 0;
        _progressBar.fillAmount = 0;
        */
        var scene = SceneManager.LoadSceneAsync(level);

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
    private void PlayLevel(int level)
    {

        _currentLevel = level % SceneManager.sceneCountInBuildSettings;
        LoadScene(_currentLevel);

    }
    public void PlayMainMenu()
    {
        LoadScene(0);
        _currentLevel = 0;
    }
    public void PlayFirstLevel()
    {
        PlayLevel(1);
    }
    public void PlayNextLevel()
    {
        PlayLevel(_currentLevel + 1);
    }
    public void RestartLevel()
    {
        PlayLevel(_currentLevel);
    }
    public bool IsTutorialLevel()
    {
        if (_currentLevel == 1)
            return true;
        return false;
    }


}


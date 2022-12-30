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
    [SerializeField] private GameObject endAnimation;
    [SerializeField] private GameObject startAnimation;
    private GameObject anim;

    private int _levelsFinished = 0;
    //private Scene _nextScene;


    //[SerializeField] private GameObject _loaderCanvas;
    //[SerializeField] private Image _progressBar;
    //private float _target;


    private void Awake()
    {
        //IF WE WANT TO ESTABLIH A FRAMERATE WE COULD PUT IT HERE
        //Application.targetFrameRate = 60;

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
        
            Instantiate(startAnimation, startAnimation.transform.position, Quaternion.identity);
        
    }


    private void LoadScene(int level)
    {
        /*
        _target = 0;
        _progressBar.fillAmount = 0;
        */

        Instantiate(endAnimation, endAnimation.transform.position, Quaternion.identity);
        StartCoroutine("EndLevel", level);

        //_loaderCanvas.SetActive(true);


        /*do
        {
            //_target = scene.progress;
        } while (scene.progress<0.9f);
        */

        //scene.allowSceneActivation = true;

        //_loaderCanvas.SetActive(false);

    }

    /*
    private void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }
    */
    public void PlayLevel(int level)
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
        if (_currentLevel > _levelsFinished)
            _levelsFinished = _currentLevel;
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

    private IEnumerator EndLevel(int level)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        var scene = SceneManager.LoadSceneAsync(level);

    }

    public int getLevelsFinished()
    {
        return _levelsFinished;
    }

}





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
    private Statistics stats;
    private int[] starsPerLevel;
    private float[] completionTimePerLevel;
    private int[] paradoxPerLevel;
    private int[] retryPerLevel;
    private float[] overallTimePerLevel;

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
            LoadData();
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
        if (_currentLevel != 0)
        {

            if (completionTimePerLevel[_currentLevel - 1] == 0)
            {
                completionTimePerLevel[_currentLevel - 1] = stats.GetCompletionTime();
                starsPerLevel[_currentLevel - 1] = stats.GetStars();
                overallTimePerLevel[_currentLevel - 1] = stats.GetOverallTime();
                retryPerLevel[_currentLevel - 1] = stats.GetRetrial();
                paradoxPerLevel[_currentLevel - 1] = stats.GetParadoxes();
            }
            else if (stats.GetStars() >= starsPerLevel[_currentLevel - 1] && stats.GetCompletionTime() < completionTimePerLevel[_currentLevel - 1])
            {
                completionTimePerLevel[_currentLevel - 1] = stats.GetCompletionTime();
                starsPerLevel[_currentLevel - 1] = stats.GetStars();
                overallTimePerLevel[_currentLevel - 1] = stats.GetOverallTime();
                retryPerLevel[_currentLevel - 1] = stats.GetRetrial();
                paradoxPerLevel[_currentLevel - 1] = stats.GetParadoxes();
            }

            if (_currentLevel > _levelsFinished)
            {
                _levelsFinished = _currentLevel;
            }
            //Debug.Log(stats.GetCompletionTime());
            SaveData();
        }

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
    public int[] GetStarsPerLevel()
    {
        return starsPerLevel;
    }
    public float[] GetCompletionTimePerLevel()
    {
        return completionTimePerLevel;
    }

    public float[] GetOverallTimePerLevel(){
        return overallTimePerLevel;
    }
    public int[] GetParadoxPerLevel(){
        return paradoxPerLevel;
    }
    public int[] GetRetryPerLevel(){
        return retryPerLevel;
    }

    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }


    public void LoadData()
    {
        GameData Data = SaveSystem.LoadData();
        if (Data != null)
        {
            this._levelsFinished = Data.lastLevelFinished;
            this.starsPerLevel = Data.starsPerLevel;
            this.completionTimePerLevel = Data.completionTimePerLevel;
            this.overallTimePerLevel = Data.overallTimePerLevel;
            this.paradoxPerLevel = Data.paradoxPerLevel;
            this.retryPerLevel = Data.retryPerLevel;
            ScanDebug();
        }
        else
        {
            starsPerLevel = new int[SceneManager.sceneCountInBuildSettings-1];
            completionTimePerLevel = new float[SceneManager.sceneCountInBuildSettings-1];
            overallTimePerLevel = new float[SceneManager.sceneCountInBuildSettings-1];
            paradoxPerLevel = new int[SceneManager.sceneCountInBuildSettings-1];
            retryPerLevel = new int[SceneManager.sceneCountInBuildSettings-1];
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 1; i++)
            {
                starsPerLevel[i] = 0;
                completionTimePerLevel[i] = 0f;
                overallTimePerLevel[i] = 0f;
                retryPerLevel[i] = 0;
                paradoxPerLevel[i] = 0;
            }
        }
    }

    public Statistics GetStatisticsLevel()
    {
        return stats;
    }

    public void ScanDebug()
    {
        var i = 0;
        while (completionTimePerLevel[i] != 0)
        {
            Debug.Log("Completion time for level " + i + " is :" + completionTimePerLevel[i]);
            Debug.Log("Num of stars earned in level " + i + " is :" + starsPerLevel[i]);
            Debug.Log("Num of paradox in level " + i + " is :" + paradoxPerLevel[i]);
            Debug.Log("Num of retrial in level " + i + " is :" + retryPerLevel[i]);
            Debug.Log("Overall time for level " + i + " is :" + overallTimePerLevel[i]);
            i++;
        }
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public void FindStats()
    {
        //Debug.Log("Entered here");

        stats = GameObject.Find("Managers").transform.GetChild(0).GetComponentInChildren<Statistics>();

        /*if (stats != null)
        {
            Debug.Log("Found stats");
            Debug.Log(stats.GetStars());
            Debug.Log(stats.GetCompletionTime());
        }*/
    }


}





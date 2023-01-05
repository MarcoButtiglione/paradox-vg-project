using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EndGameStats : MonoBehaviour
{
    private TimeSpan _totalCompTime;
    private TimeSpan _totalOverallTime;
    private int _totalParadox;
    private int _totalRetry;
    
    public TMP_Text compTimeText;
    public TMP_Text totalOverallTimeText;
    public TMP_Text totalParadoxText;
    public TMP_Text totalRetryText;
    private bool _isWaiting=false;
    private void Awake()
    {

    }

    private void Start()
    {
        LevelManager l = LevelManager.Instance;
        if (l != null)
        {
            _totalCompTime = TimeSpan.FromSeconds(l.GetCompletionTimePerLevel().Sum());
            _totalOverallTime = TimeSpan.FromSeconds(l.GetOverallTimePerLevel().Sum());
            _totalParadox = l.GetParadoxPerLevel().Sum();
            _totalRetry = l.GetRetryPerLevel().Sum();
   
            compTimeText.text = "Total completion time: " + _totalCompTime.ToString(@"mm\:ss\:ff");
            totalOverallTimeText.text = "Total overall time: " + _totalOverallTime.ToString(@"mm\:ss\:ff");
            totalParadoxText.text = "Total paradoxes: " + _totalParadox;
            totalRetryText.text = "Total retries: " + _totalRetry; 
        }
        else
        {
            Debug.Log("CAN'T GET LEVELMANAGER INSTANCE");
        }
    }

    public void GoToMainMenu()
    {
        if (!_isWaiting)
        {
            _isWaiting = true;
            LevelManager.Instance.PlayMainMenu();
        }
    }
}

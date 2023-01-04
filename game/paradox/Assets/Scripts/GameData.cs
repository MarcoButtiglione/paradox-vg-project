using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int lastLevelFinished;
    public int[] starsPerLevel;
    public float[] completionTimePerLevel;
    public int[] paradoxPerLevel;
    public int[] retryPerLevel;
    public float[] overallTimePerLevel;

    //CONSTRUCTOR
    public GameData(LevelManager levManager){
        lastLevelFinished = levManager.getLevelsFinished();
        starsPerLevel = levManager.GetStarsPerLevel();
        completionTimePerLevel = levManager.GetCompletionTimePerLevel();
        paradoxPerLevel = levManager.GetParadoxPerLevel();
        retryPerLevel = levManager.GetRetryPerLevel();
        overallTimePerLevel = levManager.GetOverallTimePerLevel();
    }
}

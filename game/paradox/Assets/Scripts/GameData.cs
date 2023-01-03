using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int lastLevelFinished;
    public int[] starsPerLevel;
    public float[] completionTimePerLevel;

    //CONSTRUCTOR
    public GameData(LevelManager levManager){
        lastLevelFinished = levManager.getLevelsFinished();
        starsPerLevel = levManager.GetStarsPerLevel();
        completionTimePerLevel = levManager.GetCompletionTimePerLevel();
    }
}

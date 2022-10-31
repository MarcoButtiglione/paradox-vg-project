using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradox/Timer Levels Parameters")]
public class TimerLevelsParameters : ScriptableObject
{
    /*
     * Use the code below in the other scripts
     * [SerializeField] private TimerLevelsParameters _timerLevelsParameters;
     */
    [Header("Timer (sec)")] 
    public float timerLevel0 = 5f;
    public float timerLevel1 = 5f;
    public float timerLevel2 = 5f;
    public float timerLevel3 = 5f;
    public float timerLevel4 = 5f;
    public float timerLevel5 = 5f;
}

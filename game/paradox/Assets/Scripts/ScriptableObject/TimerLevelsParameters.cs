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
    public float timerLevel = 5f;
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradox/Players Parameters")]
public class PlayersParameters : ScriptableObject
{
    /*
     * Use the code below in the other scripts
     * [SerializeField] private PlayerParameters _playerParameters;
     */
    [Header("Young professor")] 
    public float youngSpeed = 5f;
    public float youngJumpForce = 5f;
    [Header("Old professor")] 
    public float oldSpeed = 5f;
    public float oldJumpForce = 5f;
}

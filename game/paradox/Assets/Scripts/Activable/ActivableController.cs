using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivableController : MonoBehaviour
{
    [SerializeField] private UnityEvent interactAction;
    
    
    public void SwitchState()
    {
        interactAction.Invoke();
    }
}

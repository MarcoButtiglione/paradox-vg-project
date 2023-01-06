using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToMenuManager : MonoBehaviour
{
    private GameObject _menuManager;
    private bool _isWaiting;
    private void Awake()
    {
        _menuManager = GameObject.Find("MenuManager");
        _isWaiting = false;
    }
    

    public void GoToMainMenu()
    {
        if (!_isWaiting)
        {
            _isWaiting = true;
            StartCoroutine("Clicked");
            _menuManager.GetComponent<MenuManager>().GoToMainMenu();
        }
        
    }
    public void RestartDoor()
    {
        if (!_isWaiting)
        {
            _isWaiting = true;
            StartCoroutine("Clicked");
            _menuManager.GetComponent<MenuManager>().RestartDoor();
        }
    }
    public void NextLevelDoor()
    {
        if (!_isWaiting)
        {
            _isWaiting = true;
            StartCoroutine("Clicked");
            _menuManager.GetComponent<MenuManager>().NextLevelDoor();
        }
    }
    private IEnumerator Clicked()
    {
        yield return new WaitForSecondsRealtime(1f);
        _isWaiting = false;
    }
}

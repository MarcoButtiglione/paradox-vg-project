using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControllerTutorial : MonoBehaviour
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private GameObject stick;
    [SerializeField] private GameObject[] _objToActivate;
    [SerializeField] private GameObject questionMarks;



    public void TriggerLever()
    {
        if (GameManager.Instance.State == GameState.SecondPart)
        {
            questionMarks.SetActive(true);
            AudioManager a = FindObjectOfType<AudioManager>();
            if (a)
                a.Play("Click");

            StartCoroutine(activatePlatform());

        }
        else
        {
            _isActive = !_isActive;
            if (_isActive)
            {
                for (int i = 0; i < _objToActivate.Length; i++)
                {
                    _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
                }
                stick.transform.Rotate(0.0f, 0.0f, 90.0f);
            }
            else
            {
                for (int i = 0; i < _objToActivate.Length; i++)
                {
                    _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
                }
                stick.transform.Rotate(0.0f, 0.0f, -90.0f);
            }
            //Play the click sound-----
            AudioManager a = FindObjectOfType<AudioManager>();
            if (a)
                a.Play("Click");
            //-------------------------
        }
    }

    IEnumerator activatePlatform()
    {

        //TODO
        //Activate canvas in second part 

        _isActive = !_isActive;
        if (_isActive)
        {
            for (int i = 0; i < _objToActivate.Length; i++)
            {
                _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
            }
            stick.transform.Rotate(0.0f, 0.0f, 90.0f);
        }
        else
        {
            for (int i = 0; i < _objToActivate.Length; i++)
            {
                _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
            }
            stick.transform.Rotate(0.0f, 0.0f, -90.0f);
        }

        yield return new WaitForSecondsRealtime(2.3f);

        _isActive = !_isActive;
        if (_isActive)
        {
            for (int i = 0; i < _objToActivate.Length; i++)
            {
                _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
            }
            stick.transform.Rotate(0.0f, 0.0f, 90.0f);
        }
        else
        {
            for (int i = 0; i < _objToActivate.Length; i++)
            {
                _objToActivate[i].SetActive(!_objToActivate[i].activeSelf);
            }
            stick.transform.Rotate(0.0f, 0.0f, -90.0f);
        }
        questionMarks.SetActive(false);
        
        GameManager.Instance.UpdateGameState(GameState.StartingOldTurn);

    }


}



using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelInfoUI : MonoBehaviour
{
    private TextMeshProUGUI _topText;
    private TextMeshProUGUI _buttonText;
    [SerializeField] private LevelNamesParameters levelNames;

    private void Start()
    {
        _topText = gameObject.GetComponent<TextMeshProUGUI>();
        _buttonText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        var cl=LevelManager.Instance.GetCurrentLevel();
        _topText.text ="lab "+((cl-1)/10 + 1)+" - level "+ (cl-1);
        //var s = levelNames.levelNames[cl - 1];
        //_buttonText.text = "";
    }
}

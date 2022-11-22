using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject levelPrefab;
    private TMP_Text levelText;
    private int _totalLevels;

    // Start is called before the first frame update
    private void Awake()
    {
        _totalLevels = SceneManager.sceneCountInBuildSettings;

        for (int level = 1; level < _totalLevels; level++)//Hard coded build index start level
        {
            GameObject newButton = Instantiate(levelPrefab, this.transform, false);
            newButton.GetComponentInChildren<TMP_Text>().SetText((level-1).ToString());
            int sceneIndex = level;
            newButton.GetComponent<Button>().onClick.AddListener(() => LevelManager.Instance.PlayLevel(sceneIndex));
        }
    }
    
}

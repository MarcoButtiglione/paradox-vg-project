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
    private int _levelsFinished;
    [SerializeField]
    private bool _isAdmin;

    private int[] _starsPerLevel;

    // Start is called before the first frame update
    private void Awake()
    {
        _totalLevels = SceneManager.sceneCountInBuildSettings;
        _levelsFinished = LevelManager.Instance.getLevelsFinished();
        _starsPerLevel = LevelManager.Instance.GetStarsPerLevel();

        for (int level = 1; level < _totalLevels; level++)//Hard coded build index start level
        {
            GameObject newButton = Instantiate(levelPrefab, this.transform, false);
            newButton.GetComponentInChildren<TMP_Text>().SetText((level-1).ToString());
            int sceneIndex = level;
            if (_isAdmin)
            {
                newButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                if (_levelsFinished + 1 >= sceneIndex)
                {
                    newButton.GetComponent<Button>().interactable = true;
                    newButton.transform.GetChild(_starsPerLevel[level-1]).gameObject.SetActive(true);
                }
                else
                {
                    newButton.GetComponent<Button>().interactable = false;
                
                }
            }
            newButton.GetComponent<Button>().onClick.AddListener(() => LevelManager.Instance.PlayLevel(sceneIndex));
        }
    }
}

using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    private TMP_Text _timerText;
    private SceneController father;
    [SerializeField] private float TimeLeft = 60.0f;
    
    
    
    private void Awake(){
        _timerText = GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            updateTimer(TimeLeft);
        }
        else
        {
            TimeLeft = 0;
            //GAME OVER
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }

    }

    void updateTimer(float currentTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }
    
}

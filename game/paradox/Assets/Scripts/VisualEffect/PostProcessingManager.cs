using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance;
    public GameObject Camera;
    public GameObject Old_Player;
    
    private Vignette _vignette;
    private Vector2 normalizedOldPlayerPos;
    private bool upIntensityDone = false;
    public bool isProcessing = false;
   

    private void Awake()
    {
        Instance = this;
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (GameManager.Instance.IsTutorial())
        {
            if (state == GameState.StartingSecondPart&& GameManager.Instance.PreviousGameState==GameState.YoungPlayerTurn)
            {
                StartCoroutine("StartDelay");
            }

        }else{
            if (state == GameState.StartingOldTurn&& GameManager.Instance.PreviousGameState==GameState.YoungPlayerTurn)
            {
                StartCoroutine("StartDelay");
            }

        }
        
        if (state == GameState.Paradox)
        {
            Camera.GetComponent<CameraShakeScript>().setShakeTrue();
        }
        else
        {
            Camera.GetComponent<CameraShakeScript>().setShakeFalse();
        }
    }

    private void Start()
    {
        normalizedOldPlayerPos = NormPosFromZeroToOne(new Vector2(Old_Player.transform.position.x, Old_Player.transform.position.y));
        Camera.GetComponent<PostProcessVolume>().profile.TryGetSettings(out _vignette);

        //Wanted to center the camera on the old player but doesn't work in this way
        _vignette.center.value = normalizedOldPlayerPos;
    }
    
    
    
    //COROUTINE TO ADD A TRANSACTION BETWEEN FIRST AND SECOND PART
    IEnumerator StartDelay()
    {
        isProcessing = true;
        Time.timeScale = 0;

        while (_vignette.intensity.value >= 0f)
        {

            if (_vignette.intensity.value <= 1.5f && !upIntensityDone)
            {
                _vignette.intensity.value += 1.5f * Time.unscaledDeltaTime;
            }
            else
            {
                upIntensityDone = true;
                _vignette.intensity.value -= 1.5f*Time.unscaledDeltaTime;
            }


            yield return 0;
        }

        upIntensityDone = false;
        _vignette.intensity.value = 0f;

        isProcessing = false;
        Time.timeScale = 1;

        if (GameManager.Instance.IsTutorial())
        {
            GameManager.Instance.UpdateGameState(GameState.SecondPart);
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.OldPlayerTurn);
        }
        


    }

    public Vector2 NormPosFromZeroToOne(Vector2 input)
    {

        input.x = Mathf.InverseLerp(-8.6f, 8.6f, input.x);
        input.y = Mathf.InverseLerp(-4.4f, 4.4f, input.y);

        return input;
    }
}

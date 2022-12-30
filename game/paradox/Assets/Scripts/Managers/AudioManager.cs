using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    private bool _isUsingJetpack;
    private bool _wasUsingJetpack;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    /*
     * Used only for the theme sound
     */
    private void Start()
    {
        Play("Theme");
        SceneManager.activeSceneChanged += ChangedActiveScene;
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;

    }
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (_isUsingJetpack)
        {
            Stop("Jetpack");
            _isUsingJetpack = false;
            _wasUsingJetpack = true;
            return;
        }

        if (_wasUsingJetpack&& state==GameState.OldPlayerTurn&& GameManager.Instance.PreviousGameState==GameState.PauseMenu)
        {
            Play("Jetpack");
            _isUsingJetpack = true;
            _wasUsingJetpack = false;
            return;
        }
        _wasUsingJetpack = false;
        
    }
    private void ChangedActiveScene(Scene current, Scene next)
    {
        Stop("Jetpack");
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds,sound=>sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning("Sounds: "+ name+ " not found!");
            return;
        }
        s.source.Play();
        if (name == "Jetpack")
        {
            _isUsingJetpack = true;
            _wasUsingJetpack = false;
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds,sound=>sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning("Sounds: "+ name+ " not found!");
            return;
        }
        s.source.Stop();
        if (name == "Jetpack")
        {
            _isUsingJetpack = false;
            _wasUsingJetpack = false;
        }
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds,sound=>sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning("Sounds: "+ name+ " not found!");
            return;
        }
        s.source.Pause();
    }

    public void Resume(string name)
    {
        Sound s = Array.Find(sounds,sound=>sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning("Sounds: "+ name+ " not found!");
            return;
        }
        s.source.UnPause();
    }

    public void SetEffectVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            if (s.name != "Theme")
            {
                s.source.volume = s.volume * volume;
            }
        }
        //Debug.Log("Master volume: " + volume);
    }

    public void SetThemeVolume(float volume)
    {
        Sound s = Array.Find(sounds,sound=>sound.name == "Theme");
        s.source.volume = s.volume * volume;
        //Debug.Log("Theme volume: " + volume);
    }
    public void SetMasterVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * volume;
        }
        //Debug.Log("Master volume: " + volume);
    }
}

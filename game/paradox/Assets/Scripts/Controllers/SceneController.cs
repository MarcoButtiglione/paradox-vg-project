using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The script that should manage the level
//It records the movement of the player and make the objects appear and
//Disappear when needed

public class SceneController : MonoBehaviour
{


    public GameObject GhostPrefab;
    public GameObject Old_Player;
    public GameObject Young_Player;
    public GameObject Disappearing_Platform;
    public GameObject ReplayButtonPrefab;
    public GameObject Camera;
    public GameObject EndLevel;
    public GameObject DeathLine;
    public GameObject Timer;


    //ATTENTION!!! 
    //If the object has a RigidBody, we have to assure
    //That It is cynematic, I'll make the lines of code
    //and comment them

    //It'll keep track of the positions
    private List<Vector3> positions_young_p;
    private List<Vector3> positions_old_p;
    private List<TypeOfInputs> inputs;
    private TypeOfInputs structInputs;
    private PlayerMovement toTrack;

    //public static event Action OnPlayerDeath; ATTENTION Let's see when tu use

    private bool jump = false;
    private bool firstPartEnded = false;
    private bool isRewinding = false;
    private bool firstIteration = true;
    private int index;
    private int _reloadSpeed;
    [SerializeField]
    private int parameterReload = 40;
    [SerializeField]
    private float timerTime = 10.0f;
    [SerializeField]
    private float delayBetweenParts = 3.0f;


    //FOR TRAP TRIGGER
    private void OnEnable()
    {
        CollisionCheckTrap.OnPlayerDeath += RestartRewind;
    }

    void Start()
    {
        parameterReload = 40;
        index = 0;

        positions_young_p = new List<Vector3>();
        inputs = new List<TypeOfInputs>();

        GhostPrefab.SetActive(false);
        Old_Player.SetActive(false);


        GhostPrefab = Instantiate(GhostPrefab, GhostPrefab.transform.position, Quaternion.identity);
        ReplayButtonPrefab = Instantiate(ReplayButtonPrefab, ReplayButtonPrefab.transform.position, ReplayButtonPrefab.transform.rotation);

        GhostPrefab.GetComponent<GhostController>().setFather(this);
        EndLevel.GetComponent<CollisionCheckEndLevel>().setFather(this);
        toTrack = Young_Player.GetComponent<PlayerMovement>();
        Timer.GetComponentInChildren<TimerScript>().setTimeLeft(timerTime);


        foreach (CollisionCheckDeathLine line in DeathLine.GetComponentsInChildren<CollisionCheckDeathLine>())
        {
            line.setFather(this);
        }

    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.Return))
        //StartRewind();

        jump = toTrack.getJump();



    }

    void FixedUpdate()
    {
        //START OF THE SECOND PART OF LEVEL
        if (isRewinding)
        {
            positions_old_p.Insert(positions_old_p.Count, Old_Player.transform.position);
            MoveGhost();
        }
        else
        {   //FIRST PART OF THE LEVEL WITH YOUNG PLAYER
            if (!firstPartEnded)
            {
                Record();
            }
            else
            {
                //PART IN WHICH WE REWIND AND THEN RESTART
                if (index > 0)
                {
                    if (firstIteration)
                    {
                        _reloadSpeed = index / parameterReload;
                        Old_Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                        Camera.GetComponent<CameraShakeScript>().setShakeTrue();

                    }
                    RestartOldAndGhost();
                }
                else
                { //HERE WE SHOULD RESTART THE LOOP IF POSSIBLE, IN ANY CASE RELOOP IS DONE 
                    //OnPlayerDeath?.Invoke(); 
                    RepeatRewind();
                }
            }
        }
    }


    void Record()
    {
        positions_young_p.Insert(positions_young_p.Count, Young_Player.transform.position);
        setInputs();
    }

    //Protocol to execute when we pass the level with the young player

    public void StartSecondPart()
    {
        StartCoroutine("StartDelay");

        isRewinding = true;
        GhostPrefab.transform.position = positions_young_p[0];

        positions_old_p = new List<Vector3>();

        Old_Player.SetActive(true);
        Young_Player.SetActive(false);
        Disappearing_Platform.SetActive(false);
        GhostPrefab.SetActive(true);
        Timer.SetActive(false);
    }

    //When the loop has finished, we can reset the values and make the object reappear or disappear
    //according to the needs
    public void RestartRewind()
    {
        isRewinding = false;
        firstPartEnded = true;
    }

    void setInputs()
    {
        structInputs = new TypeOfInputs(toTrack.getHorizontal(), toTrack.getCrouch(), jump);
        jump = false;
        inputs.Insert(inputs.Count, structInputs);
    }


    public void RestartOldAndGhost()
    {
        if (_reloadSpeed > 0 && index - _reloadSpeed > 0)
        {
            index = index - _reloadSpeed;
        }
        else if (index - 1 > 0)
        {
            index--;
        }
        else
        {
            index = 0;
        }
        Old_Player.transform.position = positions_old_p[index];
        GhostPrefab.SetActive(false);
        ReplayButtonPrefab.SetActive(!ReplayButtonPrefab.activeSelf);
    }
    public void MoveGhost()
    {
        if (index < inputs.Count)
        {

            if (Vector2.Distance(new Vector2(GhostPrefab.transform.position.x, GhostPrefab.transform.position.y), new Vector2(positions_young_p[index].x, positions_young_p[index].y)) > 0.5)
            {
                //PARADOX
                RestartRewind();
            }


            GhostPrefab.GetComponent<CharacterController2D>().Move(inputs[index].getHorizontal() * Time.fixedDeltaTime, inputs[index].getCrouch(), inputs[index].getJump());
            index++;
        }
        else
        {

            RestartRewind(); //In future will be level wone probably
        }
    }
    public void RepeatRewind()
    {
        index = 0;
        positions_old_p = new List<Vector3>();
        isRewinding = true;
        firstIteration = true;
        Old_Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GhostPrefab.transform.position = positions_young_p[0];
        GhostPrefab.SetActive(true);
        ReplayButtonPrefab.SetActive(false);
        Camera.GetComponent<CameraShakeScript>().setShakeFalse();

    }


    //COROUTINE TO ADD A DELAY BETWEEN FIRST AND SECOND PART
    IEnumerator StartDelay()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + delayBetweenParts;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        Time.timeScale = 1;


    }


}

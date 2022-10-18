using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The script that should manage the level
//It records the movement of the player and make the objectsa
//Disappear when needed

public class SceneController : MonoBehaviour
{

    private bool isRewinding = false;
    public GameObject GhostPrefab;
    public GameObject Old_Player;
    public GameObject Young_Player;
    public GameObject Disappearing_Platform;


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
    private bool first_part_ended = false;
    private int index;



    void Start()
    {
        positions_young_p = new List<Vector3>();
        inputs = new List<TypeOfInputs>();
        GhostPrefab.SetActive(false);
        GhostPrefab = Instantiate(GhostPrefab, transform.position, Quaternion.identity);
        GhostPrefab.GetComponent<GhostController>().setFather(this);
        Old_Player.SetActive(false);
        toTrack = Young_Player.GetComponent<PlayerMovement>();
        index = 0;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
            StartRewind();

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
            if (!first_part_ended)
            {
                Record();
            }
            else
            {
                //PART IN WHICH WE REWIND AND THEN RESTART
                if (index > 0)
                {
                    RestartOldAndGhost();
                }
                else
                { //HERE WE SHOULD RESTART THE LOOP IF POSSIBLE, IN ANY CASE RELOOP IS DONE 
                    //OnPlayerDeath?.Invoke(); 

                    positions_old_p = new List<Vector3>();
                    isRewinding = true;
                    GhostPrefab.transform.position = positions_young_p[0];
                    
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

    public void StartRewind()
    {
        positions_old_p = new List<Vector3>();
        isRewinding = true;
        Old_Player.SetActive(true);
        Young_Player.SetActive(false);
        Disappearing_Platform.SetActive(false);
        GhostPrefab.transform.position = positions_young_p[0];
        GhostPrefab.SetActive(true);
    }

    //When the loop has finished, we can reset the values and make the object reappear or disappear
    //according to the needs
    public void RepeatRewind()
    {
        isRewinding = false;
        first_part_ended = true;

    }

    void setInputs()
    {
        structInputs = new TypeOfInputs(toTrack.getHorizontal(), toTrack.getCrouch(), jump);
        jump = false;
        inputs.Insert(inputs.Count, structInputs);
    }

    public List<Vector3> getPositions()
    {
        return positions_young_p;
    }
    public List<TypeOfInputs> getInputs()
    {
        return inputs; ;
    }
    public void RestartOldAndGhost()
    {
        index--;
        GhostPrefab.transform.position = positions_young_p[index];
        Old_Player.transform.position = positions_old_p[index];
    }
    public void MoveGhost()
    {
        if (index < inputs.Count)
        {

            if (Vector2.Distance(new Vector2(GhostPrefab.transform.position.x, GhostPrefab.transform.position.y), new Vector2(positions_young_p[index].x, positions_young_p[index].y)) > 0.5)
            {
                RepeatRewind();
            }

            GhostPrefab.GetComponent<CharacterController2D>().Move(inputs[index].getHorizontal() * Time.fixedDeltaTime, inputs[index].getCrouch(), inputs[index].getJump());
            index++;
        }
        else
        {
            RepeatRewind(); //In future will be level wone probably
        }
    }


}

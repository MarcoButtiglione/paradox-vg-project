using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The script that should manage the level
//It records the movement of the player and make the objectsa
//Disappear when needed

public class TimeBody : MonoBehaviour
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

    private bool jump = false;
    private bool end_level = false;
    private int index = 0;



    void Start()
    {
        positions_young_p = new List<Vector3>();
        positions_old_p = new List<Vector3>();
        inputs = new List<TypeOfInputs>();
        GhostPrefab.SetActive(false);
        GhostPrefab = Instantiate(GhostPrefab, transform.position, Quaternion.identity);
        GhostPrefab.GetComponent<GhostController>().setFather(this);
        Old_Player.SetActive(false);
        toTrack = Young_Player.GetComponent<PlayerMovement>();
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
            GhostPrefab.GetComponent<GhostController>().setRewind();
        }
        else    
        {   //FIRST PART OF THE LEVEL WITH YOUNG PLAYER
            if(!end_level){
            Record();
            }else{
                //PART IN WHICH WE REWIND AND THEN RESTART
                if(index>0){
                    RestartOldAndGhost();
                }else{ //HERE WE SHOULD RESTART THE LOOP IF POSSIBLE
                    end_level = false;
                    //isRewinding = false;
                }
            }
        }
    }


    void Record()
    {
        positions_young_p.Insert(positions_young_p.Count, Young_Player.transform.position);
        setInputs();
    }

    //Function called when we want to rewind, it'll change the value so that
    //in the fixed update we enter the rewind loop.
    public void StartRewind()
    {
        isRewinding = true;
        Old_Player.SetActive(true);
        Young_Player.SetActive(false);
        Disappearing_Platform.SetActive(false);
    }

    //When the loop has finished, we can reset the values and make the object reappear or disappear
    //according to the needs
    public void StopRewind(int index)
    {
        isRewinding = false;
        //GhostPrefab.SetActive(false);
        Disappearing_Platform.SetActive(true);
        end_level = true;
        this.index = index;
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
    public void RestartOldAndGhost(){
                    GhostPrefab.transform.position = positions_young_p[index];
                    Old_Player.transform.position = positions_old_p[index];
                    index--;
    }


}

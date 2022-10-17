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
    private List<Vector3> positions;
    private List<TypeOfInputs> inputs;
    private TypeOfInputs structInputs;
    private PlayerMovement toTrack;

    private bool jump = false;


    void Start()
    {
        positions = new List<Vector3>();
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


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }


    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            GhostPrefab.GetComponent<GhostController>().setRewind();
        }
        else
            Record();
    }


    void Record()
    {
        positions.Insert(positions.Count, Young_Player.transform.position);
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
    public void StopRewind()
    {
        isRewinding = false;
        GhostPrefab.SetActive(false);
        Disappearing_Platform.SetActive(true);
    }

    void setInputs()
    {
        structInputs = new TypeOfInputs(toTrack.getHorizontal(), toTrack.getCrouch(), jump);
        jump = false;
        inputs.Insert(inputs.Count, structInputs);
    }

    public List<Vector3> getPositions()
    {
        return positions;
    }
    public List<TypeOfInputs> getInputs()
    {
        return inputs; ;
    }


}

using System;
using System.Collections.Generic;
using UnityEngine;
public class RewindManager : MonoBehaviour
{
    //Ghost
    public GameObject _initGhostPrefab;
    private GameObject GhostPrefab;
    private Rigidbody2D rigidbodyGhost;

    //DebugWorker
    public GameObject _initWorker;
    private GameObject WorkerPrefab;


    //Old
    private Vector3 _initPosOld;
    public GameObject _initOldPrefab;
    private GameObject OldPrefab;
    public GameObject Old_Start_Position;

    //Young

    //private GameObject YoungPlayer;
    //[SerializeField] private GameObject _youngPrefab;


    private Rigidbody2D rigidbodyYoung;
    public GameObject _initYoungPrefab;
    private GameObject YoungPrefab;

    public GameObject Young_Start_Position;
    private Vector3 _initPosYoung;

    private List<Vector3> positions_young_p;
    private List<TypeOfInputs> inputs;

    private List<Vector3> positions_old_p;
    //private TypeOfInputs structInputs;
    private PlayerMovement toTrack;

    //private bool jump = false;

    private int index;
    private int _reloadSpeed;

    [SerializeField]
    private int parameterReload = 40;

    [SerializeField]
    private float tresHold = 0.5f;


    //Event managment 
    private void Awake()
    {
        //It is subscribing to the event
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;

        //Old
        _initPosOld = Old_Start_Position.transform.position;
        //_initOldPrefab = OldPrefab;
        //Young
        _initPosYoung = Young_Start_Position.transform.position;
        //_initYoungPrefab = YoungPrefab;
        //Ghost
        //_initGhostPrefab = GhostPrefab;

    }
    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.StartingYoungTurn)
        {
            Init();
        }
        else if (state == GameState.YoungPlayerTurn)
        {
            rigidbodyYoung.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (state == GameState.StartingSecondPart)
        {
            StartSecondPartTutorial();
        }
        /*else if (state == GameState.ThirdPart)
        {
            rigidbodyGhost.bodyType = RigidbodyType2D.Dynamic;
        }*/
        else if (state == GameState.StartingThirdPart)
        {
            StartThirdPartTutorial();
        }
        else if (state == GameState.StartingOldTurn)
        {
            StartSecondPart();
        }
        else if (state == GameState.OldPlayerTurn)
        {
            rigidbodyGhost.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (state == GameState.Paradox)
        {
            if (GameManager.Instance.PreviousGameState == GameState.OldPlayerTurn)
            {
                _reloadSpeed = index / parameterReload;
                OldPrefab.GetComponent<OldPlayerMovement>().enabled = false;
            }
        }
    }

    private void Init()
    {
        //GhostPrefab.SetActive(false);



        //Old_Player.SetActive(false);
        //Old_Player.transform.position = _initPosOld;

        if (GhostPrefab != null)
        {
            Destroy(GhostPrefab);
        }

        if (OldPrefab != null)
        {
            Destroy(OldPrefab);
        }

        if (YoungPrefab != null)
        {
            Destroy(YoungPrefab);
        }
        if (WorkerPrefab != null)
        {
            Destroy(WorkerPrefab);
        }

        /*
        if (YoungPlayer != null)
        {
            Destroy(YoungPlayer);
        }
        YoungPlayer = Instantiate(_youngPrefab, _initPosYoung, Quaternion.identity);
        */

        //Init Young
        YoungPrefab = Instantiate(_initYoungPrefab, _initPosYoung, Quaternion.identity);
        rigidbodyYoung = YoungPrefab.GetComponent<Rigidbody2D>();
        rigidbodyYoung.bodyType = RigidbodyType2D.Static;
        toTrack = YoungPrefab.GetComponent<PlayerMovement>();


        //Young_Player.SetActive(true);
        //Young_Player.transform.position = _initPosYoung;

        //Young_Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        //Young_Player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        //Young_Player.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        //Young_Player.transform.position = _initPosYoung;

        /*
        if(Young_Player)
            Destroy(Young_Player);
        Young_Player = Instantiate(_initYoungPrefab, _initYoungPrefab.transform.position, Quaternion.identity);
        
        
        Young_Player.SetActive(true);
        */
        //-------------

        //GhostPrefab.SetActive(false);

        positions_young_p = new List<Vector3>();


        //inputs = new List<TypeOfInputs>();
        //inputs.Insert(inputs.Count, new TypeOfInputs(0, false, false));

        parameterReload = 40;
        index = 0;
    }

    private void FixedUpdate()
    {
        /*   MAYBE NOT USED NOW, DO IT IN PLAYERMOVEMENT DIRECTLY
        if (GameManager.Instance.State == GameState.YoungPlayerTurn)
        {
            Record();
        }
        else*/
        if (GameManager.Instance.State == GameState.OldPlayerTurn)
        {
            positions_old_p.Insert(positions_old_p.Count, OldPrefab.transform.position);
            MoveGhost();
        }
        else if (GameManager.Instance.State == GameState.ThirdPart)
        {
            MoveOnlyGhost();
        }
        else if (GameManager.Instance.State == GameState.Paradox)
        {
            Destroy(GhostPrefab);
            Destroy(WorkerPrefab);
            RestartOldAndGhost();
        }
    }

    private void LateUpdate()
    {

        if (GameManager.Instance.State == GameState.OldPlayerTurn)
        {
            Debug.Log(" Delta position x:" + (GhostPrefab.transform.position.x - positions_young_p[index].x));
            Debug.Log(" Delta position y :" + (GhostPrefab.transform.position.y - positions_young_p[index].y));
            if (Vector2.Distance(new Vector2(GhostPrefab.transform.position.x, GhostPrefab.transform.position.y), new Vector2(positions_young_p[index].x, positions_young_p[index].y)) > tresHold)
            {
                Destroy(GhostPrefab);
                Destroy(WorkerPrefab);
                GameManager.Instance.UpdateGameState(GameState.Paradox);
                return;
            }
        }
        else if (GameManager.Instance.State == GameState.ThirdPart)
        {
            if (Vector2.Distance(new Vector2(GhostPrefab.transform.position.x, GhostPrefab.transform.position.y), new Vector2(positions_young_p[index].x, positions_young_p[index].y)) > tresHold)
            {
                Destroy(GhostPrefab);
                Destroy(WorkerPrefab);
                GameManager.Instance.UpdateGameState(GameState.StartingSecondPart);
                return;
            }
        }

    }


    /*
    void Record()
    {

        Debug.Log("YoungPosition: " + Time.fixedDeltaTime);

        //positions_young_p.Insert(positions_young_p.Count, YoungPrefab.transform.position);




        //structInputs = new TypeOfInputs(toTrack.getHorizontal(), toTrack.getCrouch(), jump);
        //jump = false;
        //inputs.Insert(inputs.Count, structInputs);
    }
    */

    private void StartSecondPart()
    {

        inputs = toTrack.getListInputs();
        positions_young_p = toTrack.getPosYoung();


        if (GhostPrefab != null)
        {
            Destroy(GhostPrefab);
        }

        if (OldPrefab != null)
        {
            Destroy(OldPrefab);
        }

        if (YoungPrefab != null)
        {
            Destroy(YoungPrefab);
        }

        if (WorkerPrefab != null)
        {
            Destroy(WorkerPrefab);
        }

        index = 0;

        //Init Ghost
        GhostPrefab = Instantiate(_initGhostPrefab, _initPosYoung, Quaternion.identity);
        rigidbodyGhost = GhostPrefab.GetComponent<Rigidbody2D>();
        rigidbodyGhost.bodyType = RigidbodyType2D.Static;
        //-------------

        //Init Worker
        WorkerPrefab = Instantiate(_initWorker, _initPosYoung, Quaternion.identity);
        //-------------

        //Init Old
        OldPrefab = Instantiate(_initOldPrefab, _initPosOld, Quaternion.identity);
        positions_old_p = new List<Vector3>();


        //Old_Player.transform.position = _initPosOld;

        //Old_Player.SetActive(true);
        //-------------

        //Young_Player.SetActive(false);

    }

    private void RestartOldAndGhost()
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
            OldPrefab.transform.position = positions_old_p[0];
            GameManager.Instance.UpdateGameState(GameState.StartingOldTurn);
            return;
        }
        OldPrefab.transform.position = positions_old_p[index];
    }

    private void MoveGhost()
    {

        if (index < inputs.Count)
        {
            //This part was changed
            //We confront the actual position of the ghost with the previous one of the real
            //index++;

            /*
            if (Vector2.Distance(new Vector2(GhostPrefab.transform.position.x, GhostPrefab.transform.position.y), new Vector2(positions_young_p[index-1].x, positions_young_p[index-1].y)) > tresHold)
            {
                Destroy(GhostPrefab);
                Destroy(WorkerPrefab);
                GameManager.Instance.UpdateGameState(GameState.Paradox);
                return;
            } */

            //Debug.Log("MoveGhost: " + Time.fixedDeltaTime);
            WorkerPrefab.transform.position = new Vector3(positions_young_p[index].x, positions_young_p[index].y, WorkerPrefab.transform.position.z);
            GhostPrefab.GetComponent<Animator>().SetFloat("Speed", Math.Abs(inputs[index].getHorizontal()));
            GhostPrefab.GetComponent<CharacterController2D>().Move(inputs[index].getHorizontal(), inputs[index].getCrouch(), inputs[index].getJump());
            index++;
        }

    }
    private void MoveOnlyGhost()
    {
        //This part was changed
        //We confront the actual position of the ghost with the previous one of the real
        //index++;
        if (index < inputs.Count)
        {
            /*if (Vector2.Distance(new Vector2(GhostPrefab.transform.position.x, GhostPrefab.transform.position.y), new Vector2(positions_young_p[index - 1].x, positions_young_p[index - 1].y)) > tresHold)
            {
                Destroy(GhostPrefab);
                Destroy(WorkerPrefab);
                GameManager.Instance.UpdateGameState(GameState.StartingSecondPart);
                return;
            }*/
            WorkerPrefab.transform.position = new Vector3(positions_young_p[index].x, positions_young_p[index].y, WorkerPrefab.transform.position.z);
            GhostPrefab.GetComponent<Animator>().SetFloat("Speed", Math.Abs(inputs[index].getHorizontal()));
            GhostPrefab.GetComponent<CharacterController2D>().Move(inputs[index].getHorizontal(), inputs[index].getCrouch(), inputs[index].getJump());
            index++;
        }

    }
    private void StartSecondPartTutorial()
    {
        if (GhostPrefab != null)
        {
            Destroy(GhostPrefab);
        }

        if (OldPrefab != null)
        {
            Destroy(OldPrefab);
        }

        if (YoungPrefab != null)
        {
            Destroy(YoungPrefab);
        }
        if (WorkerPrefab != null)
        {
            Destroy(WorkerPrefab);
        }

        //Old_Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        OldPrefab = Instantiate(_initOldPrefab, _initPosOld, Quaternion.identity);
        positions_old_p = new List<Vector3>();


    }
    private void StartThirdPartTutorial()
    {
        inputs = toTrack.getListInputs();
        positions_young_p = toTrack.getPosYoung();

        if (GhostPrefab != null)
        {
            Destroy(GhostPrefab);
        }

        if (OldPrefab != null)
        {
            Destroy(OldPrefab);
        }

        if (YoungPrefab != null)
        {
            Destroy(YoungPrefab);
        }
        if (WorkerPrefab != null)
        {
            Destroy(WorkerPrefab);
        }


        index = 0;
        GhostPrefab = Instantiate(_initGhostPrefab, _initPosYoung, Quaternion.identity);
        //rigidbodyGhost = GhostPrefab.GetComponent<Rigidbody2D>();
        //rigidbodyGhost.bodyType = RigidbodyType2D.Static;
        WorkerPrefab = Instantiate(_initWorker, _initPosYoung, Quaternion.identity);

    }

}


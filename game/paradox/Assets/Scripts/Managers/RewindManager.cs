using System.Collections.Generic;
using UnityEngine;
public class RewindManager : MonoBehaviour
{
    public GameObject GhostPrefab;
    private GameObject _initGhostPrefab;

    public GameObject Old_Player;
    private Vector3 _initPosOld;
    public GameObject Young_Player;
    private Vector3 _initPosYoung;
    private List<Vector3> positions_young_p;
    private List<TypeOfInputs> inputs;

    private List<Vector3> positions_old_p;
    private TypeOfInputs structInputs;
    private PlayerMovement toTrack;

    private bool jump = false;

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

        _initPosOld = Old_Player.transform.position;
        _initPosYoung = Young_Player.transform.position;
        _initGhostPrefab = GhostPrefab;
        //GhostPrefab = Instantiate(GhostPrefab, GhostPrefab.transform.position, Quaternion.identity);

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
        else if (state == GameState.StartingSecondPart)
        {
            StartSecondPartTutorial();
        }
        else if (state == GameState.StartingThirdPart)
        {
            StartThirdPartTutorial();
        }
        else if (state == GameState.StartingOldTurn)
        {
            StartSecondPart();
        }
        else if (state == GameState.Paradox)
        {
            if (GameManager.Instance.PreviousGameState == GameState.OldPlayerTurn)
            {
                _reloadSpeed = index / parameterReload;
                Old_Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }

    private void Init()
    {
        //GhostPrefab.SetActive(false);
        

        Old_Player.SetActive(false);
        Old_Player.transform.position = _initPosOld;
        Young_Player.SetActive(true);
        Young_Player.transform.position = _initPosYoung;
        
        GhostPrefab.SetActive(false);

        positions_young_p = new List<Vector3>();
        inputs = new List<TypeOfInputs>();

        positions_old_p = new List<Vector3>();

        parameterReload = 40;
        index = 0;
    }

    private void Start()
    {
        toTrack = Young_Player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        jump = toTrack.getJump();
    }


    private void FixedUpdate()
    {
        if (GameManager.Instance.State == GameState.YoungPlayerTurn)
        {
            Record();
        }
        else if (GameManager.Instance.State == GameState.OldPlayerTurn)
        {
            positions_old_p.Insert(positions_old_p.Count, Old_Player.transform.position);
            MoveGhost();
        }
        else if (GameManager.Instance.State == GameState.ThirdPart)
        {
            MoveOnlyGhost();
        }
        else if (GameManager.Instance.State == GameState.Paradox)
        {
            RestartOldAndGhost();
        }
    }

    void Record()
    {
        positions_young_p.Insert(positions_young_p.Count, Young_Player.transform.position);

        structInputs = new TypeOfInputs(toTrack.getHorizontal(), toTrack.getCrouch(), jump);
        jump = false;
        inputs.Insert(inputs.Count, structInputs);
    }

    private void StartSecondPart()
    {
        index = 0;
        if(GhostPrefab)
            Destroy(GhostPrefab);
        //GhostPrefab.transform.position = positions_young_p[0];
        GhostPrefab = Instantiate(_initGhostPrefab, positions_young_p[0], Quaternion.identity);
        GhostPrefab.SetActive(true);

        positions_old_p = new List<Vector3>();
        Old_Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Old_Player.transform.position = _initPosOld;

        Old_Player.SetActive(true);
        Young_Player.SetActive(false);
        
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
            Old_Player.transform.position = positions_old_p[0];
            GameManager.Instance.UpdateGameState(GameState.StartingOldTurn);
            return;
        }
        Old_Player.transform.position = positions_old_p[index];
        GhostPrefab.SetActive(false);
    }

    private void MoveGhost()
    {

        if (index < inputs.Count)
        {
            if (Vector2.Distance(new Vector2(GhostPrefab.transform.position.x, GhostPrefab.transform.position.y), new Vector2(positions_young_p[index].x, positions_young_p[index].y)) > tresHold)
            {
                Destroy(GhostPrefab);
                GameManager.Instance.UpdateGameState(GameState.Paradox);
                return;
            }
            GhostPrefab.GetComponent<CharacterController2D>().Move(inputs[index].getHorizontal() * Time.fixedDeltaTime, inputs[index].getCrouch(), inputs[index].getJump());
            index++;
        }
        
    }
    private void MoveOnlyGhost()
    {

        if (index < inputs.Count)
        {
            if (Vector2.Distance(new Vector2(GhostPrefab.transform.position.x, GhostPrefab.transform.position.y), new Vector2(positions_young_p[index].x, positions_young_p[index].y)) > tresHold)
            {
                Destroy(GhostPrefab);
                GameManager.Instance.UpdateGameState(GameState.StartingSecondPart);
                return;
            }
            GhostPrefab.GetComponent<CharacterController2D>().Move(inputs[index].getHorizontal() * Time.fixedDeltaTime, inputs[index].getCrouch(), inputs[index].getJump());
            index++;
        }
        
    }
    private void StartSecondPartTutorial()
    {

        Old_Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Old_Player.transform.position = _initPosOld;
        Old_Player.SetActive(true);
        Young_Player.SetActive(false);
    }
    private void StartThirdPartTutorial()
    {
        index = 0;
        GhostPrefab = Instantiate(_initGhostPrefab, positions_young_p[0], Quaternion.identity);

        Old_Player.SetActive(false);
        GhostPrefab.SetActive(true);
        Young_Player.SetActive(false);
        
    }

}


using TMPro;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    private int youngCollectable = 0;
    private int ghostCollectable = 0;

    private GameObject[] collectables;
    private int _numberToCollect;
    private GameObject _door;
    private CollisionCheckEndLevel _doorActivate;
    public TMP_Text countText;
    private GameObject _chipCounter;

    private void Awake()
    {
        //It is subscribing to the event
        _door = GameObject.FindGameObjectWithTag("EndLevel");
        _doorActivate = _door.GetComponent<CollisionCheckEndLevel>();
        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        _numberToCollect = collectables.Length;
        _chipCounter = GameObject.Find("ChipCounter");
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy()
    {
        //It is unsubscribing to the event
        GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameState state)
    {
        if (state == GameState.StartingOldTurn || state == GameState.StartingYoungTurn)
        {
            youngCollectable = 0;
            ghostCollectable = 0;
            SetActiveCollectables();
            if (_numberToCollect == 0)
            {
                _doorActivate.ActivateDoor();
            }
            else
            {
                _doorActivate.DisableDoor();
            }

            if (state == GameState.StartingOldTurn)
            {
               countText.text = "";
            }
            else
            {
                countText.text = "0/" + _numberToCollect;
            }
        }

        if (state == GameState.StartingOldTurn || state == GameState.StartingThirdPart)
        {
            _chipCounter.SetActive(false);
        }
    }
    public void Start()
    {
        if (_numberToCollect == 0)
        {
            _doorActivate.ActivateDoor();
        }

    }


    public void SetActiveCollectables()
    {
        for (int i = 0; i < _numberToCollect; i++)
        {
            collectables[i].SetActive(true);
        }
    }

    public void AddCollectableCount(string tag)
    {
        if (tag == "Young")
        {
            youngCollectable = youngCollectable + 1;
            SetCountText();
        }
        else if (tag == "Ghost")
        {
            ghostCollectable = ghostCollectable + 1;
        }

        if (youngCollectable == _numberToCollect || ghostCollectable == _numberToCollect)
        {
            _doorActivate.ActivateDoor();
        }
    }

    void SetCountText ()
    {
        countText.text = youngCollectable + "/" + _numberToCollect;
    }

    public int GetYoungCollectableCount()
    {
        return youngCollectable;
    }

    public int GetGhostCollectableCount()
    {
        return ghostCollectable;
    }

    public void ResetCollectableCount()
    {
        ghostCollectable = 0;
        youngCollectable = 0;
    }

    public int GetNumberToCollect()
    {
        return _numberToCollect;
    }
}

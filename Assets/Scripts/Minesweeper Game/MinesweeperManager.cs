using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MinesweeperManager : MonoBehaviour
{
    public static MinesweeperManager Instance;
    [SerializeField] GameObject _bombPrefab;
    [SerializeField] MSSlot[] _slots;
    [SerializeField] GameObject _winCanvas;
    [SerializeField] GameObject _loseCanvas;

    bool _isWin;
    int _revealedSlots;
    int _bombAmount;
    List<Vector2Int> _bombPoses;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SpawnBombs();
    }

    public void ContinueButton()
    {
        foreach (var slot in _slots)
        {
            slot.SlotReset();
        }
        _revealedSlots = 0;
        _winCanvas.SetActive(false);
        _loseCanvas.SetActive(false);
        SpawnBombs();
    }
    public void SlotReveal(MSSlot slot)
    {
        slot.SlotReveal();
        _revealedSlots++;
        print(_revealedSlots + " | " + _slots.Length + " " + _bombAmount);
        foreach (var bombPos in _bombPoses)
        {
            if (slot.Position == bombPos)
            {
               _isWin = false;
                StartCoroutine(IAllBombsReveal());

            }
        }
        if(_revealedSlots >= _slots.Length - _bombAmount)
        {
            _isWin = true;
            StartCoroutine(IAllBombsReveal());
        }

    }

    IEnumerator IAllBombsReveal()
    {
        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < _bombPoses.Count; i++)
        {
            foreach (var slot in _slots)
            {
                if (slot.Position == _bombPoses[i])
                {
                    slot.SlotReveal();
                    break;
                }
            }

            yield return new WaitForSeconds(.5f);

        }
        if (_isWin)
        {
            _winCanvas.SetActive(true);
            MoneyManager.Instance.AddMoney(200);
        }
        else
        {
            _loseCanvas.SetActive(true);
        }
        yield return null;
    }

    void SpawnBombs()
    {
        _bombAmount = Random.Range(2, 4);
        _bombPoses = new List<Vector2Int>();

        for (int i = 0; i < _bombAmount; i++)
        {
            StartCoroutine(ISpawnBomb());
        }
        SetNumbers();
    }

    IEnumerator ISpawnBomb()
    {
        var bombPos = new Vector2Int(Random.Range(1, 6), Random.Range(1, 6));
        bool duplicate = false;
        foreach (var bomb in _bombPoses)
        {
            if (bombPos == bomb)
            {
                duplicate = true;
            }
        }

        if (duplicate)
        {
            StartCoroutine(ISpawnBomb());
        }
        else
        {
            foreach (var slot in _slots)
            {
                if (slot.Position == bombPos)
                {
                    _bombPoses.Add(bombPos);
                    slot.EnableBomb();
                    //Instantiate(_bombPrefab, slot.gameObject.transform);
                }
            }
        }
        yield return null;
    }

    void SetNumbers()
    {
        foreach(var bomb in _bombPoses)
        {
            Vector2Int startPos = new Vector2Int(bomb.x - 1, bomb.y - 1);
            Vector2Int currentPos = startPos;

            //Set numbers around bomb
            for (int i = 1; i <= 3; i++)
            {
                //Check if row is in bound
                if (currentPos.x <= 0 || currentPos.x > 5)
                {
                    currentPos.x++;
                    continue;
                }
                currentPos.y = startPos.y;

                for (int j = 1; j <= 3; j++)
                {
                    //bool duplicate = false;
                    ////Check if there is no bomb spawned in a slot
                    //foreach ( var bPos in bombPoses)
                    //{
                    //    print("times");
                    //    if (currentPos == bPos)
                    //    {
                    //        duplicate = true;
                    //        break;
                    //    }
                    //}

                    //Check if column is in bound
                    if (currentPos.y <= 0 || currentPos.y > 5)
                    {
                        currentPos.y++;
                        continue;
                    }

                    //Add number to the slot
                    foreach (var slot in _slots)
                    {
                        if (slot.Position == currentPos)
                        {
                            slot.AddNumber();
                        }
                    }
                    currentPos.y++;
                }
                currentPos.x++;
            }
        }
    }
}

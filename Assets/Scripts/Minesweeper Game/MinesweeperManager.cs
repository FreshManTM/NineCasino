using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MinesweeperManager : MonoBehaviour
{
    [SerializeField] GameObject _bombPrefab;

    [SerializeField] MSSlot[] _slots;
    int bombAmount;
    List<Vector2Int> bombPoses;
    void Start()
    {
        SpawnBombs();
    }

    void SpawnBombs()
    {
        bombAmount = Random.Range(2, 4);
        bombPoses = new List<Vector2Int>();

        for (int i = 0; i < bombAmount; i++)
        {
            StartCoroutine(SpawnBomb());
        }
        SetNumbers();
    }

    IEnumerator SpawnBomb()
    {
        var bombPos = new Vector2Int(Random.Range(1, 6), Random.Range(1, 6));
        bool duplicate = false;
        foreach (var bomb in bombPoses)
        {
            if (bombPos == bomb)
            {
                duplicate = true;
            }
        }

        if (duplicate)
        {
            StartCoroutine(SpawnBomb());
        }
        else
        {
            foreach (var slot in _slots)
            {
                if (slot.Position == bombPos)
                {
                    bombPoses.Add(bombPos);
                    Instantiate(_bombPrefab, slot.gameObject.transform);
                }
            }
        }
        yield return null;
    }

    void SetNumbers()
    {
        foreach(var bomb in bombPoses)
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
                    bool duplicate = false;
                    //Check if there is no bomb spawned in a slot
                    foreach ( var bPos in bombPoses)
                    {
                        print("times");
                        if (currentPos == bPos)
                        {
                            duplicate = true;
                            break;
                        }
                    }

                    //Check if column is in bound
                    if (currentPos.y <= 0 || currentPos.y > 5 || duplicate)
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

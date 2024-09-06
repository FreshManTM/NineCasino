using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeSlot : MonoBehaviour, IDropHandler
{
    GameObject dropped;
    GameObject child;
    Item item;
    MergeGameManager _gameManager;

    private void Start()
    {
        _gameManager = MergeGameManager.Instance;
    }
    public void OnDrop(PointerEventData eventData)
    {
        dropped = eventData.pointerDrag;
        item = dropped.GetComponent<Item>();
        if (transform.childCount == 0)
        {            
            item.ParentAfterDrag = transform;
        }
        else
        {
            child = transform.GetChild(0).gameObject;

            if (dropped.tag == child.tag)
            {
                MergeItems();
            }
        }
    }

    void MergeItems()
    {
        int prefabNum = 0;
        int scoreAdd = 0;
        switch (child.tag)
        {
            case "Rhombus":
                prefabNum = 1;
                scoreAdd = 20;
                break;
            case "House":
                prefabNum = 2;
                scoreAdd = 30;
                break;
            case "Star":
                prefabNum = 3;
                scoreAdd = 40;
                break;
            case "Diamond":
                prefabNum = 4;
                scoreAdd = 50;
                break;
            case "Cube":
                prefabNum = 5;
                scoreAdd = 60;
                break;
            case "Sphere":
                prefabNum = 5;
                scoreAdd = 70;
                break;
        }
        var newGO = Instantiate(_gameManager.GameItemPrefabs[prefabNum], transform);
        newGO.transform.DOShakeScale(.3f, .3f, 10);
        Destroy(child);
        Destroy(dropped);
        _gameManager.AddScore(scoreAdd);
    }
}

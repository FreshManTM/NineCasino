using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform _tabToMove;
    [SerializeField] Transform[] _tabsTransform;

    private void Start()
    {
        print(Screen.width);
    }

    public void TabMove(int tabNum)
    {
        _tabToMove.transform.DOLocalMoveX(-_tabsTransform[tabNum].localPosition.x, 1f);
        print(_tabToMove.transform.position);
    }
}

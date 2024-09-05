using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform _tabToMove;
    [SerializeField] Transform[] _tabsTransform;

    
    public void TabMove(int tabNum)
    {
        _tabToMove.transform.DOLocalMoveX(-_tabsTransform[tabNum].localPosition.x, 1f);
    }
}

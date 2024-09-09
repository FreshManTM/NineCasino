using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform _tabToMove;
    [SerializeField] Transform[] _tabsTransform;
    [SerializeField] Image[] _tabButtonImages;
    [SerializeField] Color _selectedButtonColor;
    int _prevTabNumber = 0;

    private void Start()
    {
        _tabButtonImages[0].color = _selectedButtonColor;
    }
    public void TabMove(int tabNum)
    {
        //foreach (var tab in _tabsTransform)
        //{
        //    tab.gameObject.SetActive(true);
        //}
        _tabButtonImages[tabNum].color = _selectedButtonColor;
        _tabToMove.transform.DOLocalMoveX(-_tabsTransform[tabNum].localPosition.x, 1f);
        _tabButtonImages[_prevTabNumber].color = Color.white;
        _prevTabNumber = tabNum;
    }

    //void DisableTab(int currentTab)
    //{
    //    foreach (var tab in _tabsTransform)
    //    {
    //        if(tab != _tabsTransform[currentTab])
    //        {
    //            tab.gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            tab.gameObject.SetActive(true);
    //        }
    //    }
        
    //}
}

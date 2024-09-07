using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MSSlot : MonoBehaviour
{
    public Vector2Int Position;
    [SerializeField] GameObject _bombImage;
    [SerializeField] TextMeshProUGUI _numberText;
    [SerializeField] Transform CoverImage;
    int number;

    public void AddNumber()
    {
        number++;
       // _numberText.gameObject.SetActive(true);
        _numberText.text = number.ToString();
    }

    public void EnableBomb()
    {
        _bombImage.SetActive(true);
    }

    public void SlotReveal()
    {
        CoverImage.DOScale(0, .2f);
    }

    public void SlotReset()
    {
        number = 0;
        _numberText.text = number.ToString();
        _bombImage.SetActive(false);
        CoverImage.localScale = Vector3.one;
    }
}

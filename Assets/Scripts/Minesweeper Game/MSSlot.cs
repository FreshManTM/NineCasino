using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MSSlot : MonoBehaviour
{
    public Vector2Int Position;
    [SerializeField] TextMeshProUGUI _numberText;
    int number;

    public void AddNumber()
    {
        number++;
        _numberText.gameObject.SetActive(true);
        _numberText.text = number.ToString();
    }
}

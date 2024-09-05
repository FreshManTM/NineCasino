using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    [SerializeField] TextMeshProUGUI _moneyText; 
    int _money; 
    private void Awake()
    {
        Instance = this;
        _money = PlayerPrefs.GetInt("_money");
        _moneyText.text = _money.ToString();
    }

    public void AddMoney(int money)
    {
        _money += money;
        _moneyText.text = _money.ToString();
        _moneyText.transform.DOShakePosition(.3f, 5, 20);
        PlayerPrefs.SetInt("_money", _money);
    }

}

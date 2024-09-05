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
    }

    public void AddMoney(int money)
    {
        _money += money;
        _moneyText.text = money.ToString();
    }

}

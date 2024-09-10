using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] int _upgradeNumber;
    [SerializeField] GameObject _purchasedBG;
    [SerializeField] TextMeshProUGUI _upgradeNameText;
    [SerializeField] TextMeshProUGUI _upgradeDescriptionText;
    [SerializeField] TextMeshProUGUI _priceText;
    [SerializeField] Button _upgradeButton;
    [SerializeField] AudioSource _upgradeSound;
    CharacterInfo _characterInfo;
    public void SetButton(CharacterInfo character)
    {
        _characterInfo = character;

        _upgradeNameText.text = _characterInfo.UpgradeName[_upgradeNumber];
        _upgradeDescriptionText.text = _characterInfo.UpgradeDescription[_upgradeNumber];
        if(_upgradeNumber != 0)
        {
            if (PlayerPrefs.HasKey(_characterInfo.Name + _upgradeNumber.ToString()))
            {
                SetPurchased();
            }
            else
            {
                _purchasedBG.SetActive(false);
                _upgradeButton.interactable = true;
            }
            _priceText.text = _characterInfo.UpgradePrice[_upgradeNumber - 1].ToString();
        }
    }

    public void Upgrade()
    {
        bool enoughMoney = MoneyManager.Instance.RemoveMoney(_characterInfo.UpgradePrice[_upgradeNumber - 1]);
        if(enoughMoney)
        {
            _upgradeSound.Play();
            SetPurchased();
            PlayerPrefs.SetInt(_characterInfo.Name + _upgradeNumber.ToString(), 1);
        }
    }

    void SetPurchased()
    {
        ProfileManager.Instance.AddUpgradeAmount();
        _purchasedBG.SetActive(true);
        _upgradeButton.interactable = false;
    }
}

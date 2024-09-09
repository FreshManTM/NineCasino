using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    [Header("Upgrades")]
    [SerializeField] CharacterInfo[] _characterInfos;
    [SerializeField] TextMeshProUGUI[] _upgradeNamesText;
    [SerializeField] TextMeshProUGUI[] _upgradeDescriptionsText;
    [SerializeField] TextMeshProUGUI[] _pricesText;
    [Header("Avatars")]
    [SerializeField] Transform[] _avatarImages;
    [SerializeField] TextMeshProUGUI _avatarNameText;
    [SerializeField] TextMeshProUGUI _avatarDescriptionText;
    [SerializeField] GameObject _avatarTabToMove;
    int _currentAvatar;
    int _currentCharacter;

    private void Start()
    {
        SetCharacter();
    }

    void SetCharacter()
    {
        for (int i = 0; i < _upgradeNamesText.Length; i++)
        {
            _upgradeNamesText[i].text = _characterInfos[_currentCharacter].UpgradeName[i];
            _upgradeDescriptionsText[i].text = _characterInfos[_currentCharacter].UpgradeDescription[i];
            if (i != 0)
            {
                _pricesText[i - 1].text = _characterInfos[_currentCharacter].UpgradePrice[i - 1].ToString();
            }
        }
    }

    public void NextButton()
    {
        if (_currentAvatar < _avatarImages.Length - 1)
        {
            _currentAvatar++;
            _avatarTabToMove.transform.DOLocalMoveX(-_avatarImages[_currentAvatar].localPosition.x, .5f).OnComplete(SetAvatarText);
        }
    }

    public void PrevButton()
    {
        if (_currentAvatar > 0)
        {
            _currentAvatar--;
            _avatarTabToMove.transform.DOLocalMoveX(-_avatarImages[_currentAvatar].localPosition.x, .5f).OnComplete(SetAvatarText);
        }
    }

    public void ChooseButton()
    {
        _currentCharacter = _currentAvatar;
        SetCharacter();
    }

    void SetAvatarText()
    {
        _avatarNameText.text = _characterInfos[_currentAvatar].Name;
        _avatarDescriptionText.text = _characterInfos[_currentAvatar].Description;
    }
}

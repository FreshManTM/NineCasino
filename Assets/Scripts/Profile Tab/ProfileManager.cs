using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager Instance;
    [Header("Character")]
    [SerializeField] CharacterInfo[] _characterInfos;
    [SerializeField] TextMeshProUGUI _characterNameText;
    [SerializeField] TextMeshProUGUI _characterUpgradesText;
    [SerializeField] Slider _sliderPB;
    [SerializeField] Image _profileImage;
    [SerializeField] UpgradeButton[] _upgradeButtons;
    [Header("Avatars")]
    [SerializeField] Transform[] _avatarImages;
    [SerializeField] TextMeshProUGUI _avatarNameText;
    [SerializeField] TextMeshProUGUI _avatarDescriptionText;
    [SerializeField] GameObject _avatarTabToMove;

    int UpgradesAmount;
    int _currentAvatar;
    int _currentCharacter;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _currentCharacter = PlayerPrefs.GetInt("_currentCharacter");
        SetCharacter();
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
        PlayerPrefs.SetInt("_currentCharacter", _currentCharacter);
        SetCharacter();
    }

    public void AddUpgradeAmount()
    {
        UpgradesAmount++;
        _characterUpgradesText.text = $"{UpgradesAmount}/{_upgradeButtons.Length}";
        _sliderPB.value = UpgradesAmount;
    }

    void SetCharacter()
    {
        UpgradesAmount = 1;

        _characterNameText.text = _characterInfos[_currentCharacter].Name;
        _profileImage.sprite = _avatarImages[_currentCharacter].GetComponent<Image>().sprite;
        for (int i = 0; i < _upgradeButtons.Length; i++)
        {
            _upgradeButtons[i].SetButton(_characterInfos[_currentCharacter]);
        }
        _characterUpgradesText.text = $"{UpgradesAmount}/{_upgradeButtons.Length}";
        _sliderPB.value = UpgradesAmount;
    }

    void SetAvatarText()
    {
        _avatarNameText.text = _characterInfos[_currentAvatar].Name;
        _avatarDescriptionText.text = _characterInfos[_currentAvatar].Description;
    }

}

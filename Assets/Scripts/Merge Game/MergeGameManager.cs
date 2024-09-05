using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeGameManager : MonoBehaviour
{
    public static MergeGameManager Instance;
    public GameObject[] GameItemPrefabs;

    [SerializeField] GameObject[] _slots;
    [SerializeField] TextMeshProUGUI _cScore_Text;
    [SerializeField] TextMeshProUGUI _tScore_Text;
    List<Transform> _slotsList = new List<Transform>();
    int _currentScore, _targetScore;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SpawnStartItems();
        if (PlayerPrefs.HasKey("_targetScore"))
            _targetScore = PlayerPrefs.GetInt("_targetScore");
        else
            _targetScore = 100;     //default value for the first game load

        _tScore_Text.text = _targetScore.ToString();
    }

    public void SpawnItem(GameObject spawnItem)
    {
        _slotsList.Clear();
        StartCoroutine(FindFreeSlot(spawnItem));
    }

    IEnumerator FindFreeSlot(GameObject spawnItem)
    {
        if(_slotsList.Count < _slots.Length)
        {
            Transform slotToSpawn = _slots[Random.Range(0, _slots.Length)].transform; ;
            if (slotToSpawn.childCount != 0)
            {
                if(!_slotsList.Contains(slotToSpawn))
                    _slotsList.Add(slotToSpawn);
                StartCoroutine(FindFreeSlot(spawnItem));
            }
            else
            {
                Instantiate(spawnItem, slotToSpawn);
            }
        }
        yield return null;
    }

    void SpawnStartItems()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnItem(GameItemPrefabs[Random.Range(0, 3)]);
        }
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        if (_currentScore >= _targetScore)
        {
            Win();
        }
        _cScore_Text.text = _currentScore.ToString();
    }

    void Win()
    {
        _currentScore = 0;
        _targetScore += 100;
        _tScore_Text.text = _targetScore.ToString();
        PlayerPrefs.SetInt("_targetScore", _targetScore);
        MoneyManager.Instance.AddMoney(200);

        foreach (GameObject slot in _slots)
        {
            if (slot.transform.childCount > 0) 
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }

        SpawnStartItems();
    }
}

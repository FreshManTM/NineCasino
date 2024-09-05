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
        for (int i = 0; i < 3; i++)
        {
            SpawnItem(GameItemPrefabs[Random.Range(0, 3)]);
        }
        _targetScore = 100;
        _tScore_Text.text = _tScore_Text.text;
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

    public void AddScore(int score)
    {
        _currentScore += score;
        _cScore_Text.text = _currentScore.ToString();
        if (_currentScore >= _targetScore)
        {
            _currentScore = _targetScore;
            MoneyManager.Instance.AddMoney(200);
        }
    }
}

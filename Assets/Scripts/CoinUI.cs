using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUI : MonoBehaviour {


    private PlayerStats _playerStats;

    private TMP_Text _coinCount;
   

    void Start()
    {
        _playerStats = FindAnyObjectByType<PlayerStats>();
        _coinCount = GetComponent<TMP_Text>();
        _coinCount.text = _playerStats.CoinAmount.ToString();
    }

    void Update()
    {
        _coinCount.text = _playerStats.CoinAmount.ToString();
    }
}

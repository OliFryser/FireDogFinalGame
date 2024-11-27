using UnityEngine;
using System.Collections.Generic;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _heartPrefab;
    private List<HeartUI> _hearts = new List<HeartUI>();
    private PlayerStats _playerStats;

    void Start()
    {
        _playerStats = FindAnyObjectByType<PlayerStats>();
        CreateHearts();
    }

    public void CreateHearts()
    {
        _hearts.ForEach(h => Destroy(h.gameObject));
        _hearts.Clear();

        int numHearts = Mathf.CeilToInt(_playerStats.MaxHealth / 2f);

        for (int i = 0; i < numHearts; i++)
        {
            GameObject heartObj = Instantiate(_heartPrefab, transform);
            HeartUI heart = heartObj.GetComponent<HeartUI>();
            _hearts.Add(heart);
        }

        UpdateHearts(_playerStats.CurrentHealth);
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (currentHealth >= 2)
            {
                _hearts[i].SetFullHeart();
                currentHealth -= 2;
            }
            else if (currentHealth == 1)
            {
                _hearts[i].SetHalfHeart();
                currentHealth -= 1;
            }
            else
            {
                _hearts[i].SetEmptyHeart();
            }
        }
    }
}

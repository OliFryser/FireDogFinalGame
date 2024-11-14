using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUIManager : MonoBehaviour
{
    public GameObject heartPrefab; // Assign the HeartUI prefab here
    private List<HeartUI> hearts = new List<HeartUI>();
    private PlayerStats _playerStats;

    void Start()
    {
        // Find the PlayerStats component
        _playerStats = FindAnyObjectByType<PlayerStats>();

        // Create hearts based on player's max health
        CreateHearts();
        UpdateHearts(_playerStats.GetCurrentHealth());
    }

    void CreateHearts()
    {
        int numHearts = Mathf.CeilToInt(_playerStats.MaxHealth / 2f);

        for (int i = 0; i < numHearts; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, transform);
            HeartUI heart = heartObj.GetComponent<HeartUI>();
            hearts.Add(heart);
        }
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (currentHealth >= 2)
            {
                hearts[i].SetFullHeart();
                currentHealth -= 2;
            }
            else if (currentHealth == 1)
            {
                hearts[i].SetHalfHeart();
                currentHealth -= 1;
            }
            else
            {
                hearts[i].SetEmptyHeart();
            }
        }
    }
}

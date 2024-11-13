using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUIManager : MonoBehaviour
{
    public GameObject heartPrefab; // Assign the HeartUI prefab here
    private List<HeartUI> hearts = new List<HeartUI>();
    private PlayerStats playerStats;

    void Start()
    {
        // Find the PlayerStats component
        playerStats = FindAnyObjectByType<PlayerStats>();

        // Create hearts based on player's max health
        CreateHearts();
        UpdateHearts();
    }

    void CreateHearts()
    {
        int numHearts = Mathf.CeilToInt(playerStats.MaxHealth / 2f);

        for (int i = 0; i < numHearts; i++)
        {
            GameObject heartObj = Instantiate(heartPrefab, transform);
            HeartUI heart = heartObj.GetComponent<HeartUI>();
            hearts.Add(heart);
        }
    }

    public void UpdateHearts()
    {
        int hp = playerStats.CurrentHealth;

        for (int i = 0; i < hearts.Count; i++)
        {
            if (hp >= 2)
            {
                hearts[i].SetFullHeart();
                hp -= 2;
            }
            else if (hp == 1)
            {
                hearts[i].SetHalfHeart();
                hp -= 1;
            }
            else
            {
                hearts[i].SetEmptyHeart();
            }
        }
    }
}

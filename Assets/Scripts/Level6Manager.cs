using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level6 : MonoBehaviour
{
    [Header("Music Controller")]
    [SerializeField]
    private MusicController musicController;

    [Header("Room Manager")]
    [SerializeField]
    private RoomManager roomManager;

    [Header("Cleanup Manager")]
    [SerializeField]
    private CleanupManager cleanupManager;

    [Header("Door to Next Level")]
    [SerializeField]
    private Door doorToNextLevel;

    [Header("Lamp Sprites and Enemies")]
    [SerializeField]
    private List<GameObject> lampOffSprites;

    [SerializeField]
    private List<GameObject> lampEnemies;

    [Header("Decoy Enemy")]
    [SerializeField]
    private GameObject decoyEnemy;

    [Header("Interaction Settings")]
    [SerializeField]
    private float interactionDistance = 2f;

    [SerializeField]
    private KeyCode interactionKey = KeyCode.E;

    private bool hasInteracted = false;
    private int remainingEnemies;

    private void Awake()
    {
        if (musicController != null)
        {
            musicController.SetBattleState(0.5f);
        }
    }

    private void Start()
    {
        remainingEnemies = lampEnemies.Count;

        foreach (GameObject enemy in lampEnemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }

        foreach (GameObject offSprite in lampOffSprites)
        {
            if (offSprite != null)
            {
                offSprite.SetActive(true);
            }
        }

        if (decoyEnemy != null)
        {
            decoyEnemy.SetActive(true);
        }
    }

    private void Update()
    {
        if (!hasInteracted && IsPlayerInRange() && Input.GetKeyDown(interactionKey))
        {
            hasInteracted = true;
            ActivateLamps();
        }
    }

    private bool IsPlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance <= interactionDistance;
        }
        return false;
    }

    private void ActivateLamps()
    {
        for (int i = 0; i < lampOffSprites.Count; i++)
        {
            if (i < lampOffSprites.Count && lampOffSprites[i] != null)
            {
                lampOffSprites[i].SetActive(false);
            }

            if (i < lampEnemies.Count && lampEnemies[i] != null)
            {
                lampEnemies[i].SetActive(true);
            }
        }

        if (decoyEnemy != null)
        {
            decoyEnemy.SetActive(false);
        }

        if (musicController != null)
        {
            musicController.SetBattleState(0.0f);
        }
    }

    public void OnPlayerDeath()
    {
        if (musicController != null)
        {
            musicController.ResetBattleState();
        }
    }

    private void HandleLampDeath()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
        {
            OpenDoorToNextLevel();
        }
    }

    private void OpenDoorToNextLevel()
    {
        if (doorToNextLevel != null)
        {
            doorToNextLevel.OpenDoor();
        }

        if (musicController != null)
        {
            musicController.ClearRoom();
        }

        if (roomManager != null)
        {
            roomManager.ClearRoom();
        }
    }

    private void OnDestroy()
    {
        if (musicController != null)
        {
            musicController.ResetBattleState();
        }
    }
}

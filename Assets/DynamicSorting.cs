using UnityEngine;

public class DynamicSorting : MonoBehaviour
{
    public SpriteRenderer lampSpriteRenderer; // Reference to the lamp's SpriteRenderer
    private Transform playerTransform;         // Reference to the player's transform
    private SpriteRenderer playerSpriteRenderer; // Reference to the player's SpriteRenderer

    void Start()
    {
        // Dynamically find the player in the scene by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            playerSpriteRenderer = playerObject.GetComponent<SpriteRenderer>();

            if (playerSpriteRenderer == null)
            {
                Debug.LogError("Player GameObject does not have a SpriteRenderer component.");
            }
        }
        else
        {
            Debug.LogError("Player not found in the scene. Ensure the player has the 'Player' tag.");
        }

        // Validate lampSpriteRenderer
        if (lampSpriteRenderer == null)
        {
            Debug.LogError("Lamp SpriteRenderer is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (playerTransform != null && playerSpriteRenderer != null && lampSpriteRenderer != null)
        {
            // Compare Y positions
            if (playerTransform.position.y < transform.position.y)
            {
                // Player is behind the lamp
                playerSpriteRenderer.sortingOrder = lampSpriteRenderer.sortingOrder - 1;
            }
            else
            {
                // Player is in front of the lamp
                playerSpriteRenderer.sortingOrder = lampSpriteRenderer.sortingOrder + 1;
            }
        }
    }
}

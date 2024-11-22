using UnityEngine;
using Unity.Cinemachine;

public class PlayerCameraFollow : MonoBehaviour
{
    public string playerTag = "Player"; // The tag used for the player
    private CinemachineCamera virtualCamera;

    void Start()
    {
        // Get the Cinemachine Virtual Camera component
        virtualCamera = GetComponent<CinemachineCamera>();

        // Look for the player after a slight delay (to ensure it's spawned)
        StartCoroutine(FindPlayer());
    }

    System.Collections.IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(0.01f); // Small delay to ensure player is spawned

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            virtualCamera.Follow = player.transform; // Set the player as the Follow target
        }
        else
        {
            Debug.LogWarning("Player not found in the scene!");
        }
    }
}


using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public string playerTag = "Player"; // Tag assigned to the player
    public Collider2D bounds;          // The bounds collider

    private Transform target;          // Player's transform
    private Camera cam;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        cam = Camera.main;

        // Calculate the camera's dimensions in world units
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        // Find the player by tag
        FindPlayer();
    }

    void LateUpdate()
    {
        if (bounds == null || target == null)
        {
            if (target == null)
                FindPlayer(); // Try to find the player again if null
            return;
        }

        // Get the bounds of the collider
        Bounds b = bounds.bounds;

        // Clamp calculations
        float clampedX = Mathf.Clamp(target.position.x, b.min.x + halfWidth, b.max.x - halfWidth);
        float clampedY = Mathf.Clamp(target.position.y, b.min.y + halfHeight, b.max.y - halfHeight);

        // Force clamping to ensure no overshoot
        clampedX = Mathf.Max(b.min.x + halfWidth, Mathf.Min(clampedX, b.max.x - halfWidth));
        clampedY = Mathf.Max(b.min.y + halfHeight, Mathf.Min(clampedY, b.max.y - halfHeight));

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void FindPlayer()
    {
        // Find the player in the scene by tag
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            target = player.transform;
        }
    }
}

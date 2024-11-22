using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractListener : MonoBehaviour
{
    void OnInteract(InputValue _)
    {
        var closestInteractable =
            FindObjectsByType<Interactable>(FindObjectsSortMode.None)
                .Where(i => i.IsInteractable)
                .OrderByDescending(i => Vector3.Distance(transform.position, i.transform.position))
                .FirstOrDefault();

        if (closestInteractable is not null)
        {
            closestInteractable.Interact();
        }
    }
}

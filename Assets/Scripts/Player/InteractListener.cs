using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractListener : MonoBehaviour
{
    private Interactable _closestInteractable;

    void Update()
    {
        var newClosestInteractable =
            FindObjectsByType<Interactable>(FindObjectsSortMode.None)
                .Where(i => i.IsInteractable)
                .OrderBy(i => Vector3.Distance(transform.position, i.transform.position))
                .FirstOrDefault();

        if (_closestInteractable != newClosestInteractable)
        {
            _closestInteractable?.UnHighlight();
            newClosestInteractable?.Highlight();
            _closestInteractable = newClosestInteractable;
        }
    }

    void OnInteract(InputValue _)
    {
        _closestInteractable?.Interact();
        _closestInteractable?.UnHighlight();
        _closestInteractable = null;
    }
}

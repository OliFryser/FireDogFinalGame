using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool IsInteractable;
    public abstract void Interact();
}

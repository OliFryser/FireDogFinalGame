using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected float _interactDistance = 1.5f;
    public bool IsInteractable;
    protected bool _hasInteracted;
    protected Transform _playerTransform;

    public abstract void Highlight();
    public abstract void UnHighlight();

    public virtual void Interact()
    {
        _hasInteracted = true;
        IsInteractable = false;
    }

    protected virtual void Start()
    {
        _playerTransform = FindAnyObjectByType<Movement>().transform;
    }

    protected virtual void Update()
    {
        if (_hasInteracted)
            return;

        float distance = Vector3.Distance(_playerTransform.position, transform.position);
        IsInteractable = distance <= _interactDistance;
    }
}

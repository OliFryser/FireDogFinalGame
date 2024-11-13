using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool _isOpen = false;

    [SerializeField]
    private Sprite _openDoor;

    [SerializeField]
    private Sprite _closedDoor;

    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private BoxCollider2D _closedDoorCollider;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_isOpen)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        _closedDoorCollider.gameObject.SetActive(false);
        _spriteRenderer.sprite = _openDoor;
    }
}

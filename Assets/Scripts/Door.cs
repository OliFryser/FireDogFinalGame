using UnityEngine;
using FMODUnity;

public class Door : MonoBehaviour
{
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

    public void OpenDoor()
    {
        _closedDoorCollider.gameObject.SetActive(false);
        _spriteRenderer.sprite = _openDoor;
        RuntimeManager.PlayOneShot("event:/Environment/Door Unlock");
    }
}

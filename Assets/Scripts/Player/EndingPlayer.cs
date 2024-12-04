using UnityEngine;

public class EndingPlayer : MonoBehaviour
{
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Horizontal", 1);
        _animator.SetFloat("Idle Speed", 1);
    }
}

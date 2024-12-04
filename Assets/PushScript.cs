using UnityEngine;

public class PushScript : MonoBehaviour
{

    [SerializeField]
    protected float _pushBackOnPlayerHit;


    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<EnemyMovement>().GetPushedBack(_pushBackOnPlayerHit, 0);
        }
    }
}

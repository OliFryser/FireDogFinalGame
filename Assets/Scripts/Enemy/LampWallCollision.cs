using UnityEngine;

public class LampWallCollision : MonoBehaviour
{
    private PlayerStats _playerStats;

    void Start(){
        _playerStats = FindAnyObjectByType<PlayerStats>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnCollisionEnter2D(Collision2D other) {
        if (_playerStats.BaseballConnoisseur){
            transform.parent.GetComponent<LampHitDetection>().WallCollisionDetected(other);
        }
    }
}

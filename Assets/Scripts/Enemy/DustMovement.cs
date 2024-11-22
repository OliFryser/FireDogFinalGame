using System.Runtime.CompilerServices;
using UnityEngine;

public class DustMovement : EnemyMovement
{
    [Header("Randomized Speed")]
    [SerializeField]
    private float _minSpeed = 3.0f;
    [SerializeField]
    private float _maxSpeed = 5.0f;
    protected override void Start()
    {
        _movementSpeed = Random.Range(_minSpeed, _maxSpeed);
        base.Start();
    }

}

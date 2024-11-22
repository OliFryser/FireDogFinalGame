using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class SpawnDustEnemy : EnemyHitDetection
{

    [SerializeField]
    private GameObject _dustEnemy;


    protected override void GetHitLightAttack(){
        base.GetHitLightAttack();
        Instantiate(_dustEnemy, transform.position, quaternion.identity);
        Instantiate(_dustEnemy, transform.position, quaternion.identity);

    }

    protected override void GetHitHeavyAttack(){
        base.GetHitHeavyAttack();
        Instantiate(_dustEnemy, transform.position, quaternion.identity);
        Instantiate(_dustEnemy, transform.position, quaternion.identity);
    }

}

using UnityEngine;
using System.Collections;

public class Coins : MonoBehaviour

{
    private PlayerStats _playerStats;

    void Start()
    {
        _playerStats = FindAnyObjectByType<PlayerStats>();
        StartCoroutine(PickupTimer(1.0f));
    }


    IEnumerator PickupTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        _playerStats.AddCoins(1);
        //TO DO animation for showing coins added to player
        Destroy(gameObject);
    }

}

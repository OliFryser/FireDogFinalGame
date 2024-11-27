using UnityEngine;

public class PersistentPlayerStats : MonoBehaviour
{
    private int _coins;
    public int Coins => _coins;

    public void AddCoins(int amount)
    {
        _coins += amount;
    }

    public void SpendCoins(int amount)
    {
        _coins -= amount;
    }
}

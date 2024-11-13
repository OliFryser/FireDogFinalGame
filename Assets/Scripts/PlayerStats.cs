using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float MovementSpeed = 50.0f;
    public int MaxHealth = 6;
    public float Damage = 10.0f;
    public int CurrentHealth = 6;
    public float EnemyStunDuration;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Reset()
    {
        MovementSpeed = 50.0f;
        MaxHealth = 6;
        Damage = 10.0f;
        CurrentHealth = 6;
        EnemyStunDuration = 1.0f;
    }
}

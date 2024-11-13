using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float MovementSpeed = 50.0f;
    public int MaxHealth = 6;
    public float Damage = 5.0f;
    public int CurrentHealth = 6;


    public void Reset(){
        MovementSpeed = 50.0f;
        MaxHealth = 6;
        Damage = 5.0f;
        CurrentHealth = 6;
    }
}

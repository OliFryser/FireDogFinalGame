using UnityEngine;

public enum HubUpgradeType
{
    HealthBoost,
    DamageBoost,
    SpeedBoost
}

[CreateAssetMenu(fileName = "HubUpgrade", menuName = "Scriptable Objects/HubUpgrade")]
public class HubUpgrade : ScriptableObject
{
    public string Title;
    public string Description;
    public int Cost;

}

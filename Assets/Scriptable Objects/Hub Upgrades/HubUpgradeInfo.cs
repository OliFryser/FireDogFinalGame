using UnityEngine;

public enum HubUpgradeType
{
    FlashLightRadius,
    HealthGain,
    CriticalAttack,
    BetterCleaningLoot
}

[CreateAssetMenu(fileName = "HubUpgradeInfo", menuName = "Scriptable Objects/HubUpgradeInfo")]
public class HubUpgradeInfo : ScriptableObject
{
    public Sprite Icon;
    public string Title;
    public string Description;
    public int BaseCost;
    public int IncrementPerTier;
    public int MaxTier;
    public HubUpgradeType HubUpgradeType;

}

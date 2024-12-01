using UnityEngine;

public enum UpgradeType
{
  HealthBoost,
  DamageLightBoost,
  DamageHeavyBoost,
  SpeedBoost,
  StackingDamageBoost,
  CritChanceBoost,
  LargeCritBoost,
  CleaningSpreeMoney,
  CleaningSpreeDamage,
  HeavyMetal,
  BowlingChampion,
  BaseballConnoisseur

}

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
  public string Title;
  public string Description;
  public UpgradeType UpgradeType;
  // TODO add more things, like sprite, description, percentage etc.
}

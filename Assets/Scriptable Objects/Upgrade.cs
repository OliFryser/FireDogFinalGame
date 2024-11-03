using UnityEngine;

public enum UpgradeType
{
  HealthBoost,
  DamageBoost,
  SpeedBoost
}

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable Objects/Upgrade")]
public class Upgrade : ScriptableObject
{
  public string Title;
  public UpgradeType UpgradeType;
  // TODO add more things, like sprite, description, percentage etc.
}

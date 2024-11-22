using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Speaker", menuName = "Scriptable Objects/Speaker")]
public class Speaker : ScriptableObject
{
  public Sprite Sprite;
  public string Name;
}

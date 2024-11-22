using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSequence", menuName = "Scriptable Objects/DialogueSequence")]
public class DialogueSequence : ScriptableObject
{
  public List<DialogueLine> Lines;
}

[System.Serializable]
public struct DialogueLine
{
  [TextArea(10, 15)]
  public string DialogContent;
  public Speaker Speaker;
}
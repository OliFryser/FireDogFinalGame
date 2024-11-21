using TMPro;
using UnityEngine;
using System.Collections.Generic;

public static class TextSplitter
{

  public static List<string> SplitTextToFit(string inputText, TMP_Text textObject, RectTransform textRectTransform)
  {
    List<string> result = new();
    string[] words = inputText.Split(' ');
    string currentSegment = "";

    foreach (string word in words)
    {
      // Test if adding this word would exceed bounds
      string testSegment = string.IsNullOrEmpty(currentSegment) ? word : currentSegment + " " + word;
      Vector2 preferredSize = textObject.GetPreferredValues(testSegment);

      // Check if the text fits
      if (preferredSize.x <= textRectTransform.rect.width && preferredSize.y <= textRectTransform.rect.height)
      {
        currentSegment = testSegment;
      }
      else
      {
        // Add the current segment to the result and start a new one
        result.Add(currentSegment);
        currentSegment = word;
      }
    }

    // Add the last segment
    if (!string.IsNullOrEmpty(currentSegment))
    {
      result.Add(currentSegment);
    }

    List<string> resultsAdded = new();

    for (int i = 0; i < result.Count; i += 5)
    {
      var wholeLine = "";
      for (int j = 0; j < 5; j++)
      {
        var index = i + j;
        if (index >= result.Count) break; ;
        wholeLine += result[index] + "\n";
      }
      resultsAdded.Add(wholeLine);
    }

    return resultsAdded;
  }
}
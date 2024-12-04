using UnityEngine;

public class CleanupDisable : MonoBehaviour
{
    public void DisableObject(GameObject target)
    {
        if (target != null)
        {
            target.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Target object is null! Cannot disable.");
        }
    }

    public void DisableObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
}

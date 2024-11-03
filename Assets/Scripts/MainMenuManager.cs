using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private UnityEditor.SceneAsset level1Scene; // SceneAsset for Inspector

    public void StartGame()
    {
        if (level1Scene != null)
        {
            string sceneName = level1Scene.name;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Level 1 scene is not assigned.");
        }
    }
}

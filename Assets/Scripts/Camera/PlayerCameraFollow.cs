using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerCameraFollow : MonoBehaviour
{
    private const string CAMERA_BOUNDS_TAG = "CameraBounds";
    private CinemachineConfiner2D _confiner;

    void Awake()
    {
        _confiner = GetComponent<CinemachineConfiner2D>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject sceneBounds = GameObject.FindGameObjectWithTag(CAMERA_BOUNDS_TAG);
        _confiner.BoundingShape2D = sceneBounds?.GetComponent<Collider2D>();
    }
}


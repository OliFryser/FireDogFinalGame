using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerCameraFollow : MonoBehaviour
{
    private const string CAMERA_BOUNDS_TAG = "CameraBounds";
    private CinemachineConfiner2D _confiner;
    private CinemachineCamera _camera;

    [SerializeField] 
    private Transform _dialogueTarget;
    private Transform _initialTarget;
    
    private void Awake()
    {
        _confiner = GetComponent<CinemachineConfiner2D>();
        _camera = GetComponent<CinemachineCamera>();
        _initialTarget = _camera.Target.TrackingTarget;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var sceneBounds = GameObject.FindGameObjectWithTag(CAMERA_BOUNDS_TAG);
        _confiner.BoundingShape2D = sceneBounds?.GetComponent<Collider2D>();
    }

    public void MoveCameraForDialog()
    {
        _camera.Target.TrackingTarget = _dialogueTarget;
    }
    
    public void MoveCameraBackAfterDialogue()
    {
        _camera.Target.TrackingTarget = _initialTarget;
    }
}


using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin _perlinNoiseShaker;

    [SerializeField]
    private float _shakeStrength = 2.0f;

    [SerializeField]
    private float _shakeDuration = 0.2f;

    private float _timer;

    void Start()
    {
        _perlinNoiseShaker = GetComponent<CinemachineBasicMultiChannelPerlin>();
        _perlinNoiseShaker.AmplitudeGain = 0.0f;
    }

    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _perlinNoiseShaker.AmplitudeGain = Mathf.Lerp(_shakeStrength, 0.0f, 1 - (_timer / _shakeDuration));
        }
    }

    public void StartShake()
    {
        _timer = _shakeDuration;
        _perlinNoiseShaker.AmplitudeGain = _shakeStrength;
    }
}

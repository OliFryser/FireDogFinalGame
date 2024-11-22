using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

[RequireComponent(typeof(Slider))]
public class UISliderSFX : MonoBehaviour
{
    [SerializeField] private EventReference _sliderAdjustSound;
    [SerializeField] private string _vcaName;

    private FMOD.Studio.VCA _vcaControl;
    private Slider _volumeSlider;

    void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
        _volumeSlider.onValueChanged.AddListener(UpdateVolume);


        if (!string.IsNullOrEmpty(_vcaName))
        {
            _vcaControl = RuntimeManager.GetVCA("vca:/" + _vcaName);
            if (!_vcaControl.isValid())
            {
                Debug.LogWarning("Invalid VCA: " + _vcaName);
            }
        }
        else
        {
            Debug.LogWarning("VCA Name is not set in the Inspector for " + gameObject.name);
        }
    }

    public void UpdateVolume(float value)
    {
        if (!_sliderAdjustSound.IsNull)
        {
            RuntimeManager.PlayOneShot(_sliderAdjustSound);
        }

        if (_vcaControl.isValid())
        {
            float normalizedValue = Mathf.Clamp01(value);
            _vcaControl.setVolume(normalizedValue);
        }
    }

    void OnDestroy()
    {
        if (_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.RemoveListener(UpdateVolume);
        }
    }
}

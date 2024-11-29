using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

[RequireComponent(typeof(Slider))]
public class UISliderSFX : MonoBehaviour
{
    [SerializeField] private EventReference _sliderAdjustSound;
    [SerializeField] private string _vcaName;
    [SerializeField] private string _sliderName; 
    [SerializeField] private float _defaultValue = 1f;

    private FMOD.Studio.VCA _vcaControl;
    private Slider _volumeSlider;

    void Awake()
    {
        _volumeSlider = GetComponent<Slider>();

        ResetSliderToSavedValue();

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

        // Set FMOD VCA volume
        if (_vcaControl.isValid())
        {
            float normalizedValue = Mathf.Clamp01(value);
            _vcaControl.setVolume(normalizedValue);
        }

        // Save the slider value
        if (!string.IsNullOrEmpty(_sliderName))
        {
            PlayerPrefs.SetFloat(_sliderName, value);
            PlayerPrefs.Save();
        }
    }

    public void ResetToDefault()
    {
        if (!string.IsNullOrEmpty(_sliderName))
        {
            PlayerPrefs.DeleteKey(_sliderName);
        }
        ResetSlider(_defaultValue);
    }

    private void ResetSlider(float value)
    {
        _volumeSlider.value = value; 
        UpdateVolume(value);
    }

    private void ResetSliderToSavedValue()
    {
        float savedValue = PlayerPrefs.GetFloat(_sliderName, _defaultValue);
        ResetSlider(savedValue); // Set both slider and VCA to saved value or default
    }

    void OnApplicationQuit()
    {
        ResetToDefault(); // Reset when the application quits
    }

    void OnDestroy()
    {
        if (_volumeSlider != null)
        {
            _volumeSlider.onValueChanged.RemoveListener(UpdateVolume);
        }
    }
}

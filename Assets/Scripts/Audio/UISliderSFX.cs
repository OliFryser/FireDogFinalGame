using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class UISliderSFX : MonoBehaviour
{
    [SerializeField] private EventReference sliderAdjustSound;
    [SerializeField] private string vcaName;

    private FMOD.Studio.VCA vcaControl;
    private Slider volumeSlider;

    void Awake()
    {
        volumeSlider = GetComponent<Slider>();

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
        }
        else
        {
            Debug.LogWarning("Slider component is missing on " + gameObject.name);
        }

        if (!string.IsNullOrEmpty(vcaName))
        {
            vcaControl = RuntimeManager.GetVCA("vca:/" + vcaName);
            if (!vcaControl.isValid())
            {
                Debug.LogWarning("Invalid VCA: " + vcaName);
            }
        }
        else
        {
            Debug.LogWarning("VCA Name is not set in the Inspector for " + gameObject.name);
        }
    }

    public void UpdateVolume(float value)
    {
        if (!sliderAdjustSound.IsNull)
        {
            RuntimeManager.PlayOneShot(sliderAdjustSound);
        }

        if (vcaControl.isValid())
        {
            float normalizedValue = Mathf.Clamp01(value);
            vcaControl.setVolume(normalizedValue);
        }
    }

    void OnDestroy()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(UpdateVolume);
        }
    }
}

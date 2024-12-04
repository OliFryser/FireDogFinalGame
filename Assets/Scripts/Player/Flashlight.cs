using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    private Light2D _light;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    public void TurnOffFlashlight()
    {
        gameObject.SetActive(false);
    }

    public void TurnOnFlashlight()
    {
        gameObject.SetActive(true);
    }
}

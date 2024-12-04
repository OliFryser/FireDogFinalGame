using UnityEngine;

public class EndingMerchant : MonoBehaviour
{
    [SerializeField] private GameObject _endingMerchant;
    [SerializeField] private GameObject _wizard;

    public void SwitchToWizard()
    {
        _endingMerchant.SetActive(false);
        _wizard.SetActive(true);
    }
}

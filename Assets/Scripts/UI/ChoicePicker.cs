using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoicePicker : MonoBehaviour
{
    [SerializeField] private Button _firstChoice;
    private void OnEnable()
    {
        SelectFirstSelectable();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            SelectFirstSelectable();
    }

    private void SelectFirstSelectable()
    {
        _firstChoice.Select();
    }
}

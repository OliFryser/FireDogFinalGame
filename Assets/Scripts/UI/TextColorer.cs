using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextColorer : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private TMP_Text _text;
    
    private Color _defaultColor;
    
    [SerializeField]
    private Color _selectedColor;
    
    void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        _defaultColor = _text.color;
    }

    public void OnSelect(BaseEventData eventData)
    {
        _text.color = _selectedColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _text.color = _defaultColor;
    }
}

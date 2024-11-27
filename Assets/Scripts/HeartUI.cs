using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField]
    private Image _fullHeart;
    [SerializeField]
    private Image _halfHeart;
    [SerializeField]
    private Image _emptyHeart;

    public void SetFullHeart()
    {
        _fullHeart.enabled = true;
        _halfHeart.enabled = false;
        _emptyHeart.enabled = false;
    }

    public void SetHalfHeart()
    {
        _fullHeart.enabled = false;
        _halfHeart.enabled = true;
        _emptyHeart.enabled = false;
    }

    public void SetEmptyHeart()
    {
        _fullHeart.enabled = false;
        _halfHeart.enabled = false;
        _emptyHeart.enabled = true;
    }
}


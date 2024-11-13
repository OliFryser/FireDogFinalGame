using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Image fullHeart;
    public Image halfHeart;

    public void SetFullHeart()
    {
        fullHeart.enabled = true;
        halfHeart.enabled = false;
    }

    public void SetHalfHeart()
    {
        fullHeart.enabled = false;
        halfHeart.enabled = true;
    }

    public void SetEmptyHeart()
    {
        fullHeart.enabled = false;
        halfHeart.enabled = false;
    }
}


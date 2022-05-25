using UnityEngine;
using UnityEngine.UI;

public class ChangeIconButton : MonoBehaviour
{
    [SerializeField] private Sprite _imageToSwap;

    public void Swap()
    {
        var iconHolder = GetComponent<Image>();
        if (iconHolder != null)
        {
            var temp = iconHolder.sprite;
            iconHolder.sprite = _imageToSwap;
            _imageToSwap = temp;
        }
    }
}

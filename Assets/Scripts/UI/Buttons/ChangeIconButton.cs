using UnityEngine;
using UnityEngine.UI;

public class ChangeIconButton : MonoBehaviour
{
    [SerializeField] private Sprite _imageToSwap;

    private Image iconHolder;

    private void Start()
    {
        iconHolder = GetComponent<Image>();
    }

    public void Swap()
    {
        if (iconHolder != null)
        {
            var temp = iconHolder.sprite;
            iconHolder.sprite = _imageToSwap;
            _imageToSwap = temp;
        }
    }
}

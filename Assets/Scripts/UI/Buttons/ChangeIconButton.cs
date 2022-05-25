using UnityEngine;
using UnityEngine.UI;

enum ButtonState
{
    TURN_ON,
    TURN_OFF
}

public class ChangeIconButton : MonoBehaviour
{
    [SerializeField] private Sprite _imageToSwap;

    private Image iconHolder;
    private ButtonState _currentState;

    private void Start()
    {
        iconHolder = GetComponent<Image>();

        //_currentState = GetGlobalSettings().isSoundOn();
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

using UnityEngine;

public class InputDetection : MonoBehaviour
{
    public static event OnTapInput TapEvent;
    public delegate void OnTapInput();

    private bool _isTouching;
    private bool _isMobile;
    private bool _detectionAllowed;

    void Start()
    {
        _detectionAllowed = false;
        _isTouching = false;
        _isMobile = Application.isMobilePlatform;
    }

    public void AllowDetection(bool value)
    {
        _detectionAllowed = value;
    }

    void Update()
    {
        if (!_detectionAllowed)
        {
            return;
        }

        if (_isMobile)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (!_isTouching)
                    {
                        TapEvent();
                        _isTouching = true;
                    }
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                    Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    _isTouching = false;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (!_isTouching)
                {
                    TapEvent();
                    _isTouching = true;
                }
            }
            else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
            {
                _isTouching = false;
            }
        }
    }
}

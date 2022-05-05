using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public static event OnTapInput TapEvent;
    public delegate void OnTapInput();

    private bool _isTouching;
    private bool _isMobile;

    void Start()
    {
        _isTouching = false;
        _isMobile = Application.isMobilePlatform;
    }

    void Update()
    {
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
            if (Input.GetMouseButtonDown(0))
            {
                if (!_isTouching)
                {
                    TapEvent();
                    _isTouching = true;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
            }
        }
    }
}

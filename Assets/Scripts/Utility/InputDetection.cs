using UnityEngine;

public class InputDetection : MonoBehaviour
{
    public static event OnTapInput TapEvent;
    public delegate void OnTapInput();

    private bool _isTouching;
    private bool _isMobile;
    private bool _isGameRunning;

    void Start()
    {
        _isGameRunning = false;
        _isTouching = false;
        _isMobile = Application.isMobilePlatform;

        GameManager.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        _isGameRunning = state == GameState.GameRunning;
    }

    void Update()
    {
        if (!_isGameRunning)
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

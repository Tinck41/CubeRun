using System;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public bool isDead { get; set; }

    public static event Action PlayerDead;

    public int score { get; private set; }

    private Vector2 _currentDirection;
    private Vector3 _actualDirection;

    private IMovable _movement;
    private ScoreCounter _scoreCounter;

    private Quaternion _initialRotation;

    public void Reload()
    {
        _movement.StopMove();
        _scoreCounter.Reset();

        GetComponent<BoxCollider>().isTrigger = false;
        isDead = false;
        transform.rotation = _initialRotation;
        transform.position = new Vector3(0, 1, 0);

        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = transform;

        _currentDirection = Vector3.zero;
        _movement.StartMove();
    }

    public void Start()
    {
        _movement = GetComponent<IMovable>();
        _scoreCounter = GetComponent<ScoreCounter>();

        _currentDirection = Vector2.zero;
        _actualDirection = Vector3.zero;

        _initialRotation = transform.rotation;

        _movement.StartMove();

        InputDetection.TapEvent += OnTap;
    }

    public void OnDestroy()
    {
        InputDetection.TapEvent -= OnTap;
    }

    public void StartMoving()
    {
        _currentDirection = Vector2.right;
        _actualDirection = Vector3.forward;
        _movement.ChangeDirection(_actualDirection);
    }

    private void OnTap()
    {
        if (!isDead)
        {
            if (_currentDirection == Vector2.right)
            {
                _currentDirection = Vector2.left;
                _actualDirection = Vector3.left;
                _movement.ChangeDirection(_actualDirection);
            }
            else
            {
                _currentDirection = Vector2.right;
                _actualDirection = Vector3.forward;
                _movement.ChangeDirection(_actualDirection);
            }
        }
    }

    public void SetDead(AnalyticsHelper.DeadReason reason)
    {
        if (isDead) return;

        isDead = true;

        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = null;

        _movement.StopMove();
        PlayerDataHelper.SetScore(_scoreCounter.score);

        PlayerDead?.Invoke();
        Debug.Log("Player dead");

        AnalyticsHelper.OnPlayerDead(score, reason);
    }
}

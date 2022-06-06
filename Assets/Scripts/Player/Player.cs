using System;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public bool isDead { get; private set; }

    public static event Action PlayerDead;

    private Vector2 _currentDirection;
    private Vector3 _actualDirection;

    private IMovable _movement;

    private ScoreCounter _scoreCounter;
    public SkinManager skinManager { get; private set; }

    private Quaternion _initialRotation;

    public void Revive(Vector3 position)
    {
        transform.SetPositionAndRotation(position + Vector3.up, _initialRotation);
        GetComponent<BoxCollider>().isTrigger = false;

        skinManager.Reset();

        _virtualCamera.Follow = transform;

        isDead = false;

        _currentDirection = Vector3.zero;
        _movement.StartMove();
    }

    public void Reload()
    {
        _movement.StopMove();
        _scoreCounter.Reset();
        skinManager.Reset();

        GetComponent<BoxCollider>().isTrigger = false;
        isDead = false;
        transform.rotation = _initialRotation;
        transform.position = new Vector3(0, 1, 0);

        _virtualCamera.Follow = transform;

        _currentDirection = Vector3.zero;
        _movement.StartMove();
    }

    public void Start()
    {
        _movement = GetComponent<IMovable>();
        _scoreCounter = GetComponent<ScoreCounter>();
        skinManager = GetComponent<SkinManager>();

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

    public int GetScroe()
    {
        var scoreCounter = GetComponent<ScoreCounter>();
        if (scoreCounter != null)
        {
            return scoreCounter.score;
        }

        Debug.LogError("Score Counter is missing");
        return 0;
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

        _virtualCamera.Follow = null;

        _movement.StopMove();
        _scoreCounter.CheckForRecord();

        PlayerDead?.Invoke();
        Debug.Log("Player dead");

        AnalyticsHelper.OnPlayerDead(_scoreCounter.score, reason);
    }
}

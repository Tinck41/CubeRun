using System;
using UnityEngine;
using Cinemachine;

public enum SkinType
{
    UNDEFINED = 0,
    ORANGE,
    YELLOW
}

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject[] _skins;
    [SerializeField] private SkinType _initialSkin;

    public bool isDead { get; set; }

    public static event Action PlayerDead;

    public int score { get; private set; }

    private Vector2 _currentDirection;
    private Vector3 _actualDirection;

    private IMovable _movement;
    private ScoreCounter _scoreCounter;

    private Quaternion _initialRotation;

    private SkinType _currentSkin;

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
        SetSkin(_initialSkin);

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

    public void SetSkin(SkinType type)
    {
        if (type == SkinType.UNDEFINED)
        {
            Debug.LogError("Player skin is " + type.ToString());
            throw new NullReferenceException();
        }

        foreach(var skin in _skins)
        {
            skin.gameObject.SetActive(false);
        }

        _currentSkin = type;
        _skins[Convert.ToInt32(type) - 1].gameObject.SetActive(true);
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

    public SkinType GetSkin()
    {
        return _currentSkin;
    }
}

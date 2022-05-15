using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Player : MonoBehaviour
{
    public bool isDead { get; set; }

    public UnityAction PlayerDead;

    public int score { get; private set; }

    private Vector2 _currentDirection;

    private IMovable _movement;
    private IGroundChecker _groundChecker;

    public void Reload()
    {
        GetComponent<BoxCollider>().isTrigger = false;
        isDead = false;
        transform.position = new Vector3(0, 1, 0);

        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = transform;

        _movement = GetComponent<IMovable>();
        _groundChecker = GetComponent<IGroundChecker>();

        _currentDirection = Vector3.zero;
    }

    public void Start()
    {
        _movement = GetComponent<IMovable>();
        _groundChecker = GetComponent<IGroundChecker>();

        _currentDirection = Vector2.zero;

        InputDetection.TapEvent += OnTap;
    }

    public void OnDestroy()
    {
        InputDetection.TapEvent -= OnTap;
    }

    public void StartRolling()
    {
        OnTap();
    }

    private void OnTap()
    {
        if (!isDead)
        {
            if (_currentDirection == Vector2.right)
            {
                _currentDirection = Vector2.left;
                _movement.Move(Vector3.left);
            }
            else
            {
                _currentDirection = Vector2.right;
                _movement.Move(Vector3.forward);
            }
        }
    }

    public void Update()
    {
    }

    public void SetDead(AnalyticsHelper.DeadReason reason)
    {
        isDead = true;

        var vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.Follow = null;

        _movement.Stop();

        PlayerDead?.Invoke();
        Debug.Log("Player dead");

        AnalyticsHelper.OnPlayerDead(score, reason);
    }
}

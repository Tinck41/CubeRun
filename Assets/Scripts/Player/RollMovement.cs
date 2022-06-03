using System.Collections;
using UnityEngine;

public class RollMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioSource _rollSound;

    private Vector3 _rotationPoint;

    private IGroundChecker _groundChecker;
    private ScoreCounter _scoreCounter;

    private bool _canRoll = false;


    public void Start()
    {
        _groundChecker = GetComponent<IGroundChecker>();
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    public void StartMove()
    {
        _canRoll = true;
        StartCoroutine(Roll());
    }

    public void ChangeDirection(Vector3 direction)
    {
        _rotationPoint = Quaternion.Euler(0, 45, 0) * direction;
    }

    public void StopMove()
    {
        _canRoll = false;
        _rotationPoint = Quaternion.Euler(0, 45, 0) * Vector3.zero;
        StopAllCoroutines();
    }

    private IEnumerator Roll()
    {
        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + _rotationPoint / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, _rotationPoint);

        while (remainingAngle > 0 && _canRoll)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * _speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        if (_groundChecker !=  null && _canRoll)
        {
            _groundChecker.IsOnGround();
        }

        if (_rotationPoint != Vector3.zero)
        {
            _scoreCounter?.AddScore(1);
            _rollSound.Play();
        }

        if (_canRoll)
            yield return StartCoroutine(Roll());
    }
}

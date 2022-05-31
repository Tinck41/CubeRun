using System.Collections;
using UnityEngine;

public class RollMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed;

    private Vector3 _rotationPoint;

    private IGroundChecker _groundChecker;
    private ScoreCounter _scoreCounter;

    public void Start()
    {
        _groundChecker = GetComponent<IGroundChecker>();
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    public void StartMove()
    {
        StartCoroutine(Roll());
    }

    public void ChangeDirection(Vector3 direction)
    {
        _rotationPoint = Quaternion.Euler(0, 45, 0) * direction;
    }

    public void StopMove()
    {
        _rotationPoint = Quaternion.Euler(0, 45, 0) * Vector3.zero;
        StopAllCoroutines();
    }

    private IEnumerator Roll()
    {
        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + _rotationPoint / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, _rotationPoint);

        while (remainingAngle > 0)
        {
            float rotationAngle = Mathf.Min(Time.deltaTime * _speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
            remainingAngle -= rotationAngle;
            yield return null;
        }

        if (_groundChecker !=  null)
        {
            _groundChecker.IsOnGround();
        }

        if (_rotationPoint != Vector3.zero)
        {
            _scoreCounter?.AddScore(1);
        }

        yield return StartCoroutine(Roll());
    }
}

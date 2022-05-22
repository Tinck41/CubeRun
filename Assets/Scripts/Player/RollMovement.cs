using System.Collections;
using UnityEngine;

public class RollMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _speed;

    private Vector3 _rotationPoint;

    private IGroundChecker _groundChecker;

    private bool _canMove;

    public void Start()
    {
        _groundChecker = GetComponent<IGroundChecker>();
        _canMove = true;

        StartCoroutine(Roll());
    }

    public void Move(Vector3 direction)
    {
        _rotationPoint = Quaternion.Euler(0, 45, 0) * direction;
    }

    public void Stop()
    {
        _rotationPoint = Quaternion.Euler(0, 45, 0) * Vector3.zero;
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
            _canMove = _groundChecker.IsOnGround();
        }

        yield return StartCoroutine(Roll());
    }
}

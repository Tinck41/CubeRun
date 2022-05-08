using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _rotationPoint;

    private Vector2 _currentDirection;

    void Start()
    {
        _currentDirection = Vector2.zero;

        InputDetection.TapEvent += OnTap;

        StartCoroutine(Roll());
    }

    private void OnTap()
    {
        if (_currentDirection == Vector2.right)
        {
            _currentDirection = Vector2.left;
            _rotationPoint = Quaternion.Euler(0, 45, 0) * Vector3.left;
        }
        else
        {
            _currentDirection = Vector2.right;
            _rotationPoint = Quaternion.Euler(0, 45, 0) * Vector3.forward;
        }
    }

    void Update()
    {
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


        if (CheckAfterRoll())
        {
            yield break;
        }

        yield return StartCoroutine(Roll());
    }

    private bool CheckAfterRoll()
    {
        int layerMask = 1 << 6;
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1, layerMask))
        {
            return true;
        }

        return false;
    }
}

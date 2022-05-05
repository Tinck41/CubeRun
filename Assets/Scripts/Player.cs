using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 forwardRotationPoint;
    private Vector3 rightRotationPoint;
    private Vector3 _rotationPoint;

    private Vector2 _currentDirection;

    void Start()
    {
        var bounds = GetComponent<MeshRenderer>().bounds;
        forwardRotationPoint = new Vector3(0, -bounds.extents.y, bounds.extents.z);
        rightRotationPoint = new Vector3(bounds.extents.x, -bounds.extents.y, 0);

        _currentDirection = Vector2.zero;

        SwipeDetection.TapEvent += OnTap;

        StartCoroutine(Roll());
    }

    private void OnTap()
    {
        if (_currentDirection == Vector2.right)
        {
            _currentDirection = Vector2.left;
            _rotationPoint = forwardRotationPoint;
        }
        else
        {
            _currentDirection = Vector2.right;
            _rotationPoint = rightRotationPoint;
        }
    }

    void Update()
    {
        
    }

    private IEnumerator Roll()
    {
        Vector3 point = transform.position + _rotationPoint;
        Vector3 axis = Vector3.Cross(Vector3.up, _rotationPoint).normalized;
        float angle = 90;
        float a = 0;

        while (angle > 0)
        {
            a = Time.deltaTime * speed;
            transform.RotateAround(point, axis, a);
            angle -= a;
            yield return null;
        }
        transform.RotateAround(point, axis, angle);

        yield return StartCoroutine(Roll());
    }
}

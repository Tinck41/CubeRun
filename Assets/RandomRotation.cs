using UnityEngine;
using System;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] private float _rotationAngle;
    [SerializeField] private int _directionNum;

    public void Start()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + random.Next(0, _directionNum) * _rotationAngle, transform.rotation.eulerAngles.z));
    }
}
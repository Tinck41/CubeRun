using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AnimatedObstacle : MonoBehaviour
{
    [SerializeField] private float _height;
    [SerializeField] private float _speed;

    private float _animationPeriod;
    private float _animationPause;

    void Start()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());

        _animationPeriod = (float)random.NextDouble() * 10f;
        _animationPause = random.Next(1, 5);

        transform.DOMove(new Vector3(transform.position.x, _height, transform.position.z), _speed).SetEase(Ease.OutExpo).SetLoops(-1, LoopType.Yoyo).SetDelay(_animationPeriod);
    }

}

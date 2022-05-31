using System;
using UnityEngine;
using DG.Tweening;

public class HummerBehaviour : MonoBehaviour
{
    [SerializeField] private float _raiseDuration;
    [SerializeField] private float _fallDuration;
    [SerializeField] private float _waitDuration;

    public void Start()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());
        var animationSequence = DOTween.Sequence();
        var initialRotation = transform.rotation.eulerAngles;

        animationSequence.Append(transform.DORotate(new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), _fallDuration));
        animationSequence.AppendInterval(_waitDuration);
        animationSequence.Append(transform.DORotate(initialRotation, _raiseDuration));
        animationSequence.SetLoops(-1);
    }
}
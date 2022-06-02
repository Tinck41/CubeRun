using System;
using UnityEngine;
using DG.Tweening;

public class FloatingBehaviour : MonoBehaviour
{
    [SerializeField] private float _ascendHeight;
    [SerializeField] private float _ascendDuration;
    [SerializeField] private float _descendDuration;

    [SerializeField] private Vector3 _ascendRotation;

    [SerializeField] private AudioSource _fallSound;

    public void Start()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());
        var animationSequence = DOTween.Sequence();

        animationSequence.PrependInterval(Convert.ToSingle(random.NextDouble()) * 5f);
        animationSequence.Append(transform.DOMoveY(_ascendHeight, _ascendDuration));
        animationSequence.Join(transform.DORotate(_ascendRotation + transform.rotation.eulerAngles, _ascendDuration));
        animationSequence.AppendInterval(random.Next(1, 2));
        animationSequence.Append(transform.DOMoveY(1, _descendDuration).OnComplete(() => _fallSound.Play()).SetEase(Ease.InExpo));
        animationSequence.SetLoops(-1);
    }
}

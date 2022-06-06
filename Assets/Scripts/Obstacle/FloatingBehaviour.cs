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

    private Sequence sequence;
    private Guid uid;

    public void Start()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());
        sequence = DOTween.Sequence();

        sequence.PrependInterval(Convert.ToSingle(random.NextDouble()) * 5f);
        sequence.Append(transform.DOMoveY(_ascendHeight, _ascendDuration));
        sequence.Join(transform.DORotate(_ascendRotation + transform.rotation.eulerAngles, _ascendDuration));
        sequence.AppendInterval(random.Next(1, 2));
        sequence.Append(transform.DOMoveY(1, _descendDuration).OnComplete(() => _fallSound.Play()).SetEase(Ease.InExpo));
        sequence.SetLoops(-1);

        uid = Guid.NewGuid();
        sequence.id = uid;
    }

    private void OnDestroy()
    {
        DOTween.Kill(uid);
        sequence = null;
    }
}

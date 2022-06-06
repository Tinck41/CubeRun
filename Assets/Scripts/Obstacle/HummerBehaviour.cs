using System;
using UnityEngine;
using DG.Tweening;

public class HummerBehaviour : MonoBehaviour
{
    [SerializeField] private float _raiseDuration;
    [SerializeField] private float _fallDuration;
    [SerializeField] private float _waitDuration;

    [SerializeField] private AudioSource _hammerSound;

    private Sequence sequence;
    private Guid uid;

    public void Start()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());
        var initialRotation = transform.rotation.eulerAngles;
        sequence = DOTween.Sequence();

        sequence.Append(transform.DORotate(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0), _fallDuration).OnComplete(() => _hammerSound.Play()));
        sequence.AppendInterval(_waitDuration);
        sequence.Append(transform.DORotate(initialRotation, _raiseDuration));
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

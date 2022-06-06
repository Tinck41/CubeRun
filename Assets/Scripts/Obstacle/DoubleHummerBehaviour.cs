using UnityEngine;
using DG.Tweening;
using System;


public class DoubleHummerBehaviour : MonoBehaviour
{
    [SerializeField] private float _spinDuration;
    [SerializeField] private float _waitDuration;
    
    [SerializeField] private AudioSource _whooshSound;

    private Sequence sequence;
    private Guid uid;

    public void Start()
    {
        var initialRotation = transform.rotation.eulerAngles;
        sequence = DOTween.Sequence();

        sequence.Append(transform.DORotate(new Vector3(transform.rotation.eulerAngles.x, -45, transform.rotation.eulerAngles.z), _spinDuration).OnComplete(() => _whooshSound.Play()));
        sequence.AppendInterval(_waitDuration);
        sequence.SetLoops(-1, LoopType.Incremental);

        uid = Guid.NewGuid();
        sequence.id = uid;
    }

    private void OnDestroy()
    {
        DOTween.Kill(uid);
        sequence = null;
    }
}

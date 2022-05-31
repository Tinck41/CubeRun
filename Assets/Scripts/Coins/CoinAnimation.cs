using System;
using UnityEngine;
using DG.Tweening;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private float _ascendHeight;
    [SerializeField] private float _ascendDuration;

    void Start()
    {
        var random = new System.Random(Guid.NewGuid().GetHashCode());
        var delay = Convert.ToSingle(random.NextDouble() * 2f);

        transform.DOMoveY(_ascendHeight, _ascendDuration).SetLoops(-1, LoopType.Yoyo).SetDelay(delay);
        transform.DOLocalRotate(new Vector3(0, 180, 0) + transform.rotation.eulerAngles, _ascendDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetDelay(delay);
    }
}

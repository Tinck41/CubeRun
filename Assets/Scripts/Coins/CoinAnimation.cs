using UnityEngine;
using DG.Tweening;
using System;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private float _ascendHeight;
    [SerializeField] private float _ascendDuration;

    void Start()
    {
        transform.DOMoveY(_ascendHeight, _ascendDuration).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalRotate(new Vector3(0, 180, 0) + transform.rotation.eulerAngles, _ascendDuration).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}

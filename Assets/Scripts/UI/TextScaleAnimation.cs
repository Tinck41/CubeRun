using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextScaleAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _scale;
    [SerializeField] private float _duration;

    void Start()
    {
        transform.DOScale(_scale, _duration).SetLoops(-1, LoopType.Yoyo);
    }
}

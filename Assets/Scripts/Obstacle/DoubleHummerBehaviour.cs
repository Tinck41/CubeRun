using UnityEngine;
using DG.Tweening;


public class DoubleHummerBehaviour : MonoBehaviour
{
    [SerializeField] private float _spinDuration;
    [SerializeField] private float _waitDuration;

    public void Start()
    {
        var animationSequence = DOTween.Sequence();
        var initialRotation = transform.rotation.eulerAngles;

        animationSequence.Append(transform.DORotate(new Vector3(transform.rotation.eulerAngles.x, -45, transform.rotation.eulerAngles.z), _spinDuration));
        animationSequence.AppendInterval(_waitDuration);
        animationSequence.SetLoops(-1, LoopType.Incremental);
    }
}

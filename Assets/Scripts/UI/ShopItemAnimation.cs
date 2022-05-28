using UnityEngine;
using DG.Tweening;

public class ShopItemAnimation : MonoBehaviour
{
    [SerializeField] private float _rotationTime;
    
    void Start()
    {
        transform.DORotate(new Vector3(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z), _rotationTime).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}

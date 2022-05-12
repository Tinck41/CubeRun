using UnityEngine.Events;
using UnityEngine;

public class RayGroundCheck : MonoBehaviour, IGroundChecker
{
    [SerializeField] private LayerMask _layerMask;

    public bool IsOnGround()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, 2, _layerMask))
        {
            Debug.Log("Did not hit");
            return false;
        }

        return true;
    }
}

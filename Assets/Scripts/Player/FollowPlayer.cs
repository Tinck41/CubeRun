using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject _player;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z);
    }
}

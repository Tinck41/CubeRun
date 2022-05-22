using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _holder;

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PlayerDataHelper.AddCoins(10);
            Destroy(_holder);
        }
    }
}

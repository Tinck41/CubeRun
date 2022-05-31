using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _holder;
    [SerializeField] private int _value;

    private bool _coinsAdded = false;

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null && !_coinsAdded)
        {
            SaveLoadManager.playerData.coins += _value;

            GameManager.instance.topHUD.SetCoinsValue(SaveLoadManager.playerData.coins);
            _coinsAdded = true;

            Destroy(_holder);
        }
    }
}

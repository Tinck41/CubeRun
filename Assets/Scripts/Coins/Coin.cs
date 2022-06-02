using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _holder;
    [SerializeField] private GameObject _coin;
    [SerializeField] private AudioSource _coinSound;
    [SerializeField] private int _value;

    private bool _coinsAdded = false;

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null && !_coinsAdded)
        {
            _coin.SetActive(false);

            _coinSound.Play();

            SaveLoadManager.playerData.coins += _value;

            GameManager.instance.topHUD.SetCoinsValue(SaveLoadManager.playerData.coins);
            _coinsAdded = true;

            Destroy(_holder, _coinSound.clip.length);
        }
    }
}

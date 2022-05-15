using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayTapZone : MonoBehaviour
{
    [SerializeField] private TextMeshPro _playText;

    public void OnPlayButtonTap()
    {
        //_playText.enabled = false;
        GameManager.Instance.SetState(GameState.GameRunning);
    }
}

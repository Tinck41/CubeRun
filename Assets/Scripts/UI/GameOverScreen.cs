using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;

    private void SetScore()
    {
        _score.text = PlayerDataHelper.GetScore().ToString();
    }

    public void RestartButton()
    {
    }

    public void HomeButton()
    {
    }
}

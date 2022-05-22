using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _score;

    public void Start()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    public void OnDestroy()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
    }

    void OnGameStateChanged(GameState state)
    {
        if (state == GameState.GameOver)
        {
            gameObject.SetActive(true);
            SetScore();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void SetScore()
    {
        _score.text = PlayerDataHelper.GetScore().ToString();
    }

    public void RestartButton()
    {
        GameManager.Instance.RestartGame();
    }

    public void HomeButton()
    {
        GameManager.Instance.RestartGame();
        GameManager.Instance.SetState(GameState.MainMenu);
    }
}

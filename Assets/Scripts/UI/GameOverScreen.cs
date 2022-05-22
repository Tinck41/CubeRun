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
        gameObject.SetActive(state == GameState.GameOver);
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

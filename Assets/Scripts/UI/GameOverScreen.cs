using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);

        GameManager.GameStateChanged += OnGameStateChanged;
    }

    void OnGameStateChanged(GameState state)
    {
        gameObject.SetActive(state == GameState.GameOver);
    }

    public void RestartButton()
    {
        GameManager.Instance.RestartGame();
    }
}

using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public void Start()
    {
        GameManager.GameStateChanged += OnGameStateChanged;
    }

    public void onDestroy()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        gameObject.SetActive(state == GameState.MainMenu);
    }
}

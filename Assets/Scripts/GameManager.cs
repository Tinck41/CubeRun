using System;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ChunkLoader _chunkLoader;

    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private MainMenuScreen _mainMenuScreen;

    public static GameManager Instance;

    public static event Action<GameState> GameStateChanged;

    public GameState State;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetState(State);

        Player.PlayerDead += GameOver;
    }

    public void OnDestroy()
    {
        Player.PlayerDead -= GameOver;
    }


    public void SetState(GameState newState)
    {
        State = newState;

        switch (State)
        {
            case GameState.MainMenu: 
                break;
            case GameState.GameRunning:
                break;
            case GameState.GameOver:
                break;
            default: 
                break;
        }

        GameStateChanged?.Invoke(State);
    }

    public void RestartGame()
    {
        _chunkLoader.Reload();
        _player.Reload();
        SetState(GameState.GameRunning);
    }

    void GameOver()
    {
        SetState(GameState.GameOver);
        Debug.Log("Game End");
    }
}

public enum GameState
{
    MainMenu,
    GameRunning,
    GameOver
}
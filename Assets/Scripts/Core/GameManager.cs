using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ChunkLoader _chunkLoader;

    [SerializeField] public InputDetection inputDetection;
    
    [SerializeField] public TopHUD topHUD;

    public static GameManager instance;

    public GameState currentState { get; private set; }
    public GameState previousState { get; private set; }
  
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SwitchState(GetComponent<MainMenuState>());
    }

    public void Update()
    {
        currentState.UpdateState();
    }

    public void ReloadGame()
    {
        _player.Reload();
        _chunkLoader.Reload();
    }

    public void SwitchState(GameState newState)
    {
        if (newState == null || currentState == newState)
        {
            return;
        }

        currentState?.LeaveState();

        previousState = currentState;
        currentState = newState;

        currentState?.EnterState();
    }

    public Player GetPlayer()
    {
        return _player;
    }
}
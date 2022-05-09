using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private GameOverScreen _gameOverScreen;

    void Start()
    {
        _player.PlayerDead += GameOver;
    }

    void Update()
    {
        
    }

    void GameOver()
    {
        _gameOverScreen.Setup();
        Debug.Log("Game End");
    }
}

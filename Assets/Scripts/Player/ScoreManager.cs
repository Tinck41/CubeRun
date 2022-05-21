using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highestScoreText;

    public int score { private set; get; }

    private int _highestScore;

    public void Awake()
    {
        instance = this;

        GameManager.GameStateChanged += OnGameStateChaneg;
    }

    public void OnDestroy()
    {
        GameManager.GameStateChanged -= OnGameStateChaneg;
    }

   private void OnGameStateChaneg(GameState state)
    {
        if (state == GameState.MainMenu || state == GameState.GameOver)
        {
            _scoreText.gameObject.SetActive(false);
        }
        else
        {
            _scoreText.gameObject.SetActive(true);
        }
    }

    public void Start()
    {
        Reload();
    }

    public void Reload()
    {
        score = 0;
        _highestScore = PlayerPrefs.GetInt("highestScore", 0);
        _scoreText.text = score.ToString();
        _highestScoreText.text = "Best: " + _highestScore.ToString();
    }

    public void AddScore()
    {
        score++;
        _scoreText.text = score.ToString();

        if (score > _highestScore)
        {
            PlayerPrefs.SetInt("highestScore", score);
        }
    }
}

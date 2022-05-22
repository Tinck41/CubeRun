using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public int score { get; private set; }

    public void Start()
    {
        score = 0;
    }

    public void Reset()
    {
        score = 0;
        _scoreText.text = score.ToString();
    }

    public void AddScore(int value)
    {
        score += value;

        _scoreText.text = score.ToString();
        PlayerDataHelper.AddScore(value);
    }
}

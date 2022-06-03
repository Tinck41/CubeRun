using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _playAgainButton;
    [SerializeField] private GameObject _adButton;
    [SerializeField] private GameObject _homeButton;
    [SerializeField] private GameObject _window;

    [SerializeField] private float _scoreSpeed;
    [SerializeField] private float _fadeSpeed;

    private int _score = 0;

    public void Show()
    {
        _window.GetComponent<WindowBase>().ShowWindow();

        _score = GameManager.instance.GetPlayer().GetScroe();

        _scorePanel.GetComponent<CanvasGroup>().alpha = 0;
        _playAgainButton.GetComponent<CanvasGroup>().alpha = 0;
        _playAgainButton.GetComponent<MultiImageButton>().interactable = false;
        _adButton.GetComponent<CanvasGroup>().alpha = 0;
        _adButton.GetComponent<MultiImageButton>().interactable = false;
        _homeButton.GetComponent<CanvasGroup>().alpha = 0;
        _homeButton.GetComponent<MultiImageButton>().interactable = false;

        _window.transform.localScale = Vector3.zero;

        var animationSequence = DOTween.Sequence();

        var currentScore = 0;
        animationSequence.Append(_window.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBounce));
        animationSequence.Append(_scorePanel.GetComponent<CanvasGroup>().DOFade(1, _fadeSpeed));
        animationSequence.Join(DOTween.To(() => currentScore, x => currentScore = x, _score, _scoreSpeed).OnUpdate(() => _scoreText.text = currentScore.ToString()));
        animationSequence.Append(_playAgainButton.GetComponent<CanvasGroup>().DOFade(1, _fadeSpeed).OnComplete(() => _playAgainButton.GetComponent<MultiImageButton>().interactable = true));
        animationSequence.Append(_adButton.GetComponent<CanvasGroup>().DOFade(1, _fadeSpeed).OnComplete(() => _adButton.GetComponent<MultiImageButton>().interactable = true));
        animationSequence.Append(_homeButton.GetComponent<CanvasGroup>().DOFade(1, _fadeSpeed).OnComplete(() => _homeButton.GetComponent<MultiImageButton>().interactable = true));
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    
    private GameController _gameController;
    private ProgressManager _progressManager;
    
    private void Awake()
    {
        _gameController = ServiceLocator.Get<GameController>();
        _progressManager = ServiceLocator.Get<ProgressManager>();
    }

    private void OnEnable()
    {
        _gameController.GameStarted += OnGameStarted;
        _gameController.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _gameController.GameStarted -= OnGameStarted;
        _gameController.GameOver -= OnGameOver;
    }

    public void Restart()
    {
        _gameController.Restart();;
    }

    private void OnGameOver()
    {
        scoreText.text = $"Score: {_gameController.CurrentScore}";
        highScoreText.text = $"Best: {_progressManager.GetHighScore()}";
        canvas.enabled = true;
    }

    private void OnGameStarted()
    {
        canvas.enabled = false;
    }
    
    public void GoToMainMenu() => SceneManager.LoadScene("Menu");
}

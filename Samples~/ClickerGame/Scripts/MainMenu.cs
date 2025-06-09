using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    private ProgressManager _progressManager;

    private void Awake()
    {
        _progressManager = ServiceLocator.Get<ProgressManager>();
    }

    private void Start()
    {
        RefreshHighScoreText();
    }

    public void ResetHighScore()
    {
        _progressManager.SetHighScore(0);
        RefreshHighScoreText();
    }

    public void StartGame() => SceneManager.LoadScene("Game");
    
    public void Quit() => Application.Quit();
    
    private void RefreshHighScoreText() => highScoreText.text = $"Best score: {_progressManager.GetHighScore()}";
}

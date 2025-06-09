using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float scaleSpeed;

    private GameController _gameController;
    private float scale = 1;
    
    private void Awake()
    {
        _gameController = ServiceLocator.Get<GameController>();
    }

    private void OnEnable()
    {
        _gameController.ScoreChanged += OnScoreChanged;
        text.text = _gameController.CurrentScore.ToString();
    }

    private void Update()
    {
        scale = Mathf.Lerp(scale, 1, Time.deltaTime * scaleSpeed);
        transform.localScale = Vector3.one * scale;
    }

    private void OnDisable()
    {
        _gameController.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        text.text = score.ToString();
        scale += 1;
    }
}

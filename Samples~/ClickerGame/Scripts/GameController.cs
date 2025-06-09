using System;
using UnityEngine;

public class GameController : MonoBehaviour, IInitializable
{
    public event Action<int> ScoreChanged;
    public event Action GameOver;
    public event Action GameStarted;
    
    [SerializeField] private float gameLength;

    public int CurrentScore { get; private set; }
    public float ElapsedTime => Time.time - startTime;
    public float TimeRemaining => gameLength - ElapsedTime;
    public bool IsGameRunning { get; private set; }

    private ProgressManager _progressManager;
    private float startTime;
    
    public void Initialize()
    {
        _progressManager = ServiceLocator.Get<ProgressManager>();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (IsGameRunning && TimeRemaining < 0)
        {
            IsGameRunning = false;
            _progressManager.SetHighScore(CurrentScore);
            GameOver?.Invoke();
        }
    }

    public void Restart()
    {
        CurrentScore = 0;
        ScoreChanged?.Invoke(CurrentScore);
        StartGame();
    }
    
    private void StartGame()
    {
        IsGameRunning = true;
        startTime = Time.time;
        GameStarted?.Invoke();
    }

    public void IncreaseScore() {
        if (!IsGameRunning)
            return;
        CurrentScore++;
        ScoreChanged?.Invoke(CurrentScore);
    }
}

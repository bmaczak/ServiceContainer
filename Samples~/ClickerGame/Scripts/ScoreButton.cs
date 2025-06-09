using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreButton : MonoBehaviour
{
    [SerializeField] private float margin;
    
    private GameController _gameController;
    private Camera _camera;
    
    private void Awake()
    {
        _gameController = ServiceLocator.Get<GameController>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _gameController.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        _gameController.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        MoveToRandomPosition();
    }

    public void OnMouseDown()
    {
        if (!_gameController.IsGameRunning)
            return;
        _gameController.IncreaseScore();
        MoveToRandomPosition();
    }

    private void MoveToRandomPosition()
    {
        var halfHeight = _camera.orthographicSize;
        var halfWidth = halfHeight * Screen.width / Screen.height;
        transform.position = new Vector2(
            Random.Range(-(halfWidth - margin), halfWidth - margin),
            Random.Range(-(halfHeight - margin), halfHeight - margin)
        );
    }
}

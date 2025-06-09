using TMPro;
using UnityEngine;

public class CountdownText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    private GameController _gameController;

    private void Awake()
    {
        _gameController = ServiceLocator.Get<GameController>();
    }

    private void Update()
    {
        text.text = _gameController.IsGameRunning ? _gameController.TimeRemaining.ToString("#.00") : "0";
    }
}

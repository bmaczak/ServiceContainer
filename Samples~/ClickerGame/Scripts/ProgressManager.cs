using UnityEngine;

public class ProgressManager
{
    private const string PREF_HIGHSCORE = "PREF_HIGHSCORE";
    
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(PREF_HIGHSCORE);
    }

    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(PREF_HIGHSCORE, score);
    }
}

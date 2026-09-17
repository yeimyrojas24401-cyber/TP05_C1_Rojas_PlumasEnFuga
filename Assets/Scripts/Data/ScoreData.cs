using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/Score Data")]
public class ScoreData : ScriptableObject
{
    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;

    public int CurrentScore => currentScore;
    public int HighScore => highScore;


    public UnityAction<int> OnScoreChanged;
    public UnityAction<int> OnNewHighScore;

    public void AddScore(int amount)
    {
        currentScore += amount;
        OnScoreChanged?.Invoke(currentScore);

        if (currentScore > highScore)
        {
            highScore = currentScore;
            OnNewHighScore?.Invoke(highScore);
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
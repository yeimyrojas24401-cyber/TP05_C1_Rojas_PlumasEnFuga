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
}
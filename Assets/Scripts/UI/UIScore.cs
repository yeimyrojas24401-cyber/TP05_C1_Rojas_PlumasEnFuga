using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        scoreData.OnScoreChanged += UpdateScoreText;
        UpdateScoreText(scoreData.CurrentScore);
    }

    private void OnDisable()
    {
        scoreData.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}

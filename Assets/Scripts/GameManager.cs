using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject scoreLabel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalHighScoreText;
    
    private void Start()
    {
        scoreData.ResetScore();
        scoreData.LoadHighScore();

        if (gameOverPanel != null )
            gameOverPanel.SetActive( false );
    }
    public void GameOver()
    {
        scoreData.SaveHighScore();
        scorePanel.SetActive(false);
        finalScoreText.text = "Score: " + scoreData.CurrentScore;
        finalHighScoreText.text = "Record: " + scoreData.CurrentScore;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

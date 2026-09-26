using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private ScoreDataSo scoreData;
    [SerializeField] private AudioDataSo audioData;
    [SerializeField] private GameObject gameOverPanel;
    [Header("Text")]
    [SerializeField] private GameObject scoreLabel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text finalHighScoreText;
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameOverClip;
    
    private void Start()
    {
        scoreData.ResetScore();
        scoreData.LoadHighScore();

        audioData.LoadVolumeSettings();
        audioData.ApplyAllToMixer();

        if (gameOverPanel != null )
            gameOverPanel.SetActive( false );
    }

    public void GameOver()
    {
        scoreData.SaveHighScore();
        scoreLabel.SetActive(false);
        finalScoreText.text = "Score: " + scoreData.CurrentScore;
        finalHighScoreText.text = "Record: " + scoreData.HighScore;

        audioSource.PlayOneShot(gameOverClip);

        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        Invoke(nameof(RetryAction), 0.5f);
    }

    private void RetryAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        Invoke(nameof(GoToMainMenuAction), 0.5f);
    }

    private void GoToMainMenuAction()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button btnRetry;
    [SerializeField] private Button btnMainMenu;
    [SerializeField] private Button btnExit;
    [SerializeField] private GameManager gameManager;


    private void Awake()
    {
        gameOverPanel.SetActive(false);

        btnRetry.onClick.AddListener(gameManager.Retry);
        btnMainMenu.onClick.AddListener(gameManager.GoToMainMenu);
        btnExit.onClick.AddListener(OnExitClicked);

#if UNITY_WEBGL && !UNITY_EDITOR
    btnExit.gameObject.SetActive(false);
#endif
    }

    private void OnDestroy()
    {
        btnRetry.onClick.RemoveAllListeners();
        btnMainMenu.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }

    private void OnExitClicked()
    {
        Invoke(nameof(ExitAction), 0.5f);
        Time.timeScale = 1f;
    }

    private void ExitAction()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif

    }
}

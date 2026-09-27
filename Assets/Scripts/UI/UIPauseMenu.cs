using UnityEngine;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnAudio;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnExit;
    private bool isPause = false;
    private void Awake()
    {
        btnContinue.onClick.AddListener(OnContinueClicked);
        btnAudio.onClick.AddListener(OnAudioClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);

#if UNITY_WEBGL && !UNITY_EDITOR
    btnExit.gameObject.SetActive(false);
#endif
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        audioPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
    private void Update()
    {
        if (gameOverPanel.activeSelf) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnContinueClicked();
        }
    }
    private void OnDestroy()
    {
        btnContinue.onClick.RemoveAllListeners();
        btnCredits.onClick.RemoveAllListeners();
        btnAudio.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }

    private void OnContinueClicked()
    {

        isPause = !isPause; //!igual a lo opuesto
        pausePanel.SetActive(isPause);
        if (!isPause)
        {
            audioPanel.SetActive(false);
            creditsPanel.SetActive(false);
        }
        Time.timeScale = isPause ? 0f : 1f;
    }


    private void OnAudioClicked()
    {
        audioPanel.SetActive(true);
        pausePanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    private void OnCreditsClicked()
    {
        creditsPanel.SetActive(true);
        pausePanel.SetActive(false);
        audioPanel.SetActive(false);
    }

    private void OnExitClicked()
    {
        Invoke(nameof(ExitAction),0.5f);
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

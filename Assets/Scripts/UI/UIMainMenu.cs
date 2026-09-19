using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject creditsPanel;

    [Header("Buttons")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnAudio;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnExit;

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnSettings.onClick.AddListener(OnSettingsClicked);
        btnAudio.onClick.AddListener(OnAudioClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);

#if UNITY_WEBGL && !UNITY_EDITOR
    btnExit.gameObject.SetActive(false);
#endif
    }

    private void Start()
    {
        settingsPanel.SetActive(false);
        audioPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnSettings.onClick.RemoveAllListeners();
        btnAudio.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }
    private void OnPlayClicked()
    {
        mainMenuPanel.SetActive(false);
        SceneManager.LoadScene("Gameplay");
    }

    private void OnSettingsClicked()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        audioPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    private void OnAudioClicked()
    {
        audioPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }
    private void OnCreditsClicked()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        audioPanel.SetActive(false);
    }


    private void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}

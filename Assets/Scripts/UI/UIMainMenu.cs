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

    [Header("Audio")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderBackground;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderUI;


    [Header("AudioMixer")]

    [SerializeField] private AudioMixer mixer;
    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnSettings.onClick.AddListener(OnSettingsClicked);
        btnAudio.onClick.AddListener(OnAudioClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);

        sliderMaster.onValueChanged.AddListener(OnSliderMasterValueChanged);
        sliderBackground.onValueChanged.AddListener(OnSliderBackgroundValueChanged);
        sliderSFX.onValueChanged.AddListener(OnSliderSfxValueChanged);
        sliderUI.onValueChanged.AddListener(OnSliderUiValueChanged);

#if UNITY_WEBGL && !UNITY_EDITOR
    btnExit.gameObject.SetActive(false);
#endif
    }


    private void Start()
    {
        settingsPanel.SetActive(false);
        audioPanel.SetActive(false);
        creditsPanel.SetActive(false);

        float volumeDb;

        mixer.GetFloat("VolumeMaster", out volumeDb);
        sliderMaster.value = Mathf.Pow(10, volumeDb / 20f);

        mixer.GetFloat("VolumeBackground", out volumeDb);
        sliderBackground.value = Mathf.Pow(10, volumeDb / 20f);

        mixer.GetFloat("VolumeSFX", out volumeDb);
        sliderSFX.value = Mathf.Pow(10, volumeDb / 20f);

        mixer.GetFloat("VolumeUI", out volumeDb);
        sliderUI.value = Mathf.Pow(10, volumeDb / 20f);
    }
    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnSettings.onClick.RemoveAllListeners();
        btnAudio.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();

        sliderMaster.onValueChanged.RemoveAllListeners();
        sliderBackground.onValueChanged.RemoveAllListeners();
        sliderSFX.onValueChanged.RemoveAllListeners();
        sliderUI.onValueChanged.RemoveAllListeners();
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
    private void OnSliderMasterValueChanged(float value)
    {
        mixer.SetFloat("VolumeMaster", Mathf.Log10(value) * 20f);
    }

    private void OnSliderBackgroundValueChanged(float value)
    {
        mixer.SetFloat("VolumeBackground", Mathf.Log10(value) * 20f);
    }

    private void OnSliderSfxValueChanged(float value)
    {
        mixer.SetFloat("VolumeSFX", Mathf.Log10(value) * 20f);
    }

    private void OnSliderUiValueChanged(float value)
    {
        mixer.SetFloat("VolumeUI", Mathf.Log10(value) * 20f);
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

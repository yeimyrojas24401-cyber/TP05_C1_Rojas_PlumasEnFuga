using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioDataSo", menuName = "Data/Game/AudioData")]
public class AudioDataSo : ScriptableObject
{
    [SerializeField, Range(0, 1)] private float masterVolume;
    [SerializeField, Range(0, 1)] private float backgroundVolume;
    [SerializeField, Range(0, 1)] private float sfxVolume;
    [SerializeField, Range(0, 1)] private float uiVolume;

    [SerializeField] private AudioMixer mixer;

    public float MasterVolume => masterVolume;
    public float BackgroundVolume => backgroundVolume;
    public float SfxVolume => sfxVolume;
    public float UiVolume => uiVolume;

    private const string MasterKey = "VolumeMaster";
    private const string BackgroundKey = "VolumeBackground";
    private const string SfxKey = "VolumeSFX";
    private const string UiKey = "VolumeUI";

    public UnityAction<float> OnMasterVolumeChanged;
    public UnityAction<float> OnBackgroundVolumeChanged;
    public UnityAction<float> OnSfxVolumeChanged;
    public UnityAction<float> OnUiVolumeChanged;

    private void OnEnable()
    {
        LoadVolumeSettings();
        ApplyAllToMixer();
    }
    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        OnMasterVolumeChanged?.Invoke(masterVolume);
        SaveVolumeSettings(); 
    }

    public void SetBackgroundVolume(float value)
    {
        backgroundVolume = value;
        OnBackgroundVolumeChanged?.Invoke(backgroundVolume);
        SaveVolumeSettings(); 
    }

    public void SetSfxVolume(float value)
    {
        sfxVolume = value;
        OnSfxVolumeChanged?.Invoke(sfxVolume);
        SaveVolumeSettings(); 
    }

    public void SetUiVolume(float value)
    {
        uiVolume = value;
        OnUiVolumeChanged?.Invoke(uiVolume);
        SaveVolumeSettings(); 
    }

    public void ResetVolumeSettings()
    {
        masterVolume = 1f;
        backgroundVolume = 0.4f;
        sfxVolume = 1f;
        uiVolume = 1f;

        OnMasterVolumeChanged?.Invoke(masterVolume);
        OnBackgroundVolumeChanged?.Invoke(backgroundVolume);
        OnSfxVolumeChanged?.Invoke(sfxVolume);
        OnUiVolumeChanged?.Invoke(uiVolume);

        SaveVolumeSettings();
        ApplyAllToMixer();
    }

    public void LoadVolumeSettings()
    {
        masterVolume = PlayerPrefs.GetFloat(MasterKey, 1f);
        backgroundVolume = PlayerPrefs.GetFloat(BackgroundKey, 0.4f);
        sfxVolume = PlayerPrefs.GetFloat(SfxKey, 1f);
        uiVolume = PlayerPrefs.GetFloat(UiKey, 1f);
    }

    public void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(MasterKey, masterVolume);
        PlayerPrefs.SetFloat(BackgroundKey, backgroundVolume);
        PlayerPrefs.SetFloat(SfxKey, sfxVolume);
        PlayerPrefs.SetFloat(UiKey, uiVolume);
        PlayerPrefs.Save();
    }
    public void ApplyAllToMixer()
    {
        mixer.SetFloat(MasterKey, Mathf.Log10(masterVolume) * 20f);
        mixer.SetFloat(BackgroundKey, Mathf.Log10(backgroundVolume) * 20f);
        mixer.SetFloat(SfxKey, Mathf.Log10(sfxVolume) * 20f);
        mixer.SetFloat(UiKey, Mathf.Log10(uiVolume) * 20f);
    }
}
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioDataSo", menuName = "Data/Game/AudioData")]
public class AudioDataSo : ScriptableObject
{
    [SerializeField, Range(0, 1)] private float masterVolume;
    [SerializeField, Range(0, 1)] private float backgroundVolume;
    [SerializeField, Range(0, 1)] private float sfxVolume;
    [SerializeField, Range(0, 1)] private float uiVolume;

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
        backgroundVolume = 1f;
        sfxVolume = 1f;
        uiVolume = 1f;

        OnMasterVolumeChanged?.Invoke(masterVolume);
        OnBackgroundVolumeChanged?.Invoke(backgroundVolume);
        OnSfxVolumeChanged?.Invoke(sfxVolume);
        OnUiVolumeChanged?.Invoke(uiVolume);

        SaveVolumeSettings(); 
    }

    public void LoadVolumeSettings()
    {
        masterVolume = PlayerPrefs.GetFloat(MasterKey, 1f);
        backgroundVolume = PlayerPrefs.GetFloat(BackgroundKey, 1f);
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
}
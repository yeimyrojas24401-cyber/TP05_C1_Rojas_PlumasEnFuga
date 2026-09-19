using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolumeChannel : MonoBehaviour
{
    private enum Channel
    {
        Master,
        Background,
        SFX, UI
    }

    [Header("Data")]
    [SerializeField] private AudioDataSo data;
    [SerializeField] private AudioMixer mixer;

    [Header("Config")]
    [SerializeField] private Channel channel;

    private Slider sliderVolume;

    private void Awake()
    {
        sliderVolume = GetComponent<Slider>();
        sliderVolume.minValue = 0.0001f;
        sliderVolume.maxValue = 1f;
        sliderVolume.value = GetCurrentValue();

        sliderVolume.onValueChanged.AddListener(OnValueChangedSliderVolume);
    }

    private void OnDestroy()
    {
        sliderVolume.onValueChanged.RemoveAllListeners();
    }

    private void OnValueChangedSliderVolume(float value)
    {
        switch (channel)
        {
            case Channel.Master:
                data.SetMasterVolume(value);
                break;
            case Channel.Background:
                data.SetBackgroundVolume(value);
                break;
            case Channel.SFX:
                data.SetSfxVolume(value);
                break;
            case Channel.UI:
                data.SetUiVolume(value);
                break;
        }

        ApplyVolumeToMixer(value);
    }

    private void ApplyVolumeToMixer(float value)
    {
        string paramName = channel switch
        {
            Channel.Master => "VolumeMaster",
            Channel.Background => "VolumeBackground",
            Channel.SFX => "VolumeSFX",
            Channel.UI => "VolumeUI",
            _ => null
        };

        if (paramName != null)
            mixer.SetFloat(paramName, Mathf.Log10(value) * 20f);
    }

    private float GetCurrentValue()
    {
        switch (channel)
        {
            case Channel.Master: return data.MasterVolume;
            case Channel.Background: return data.BackgroundVolume;
            case Channel.SFX: return data.SfxVolume;
            case Channel.UI: return data.UiVolume;
            default: return 1f;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeChannel : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] private AudioDataSo data;

    [Header("ChannelsEnum")]
    [SerializeField] private AudioChannel channel;

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
            case AudioChannel.Master:
                data.SetMasterVolume(value);
                break;
            case AudioChannel.Background:
                data.SetBackgroundVolume(value);
                break;
            case AudioChannel.SFX:
                data.SetSfxVolume(value);
                break;
            case AudioChannel.UI:
                data.SetUiVolume(value);
                break;
        }

    }

    private float GetCurrentValue()
    {
        switch (channel)
        {
            case AudioChannel.Master: return data.MasterVolume;
            case AudioChannel.Background: return data.BackgroundVolume;
            case AudioChannel.SFX: return data.SfxVolume;
            case AudioChannel.UI: return data.UiVolume;
            default: return 1f;
        }
    }
}
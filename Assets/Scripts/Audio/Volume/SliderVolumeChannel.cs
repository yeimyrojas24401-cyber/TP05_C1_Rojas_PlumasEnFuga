using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolumeChannel : MonoBehaviour
{
    private enum Channel 
    { Master, 
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

        Debug.Log($"[{channel}] Valor leído del SO al iniciar: {GetCurrentValue()}");

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
                mixer.SetFloat("VolumeMaster", Mathf.Log10(value) * 20f);
                break;
            case Channel.Background:
                data.SetBackgroundVolume(value);
                mixer.SetFloat("VolumeBackground", Mathf.Log10(value) * 20f);
                break;
            case Channel.SFX:
                data.SetSfxVolume(value);
                mixer.SetFloat("VolumeSFX", Mathf.Log10(value) * 20f);
                break;
            case Channel.UI:
                data.SetUiVolume(value);
                mixer.SetFloat("VolumeUI", Mathf.Log10(value) * 20f);
                break;
        }
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

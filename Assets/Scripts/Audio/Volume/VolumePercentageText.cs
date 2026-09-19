using TMPro;
using UnityEngine;

public class VolumePercentageText : MonoBehaviour
{
    private enum Channel
    {
        Master,
        Background,
        SFX, 
        UI
    }

    [Header("Data")]
    [SerializeField] private AudioDataSo data;

    [Header("Config")]
    [SerializeField] private Channel channel;

    [Header("Visuals")]
    [SerializeField] private TMP_Text percentageText;

    private void OnEnable()
    {
        UpdateText(GetCurrentValue());

        switch (channel)
        {
            case Channel.Master:
                data.OnMasterVolumeChanged += UpdateText;
                break;
            case Channel.Background:
                data.OnBackgroundVolumeChanged += UpdateText;
                break;
            case Channel.SFX:
                data.OnSfxVolumeChanged += UpdateText;
                break;
            case Channel.UI:
                data.OnUiVolumeChanged += UpdateText;
                break;
        }
    }

    private void OnDisable()
    {
        switch (channel)
        {
            case Channel.Master:
                data.OnMasterVolumeChanged -= UpdateText;
                break;
            case Channel.Background:
                data.OnBackgroundVolumeChanged -= UpdateText;
                break;
            case Channel.SFX:
                data.OnSfxVolumeChanged -= UpdateText;
                break;
            case Channel.UI:
                data.OnUiVolumeChanged -= UpdateText;
                break;
        }
    }

    private void UpdateText(float value)
    {
        int percentage = Mathf.RoundToInt(value * 100f);
        percentageText.text = percentage + "%";
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
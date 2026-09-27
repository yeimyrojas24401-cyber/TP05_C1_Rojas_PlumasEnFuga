using TMPro;
using UnityEngine;

public class VolumePercentageText : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private AudioDataSo data;

    [Header("ChannelsEnum")]
    [SerializeField] private AudioChannel channel;

    [Header("Visuals")]
    [SerializeField] private TMP_Text percentageText;

    private void OnEnable()
    {
        UpdateText(GetCurrentValue());

        switch (channel)
        {
            case AudioChannel.Master:
                data.OnMasterVolumeChanged += UpdateText;
                break;
            case AudioChannel.Background:
                data.OnBackgroundVolumeChanged += UpdateText;
                break;
            case AudioChannel.SFX:
                data.OnSfxVolumeChanged += UpdateText;
                break;
            case AudioChannel.UI:
                data.OnUiVolumeChanged += UpdateText;
                break;
        }
    }

    private void OnDisable()
    {
        switch (channel)
        {
            case AudioChannel.Master:
                data.OnMasterVolumeChanged -= UpdateText;
                break;
            case AudioChannel.Background:
                data.OnBackgroundVolumeChanged -= UpdateText;
                break;
            case AudioChannel.SFX:
                data.OnSfxVolumeChanged -= UpdateText;
                break;
            case AudioChannel.UI:
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
            case AudioChannel.Master: return data.MasterVolume;
            case AudioChannel.Background: return data.BackgroundVolume;
            case AudioChannel.SFX: return data.SfxVolume;
            case AudioChannel.UI: return data.UiVolume;
            default: return 1f;
        }
    }
}
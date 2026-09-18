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
}

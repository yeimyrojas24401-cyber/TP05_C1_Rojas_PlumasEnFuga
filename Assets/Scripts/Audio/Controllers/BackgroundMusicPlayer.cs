using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip musicClip;

    private void Awake()
    {
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        audioSource.Play();
    }
}
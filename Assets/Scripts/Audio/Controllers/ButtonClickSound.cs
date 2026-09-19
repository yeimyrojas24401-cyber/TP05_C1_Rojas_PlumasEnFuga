using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickClip;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        Debug.Log($"[{gameObject.name}] PlayClickSound ejecutado en frame {Time.frameCount}");
        audioSource.PlayOneShot(clickClip);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionHandle : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip powerUpClip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ObstacleMarker>() != null)
        {
            Destroy(gameObject);
            Object.FindFirstObjectByType<GameManager>().GameOver();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PowerUp powerUp = other.GetComponentInParent<PowerUp>();

        if (powerUp != null)
        {
            powerUp.DoAction(gameObject);

            if (powerUp.PickupClip != null)
                audioSource.PlayOneShot(powerUp.PickupClip);

            Destroy(powerUp.gameObject);
        }
    }
}

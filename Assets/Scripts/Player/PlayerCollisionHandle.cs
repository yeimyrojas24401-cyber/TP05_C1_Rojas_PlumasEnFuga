using UnityEngine;

public class PlayerCollisionHandle : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    private PlayerHealth health;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ObstacleMarker>() != null)
            health.TakeHit(collision.gameObject);
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

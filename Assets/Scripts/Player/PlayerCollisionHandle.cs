using UnityEngine;

public class PlayerCollisionHandle : MonoBehaviour
{
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
        if (other.gameObject.GetComponent<PowerUpMarker>() != null)
        {
            Destroy(other.gameObject);
            Object.FindFirstObjectByType<ObstacleSpawner>().TriggerSlowEffect(25f);
            Debug.Log("Trigger colisionado");
        }
    }
}

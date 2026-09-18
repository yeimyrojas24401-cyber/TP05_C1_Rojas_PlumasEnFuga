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
}

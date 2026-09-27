using UnityEngine;

public class MoveWithWorld : MonoBehaviour
{
    private Rigidbody2D rb;
    private ObstacleSpawner spawner;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Init(ObstacleSpawner obstacleSpawner)
    {
        spawner = obstacleSpawner;
    }
    private void FixedUpdate()
    {
        if (spawner == null) return;
        rb.linearVelocity = Vector2.left * spawner.CurrentSpeed;
    }
}

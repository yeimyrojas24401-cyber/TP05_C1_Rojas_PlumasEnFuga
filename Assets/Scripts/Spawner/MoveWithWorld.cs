using UnityEngine;

public class MoveWithWorld : MonoBehaviour
{
    [SerializeField] private WorldSpeedSo worldSpeedData;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (worldSpeedData == null) return;
        rb.linearVelocity = Vector2.left * worldSpeedData.CurrentSpeed;
    }
}

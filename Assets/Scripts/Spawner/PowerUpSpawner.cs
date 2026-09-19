using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float spawnTime = 30f;
    [SerializeField] private float moveSpeed = 1f;

    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            Spawn();
            timer = 0f;
        }
    }
    private void Spawn()
    {
        GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        GameObject spawned = Instantiate(prefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = spawned.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.left * moveSpeed;
    }
}

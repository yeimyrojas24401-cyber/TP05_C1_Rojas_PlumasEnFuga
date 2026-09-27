using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private DifficultySelectorSo difficultySelectorData;
    [SerializeField] private GameObject[] powerUpPrefabs;

    private float spawnTime;

    private float timer;

    private void Awake()
    {
        spawnTime = difficultySelectorData.CurrentDifficulty.PowerUpSpawnTime;
    }

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
        Instantiate(prefab, transform.position, Quaternion.identity);

    }
}

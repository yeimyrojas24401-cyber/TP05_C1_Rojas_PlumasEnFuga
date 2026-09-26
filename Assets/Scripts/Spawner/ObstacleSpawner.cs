using UnityEngine;
using UnityEngine.Events;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    [Header("Base Difficulty")]
    [SerializeField] private float baseObstacleSpeed = 10f;

    [Header("Difficulty Ramp")]
    [SerializeField] private float speedIncreasePerSecond = 0.02f;
    [SerializeField] private float minSpawnTime = 0.6f; //tiempo limite de spawneo con power
    [SerializeField] private float minDistanceBetweenObstacles = 20f;
    [SerializeField] private float maxDistanceBetweenObstacles = 50f;

    private float timeUntilObstacleSpawn;
    private float elapsedTime;
    private float currentSpawnTimeTarget;

    private float speedMultiplier = 1f;
    private float spawnTimeMultiplier = 1f;

    private float slowTimer;

    private float CurrentBaseSpeed => baseObstacleSpeed + (elapsedTime * speedIncreasePerSecond);
    private float CurrentBaseSpawnTime => Mathf.Max(minSpawnTime, Random.Range(minDistanceBetweenObstacles, maxDistanceBetweenObstacles) / CurrentBaseSpeed);
    private float CurrentObstacleSpeed => CurrentBaseSpeed * speedMultiplier;
    private float CurrentSpawnTime => CurrentBaseSpawnTime * spawnTimeMultiplier;


    public event UnityAction<float> OnSlowTimeChanged;
    public event UnityAction OnSlowEffectEnded;

    private void Awake()
    {
        currentSpawnTimeTarget = CalculateNextSpawnTime();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateSlowEffect();
        SpawnLoop();
    }
    private void SpawnLoop() //decide
    {
        timeUntilObstacleSpawn += Time.deltaTime;
        if (timeUntilObstacleSpawn >= currentSpawnTimeTarget * spawnTimeMultiplier)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
            currentSpawnTimeTarget = CalculateNextSpawnTime();
        }
    }
    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]; // da un numero indice ramdom en funcion del que se seleccionara el prefab y este prefab se quedara
                                                                                               // como obstacleToSpawn
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity); //aqui este opstacleToSpawn se creara en nuestra escena en su posicion transform.position y con rotacion dada por el quaternion.identity 

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>(); 
        obstacleRB.linearVelocity = Vector2.left * CurrentObstacleSpeed; //le agregamos velocidad
        //Debug.Log($"Velocidad aplicada: {CurrentObstacleSpeed}");
    }
    private float CalculateNextSpawnTime() //toma en cuenta mi velocidad actual para spawnear
    {
        float randomDistance = Random.Range(minDistanceBetweenObstacles, maxDistanceBetweenObstacles);
        return Mathf.Max(minSpawnTime, randomDistance / CurrentBaseSpeed);
    }
    public void TriggerSlowEffect(float duration, float speedFactor = 0.5f, float spawnTimeFactor = 1.5f)
    {
        speedMultiplier = speedFactor;
        spawnTimeMultiplier = spawnTimeFactor;
        slowTimer = duration;
    }
    private void UpdateSlowEffect()
    {
        if (slowTimer <= 0f) return;

        slowTimer -= Time.deltaTime;
        OnSlowTimeChanged?.Invoke(Mathf.Max(slowTimer, 0f));

        if (slowTimer <= 0f)           // se acabó
        {
            speedMultiplier = 1f;
            spawnTimeMultiplier = 1f;
            OnSlowEffectEnded?.Invoke();
        }
    }
}

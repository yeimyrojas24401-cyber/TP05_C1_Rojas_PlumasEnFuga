using UnityEngine;
using UnityEngine.Events;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    [Header("Base Difficulty")]
    [SerializeField] private float baseObstacleSpeed = 5f;

    [Header("Difficulty Ramp")]
    [SerializeField] private float speedIncreasePerSecond = 0.02f;
    [SerializeField] private float minSpawnTime = 0.6f; //tiempo limite de spawneo entre obstaculos
    [SerializeField] private float minDistanceBetweenObstacles = 20f;
    [SerializeField] private float maxDistanceBetweenObstacles = 50f;

    [Header("Slow Effect")]
    [SerializeField] private float slowSpeedFactor = 0.5f;
    [SerializeField] private float slowSpawnTimeFactor = 1.5f;

    private float timeUntilObstacleSpawn; //cronometro para decidir Spawn
    private float elapsedTime;
    private float currentSpawnTimeTarget;

    private float speedMultiplier = 1f;
    private float spawnTimeMultiplier = 1f;

    private float slowTimer;

    private float CurrentBaseSpeed => baseObstacleSpeed + (elapsedTime * speedIncreasePerSecond);
    private float CurrentObstacleSpeed => CurrentBaseSpeed * speedMultiplier;


    public float SpeedFactor => CurrentObstacleSpeed / baseObstacleSpeed;
    public float CurrentSpeed => CurrentObstacleSpeed;


    public event UnityAction<float> OnSlowTimeChanged;
    public event UnityAction OnSlowEffectEnded;

    private void Awake() //1
    {
        currentSpawnTimeTarget = CalculateNextSpawnTime();
    }

    private void OnEnable()
    {
        PowerUp.OnSlowPowerUpTaken += TriggerSlowEffect;
    }

    private void OnDisable()
    {
        PowerUp.OnSlowPowerUpTaken -= TriggerSlowEffect;
    }

    private void Update() //3
    {
        elapsedTime += Time.deltaTime;
        UpdateSlowEffect();
        SpawnLoop();
    }

    private void SpawnLoop() //4 decide
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

        spawnedObstacle.GetComponent<MoveWithWorld>().Init(this);

        //Debug.Log($"Velocidad aplicada: {CurrentObstacleSpeed}");
    }

    private float CalculateNextSpawnTime() // 2 toma en cuenta mi velocidad actual para spawnear
    {
        float randomDistance = Random.Range(minDistanceBetweenObstacles, maxDistanceBetweenObstacles);
        return Mathf.Max(minSpawnTime, randomDistance / CurrentBaseSpeed);
    }

    private void TriggerSlowEffect(float duration)
    {
        speedMultiplier = slowSpeedFactor;
        spawnTimeMultiplier = slowSpawnTimeFactor;
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

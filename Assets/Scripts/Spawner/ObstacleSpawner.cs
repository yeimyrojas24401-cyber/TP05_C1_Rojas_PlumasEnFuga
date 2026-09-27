using UnityEngine;
using UnityEngine.Events;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Difficulty")]
    [SerializeField] private DifficultySelectorSo difficultySelectorData;
    private DifficultySettingsSo difficultyData; // se llena en Awake con la dificultad elegida

    [Header("WorldSpeedData")]
    [SerializeField] private WorldSpeedSo worldSpeedData;

    [Header("Prefabs")]
    [SerializeField] private GameObject[] obstaclePrefabs;

    [Header("Slow Effect")]
    [SerializeField] private float slowSpeedFactor = 0.5f;
    [SerializeField] private float slowSpawnTimeFactor = 1.5f;

    private float timeUntilObstacleSpawn; //cronometro para decidir Spawn
    private float elapsedTime;
    private float currentSpawnTimeTarget;

    private float speedMultiplier = 1f;
    private float spawnTimeMultiplier = 1f;

    private float slowTimer;

    private float CurrentBaseSpeed => difficultyData.BaseWorldSpeed + (elapsedTime * difficultyData.SpeedIncreasePerSecond);
    private float CurrentObstacleSpeed => CurrentBaseSpeed * speedMultiplier;

    public event UnityAction<float> OnSlowTimeChanged;
    public event UnityAction OnSlowEffectEnded;

    private void Awake() //1
    {
        difficultyData = difficultySelectorData.CurrentDifficulty; // primero: los demás la usan
        worldSpeedData.ResetSpeed(difficultyData.BaseWorldSpeed);
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

        worldSpeedData.SetCurrentSpeed(CurrentObstacleSpeed);
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
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
    }

    private float CalculateNextSpawnTime() // 2 toma en cuenta mi velocidad actual para spawnear
    {
        float randomDistance = Random.Range(difficultyData.MinDistanceBetweenObstacles, difficultyData.MaxDistanceBetweenObstacles);
        return Mathf.Max(difficultyData.MinSpawnTimeLimit, randomDistance / CurrentBaseSpeed);
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
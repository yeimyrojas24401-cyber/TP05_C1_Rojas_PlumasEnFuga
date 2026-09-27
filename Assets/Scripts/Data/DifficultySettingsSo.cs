using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettingsSo", menuName = "Data/Game/DifficultySettingsData")]
public class DifficultySettingsSo : ScriptableObject
{
    [Header("World Speed")]
    [SerializeField] private float baseWorldSpeed = 5f;
    [SerializeField] private float speedIncreasePerSecond = 0.02f;

    [Header("Obstacles")]
    [SerializeField] private float minSpawnTimeLimit = 0.6f; // tiempo mínimo entre obstáculos
    [SerializeField] private float minDistanceBetweenObstacles = 17f;
    [SerializeField] private float maxDistanceBetweenObstacles = 50f;

    [Header("Power-ups")]
    [SerializeField] private float powerUpSpawnTime = 5f;

    [Header("Biome")]
    [SerializeField] private int biomeIndex = 0; // 0 = ParallaxBiomeA, 1 = ParallaxBiomeB

    public float BaseWorldSpeed => baseWorldSpeed;
    public float SpeedIncreasePerSecond => speedIncreasePerSecond;
    public float MinSpawnTimeLimit => minSpawnTimeLimit;
    public float MinDistanceBetweenObstacles => minDistanceBetweenObstacles;
    public float MaxDistanceBetweenObstacles => maxDistanceBetweenObstacles;
    public float PowerUpSpawnTime => powerUpSpawnTime;
    public int BiomeIndex => biomeIndex;
}

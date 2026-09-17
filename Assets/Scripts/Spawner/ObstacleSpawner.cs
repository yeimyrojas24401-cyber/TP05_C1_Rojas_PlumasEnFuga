using UnityEngine;

public class ObstacleSpawner: MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    public float obstacleSpawnTime = 2.0f;
    private float timeUntilObstacleSpawn;

    public float obstacleSpeed = 1.0f;

    private void Update()
    {
        SpawnLoop();
    }
    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;
        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }
    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]; // da un numero indice ramdom en funcion del que se seleccionara el prefab y este prefab se quedara
                                                                                               // como obstacleToSpawn
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity); //aqui este opstacleToSpawn se creara en nuestra escena en su posicion transform.position y con rotacion dada por el quaternion.identity 

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>(); //hacemos llamar a su collider (el del prefab)
        obstacleRB.linearVelocity = Vector2.left * obstacleSpeed; //le agregamos velocidad
    }
}

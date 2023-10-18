using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class enemyspawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;  // Array of enemy prefabs to spawn.

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;  // Number of enemies in the first wave.
    [SerializeField] private float enemiesPerSecond = 0.50f;  // Rate at which enemies spawn per second.
    [SerializeField] private float timeBetweenWaves = 5f;  // Time between waves.
    [SerializeField] private float scalingFactor = 0.75f;  // Scaling factor for increasing enemy count in each wave.
    private int currentWave = 1;  // Current wave number.
    private float timeSinceLastSpawn;  // Time elapsed since the last enemy spawn.
    private int enemiesAlive;  // Number of enemies currently alive.
    private int enemiesLeftToSpawn;  // Number of enemies left to spawn in the current wave.
    private bool isSpawning = false;  // Flag indicating if spawning is in progress.

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new();  // Event triggered when an enemy is destroyed.

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        SpawnControl();
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, enemyPoints.main.startpoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, scalingFactor));
    }

    private void SpawnControl()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class enemyspawner : MonoBehaviour
{
    [Header("reference")]
    [SerializeField] private GameObject[] enemyPrefaps;

    [Header("Attributes")]
    [SerializeField] private int baseEnemys = 8;
    [SerializeField] private float enemysPersecond = 0.50f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float scalingFactor = 0.75f;
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemysAlive;
    private int enemysLeftToSpawn;
    private bool isSpawning = false;

    [Header("events")]
    public static UnityEvent onEnemyDestroy = new();

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void EnemyDestroyed()
    {
        enemysAlive--;
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemysLeftToSpawn = EnemysPerWave();

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
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemysPersecond) && enemysLeftToSpawn > 0)
        {
            SpawnEmemie();
            enemysLeftToSpawn--;
            enemysAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemysAlive == 0 && enemysLeftToSpawn == 0)
        {
            EndWave();
        }
    }



    private void SpawnEmemie()
    {
        GameObject prefaptoSpawn = enemyPrefaps[0];
        Instantiate(prefaptoSpawn, enemyPoints.main.startpoint.position, Quaternion.identity);
    }


    private int EnemysPerWave()
    {
        return Mathf.RoundToInt(baseEnemys * Mathf.Pow(currentWave, scalingFactor));
    }


}

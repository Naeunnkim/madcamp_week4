using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static enemySpawnertest;

public class enemySpawnertest : MonoBehaviour
{
    [System.Serializable]
    public class EnemyInfo
    {
        public GameObject prefab;
        public float spawnTime;
    }

    [SerializeField]
    private EnemyInfo[] enemies;

    [SerializeField]
    private float waveTime = 10f;

    [SerializeField]
    private float waveDelay = 10f;

    [SerializeField]
    private Transform[] wayPointsRoute1;

    [SerializeField]
    private Transform[] wayPointsRoute2;

    private int currentWave = 1;

    private void Awake()
    {
        StartCoroutine(SpawnWavesWithDelay());
    }

    private IEnumerator SpawnWavesWithDelay()
    {
        // Wait for 10 seconds before starting the first wave
        yield return new WaitForSeconds(10f);

        // Start spawning waves
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (currentWave <= 4)
        {
            switch (currentWave)
            {
                case 1:
                    StartCoroutine(SpawnWave1(enemies[0], wayPointsRoute1));
                    break;
                case 2:
                    StartCoroutine(SpawnWave2(enemies[1], wayPointsRoute2));
                    break;
                case 3:
                    StartCoroutine(SpawnWave3(enemies[2]));
                    break;
                case 4:
                    StartCoroutine(SpawnWave4());
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(waveDelay);
            currentWave++;
        }
    }
    private IEnumerator SpawnWave(EnemyInfo enemyInfo, Transform[] waypoints)
    {
        float timeElapsed = 0f;

        while (timeElapsed < waveTime)
        {
            GameObject clone = Instantiate(enemyInfo.prefab);
            enemymovementtest enemy = clone.GetComponent<enemymovementtest>();
            enemy.SetGold(20);
            enemy.Setup(waypoints);

            yield return new WaitForSeconds(enemyInfo.spawnTime);
            timeElapsed += enemyInfo.spawnTime;
        }
    }

    private IEnumerator SpawnWave1(EnemyInfo enemyInfo, Transform[] waypoints)
    {
        float timeElapsed = 0f;

        while (timeElapsed < waveTime)
        {
            GameObject clone1 = Instantiate(enemyInfo.prefab);
            enemymovementtest enemy1 = clone1.GetComponent<enemymovementtest>();
            enemy1.SetGold(25);
            enemy1.Setup(wayPointsRoute1);

            yield return new WaitForSeconds(enemyInfo.spawnTime);
            timeElapsed += enemyInfo.spawnTime;
        }
    }

    private IEnumerator SpawnWave2(EnemyInfo enemyInfo, Transform[] waypoints)
    {
        float timeElapsed = 0f;

        while (timeElapsed < waveTime)
        {
            GameObject clone2 = Instantiate(enemyInfo.prefab);
            enemymovementtest enemy2 = clone2.GetComponent<enemymovementtest>();
            enemy2.SetGold(35);
            enemy2.Setup(wayPointsRoute2);

            yield return new WaitForSeconds(enemyInfo.spawnTime);
            timeElapsed += enemyInfo.spawnTime;
        }
    }

    private IEnumerator SpawnWave3(EnemyInfo enemyInfo)
    {
        float timeElapsed = 0f;

        while (timeElapsed < waveTime)
        {
            int randomIndex = Random.Range(1, 3);
            GameObject clone3 = Instantiate(enemyInfo.prefab);
            enemymovementtest enemy3 = clone3.GetComponent<enemymovementtest>();
            enemy3.SetGold(50);
            if (randomIndex == 1)
                enemy3.Setup(wayPointsRoute1);
            else
                enemy3.Setup(wayPointsRoute2);

            yield return new WaitForSeconds(enemyInfo.spawnTime);
            timeElapsed += enemyInfo.spawnTime;
        }
    }

    private IEnumerator SpawnWave4()
    {
        float timeElapsed = 0f;

        while (timeElapsed < waveTime)
        {
            GameObject clone1 = Instantiate(enemies[0].prefab);
            enemymovementtest enemy1 = clone1.GetComponent<enemymovementtest>();
            enemy1.SetGold(25);
            enemy1.Setup(wayPointsRoute1);

            GameObject clone2 = Instantiate(enemies[1].prefab);
            enemymovementtest enemy2 = clone2.GetComponent<enemymovementtest>();
            enemy2.SetGold(35);
            enemy2.Setup(wayPointsRoute2);

            int randomIndex = Random.Range(1, 3);
            GameObject clone3 = Instantiate(enemies[randomIndex].prefab);
            enemymovementtest enemy3 = clone3.GetComponent<enemymovementtest>();
            enemy3.SetGold(50);
            if (randomIndex == 1)
                enemy3.Setup(wayPointsRoute1);
            else
                enemy3.Setup(wayPointsRoute2);

            yield return new WaitForSeconds(enemies[1].spawnTime);
            timeElapsed += enemies[1].spawnTime;
        }
    }
}

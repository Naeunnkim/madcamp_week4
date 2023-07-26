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
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (currentWave <= 3)
        {
            switch (currentWave)
            {
                case 1:
                    StartCoroutine(SpawnWave(enemies[0], wayPointsRoute1));
                    break;
                case 2:
                    StartCoroutine(SpawnWave(enemies[1], wayPointsRoute2));
                    break;
                case 3:
                    StartCoroutine(SpawnWave3());
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
            enemy.Setup(waypoints);

            yield return new WaitForSeconds(enemyInfo.spawnTime);
            timeElapsed += enemyInfo.spawnTime;
        }
    }

    private IEnumerator SpawnWave3()
    {
        float timeElapsed = 0f;

        while (timeElapsed < waveTime)
        {
            GameObject clone1 = Instantiate(enemies[0].prefab);
            enemymovementtest enemy1 = clone1.GetComponent<enemymovementtest>();
            enemy1.Setup(wayPointsRoute1);

            GameObject clone2 = Instantiate(enemies[1].prefab);
            enemymovementtest enemy2 = clone2.GetComponent<enemymovementtest>();
            enemy2.Setup(wayPointsRoute2);

            int randomIndex = Random.Range(1, 3);
            GameObject clone3 = Instantiate(enemies[randomIndex].prefab);
            enemymovementtest enemy3 = clone3.GetComponent<enemymovementtest>();
            if (randomIndex == 1)
                enemy3.Setup(wayPointsRoute1);
            else
                enemy3.Setup(wayPointsRoute2);

            yield return new WaitForSeconds(enemies[0].spawnTime);
            timeElapsed += enemies[0].spawnTime;
        }
    }
}
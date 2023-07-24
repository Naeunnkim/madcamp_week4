using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnertest : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; //적 프리팹

    [SerializeField]
    private float spawnTime = 3f; //적 생성 주기

    [SerializeField]
    private Transform[] wayPointsRoute1;

    [SerializeField]
    private Transform[] wayPointsRoute2;

    private void Awake()
    {
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab);
            enemymovementtest enemy = clone.GetComponent<enemymovementtest>();

            int randomRoute = Random.Range(1, 3);

            switch (randomRoute)
            {
                case 1:
                    enemy.Setup(wayPointsRoute1);
                    break;
                case 2:
                    enemy.Setup(wayPointsRoute2);
                    break;
                default:
                    break;
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
}

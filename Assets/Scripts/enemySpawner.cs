using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; //적 프리팹

    [SerializeField]
    private float spawnTime=3f; //적 생성 주기

    [SerializeField]
    private Transform[] wayPoints;

    private void Awake() {
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy() {
        while (true) {
            GameObject clone = Instantiate(enemyPrefab);
            enemymovementtest enemy = clone.GetComponent<enemymovementtest>();

            enemy.Setup(wayPoints);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}

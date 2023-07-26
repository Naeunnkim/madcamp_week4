using System.Collections;
using UnityEngine;

public class AllySpawn : MonoBehaviour
{
    public GameObject allyPrefab;
    public GameObject ally1;
    public GameObject ally2;

    private Vector3 offset1 = new Vector3(-1f, -0.5f, 0f);
    private Vector3 offset2 = new Vector3(-1f, -1f, 0f);
    private Vector3 newPosition1;
    private Vector3 newPosition2;

    public float respawnDelay = 3f;
    private Coroutine respawnCoroutine;

    private void Start()
    {
        newPosition1 = transform.position + offset1;
        newPosition2 = transform.position + offset2;
    }

    private IEnumerator RespawnAlly(GameObject ally, Vector3 position)
    {
        yield return new WaitForSeconds(respawnDelay);
        ally = Instantiate(allyPrefab, position, Quaternion.identity);
    }

    private void Update()
    {
        if (ally1 == null || ally2 == null)
        {
            if (respawnCoroutine == null)
            {
                respawnCoroutine = StartCoroutine(RespawnAllies());
            }
        }
    }

    private IEnumerator RespawnAllies()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (ally1 == null)
        {
            ally1 = Instantiate(allyPrefab, newPosition1, Quaternion.identity);
        }

        if (ally2 == null)
        {
            ally2 = Instantiate(allyPrefab, newPosition2, Quaternion.identity);
        }

        respawnCoroutine = null;
    }
}

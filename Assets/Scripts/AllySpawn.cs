using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawn : MonoBehaviour
{
    public GameObject allyPrefab;
    private GameObject ally = null;
    // Start is called before the first frame update
    private void OnMouseUp()
    {
        if (!ally)
        {
            Vector3 newPosition = transform.position;
            newPosition.y -= 0.9f;

            ally = Instantiate(allyPrefab, newPosition, Quaternion.identity);
        }
    }
}

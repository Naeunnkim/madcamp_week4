using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject tower = null;
    // Start is called before the first frame update
    void OnMouseUp()
    {
        //2
        if (!tower)
        {
            Vector3 newPosition = transform.position;
            newPosition.y += 0.9f;// Increase Z coordinate to move higher

            tower = Instantiate(towerPrefab, newPosition, Quaternion.identity);

            // TODO: Deduct gold
        }
        else
        {
            Destroy(tower);
        }
    }
}

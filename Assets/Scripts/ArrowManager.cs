using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public GameObject ArrowPrefab;

    public void InstantiateArrow(Vector3 position, Quaternion rotation, GameObject target)
    {
        GameObject arrow = Instantiate(ArrowPrefab, position, rotation);
        ArrowController mover = arrow.GetComponent<ArrowController>();
        mover.SetTarget(target);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

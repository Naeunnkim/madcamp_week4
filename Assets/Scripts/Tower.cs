using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject tower = null;
    public UIManager uiManager; //UIManager script 참조

    void Start() {
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

    // Start is called before the first frame update
    void OnMouseUp()
    {
        //2
        if (!tower)
        {
            uiManager.ShowUI();
            // Vector3 newPosition = transform.position;
            // newPosition.y += 0.9f;// Increase Z coordinate to move higher

            // tower = Instantiate(towerPrefab, newPosition, Quaternion.identity);

            // TODO: Deduct gold
        }
    }

    public void BuildTower(Vector3 position) {
        Vector3 newPosition = transform.position;
        newPosition.y += 0.9f;

        tower = Instantiate(towerPrefab, position, Quaternion.identity);
    }
}
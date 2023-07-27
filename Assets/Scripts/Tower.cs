using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float angle = 0f;
    private GameObject tower = null;
    public TowerType type;
    private GameObject towerprefab;

    public int upgrade = 0;
    public UIManager uiManager; //UIManager script 참조

    void Start() {
    //uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    if (uiManager == null) {
        Debug.LogError("UIManager not found or not attached to any GameObject!");
    }
}
    // Start is called before the first frame update
    void OnMouseDown()
    {
        //2
        uiManager.setTower(this);
        if (!tower)
        {
            if(uiManager!= null) {
                uiManager.ShowUI1();
            }
            else {
                Debug.LogError("UIManager not assigned");
            }
            // Vector3 newPosition = transform.position;
            // newPosition.y += 0.9f;// Increase Z coordinate to move higher

            // tower = Instantiate(towerPrefab, newPosition, Quaternion.identity);

            // TODO: Deduct gold
        }

        else if(tower) {
            if(uiManager!=null) {
                uiManager.ShowUI2();
            }
            else {
                Debug.LogError("UIManager not assigned");
            }
        }

        
    }

    public void BuildTower(GameObject towerPrefab) {
        upgrade = 0;
        Vector3 newPosition = transform.position;
        newPosition.y += 0.9f;

        tower = Instantiate(towerPrefab, newPosition, Quaternion.Euler(0f, angle, 0f));
        tower.layer = LayerMask.NameToLayer("Ignore Raycast");

        if(uiManager!=null) {
            uiManager.HideUI1();
            uiManager.HideUI2();
        }
        else {
            Debug.LogError("UIManager is not assigned");
        }
    }

    public void DestroyTower() {
        if (tower!=null) {
            upgrade = 0;
            Destroy(tower);
            tower = null;
        }
    }

    public void UpgradeTower(GameObject towerPrefab)
    {
        DestroyTower();
        BuildTower(towerPrefab);
        upgrade += 1;
        upgrade = Mathf.Min(2, upgrade);
    }
}
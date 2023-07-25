using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject tower = null;
    public UIManager uiManager; //UIManager script 참조

    public GameObject uiContainer1;
    public GameObject uiContainer2;

    void Start() {
    uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    if (uiManager == null) {
        Debug.LogError("UIManager not found or not attached to any GameObject!");
    }
}
    // Start is called before the first frame update
    void OnMouseUp()
    {
        //2
        if (!tower)
        {
            if(uiManager!= null) {
                uiManager.ShowUI(uiContainer1);
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
                uiManager.ShowUI(uiContainer2);
            }
            else {
                Debug.LogError("UIManager not assigned");
            }
        }

        
    }

    public void BuildTower(Vector3 position, GameObject towerPrefab) {
        Vector3 newPosition = transform.position;
        newPosition.y += 0.9f;

        tower = Instantiate(towerPrefab, position, Quaternion.identity);
        tower.layer = LayerMask.NameToLayer("Ignore Raycast");

        if(uiManager!=null) {
            uiManager.HideUI(uiContainer1);
            uiManager.HideUI(uiContainer2);
        }
        else {
            Debug.LogError("UIManager is not assigned");
        }
    }

    public void DestroyTower() {
        if(tower!=null) {
            Destroy(tower);
            tower = null;
        }
    }
}
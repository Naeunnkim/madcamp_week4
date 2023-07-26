using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private GameObject tower = null;

    public GameObject archerTowerPrefab;
    public GameObject barrackTowerPrefab;
    public GameObject wizardTowerPrefab;

    public GameObject uiContainer1; //canvas1-build tower
    public GameObject uiContainer2; //canvas2-upgrade and sell

    public Button archerButton;
    public Button barrackButton;
    public Button wizardButton;

    public Button upgradeButton;
    public Button sellButton;

    private Tower towerScript;

    //gold test
    //public Text goldText;

    private bool isUIVisible1 = false; //canvas1
    private bool isUIVisible2 = false; //canvas2

    void Start() {
        HideUI1();
        HideUI2();
        //UpdateGoldText(GoldManager.Instance.gold);
        //GoldManager.Instance.OnGoldChanged.AddListener(UpdateGoldText);

        archerButton.onClick.AddListener(OnArcherButtonClick);
        barrackButton.onClick.AddListener(OnBarrackButtonClick);
        wizardButton.onClick.AddListener(OnWizardButtonClick);
        upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
        sellButton.onClick.AddListener(OnSellButtonClick);
    }

    public void setTower(Tower tower)
    {
        towerScript = tower;
        if (isUIVisible1)
        {
            uiContainer1.SetActive(false);
            isUIVisible1 = false;
        }
        if (isUIVisible2)
        {
            uiContainer2.SetActive(false);
            isUIVisible2 = false;
        }
    }

    /*
     public void ShowUI(GameObject uiContainer) {
        uiContainer.SetActive(true);

        if(uiContainer==uiContainer1) {
            isUIVisible1 = true;
        }
        else {
            isUIVisible2 = true;
        }
        
    }
    */

    public void ShowUI1()
    {
        if (isUIVisible2)
        {
            uiContainer2.SetActive(false);
            isUIVisible2 = false;
        }
        uiContainer1.SetActive(true);
        isUIVisible1 = true;
    }

    public void ShowUI2()
    {
        if (isUIVisible1)
        {
            uiContainer1.SetActive(false);
            isUIVisible1 = false;
        }
        uiContainer2.SetActive(true);
        isUIVisible2 = true;
    }

    /*
     public void HideUI(GameObject uiContainer) {
        uiContainer.SetActive(false);
        if(uiContainer == uiContainer1) {
            isUIVisible1 = false;
        }
        else {
            isUIVisible2 = false;
        }   
    }
    */

    public void HideUI1()
    {
        uiContainer1.SetActive(false);
        isUIVisible1 = false;
    }

    public void HideUI2()
    {
        uiContainer2.SetActive(false);
        isUIVisible2 = false;
    }

    private IEnumerator HideUICoroutine()
    {
        uiContainer1.SetActive(true);
        uiContainer2.SetActive(true);

        yield return null;

        // UI를 숨깁니다.
        uiContainer1.SetActive(false);
        uiContainer2.SetActive(false);

        isUIVisible1 = false;
        isUIVisible2 = false;
    }

    //tower build button click event handle
    public void OnArcherButtonClick() {
        int cost = 60;
        towerScript.BuildTower(archerTowerPrefab);
        HideUI1();

        // if(GoldManager.Instance.SubtractGold(cost)) {
            
        // }

        // else {
        //     Debug.Log("not enough gold");
        // }

        
    }

    //tower build button click event handle
    public void OnBarrackButtonClick() {
        towerScript.BuildTower(barrackTowerPrefab);
        HideUI1();
    }

    //tower build button click event handle
    public void OnWizardButtonClick() {
        towerScript.BuildTower(wizardTowerPrefab);
        HideUI1();
    }

    public void OnUpgradeButtonClick() {
        //upgrade tower
        HideUI2();
    }

    public void OnSellButtonClick() {
        //sell tower
        if(towerScript != null) {
            towerScript.DestroyTower();
        }
        HideUI2();
    }


    void Update() {
        /*
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 클릭 시
        {
            // UI가 표시되어 있고, 캔버스를 클릭한 경우는 숨기지 않도록 합니다.
            if (isUIVisible1 && EventSystem.current.IsPointerOverGameObject()
            || isUIVisible2 && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // 레이캐스트를 사용하여 타워를 클릭하지 않도록 합니다.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
                {
                    return;
                }
            }
            StartCoroutine(HideUICoroutine());
        }
        */
    }

    // public void UpdateGoldText(int newGoldValue) {
    //     goldText.text = newGoldValue.ToString();
    // }
}
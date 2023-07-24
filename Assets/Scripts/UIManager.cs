using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject towerPrefab;
    private GameObject tower = null;

    // public GameObject uiContainer1; //canvas1-build
    // public GameObject uiContainer2; //canvas2-upgrade and sell
    public GameObject uiContainer;
    public Button archerButton;
    public Button barrackButton;
    public Button wizardButton;
    public Tower towerScript;

    private bool isUIVisible = false;

    void Start() {
        HideUI();
        archerButton.onClick.AddListener(OnArcherButtonClick);
        barrackButton.onClick.AddListener(OnBarrackButtonClick);
        wizardButton.onClick.AddListener(OnWizardButtonClick);

        towerScript = FindObjectOfType<Tower>();
    }

    public void ShowUI() {
        uiContainer.SetActive(true);
        isUIVisible = true;
    }

    public void HideUI() {
        StartCoroutine(HideUICoroutine());
    }

    private IEnumerator HideUICoroutine()
    {
        uiContainer.SetActive(true);

        yield return null;

        // UI를 숨깁니다.
        uiContainer.SetActive(false);
        isUIVisible = false;
    }

    //tower build button click event handle
    public void OnArcherButtonClick() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y; // 카메라의 높이에 맞게 z 위치를 설정합니다.
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        towerScript.BuildTower(worldPosition);
    }

    //tower build button click event handle
    public void OnBarrackButtonClick() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Build barrack tower here if needed
        HideUI();
    }

    //tower build button click event handle
    public void OnWizardButtonClick() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // Build wizard tower here if needed
        HideUI();
    }


    void Update() {
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 클릭 시
        {
            // UI가 표시되어 있고, 캔버스를 클릭한 경우는 숨기지 않도록 합니다.
            if (isUIVisible && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            //HideUI();
            
        }
    }
}
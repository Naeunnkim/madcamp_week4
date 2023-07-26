using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomber : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject rangePrefab;
    private GameObject circle;
    private bool isMouseDown = false;
    private bool isFilling = false;
    public float shootDelay = 1f;
    private float lastShootTime = 0f;
    private float fillTimer = 0f;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        isFilling = true;
        slider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (circle != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            circle.transform.position = mousePosition;

            if (Input.GetMouseButtonUp(0) && isMouseDown)
            {
                Vector3 targetPosition = circle.transform.position;
                targetPosition.z = 0f;
                InstantiateBomb(targetPosition);
                Destroy(circle);
                circle = null;
                fillTimer = 0f;
                isFilling = true;
                lastShootTime = Time.time; // Update the last shoot time
                isMouseDown = false;
                slider.value = 0f;
            }
            else if (Input.GetMouseButtonDown(1)) // Right click
            {
                Destroy(circle);
                circle = null;
                isMouseDown = false;
            }
        }
        if (isFilling)
        {
            fillTimer += Time.deltaTime;
            float percentageFilled = fillTimer / shootDelay;
            slider.value = Mathf.Clamp01(percentageFilled);
            if (percentageFilled >= 1f)
            {
                isFilling = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Time.time < lastShootTime + shootDelay)
        {
            Debug.Log("Cooldown");
            return;
        }
        isMouseDown = true;
        if (circle == null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            circle = Instantiate(rangePrefab, mousePosition, Quaternion.identity);
        }
    }

    void InstantiateBomb(Vector3 targetPosition)
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        BombController mover = bomb.GetComponent<BombController>();
        mover.SetTarget(targetPosition);
    }
}

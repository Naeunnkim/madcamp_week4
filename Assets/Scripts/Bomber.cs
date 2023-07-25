using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject bombPrefab;
    public GameObject rangePrefab;
    private GameObject circle;
    private bool isMouseDown = false;
    public float shootDelay = 1f;
    private float lastShootTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
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
                lastShootTime = Time.time; // Update the last shoot time
                isMouseDown = false;
            }
            else if (Input.GetMouseButtonDown(1)) // Right click
            {
                Destroy(circle);
                circle = null;
                isMouseDown = false;
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

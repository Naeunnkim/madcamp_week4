using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackRange : MonoBehaviour
{
    public float distanceThreshold = 5f;
    private bool isVisible = false;
    public GameObject circleObject;

    private void Start()
    {
        circleObject.SetActive(false);
        SetCircleRadius(distanceThreshold);
    }
    void OnMouseDown()
    {
        isVisible = !isVisible;
        circleObject.SetActive(isVisible);
    }

    public void SetCircleRadius(float radius)
    {
        circleObject.transform.localScale = new Vector3(radius * 3f, radius * 1.5f, 0f);
    }
}

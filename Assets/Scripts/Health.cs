using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthSlider;
    public Transform targetTransform;

    // Start is called before the first frame update
    void Awake()
    {
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.gameObject.SetActive(false);
    }
    public void SetTarget(Transform target)
    {
        targetTransform = target;
    }
    // Update is called once per frame
    void Update()
    {
        if (targetTransform)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetTransform.position + Vector3.up * 2f);
            healthSlider.transform.position = screenPos;
        }

    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (healthSlider)
        {
            healthSlider.gameObject.SetActive(true);
            healthSlider.value = currentHealth;
            healthSlider.maxValue = maxHealth;
        }
    }

    public void HideHealthBar()
    {
        if (healthSlider)
        {
            healthSlider.gameObject.SetActive(false);
        }
    }
}

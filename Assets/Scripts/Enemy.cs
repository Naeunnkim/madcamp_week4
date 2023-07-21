using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image greenBar;
    public Image redBar;
    public float movementSpeed = 1f;
    public float movementRange = 1f;
    private Vector3 initialPosition;

    private void Start()
    {
        currentHealth = maxHealth;
        initialPosition = transform.position;
    }

    private void Update()
    {
        Vector3 newPosition = initialPosition + Vector3.up * Mathf.Sin(Time.time * movementSpeed) * movementRange;
        transform.position = newPosition;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
        //UpdateHealthBar();
    }
    void Die()
    {
        Debug.Log("Enemy defeated!");
        ArrowController[] arrows = FindObjectsOfType<ArrowController>();
        foreach (ArrowController arrow in arrows)
        {
            arrow.StopMoving();
        }

        Destroy(gameObject);
    }

    public bool IsAlive()
    {
        return currentHealth > 0f;
    }

    void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        greenBar.fillAmount = fillAmount;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("arrow"))
        {
            float arrowDamage = other.gameObject.GetComponent<ArrowController>().damage;
            TakeDamage(arrowDamage);
            Destroy(other.gameObject);
        }
    }
}

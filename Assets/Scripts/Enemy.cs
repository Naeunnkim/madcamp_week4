using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    private Vector3 initialPosition;
    private float movementSpeed = 1f;
    private float movementRange = 1f;
    public Slider healthBar;
    public Vector3 healthBarOffset = new Vector3(0f, 0.5f, 0f);
    private Coroutine damageOverTime;
    private float damageInterval = 1f;
    private bool isCollidingAlly = false;

    private void Start()
    {
        currentHealth = maxHealth;
        initialPosition = transform.position;
        healthBar = GetComponentInChildren<Slider>();
        if (healthBar != null)
        {
            healthBar.transform.position = transform.position + healthBarOffset;
        }
    }

    private void Update()
    {
        if (!isCollidingAlly)
        {
            Vector3 newPosition = initialPosition + Vector3.up * Mathf.Sin(Time.time * movementSpeed) * movementRange;
            transform.position = newPosition;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
        UpdateHealthBar();
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
        float healthPercentage = currentHealth / maxHealth;
        healthBar.value = healthPercentage;
        healthBar.transform.position = transform.position + healthBarOffset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("arrow"))
        {
            float arrowDamage = other.gameObject.GetComponent<ArrowController>().damage;
            TakeDamage(arrowDamage);
        }
        else if (other.CompareTag("bullet"))
        {
            float bulletDamage = other.gameObject.GetComponent<BulletController>().damage;
            TakeDamage(bulletDamage);
        }
        else if (other.CompareTag("ally") && !isCollidingAlly)
        {
            isCollidingAlly = true;
            damageOverTime = StartCoroutine(ApplyDamage(10f));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ally") && !isCollidingAlly)
        {
            isCollidingAlly = false;
            if (damageOverTime != null)
            {
                StopCoroutine(damageOverTime);
            }
        }
    }

    private IEnumerator ApplyDamage(float damage)
    {
        while (IsAlive() && isCollidingAlly)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    private GameObject enemy;
    public float speed = 1f;
    private Rect boundary;
    public Slider health;
    private bool isCollidingEnemy;
    private bool shouldMove;
    public float maxHealth = 150f;
    private float currentHealth;


    private Coroutine attackingCoroutine;
    private Coroutine healingCoroutine;
    public float healingAmount = 10f;
    private float Interval = 1f;
    public float enemyDamage = 10f;

    public Vector3 healthOffset = new Vector3(0f, 0.5f, 0f);

    private void Start()
    {
        boundary = new Rect(transform.position.x - 3, transform.position.y - 3, 6, 6);
        isCollidingEnemy = false;
        currentHealth = maxHealth;
        health = GetComponentInChildren<Slider>();
        if (health != null)
        {
            health.transform.position = transform.position + healthOffset;
        }
    }

    private void Update()
    {
        if (!isCollidingEnemy) { 
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = enemies.FirstOrDefault(e => boundary.Contains(e.transform.position));

            if (closestEnemy != null && closestEnemy != enemy)
            {
                enemy = closestEnemy;
            }
            if (enemy != null)
            {
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if (healingCoroutine == null)
                {
                    healingCoroutine = StartCoroutine(HealOverTime());
                }
            }
         }
    }

    private IEnumerator HealOverTime()
    {
        while (currentHealth < maxHealth)
        {
            Heal(healingAmount);
            yield return new WaitForSeconds(Interval);
        }
        healingCoroutine = null;
    }

    private void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdatehealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            Die();
        }
        UpdatehealthBar();
    }

    void UpdatehealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        health.value = healthPercentage;
        health.transform.position = transform.position + healthOffset;
    }

    private void Die()
    {
        Debug.Log("Unit defeated!");
        Destroy(gameObject);
    }

    public bool IsAlive()
    {
        return currentHealth > 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isCollidingEnemy = true;
            enemy = other.gameObject;
            attackingCoroutine = StartCoroutine(ApplyDamage(enemyDamage));
        }
    }
    private IEnumerator ApplyDamage(float damage)
    {
        while (IsAlive() && isCollidingEnemy)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(Interval);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            isCollidingEnemy = false;
            if (attackingCoroutine != null)
            {
                StopCoroutine(attackingCoroutine);
                attackingCoroutine = null;
            }
        }
    }
}

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
    public bool passEnemy = false;
    public float maxHealth = 150f;
    private float currentHealth;
    public GameObject sword;
    private Transform swordTransform;


    private Coroutine attackingCoroutine;
    private Coroutine healingCoroutine;
    private Coroutine rotateCoroutine;
    public float healingAmount = 10f;
    private float Interval = 1f;
    public float enemyDamage = 10f;
    private float rotationAngle;
    private float rotationSpeed;

    private Vector3 initialposition;
    public Vector3 healthOffset = new Vector3(0f, 0.5f, 0f);
    private List<enemymovementtest> collidingEnemies = new List<enemymovementtest>();

    private void Start()
    {
        boundary = new Rect(transform.position.x - 2, transform.position.y - 2, 4, 4);
        isCollidingEnemy = false;
        currentHealth = maxHealth;
        initialposition = transform.position;
        rotationAngle = 120f;
        rotationSpeed = 120f;
        swordTransform = sword.transform;
        health = GetComponentInChildren<Slider>();
        if (health != null)
        {
            health.transform.position = transform.position + healthOffset;
        }
    }

    private void Update()
    {
        if (!isCollidingEnemy)
        {
            if (enemy != null && boundary.Contains(enemy.transform.position))
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
                if (collidingEnemies.Count == 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, initialposition, speed * Time.deltaTime);
                }
            }
            if (!boundary.Contains(transform.position))
            {
                transform.position = initialposition;
                enemy = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isCollidingEnemy)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = enemies.FirstOrDefault(e => boundary.Contains(e.transform.position));

            if (closestEnemy != null && closestEnemy != enemy)
            {
                enemy = closestEnemy;
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
        if (other.CompareTag("Enemy") && !isCollidingEnemy)
        {
            isCollidingEnemy = true;
            collidingEnemies.Add(other.GetComponent<enemymovementtest>());
            enemy = other.gameObject;
            if (healingCoroutine != null)
            {
                StopCoroutine(healingCoroutine);
                healingCoroutine = null;
            }
            if (attackingCoroutine != null)
            {
                StopCoroutine(attackingCoroutine);
            }
            if (rotateCoroutine != null)
            {
               StopCoroutine(rotateCoroutine);
            }

            attackingCoroutine = StartCoroutine(ApplyDamage(enemyDamage));
            rotateCoroutine = StartCoroutine(RotateTo(rotationAngle, rotationSpeed));
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

    private IEnumerator RotateTo(float rotationAngle, float rotationSpeed)
    {
        float initialRotation = swordTransform.localRotation.eulerAngles.z;
        float targetRotation = initialRotation + rotationAngle;
        float currentRotation = initialRotation;
        bool rotateForward = true;

        while (isCollidingEnemy)
        {
            float step = rotationSpeed * Time.deltaTime;

            if (rotateForward)
            {
                currentRotation = Mathf.MoveTowards(currentRotation, targetRotation, step);
            }
            else
            {
                currentRotation = Mathf.MoveTowards(currentRotation, initialRotation, step);
            }

            swordTransform.localRotation = Quaternion.Euler(0, 0, currentRotation);

            if (Mathf.Abs(currentRotation - targetRotation) < 0.01f)
            {
                // Switch direction when the target rotation is reached
                rotateForward = !rotateForward;
            }

            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && isCollidingEnemy)
        {
            collidingEnemies.Remove(other.GetComponent<enemymovementtest>());
            if (collidingEnemies.Count == 0)
            {
                isCollidingEnemy = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (rotateCoroutine != null)
                {
                    StopCoroutine(rotateCoroutine);
                    rotateCoroutine = null;
                }
                if (attackingCoroutine != null)
                {
                    StopCoroutine(attackingCoroutine);
                    attackingCoroutine = null;
                    passEnemy = false;
                }
                if (swordTransform != null)
                {
                    swordTransform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
    }
}

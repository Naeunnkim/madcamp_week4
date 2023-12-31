using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemymovementtest : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private enemymovement enemyMovement;
    public float currentHealth;
    public float maxHealth = 100f;
    public Slider healthBar;
    public Vector3 healthBarOffset = new Vector3(0f, 0.5f, 0f);
    private Coroutine damageOverTime;
    private float damageInterval = 1f;
    private bool isCollidingAlly = false;
    public float arrowDamage = 20f;
    private float bulletDamage = 30f;
    public float BombDamage = 100f;
    private Unit allyUnit;
    public GameObject castleObject;
    private Castle castle;
    private List<Unit> collidingAllies = new List<Unit>();
    private GameObject goldManagerObject;
    private GoldManager goldManager;
    private int gold = 0;

    private void Start()
    {
        currentHealth = maxHealth;
        castle = castleObject.GetComponent<Castle>();
        goldManagerObject = GameObject.Find("gold");
        goldManager = goldManagerObject.GetComponent<GoldManager>();
        if (healthBar != null)
        {
            healthBar.transform.position = transform.position + healthBarOffset;
        }
    }

    private void Update()
    {
        if (healthBar != null)
        {
            healthBar.transform.position = transform.position + healthBarOffset;
        }
    }
    public void Setup(Transform[] wayPoints)
    {
        enemyMovement = GetComponent<enemymovement>();

        //적 이동 경로 wayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        //적의 위치를 첫번째 wayPoint 위치로 설정
        transform.position = wayPoints[currentIndex].position;

        //적 이동, 목표지점 설정 코루틴 함수 시작
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            //적 오브젝트 회전
            //transform.Rotate(Vector3.forward*10);


            if (!isCollidingAlly && Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * enemyMovement.MoveSpeed)
            {
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {

        //아직 이동할 waypoint가 남았다면
        if (currentIndex < wayPointCount - 1)
        {
            //적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoints[currentIndex].position;

            //이동 방향 설정 => 다음 목표지점
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            enemyMovement.MoveTo(direction);
        }

        //현재 위치가 마지막 waypoint이면
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0f;
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
        BulletController[] bullets = FindObjectsOfType<BulletController>();
        foreach (ArrowController arrow in arrows)
        {
            arrow.StopMoving();
        }
        foreach(BulletController bullet in bullets) 
        { 
            bullet.StopMoving(); 
        }
        if (goldManager != null)
        {
            goldManager.AddGold(gold);
        }
        Destroy(gameObject);
    }

    public void SetGold(int Gold) {
        gold = Gold;
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
            TakeDamage(arrowDamage);
        }
        else if (other.CompareTag("bullet"))
        {
            bulletDamage = other.GetComponent<BulletController>().damage;
            TakeDamage(bulletDamage);
        }
        else if (other.CompareTag("ally") && !isCollidingAlly && !other.GetComponent<Unit>().passEnemy)
        {
            allyUnit = other.GetComponent<Unit>();
            allyUnit.passEnemy = true;
            isCollidingAlly = true;
            collidingAllies.Add(allyUnit);
            enemyMovement.StopMoving();
            damageOverTime = StartCoroutine(ApplyDamage(15f));
        }
        else if (other.CompareTag("explosion"))
        {
            TakeDamage(BombDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ally"))
        {
            Unit exitedAlly = other.GetComponent<Unit>();
            if (collidingAllies.Contains(exitedAlly))
            {
                collidingAllies.Remove(exitedAlly);
            }
            if (collidingAllies.Count == 0)
            {
                isCollidingAlly = false;
                if (damageOverTime != null)
                {
                    StopCoroutine(damageOverTime);
                }
                enemyMovement.ResumeMoving();
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
        isCollidingAlly = false;
    }
}
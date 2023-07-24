using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArrowShoot : MonoBehaviour
{
    public GameObject ArrowPrefab;
    private float distanceThreshold;
    public float shootCool = 0.5f;

    private bool canShoot = true;
    private GameObject nearestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        distanceThreshold = GetComponentInParent<AttackRange>().distanceThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, distanceThreshold);
        foreach (Collider2D col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                enemymovementtest enemy = col.gameObject.GetComponent<enemymovementtest>();
                if (enemy != null && enemy.IsAlive() && canShoot)
                {
                    if (nearestEnemy == null || Vector2.Distance(enemy.transform.position, (Vector2)transform.position) < Vector2.Distance(nearestEnemy.transform.position, (Vector2)transform.position))
                    {
                        nearestEnemy = enemy.gameObject;
                    }
                }
            }
        }
        if (nearestEnemy != null && canShoot)
        {
            ShootAtEnemy(nearestEnemy);
        }
    }

    void ShootAtEnemy(GameObject enemy)
    {
        if (Vector2.Distance(enemy.transform.position, (Vector2)transform.position) <= distanceThreshold)
        {
            GameObject arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
            ArrowController mover = arrow.GetComponent<ArrowController>();
            mover.SetTarget(enemy);
            canShoot = false;
            StartCoroutine(ResetShootCooldown());
        }
    }
    IEnumerator ResetShootCooldown()
    {
        yield return new WaitForSeconds(shootCool);
        canShoot = true;
    }
}

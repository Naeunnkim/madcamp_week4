using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float distanceThreshold = 5f;
    public float shootCool = 0.5f;

    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, distanceThreshold);
        foreach (Collider2D col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy != null && enemy.IsAlive() && canShoot)
                {
                    ShootAtEnemy(col.gameObject);
                }
            }
        }
    }

    void ShootAtEnemy(GameObject enemy)
    {
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        FireController mover = bullet.GetComponent<FireController>();
        mover.SetTarget(enemy);
        canShoot = false;
        StartCoroutine(ResetShootCooldown());
    }
    IEnumerator ResetShootCooldown()
    {
        yield return new WaitForSeconds(shootCool);
        canShoot = true;
    }
}

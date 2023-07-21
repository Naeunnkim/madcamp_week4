using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private GameObject target;
    private Vector3 targetPosition;
    public float speed = 5f;
    public float damage = 20f;
    private bool isMoving = false;

    public void SetTarget(GameObject enemy)
    {
        target = enemy;
        if (target != null)
        {
            targetPosition= target.transform.position;
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed * 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            Vector2 direction = target.transform.position - transform.position;
            float angleRadians = Mathf.Atan2(direction.y, direction.x);
            float angleDegrees = angleRadians * Mathf.Rad2Deg;

            angleDegrees += 270f;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleDegrees));
            if (!target.GetComponent<Enemy>().IsAlive() || Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                isMoving = true;
            }
        }
        else if (isMoving) // If not following the enemy, continue moving towards the target position
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("Arrow collided with an enemy!");
        }
    }
    public void StopMoving()
    {
        isMoving = true;
        Destroy(gameObject, 1f);
    }
}

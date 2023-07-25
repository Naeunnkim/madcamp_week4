using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject explosionPrefab;
    private Vector3 targetPosition;
    public float speed = 5f;
    public float damage = 50f;
    public float rotationSpeed = 200f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed * 1.5f;
    }

    public void SetTarget(Vector3 position) { 
        targetPosition = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);

            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();
            float angleRadians = Mathf.Atan2(direction.y, direction.x);
            float angleDegrees = angleRadians * Mathf.Rad2Deg;
            transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                Explode();
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= 2.5f)
                {
                    Explode();
                }
            }
        }
    }

    private void Explode()
    {
        Debug.Log("exploded!");
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, 1f);
    }
}

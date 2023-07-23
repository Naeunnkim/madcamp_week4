using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private GameObject target;
    private Vector3 targetPosition;
    public float speed = 10f;
    public float damage = 30f;
    private bool isMoving = false;

    public void SetTarget(GameObject enemy)
    {
        target = enemy;
        if (target != null)
        {
            targetPosition = target.transform.position;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            if (!target.GetComponent<Enemy>().IsAlive() || Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                isMoving = true;
            }
        }
        else if (isMoving)
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
            Debug.Log("Fire collided with an enemy!");
        }
    }
    public void StopMoving()
    {
        isMoving = true;
        Destroy(gameObject, 0.1f);
    }
}

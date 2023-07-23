using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;

    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed => moveSpeed; //moveSpeed 변수의 property

    // Update is called once per frame
    private void Update()
    {
        transform.position += moveDirection*moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction) {
        moveDirection = direction;
    } 
}

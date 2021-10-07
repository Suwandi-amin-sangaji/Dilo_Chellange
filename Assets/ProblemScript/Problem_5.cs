using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem_5 : MonoBehaviour
{
    public float moveSpeed = 5;

    private Vector3 targetMove;
    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetMove = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetMove.z = 0;
        }

        if(transform.position != targetMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetMove, moveSpeed * Time.deltaTime);
        }
    }

}

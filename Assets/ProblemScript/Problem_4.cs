using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem_4 : MonoBehaviour
{
    public float moveSpeed = 7;

    private Vector2 movement;
    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        movement.Set(xMove, yMove);
    }

    private void FixedUpdate()
    {
        _rigidBody2D.velocity = movement.normalized * moveSpeed;   
    }
}

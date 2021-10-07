using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float moveSpeed;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10)
            gameObject.SetActive(false);
        if (transform.position.y > 10 || transform.position.y < -10)
            gameObject.SetActive(false);
    }

    public void MoveTo(Vector2 direction)
    {
        moveSpeed = Random.Range(5, 8);
        rigidBody.velocity = direction.normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage();
            gameObject.SetActive(false);
        }
    }
}

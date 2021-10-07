using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int health = 3;
    public float moveSpeed;
    private Vector3 targetMove;

    [SerializeField] GameObject[] heartImage;

    private Rigidbody2D p_rigidbody;

    private void Awake()
    {
        p_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetHealthUI();
    }

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        targetMove = new Vector2(xInput, yInput).normalized;
    }

    private void FixedUpdate()
    {
        p_rigidbody.velocity = targetMove * moveSpeed;
    }

    public void TakeDamage()
    {
        health--;
        SetHealthUI();
        if(health == 0)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }

    private void SetHealthUI()
    {
        for (int i = 0; i < heartImage.Length; i++)
        {
            if(i < health)
            {
                heartImage[i].SetActive(true);
            }
            else
            {
                heartImage[i].SetActive(false);
            }
        }
    }
}

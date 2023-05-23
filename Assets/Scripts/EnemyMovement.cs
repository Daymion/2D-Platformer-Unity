using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyMovement : MonoBehaviour
{
    // Enemy Movement Variables
    private Rigidbody2D rb;
    public float enemySpeed;
    private float maxSpeed = 1f;
    private float moveDirection = -1f;
    public float xVel;
    private Vector2 moveVel;

    // Player Kill Variables
    public GameObject Player;
    public GameObject startPoint;

    // Respawn Variables
    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    void Update()
    {
        xVel = rb.velocity.x;
        moveVel = new Vector2(enemySpeed * moveDirection, 0);

        moveEnemy();
    }

    /**
     * A function to move the enemy based on speed without passing max speed
     */
    private void moveEnemy()
    {
        rb.AddForce(moveVel);

        if (xVel > maxSpeed || xVel < -maxSpeed)
        {
            xVel = maxSpeed * Mathf.Sign(xVel);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.transform.position = startPoint.transform.position;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            transform.position = startPos;
            rb.velocity = Vector2.zero;
            moveDirection = -1f;
        }

        if (other.gameObject.CompareTag("Untagged"))
        {
            moveDirection = -moveDirection;
        }
    }
}

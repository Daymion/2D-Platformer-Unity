using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 pos;

    // Variables for movement
    public float playerSpeed;
    private float maxSpeed = 1f;
    private float moveDirection;
    private Vector2 moveVel;
    public bool isMoving;
    public float xVel;
    private float yVel;

    // Variables for jumping
    RaycastHit2D groundRay;
    private float groundRayDistance = 0.8f;
    private Vector2 jumpForce = new Vector2(0f, 350f);

    // Variable for wall jumping
    RaycastHit2D wallRayRight;
    private Vector2 wallJumpForce;
    private float wallRayDistance = 0.8f;
    private float wallRayHeight = 1.6f;
    private float wallJumpDirection = 0f;
    private bool onWall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Update Variables
        groundRay = Physics2D.Raycast(pos, Vector3.down);
        Debug.DrawRay(pos, Vector3.down, Color.red);

        wallRayRight = Physics2D.Raycast(pos, Vector3.right, wallRayDistance);
        Debug.DrawRay(pos, Vector3.right * wallRayDistance, Color.red);

        // Jump Functions
        JumpPlayer();
        WallJumpPlayer();
    }

    void Update()
    {
        // Update variables
        pos = transform.position;

        xVel = rb.velocity.x;
        yVel = rb.velocity.y;

        moveDirection = Input.GetAxis("Horizontal");
        moveVel = new Vector2(playerSpeed * moveDirection, 0);

        // Movement Functions
        MovePlayer();
        StopPlayer();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            onWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            onWall = false;
        }
    }

    /**
     * Adds force to the player character in the upward direction and opposite direction of the wall
     */
    private void WallJumpPlayer ()
    {
        if (groundRay.distance > wallRayHeight && onWall == true)
        {
            if (wallRayRight)
            {
                wallJumpDirection = -200f;
            } 
            else
            {
                wallJumpDirection = 200f;
            }

            wallJumpForce = new Vector2(wallJumpDirection, 200f);

            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(wallJumpForce);
            }
        }
    }

    /**
     * Adds force to the player character in the upward direction.
     */
    private void JumpPlayer()
    {
        if (groundRay.distance < groundRayDistance)
        {
            if (groundRay.collider.CompareTag("Ground") && Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(jumpForce);
            }
        }
    }

    /**
     * Adds force to the player character in the direction of the arrow key.
     * Prevents the player character from going past the maximum horizontal speed.
     */
    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(moveVel);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (xVel > maxSpeed || xVel < -maxSpeed)
        {
            xVel = maxSpeed * Mathf.Sign(xVel);
        }
    }

    /**
     * Adds force to the player chararcter when not moving to bring the x velocity closer to 0.
     * When within range of 0, the x velocity is set to 0.
     */
    private void StopPlayer()
    {
        float stopRange = 0.225f; // Range of how close the x velocity needs to be to 0
        float minusXVel = (playerSpeed / 0.5f) * -Mathf.Sign(xVel); // The force added to the x velocity to bring it closer to 0
        Vector2 minusVel = new Vector2(minusXVel, 0);

        if (xVel != 0 && isMoving == false)
        {
            if (xVel > -stopRange && xVel < stopRange)
            {
                rb.velocity = new Vector2(0, yVel);
            }
            else
            {
                rb.AddForce(minusVel);
            }
        }
    }
}

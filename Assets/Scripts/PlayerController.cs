using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MOVEMENT")]
    public Vector3 orientation = new Vector3(1, 1, 1);     // x = -1 if facing left, 1 if facing right
    public float speed = 5.0f;
    public float climbSpeedPercent = 0.5f;
    public float jumpForce = 200.0f;
    public LayerMask groundLayer;
    public float xBoundary = 8.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D rb;
    private float def_speed;
    private float gravity;
    private bool isOnLadder = false;
    private const string TAG_LADDER = "Ladder";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        def_speed = speed;
        gravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    // Handle player movement
    private void MovePlayer()
    {
        // Get player inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Allow player to climb up and down ladders if within range
        if (isOnLadder && Input.GetButton("Vertical"))
        {
            float verticalSpeed = Time.deltaTime * verticalInput * speed * climbSpeedPercent;
            rb.velocity = new Vector2(0, 0);    // Reset velocity to prevent jumping onto ladder from altering speed
            transform.Translate(Vector3.up * verticalSpeed);
        }

        // Move the player in direction they pressed
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            float horizontalSpeed = Time.deltaTime * horizontalInput * speed;

            // Flip player if facing wrong direction
            if (horizontalSpeed * orientation.x < 0)
                FlipPlayer();

            // Do not allow movement beyond boundary
            if (Mathf.Abs(transform.position.x + horizontalSpeed) < Mathf.Abs(xBoundary))
                transform.Translate(Vector3.right * horizontalSpeed);            
        }

        // Make player jump
        if (IsGrounded() && Input.GetButtonDown("Jump") && !isOnLadder)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
    }

    // Flip player towards direction they are moving
    private void FlipPlayer()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        orientation.x = -orientation.x;
    }

    // https://kylewbanks.com/blog/unity-2d-checking-if-a-character-or-object-is-on-the-ground-using-raycasts
    // Check if player is on the ground
    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.75f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        
        if (hit.collider != null)
            return true;

        return false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if player is on ladder
        if (other.tag == TAG_LADDER)
        {
            isOnLadder = true;
            rb.gravityScale = 0.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == TAG_LADDER)
        {
            isOnLadder = false;
            rb.gravityScale = gravity;
        }
    }

}

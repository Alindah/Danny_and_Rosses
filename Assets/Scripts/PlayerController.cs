using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MOVEMENT")]
    public float speed = 5.0f;
    public Vector3 orientation = new Vector3(1, 1, 1);     // x = -1 if facing left, 1 if facing right
    public float xBoundary = 8.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Get player inputs
        horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");

        // Move the player in direction they pressed
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            float horizontalSpeed = Time.deltaTime * horizontalInput * speed;
            float verticalSpeed = Time.deltaTime * verticalInput * speed;

            // Flip player if facing wrong direction
            if (horizontalSpeed * orientation.x < 0)
                FlipPlayer();

            // Do not allow movement beyond boundary
            if (Mathf.Abs(transform.position.x + horizontalSpeed) < Mathf.Abs(xBoundary))
                transform.Translate(Vector3.right * horizontalSpeed);
            
            //transform.Translate(Vector3.up * verticalSpeed);
        }
    }

    private void FlipPlayer()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        orientation.x = -orientation.x;
    }
}

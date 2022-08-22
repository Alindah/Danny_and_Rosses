using UnityEngine;

public class Enemy : Entity
{
    public float damage = 1;
    public float knockback = 50;
    public float patrolXBoundaryRight;
    public float patrolXBoundaryLeft;

    protected void Patrol()
    {
        // Walk towards direction facing
        transform.Translate(new Vector3(orientation.x, 0, 0) * Time.deltaTime * speed);

        // Flip if reached boundaries
        if (transform.position.x > xBoundaryRight)
        {
            FlipEntity();
            transform.position = new Vector2(xBoundaryRight, transform.position.y);
        }
        else if (transform.position.x < xBoundaryLeft)
        {
            FlipEntity();
            transform.position = new Vector2(xBoundaryLeft, transform.position.y);
        }
        else if (transform.position.x > patrolXBoundaryRight || transform.position.x < patrolXBoundaryLeft)
        {
            FlipEntity();
        }
    }
}

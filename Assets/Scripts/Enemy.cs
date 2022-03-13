using UnityEngine;

public class Enemy : Entity
{
    protected void Patrol()
    {
        // Walk towards direction facing
        transform.Translate(new Vector3(orientation.x, 0, 0) * Time.deltaTime * speed);

        // Flip if reached boundaries
        if (Mathf.Abs(transform.position.x) > Mathf.Abs(xBoundary))
            FlipEntity();
    }
}

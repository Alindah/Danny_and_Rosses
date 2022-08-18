using UnityEngine;
using static Constants;

public class Entity : MonoBehaviour
{
    public float speed;
    public float xBoundaryRight;
    public float xBoundaryLeft;
    public Vector3 orientation = new Vector3(1, 1, 1);     // x = -1 if facing left, 1 if facing right
    public float hitpoints;

    protected GameController gameController;
    protected Rigidbody2D rb;
    protected Collider2D col;

    protected virtual void Start()
    {
        gameController = GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
    }

    // Flip entity towards direction they are moving
    protected void FlipEntity()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        orientation.x = -orientation.x;
    }

    public void TakeDamage(float damage)
    {
        hitpoints -= damage;

        if (hitpoints <= 0)
            OnDeath();
    }

    protected void OnDeath()
    {
        Destroy(gameObject);
    }
}

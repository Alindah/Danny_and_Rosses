using UnityEngine;
using System.Collections;
using static Constants;

public class Entity : MonoBehaviour
{
    public float speed;
    public float xBoundaryRight;
    public float xBoundaryLeft;
    public Vector3 orientation = new Vector3(1, 1, 1);     // x = -1 if facing left, 1 if facing right
    public float hitpoints;
    public float invincibilityTime;

    protected GameController gameController;
    protected Rigidbody2D rb;
    protected Collider2D col;

    private bool isInvincible;

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

        StartCoroutine("ApplyInvincibility");
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }

    public void KnockBack(Vector2 direction)
    {
        transform.Translate(direction * Time.deltaTime);
    }

    private IEnumerator ApplyInvincibility()
    {
        // Play flashing animation
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
        // End flashing animation
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}

using UnityEngine;
using static Constants;

public class Thorn : Enemy
{
    public Vector2 knockbackDirection;

    protected override void OnDeath()
    {
        // Do nothing because thorns never die
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy enemies that make contact with thorn
        if (collision.CompareTag(ENEMY_TAG))
            Destroy(collision.gameObject);

        // Knock back players
        if (collision.CompareTag(PLAYER_TAG))
            collision.gameObject.GetComponent<Entity>().KnockBack(new Vector2(knockback * knockbackDirection.x, knockbackDirection.y));
    }
}

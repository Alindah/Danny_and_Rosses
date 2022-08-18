using UnityEngine;
using static Constants;

public class Thorn : Enemy
{
    protected override void OnDeath()
    {
        // Do nothing because thorns never die
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy enemies that make contact with thorn
        if (collision.CompareTag(ENEMY_TAG))
            Destroy(collision.gameObject);
    }
}

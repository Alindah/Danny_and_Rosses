using UnityEngine;
using static Constants;

public class Bullet : MonoBehaviour
{
    private GameObject player;
    private Weapon weapon;
    private float playerOrientation;

    private void Start()
    {
        player = GameController.Player.gameObject;
        playerOrientation = player.GetComponent<PlayerController>().orientation.x;     // Direction player facing at time of firing
        weapon = player.transform.GetComponentInChildren<Weapon>();
        transform.parent = GameObject.Find(SPAWN_CONTAINER).transform;  // Place bullet in Spawn Container to avoid flipping when player flips
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * weapon.projectileSpeed * playerOrientation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMY_TAG))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(weapon.damage);
            enemy.KnockBack(new Vector2(playerOrientation * weapon.knockback, 0));
            Destroy(gameObject);
        }
    }
}

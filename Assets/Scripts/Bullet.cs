using UnityEngine;
using static Constants;

public class Bullet : MonoBehaviour
{
    private GameObject player;
    private float playerOrientation;
    private float speed = 5.0f;

    private void Start()
    {
        player = GameController.Player.gameObject;
        playerOrientation = player.GetComponent<PlayerController>().orientation.x;     // Direction player facing at time of firing
        speed = player.transform.GetComponentInChildren<Weapon>().projectileSpeed;
        transform.parent = GameObject.Find(SPAWN_CONTAINER).transform;  // Place bullet in Spawn Container to avoid flipping when player flips
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed * playerOrientation);
    }
}

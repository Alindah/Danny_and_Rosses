using UnityEngine;
using static Constants;

public class Bullet : MonoBehaviour
{
    public float speed;

    private PlayerController player;
    private float playerOrientation;

    private void Start()
    {
        player = GameController.Player.GetComponent<PlayerController>();
        playerOrientation = player.orientation.x;     // Direction player facing at time of firing
        transform.parent = GameObject.Find(SPAWN_CONTAINER).transform;  // Place bullet in Spawn Container to avoid flipping when player flips
    }

    private void Update()
    {
        //Vector2 direction = playerOrientation == 1 ? Vector2.right : Vector2.left;
        transform.Translate(Vector2.right * Time.deltaTime * speed * playerOrientation);
    }
}

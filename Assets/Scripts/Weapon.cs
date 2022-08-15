using UnityEngine;
using static Constants;

public class Weapon : MonoBehaviour
{
    public GameObject ammoObject;
    public int ammoCapacity;
    public int ammoAvailable;
    public Vector3 orientation = new Vector3(1, 1, 1);     // x = -1 if facing left, 1 if facing right

    private bool isColliding = false;   // Check if player is touching weapon

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColliding)
        {
            // Identify transforms
            Transform playerWeaponContainer = GameController.Player.transform.Find(PLAYER_WEAPON_CONTAINER);
            Transform weaponContainer = gameObject.transform.parent;

            Inventory.weapon = gameObject.GetComponent<Weapon>();
            Destroy(ammoObject);

            // If player and weapon are facing opposite directions when picked up, flip the weapon
            if (GameController.Player.GetComponent<Entity>().orientation != orientation)
                weaponContainer.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            // Set player as the parent
            weaponContainer.parent = playerWeaponContainer;
            weaponContainer.position = playerWeaponContainer.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
            isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
            isColliding = false;
    }
}

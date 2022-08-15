using UnityEngine;
using static Constants;

public class Weapon : MonoBehaviour
{
    public GameObject ammoObject;
    public int ammoCapacity;
    public int ammoAvailable;
    public Vector3 orientation = new Vector3(1, 1, 1);     // x = -1 if facing left, 1 if facing right

    private bool isColliding = false;   // Check if player is touching weapon
    private Transform weaponContainer;      // The container that holds the weapon
    private Transform allWeaponsContainer;  // Original parent of weapons and where they will drop in the hierarchy
    private Transform playerWeaponContainer;    // Container where player would be holding the weapon

    private void Start()
    {
        weaponContainer = transform.parent;
        allWeaponsContainer = weaponContainer.parent;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && Inventory.weapon != null && Inventory.weapon.gameObject == gameObject)
            DropWeapon();

        if (Input.GetKeyDown(KeyCode.E) && isColliding)
        {
            // Get transform of current player's "hands"
            if (!playerWeaponContainer)
                playerWeaponContainer = GameController.Player.transform.Find(PLAYER_WEAPON_CONTAINER);

            DropWeapon();
            PickUpWeapon();
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

    private void PickUpWeapon()
    {
        // If player and weapon are facing opposite directions when picked up, flip the weapon
        if (GameController.Player.GetComponent<Entity>().orientation != orientation)
            weaponContainer.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        // Set player as the parent
        weaponContainer.parent = playerWeaponContainer;
        weaponContainer.position = playerWeaponContainer.position;

        Inventory.weapon = gameObject.GetComponent<Weapon>();
        Destroy(ammoObject);
    }

    private void DropWeapon()
    {
        if (Inventory.weapon == null)
            return;

        // Only drop weapons that are being held
        playerWeaponContainer.GetChild(0).parent = allWeaponsContainer;
        Inventory.weapon = null;
    }
}

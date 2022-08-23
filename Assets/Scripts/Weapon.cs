using UnityEngine;
using System.Collections;
using static Constants;

public class Weapon : MonoBehaviour
{
    public GameObject ammoFloaty;
    public GameObject ammoObject;
    public Transform bulletContainer;
    public int ammoCapacity;
    public int ammoAvailable;
    public float projectileSpeed;
    public float damage;
    public float knockback;
    public float fireDelay;
    public Vector3 orientation = new Vector3(1, 1, 1);     // x = -1 if facing left, 1 if facing right

    [Header("ENVIRONMENT")]
    public LayerMask groundLayer;

    private bool isColliding = false;   // Check if player is touching weapon
    private Transform weaponContainer;      // The container that holds the weapon
    private Transform allWeaponsContainer;  // Original parent of weapons and where they will drop in the hierarchy
    private Transform playerWeaponContainer;    // Container where player would be holding the weapon
    private bool isFired = false;
    private Rigidbody2D rb;

    private void Start()
    {
        weaponContainer = transform.parent;
        allWeaponsContainer = weaponContainer.parent;
        rb = weaponContainer.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Drop weapon
        if (Input.GetKeyDown(KeyCode.LeftShift) && IsWeaponHeld())
            DropWeapon();

        // Pick up or swap weapon
        if (Input.GetKeyDown(KeyCode.E) && isColliding)
        {
            // Get transform of current player's "hands"
            if (!playerWeaponContainer)
                playerWeaponContainer = GameController.Player.transform.Find(PLAYER_WEAPON_CONTAINER);

            DropWeapon();
            PickUpWeapon();
        }

        // Fire weapon
        if (Input.GetKeyDown(KeyCode.X) && IsWeaponHeld())
            FireWeapon();

        // Make weapon fall to ground if not grounded
        if (Inventory.weapon != this && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (IsGrounded())
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
            }                
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

    private bool IsGrounded()
    {
        Vector2 position = weaponContainer.position;
        Vector2 direction = Vector2.down;
        float distance = 0.35f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        Debug.DrawRay(position, direction, Color.blue);

        if (hit.collider != null)
            return true;

        return false;
    }

    public void AlignWithPlayerOrientation()
    {
        Vector3 playerOrientation = GameController.Player.GetComponent<Entity>().orientation;

        // If weapon is held, set it to the player's orientation
        if (Inventory.weapon)
        {
            orientation = weaponContainer.localScale;
            return;
        }

        // If weapon is not held, make sure it's correctly oriented to the player before picking up
        if (orientation != playerOrientation)
            weaponContainer.localScale = new Vector3(-weaponContainer.transform.localScale.x, weaponContainer.transform.localScale.y, weaponContainer.transform.localScale.z);
    }

    private bool IsWeaponHeld()
    {
        return Inventory.weapon != null && Inventory.weapon.gameObject == gameObject;
    }

    private void PickUpWeapon()
    {
        // Make sure player and weapon are facing the same direction
        AlignWithPlayerOrientation();

        // Set player as the parent
        weaponContainer.parent = playerWeaponContainer;
        weaponContainer.position = playerWeaponContainer.position;

        Inventory.weapon = gameObject.GetComponent<Weapon>();
        Inventory.weapon.rb.bodyType = RigidbodyType2D.Kinematic;
        Inventory.weapon.GetComponent<Collider2D>().enabled = false;    // Turn off collider
        Destroy(ammoFloaty);
    }

    private void DropWeapon()
    {
        if (Inventory.weapon == null)
            return;

        // Only drop weapons that are being held and make sure it's set to the correct facing orientation
        playerWeaponContainer.GetChild(0).parent = allWeaponsContainer;
        AlignWithPlayerOrientation();
        Inventory.weapon.GetComponent<Collider2D>().enabled = true;     // Turn collider back on
        Inventory.weapon.rb.bodyType = RigidbodyType2D.Dynamic;
        Inventory.weapon = null;
    }

    private void FireWeapon()
    {
        if (IsInvoking("SpawnProjectile"))
            return;

        if (ammoAvailable > 0)
        {
            if (!GameController.infiniteAmmo)
                ammoAvailable--;

            if (!isFired)
                StartCoroutine("SpawnProjectile");
        }
        else
        {
            // Click click click
        }
    }

    private IEnumerator SpawnProjectile()
    {
        isFired = true;
        Instantiate(ammoObject, bulletContainer);
        yield return new WaitForSeconds(fireDelay);
        isFired = false;
    }
}

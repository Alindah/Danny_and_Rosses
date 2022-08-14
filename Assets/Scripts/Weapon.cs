using UnityEngine;
using static Constants;

public class Weapon : MonoBehaviour
{
    public GameObject ammoObject;
    public int ammoCapacity;
    public int ammoAvailable;

    private bool isColliding = false;   // Check if player is touching weapon

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isColliding)
        {
            Inventory.weapon = gameObject.GetComponent<Weapon>();
            Destroy(ammoObject);
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

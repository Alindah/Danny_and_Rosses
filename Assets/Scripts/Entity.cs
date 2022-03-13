using UnityEngine;
using static Constants;

public class Entity : MonoBehaviour
{
    public float speed;
    public float xBoundary;
    public Vector3 orientation = new Vector3(1, 1, 1);     // x = -1 if facing left, 1 if facing right
    public bool flipOrientation = false;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameController>();

        if (flipOrientation)
            FlipEntity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Flip player towards direction they are moving
    protected void FlipEntity()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        orientation.x = -orientation.x;
    }

}

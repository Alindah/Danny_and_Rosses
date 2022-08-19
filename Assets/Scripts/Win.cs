using UnityEngine;
using static Constants;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
            WinGame();
    }

    public void WinGame()
    {
        Debug.Log("Hooray you win!!!");
        GameController.PauseGame();
        // Show win dialog box
    }
}

using UnityEngine;
using static Constants;

public class Win : MonoBehaviour
{
    public Dialog winDialog;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
            WinGame();
    }

    public void WinGame()
    {
        GameController.PauseGame();
        winDialog.DisplayPanel();
        GameController.GameIsComplete = true;
    }
}

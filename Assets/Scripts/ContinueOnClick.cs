using UnityEngine;

public class ContinueOnClick : Dialog
{
    private void Update()
    {
        // Continue upon pressing any key
        if (Input.anyKeyDown)
        {
            GameController.GameIsStarted = true;

            HidePanel();

            if (GameController.GameIsComplete)
                GameController.RestartGame();
        }
    }
}

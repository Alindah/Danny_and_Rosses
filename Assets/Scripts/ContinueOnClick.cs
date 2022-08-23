using UnityEngine;

public class ContinueOnClick : Dialog
{
    private void Update()
    {
        // Continue upon pressing any key
        if (Input.anyKeyDown)
        {
            HidePanel();

            if (!GameController.GameIsActive)
                GameController.RestartGame();
        }
    }
}

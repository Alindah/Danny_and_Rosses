using UnityEngine;

public class ContinueOnClick : UIHandler
{
    private void Update()
    {
        // Continue upon pressing any key
        if (Input.anyKeyDown)
            HidePanel(gameObject);
    }
}

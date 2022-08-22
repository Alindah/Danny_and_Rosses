using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject overlay;
    public GameObject instructionsPanel;
    public GameObject losePanel;
    public GameObject winPanel;

    private void Update()
    {
        // Continue upon pressing any key
        if (Input.anyKeyDown)
        {
            HidePanel(overlay);
            HidePanel(instructionsPanel);
        }
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void DisplayPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
}

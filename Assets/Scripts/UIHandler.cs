using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject overlay;

    public void HidePanel(GameObject panel)
    {
        overlay.SetActive(false);
        panel.SetActive(false);
    }

    public void DisplayPanel(GameObject panel)
    {
        overlay.SetActive(true);
        panel.SetActive(true);
    }
}

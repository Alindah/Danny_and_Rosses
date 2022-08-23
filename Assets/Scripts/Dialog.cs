using UnityEngine;

public class Dialog : MonoBehaviour
{
    public GameObject overlay;

    public void HidePanel()
    {
        gameObject.SetActive(false);
        overlay.SetActive(false);
    }

    public void DisplayPanel()
    {
        gameObject.SetActive(true);
        overlay.SetActive(true);
    }
}

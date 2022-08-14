using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] rossesObjects;      // First item must be player Rosses
    public GameObject[] dannyObjects;       // First item must be player Danny
    public GameObject[] labelObjects;

    private GameObject player;

    public void Start()
    {

    }

    public void DestroyUnselected()
    {
        // Destroy objects associated with Danny if user chose Rosses
        if (player == rossesObjects[0])
        {
            foreach (GameObject obj in dannyObjects)
                Destroy(obj);
        }
        // Destroy objects associated with Rosses if user chose Danny
        else if (player == dannyObjects[0])
        {
            foreach (GameObject obj in rossesObjects)
                Destroy(obj);
        }

        foreach (GameObject obj in labelObjects)
            Destroy(obj);
    }

    public GameObject AssignPlayer
    {
        get { return player; }
        set { player = value; }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public static bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }
}

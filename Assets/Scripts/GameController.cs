using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Dev Mode")]
    public bool devMode = false;    // Enable dev tools
    public static bool godMode = true;
    public static bool infiniteAmmo = true;

    [Header("Game Objects")]
    public GameObject[] rossesObjects;      // First item must be player Rosses
    public GameObject[] dannyObjects;       // First item must be player Danny
    public GameObject[] labelObjects;

    [Header("Panels")]
    public Dialog losePanel;

    private static bool gameIsStarted = false;
    private static bool gameIsComplete = false;
    private static GameObject player;

    public void Start()
    {
        if (!devMode)
        {
            godMode = false;
            infiniteAmmo = false;
        }
        
        PauseGame();
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

    public static GameObject Player
    {
        get { return player; }
        set { player = value; }
    }

    public static void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene(0);
        gameIsComplete = false;
        gameIsStarted = false;
    }

    public static bool IsGamePaused()
    {
        return Time.timeScale == 0f;
    }

    public void LoseGame()
    {
        PauseGame();
        losePanel.DisplayPanel();
        GameIsComplete = true;
    }

    public static bool GameIsStarted
    {
        get { return gameIsStarted; }
        set { gameIsStarted = value; }
    }

    public static bool GameIsComplete
    {
        get { return gameIsComplete; }
        set { gameIsComplete = value; }
    }
}

using System.Collections;
using System.Collections.Generic;
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
        // Hide objects associated with Danny if user chose Rosses
        if (player == rossesObjects[0])
        {
            foreach (GameObject obj in dannyObjects)
                Destroy(obj);
        }
        // Hide objects associated with Rosses if user chose Danny
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
}
